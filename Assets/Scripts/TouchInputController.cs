using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchInputController : MonoBehaviour
{
    //public Dance danceScript;
    public GameObject touchIndicator;
    public Canvas canvas;
    public Text text;
    [Space(10)]
    public LayerMask touchInputMask;

    //interaction variables
    GameObject objectInteractingWith;//TODO: make it work with multiple touches
    Vector3 interactionStartScreenPos;
    Vector3 interactionStartWorldPos;
    
    Vector3[] touchStartScreenPositions = new Vector3[10];

    float tapDuration = 0.2f;
    float tapTimer = 0;
    bool tap = true;

    private void Start()
    {
        Input.multiTouchEnabled = true;
    }

    void Update()
    {
        
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
                    touchStartScreenPositions[i] = latestScreenPos;

                    //this is the first frame, so reset the tap timer
                    tap = true;
                    tapTimer = 0;

                    
                    if (hit.collider)
                    {
                        GameObject hitObject = hit.collider.gameObject;
                        
                        if (!objectInteractingWith)
                        {
                            objectInteractingWith = hitObject;
                            interactionStartScreenPos = latestScreenPos;
                            interactionStartWorldPos = latestWorldPos;
                            hitObject.SendMessage("OnTouchDown", SendMessageOptions.DontRequireReceiver);
                            
                        }
                    }
                    
                    break;
                case TouchPhase.Moved:
                    float absXDelta = Mathf.Abs(Input.touches[i].deltaPosition.x);
                    float totalXDelta = touchStartScreenPositions[i].x - latestScreenPos.x;

                    if (objectInteractingWith)
                    {
                        objectInteractingWith.SendMessage("OnTouchMove", Input.touches[i].deltaPosition, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
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
                    
                    //IF TAPPING / DRAGGING ON OBJECT
                    if (objectInteractingWith)
                    {
                        //check if it's a bodypart
                        if (Dance.danceScript.bodyParts.Contains(objectInteractingWith.GetComponent<BodyPart>()))
                        {
                            if (tap)
                            {

                                //let the object know it was tapped
                                objectInteractingWith.SendMessage("OnTap");
                            }
                        }

                        //end interaction, clean up
                        objectInteractingWith.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
                        objectInteractingWith = null;
                    }
                    else //not tapping / dragging
                    {
                        //TODO: this feels a little gross, hard to stop the model. needs delta check probably.
                        //on release, send swipe spin
                        Dance.danceScript.SendMessage("SwipeSpin", Input.touches[i].deltaPosition.x, SendMessageOptions.DontRequireReceiver);
                    }
                    break;
                case TouchPhase.Canceled:
                    if (objectInteractingWith)
                    {
                        objectInteractingWith.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);

                        //end interaction, clean up
                        objectInteractingWith = null;
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
        text.text = value;
    }
}