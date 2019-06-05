using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour
{
    public GameObject touchIndicator;
    public Canvas canvas;
    public Text text;
    [Space(10)]
    public LayerMask touchInputMask;

    //interaction variables
    GameObject objectInteractingWith;//call them handles?
    Vector3 interactionStartScreenPos;
    Vector3 interactionStartWorldPos;

    //delta at which a swipe is triggered
    float swipeDelta = 2f;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            //locations
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Vector3 touchScreenPos = Input.touches[i].position;
            TouchPhase phase = Input.touches[i].phase;

            //raycast
            Ray ray = Camera.main.ScreenPointToRay(touchScreenPos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            switch (phase)
            {
                case TouchPhase.Began:
                    if (hit.collider)
                    {
                        GameObject hitObject = hit.collider.gameObject;
                        if (!objectInteractingWith)
                        {
                            objectInteractingWith = hitObject;
                            interactionStartScreenPos = touchScreenPos;
                            interactionStartWorldPos = touchWorldPos;
                            hitObject.SendMessage("OnTouchDown", SendMessageOptions.DontRequireReceiver);
                        }
                    }
                    
                    break;
                case TouchPhase.Moved:
                    float absXDelta = Mathf.Abs(Input.touches[i].deltaPosition.x);

                    if ( absXDelta >= swipeDelta)
                    {
                        ShowText("swipe: " + absXDelta);
                    }

                    if (objectInteractingWith)
                    {
                        Vector3 interactionScreenDistance = touchScreenPos - interactionStartScreenPos;
                        Vector3 interactionWorldDistance = touchWorldPos - interactionStartWorldPos;

                        


                        objectInteractingWith.SendMessage("Rotate", Input.touches[i].deltaPosition, SendMessageOptions.DontRequireReceiver);
                    }
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    if (objectInteractingWith)
                    {
                        objectInteractingWith.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);

                        //end interaction, clean up
                        objectInteractingWith = null;
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

        //SCRAPHEAP===========================
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseScreenPos = Input.mousePosition;
            Debug.DrawLine(Vector3.zero, mouseWorldPos, Color.red);
            RayCast(mouseScreenPos);
        }
#endif
    }


    void RayCast(Vector3 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        if (hit.collider)
        {
            hit.collider.gameObject.SendMessage("OnTouchDown", SendMessageOptions.DontRequireReceiver);
            ShowText(hit.collider.name);
        }
        else
        {
            ShowText("");
        }
    }

    void ShowText(string value)
    {
        text.text = value;
    }
}