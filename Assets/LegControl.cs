using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegControl : MonoBehaviour
{
    Dance dancer;
    public Button upperLegButton;
    public Button lowerLegButton;
    RectTransform upperLegControl;
    public RectTransform lowerLegControl;

    bool draggingUpperLeg = false;
    bool draggingLowerLeg = false;

    Vector3 offset = Vector3.zero;


    Quaternion originalRotation;
    float startAngle = 0;

    [HideInInspector]
    public float upperLegAngle = 0;
    [HideInInspector]
    public float lowerLegAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        dancer = FindObjectOfType<Dance>();
        upperLegControl = (RectTransform)transform;
    }

    private void Update()
    {
        if (draggingUpperLeg)
        {
            Vector3 vector = Input.mousePosition -upperLegControl.position;

            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - startAngle;
            angle += 90;
            Debug.Log(angle);
            upperLegControl.transform.eulerAngles = new Vector3(0, 0, angle);
            upperLegAngle = angle;
        }

        if (draggingLowerLeg)
        {
            Vector3 vector = Input.mousePosition - lowerLegControl.position;

            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - startAngle;
            angle += 90;
            Debug.Log(angle);

            lowerLegAngle = angle - upperLegAngle;
            lowerLegControl.transform.eulerAngles = new Vector3(0, 0, angle);

            if (lowerLegAngle > 0)
            {
                lowerLegAngle = 0;
                lowerLegControl.transform.eulerAngles = new Vector3(0, 0, 0);

            }
            


        }

    }

    //called when player starts dragging
    public void BeginKneeDrag()
    {
        draggingUpperLeg = true;
        offset = upperLegButton.transform.position - Input.mousePosition;

        originalRotation = upperLegControl.localRotation;
        Vector3 vector = Input.mousePosition - upperLegControl.localPosition;
        startAngle = Mathf.Atan2(vector.y, vector.x * Mathf.Rad2Deg);
    }
    //called when dragging ends (over the button, might need to change that to mouseUp)
    public void EndKneeDrag()
    {
        draggingUpperLeg = false;
        offset = Vector3.zero;
    }

    //called when player starts dragging
    public void BeginFootDrag()
    {
        draggingLowerLeg = true;
        offset = lowerLegButton.transform.position - Input.mousePosition;

        originalRotation = lowerLegControl.localRotation;
        Vector3 vector = Input.mousePosition - lowerLegControl.localPosition;
        startAngle = Mathf.Atan2(vector.y, vector.x * Mathf.Rad2Deg);
    }
    //called when dragging ends (over the button, might need to change that to mouseUp)
    public void EndFootDrag()
    {
        draggingLowerLeg = false;
        offset = Vector3.zero;
    }
}
