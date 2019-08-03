using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchInputController : MonoBehaviour
{
    //public GameObject touchIndicator;
    //public Canvas canvas;
    public Text debugTextBox;
    [Space(10)]
    public LayerMask touchInputMask;//think this is for raycasting?

    //interaction variables
    //TODO: make it work with multiple touches
    GameObject objectInteractingWith;
    Vector3 interactionStartScreenPos;
    Vector3 interactionStartWorldPos;
    //Vector3[] touchStartScreenPositions = new Vector3[10];
    //-----------------------------------

    public struct Interaction
    {
        public GameObject target;
        public Vector3 startScreenPos;
        public Interaction(GameObject target, Vector3 startScreenPos)
        {
            this.target = target;
            this.startScreenPos = startScreenPos;
        }
    }

    public Interaction[] interactions = new Interaction[10];



    //Tap check variables
    float tapDuration = 0.2f;
    float tapTimer = 0;
    bool tap = true;

    private void Start()
    {
        Input.multiTouchEnabled = true;
    }

    void Update()
    {
        Vector3 thisFrameScreenPos = Input.mousePosition;

        //First frame mouse button down:
        if (Input.GetMouseButtonDown(0))
        {
            //raycast for 3D objects
            Ray ray = Camera.main.ScreenPointToRay(thisFrameScreenPos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 20, touchInputMask);

            //if the raycast hit something
            if (hit.collider)
            {
                GameObject hitObject = hit.collider.gameObject;

                interactions[0] = new Interaction(hitObject, thisFrameScreenPos);
                interactions[0].target.SendMessage("StartInteraction");//Used by ControlSphere

                if(hitObject.GetComponent<ControlSphere>() == false)
                {
                   
                        FindObjectOfType<ControlSphereStack>().target = hit.collider.gameObject;
                    
                }
            }
        }

        //Mouse button held down:
        if (Input.GetMouseButton(0))
        {
            //check the interaction slot to see if there is something
            if (interactions[0].target)
            {
                //Notify the target it is being HELD
            }
            else
            {
                //look for a target
            }
        }

        //Mouse button released:
        if (Input.GetMouseButtonUp(0))
        {
            //check the interaction slot to see if there is something
            if (interactions[0].target)
            {
                interactions[0].target.SendMessage("EndInteraction");//Used by ControlSphere
                interactions[0] = new Interaction();
            }
            
            
        }

        //Mobile Code below
        //========================

        
        

        //Loop through touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            //locations
            Vector3 latestWorldPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Vector3 latestScreenPos = Input.touches[i].position;
            TouchPhase phase = Input.touches[i].phase;

            //raycast for 3D objects
            Ray ray = Camera.main.ScreenPointToRay(latestScreenPos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            //increment & check if the hold time is longer than a tap
            if (tap) tapTimer += Time.deltaTime;
            if (tapTimer >= tapDuration) tap = false;
            

            switch (phase)
            {
                case TouchPhase.Began:
                    //record where this touch began using its index
                    //touchStartScreenPositions[i] = latestScreenPos;

                    //this is the first frame, so reset the tap timer
                    tap = true;
                    tapTimer = 0;

                    //if the raycast hit something
                    if (hit.collider)
                    {
                        GameObject hitObject = hit.collider.gameObject;

                        interactions[i] = new Interaction(hitObject, latestScreenPos);
                        interactions[i].target.SendMessage("StartInteraction");//Used by ControlSphere
                    }
                    
                    break;
                case TouchPhase.Moved:
                    //this frame delta
                    float absXDelta = Mathf.Abs(Input.touches[i].deltaPosition.x);
                    //total delta this interaction
                    //float totalXDelta = touchStartScreenPositions[i].x - latestScreenPos.x;

                    //TODO: make this work with multiple touches
                    if (objectInteractingWith)
                    {
                        //objectInteractingWith.SendMessage("OnTouchMove", Input.touches[i].deltaPosition, SendMessageOptions.DontRequireReceiver);
                        
                        //Here I can move a 3D object
                    }
                    else
                    {
                        //Backup if not interacting, go to rotating the dancer
                        Dance.danceScript.SendMessage("DirectRotate", Input.touches[i].deltaPosition.x, SendMessageOptions.DontRequireReceiver);
                    }
                    break;

                case TouchPhase.Stationary:

                    //if finger is not moving, hold char still
                    if (!objectInteractingWith)
                    {
                        Dance.danceScript.SendMessage("DirectRotate", 0, SendMessageOptions.DontRequireReceiver);
                    }
                    break;

                case TouchPhase.Ended:
                    if (tap)
                    {
                        //if you hit anything that's not a control sphere
                        if (hit.collider && !hit.collider.gameObject.GetComponent<ControlSphere>())
                        {
                            FindObjectOfType<ControlSphereStack>().target = hit.collider.gameObject;
                        }
                    }

                    //check the interaction slot to see if there is something
                    if (interactions[i].target)
                    {
                        interactions[i].target.SendMessage("EndInteraction");//Used by ControlSphere
                        interactions[i] = new Interaction();
                    }
                    break;
                case TouchPhase.Canceled:
                    //check the interaction slot to see if there is something
                    if (interactions[i].target)
                    {
                        interactions[i].target.SendMessage("EndInteraction");//Used by ControlSphere
                        interactions[i] = new Interaction();
                    }
                    break;
                default:
                    break;
            }
        }

        //quit the app
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    void ShowText(string value)
    {
        debugTextBox.text = value;
    }
}