using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour
{
    public Dance danceScript;
    public GameObject touchIndicator;
    public Canvas canvas;
    public Text text;
    [Space(10)]
    public LayerMask touchInputMask;

    //interaction variables
    GameObject objectInteractingWith;//TODO: make it work with 
    Vector3 interactionStartScreenPos;
    Vector3 interactionStartWorldPos;

    //delta at which a swipe is triggered
    float swipeDeltaThreshold = 2f;

    Vector3[] touchStartScreenPositions = new Vector3[10];

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            //locations
            Vector3 latestWorldPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Vector3 latestScreenPos = Input.touches[i].position;
            TouchPhase phase = Input.touches[i].phase;

            //raycast
            Ray ray = Camera.main.ScreenPointToRay(latestScreenPos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            switch (phase)
            {
                case TouchPhase.Began:
                    //record where this touch began using its index
                    touchStartScreenPositions[i] = latestScreenPos;


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
                    
                    

                    //for use with Control Sphere atm, will need to change for tap to select parts
                    if (objectInteractingWith)
                    {
                        objectInteractingWith.SendMessage("OnTouchMove", Input.touches[i].deltaPosition, SendMessageOptions.DontRequireReceiver);
                        ShowText(Input.touches[i].deltaPosition.ToString());
                    }
                    else
                    {
                        danceScript.SendMessage("DirectRotate", Input.touches[i].deltaPosition.x, SendMessageOptions.DontRequireReceiver);
                    }
                    break;
                case TouchPhase.Stationary:
                    //if finger is not moving, hold char still

                    danceScript.SendMessage("DirectRotate", 0, SendMessageOptions.DontRequireReceiver);

                    break;
                case TouchPhase.Ended:
                    if (objectInteractingWith)
                    {
                        objectInteractingWith.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);

                        //end interaction, clean up
                        objectInteractingWith = null;
                        ShowText("");
                    }
                    else
                    {
                        //on release, send swipe spin
                        danceScript.SendMessage("SwipeSpin", Input.touches[i].deltaPosition.x, SendMessageOptions.DontRequireReceiver);
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

    }
    
    void ShowText(string value)
    {
        text.text = value;
    }
}