using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSphere : MonoBehaviour
{
    public enum ControlAxis { X,Y,Z}
    public ControlAxis axis = ControlAxis.X;

    [ColorUsage(true,true)]
    public Color defaultColor;
    [ColorUsage(true, true)]
    public Color selectedColor;
    Material mat;

    //Held anim vars
    bool held = false;
    Vector3 normalScale;
    public float selectedScale = 1.2f;
    float selectionAnimSeconds;
    public float selectionAnimSecondsMax = 0.2f;

    //Lerpback anim vars
    bool lerpBack = false;
    float lerpBackSeconds;
    public float lerpBackSecondsMax = 0.2f;

    public LineRenderer axisLine;
    public GameObject sphere;

    Vector2 axisLineVector;

    private void Start()
    {
        mat = sphere.GetComponent<Renderer>().material;
        normalScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 corner0 = axisLine.transform.TransformPoint( axisLine.GetPosition(0) );
        Vector3 corner1 = axisLine.transform.TransformPoint(axisLine.GetPosition(1));

        corner0 = Camera.main.WorldToScreenPoint(corner0);
        corner1 = Camera.main.WorldToScreenPoint(corner1);

        //Vector3 lineCenter

        axisLineVector = corner1 - corner0;
        axisLineVector.Normalize();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(name + " " + corner0 + " " + corner1);
            //Debug.Log(name + " " + vector);
            Debug.Log(sphere.transform.localPosition);
            //Debug.Log(NearestPointOnLine());
        }

        if (held)
        {
            selectionAnimSeconds += Time.deltaTime;
            if (selectionAnimSeconds > selectionAnimSecondsMax)
            {
                selectionAnimSeconds = selectionAnimSecondsMax;
            }

        }
        else
        {
            selectionAnimSeconds -= Time.deltaTime;
            if (selectionAnimSeconds < 0)
            {
                selectionAnimSeconds = 0;
            }
        }

        float selectionProgress = selectionAnimSeconds / selectionAnimSecondsMax;
        float math = Mathf.Lerp(1, selectedScale, selectionProgress);

        transform.localScale = normalScale * math;

        //I feel like there is some duplicated functionality here
        if (lerpBack)
        {
            lerpBackSeconds += Time.deltaTime;
            float lerpBackProgress = lerpBackSeconds / lerpBackSecondsMax;

            if (lerpBackProgress >= 1)
            {
                lerpBack = false;
                lerpBackSeconds = 0;
            }

            sphere.transform.localPosition = Vector3.Lerp(sphere.transform.localPosition, Vector3.zero, lerpBackProgress);

        }

    }

    public void StartInteraction()//Called by TouchInputController
    {
        held = true;
    }
    public void EndInteraction()
    {
        held = false;
        lerpBack = true;
    }


    float dragSpeed = 0.015f;
    public void OnDrag(Interaction interaction)
    {
        




        Vector2 delta = interaction.startScreenPos - interaction.thisFrameScreenPos;

        switch (axis)
        {
            case ControlAxis.X:
                sphere.transform.localPosition = new Vector3(-delta.x * dragSpeed, 0, 0);
                break;
            case ControlAxis.Y:
                sphere.transform.localPosition = new Vector3(0, -delta.y * dragSpeed, 0);
                break;
            case ControlAxis.Z:
                sphere.transform.localPosition = new Vector3(0, 0, -delta.y * dragSpeed);
                break;
            default:
                break;
        }
    }
    public static Vector3 NearestPointOnLine(Vector3 linePnt, Vector3 lineDir, Vector3 pnt)
    {
        lineDir.Normalize();//this needs to be a unit vector
        var v = pnt - linePnt;
        var d = Vector3.Dot(v, lineDir);
        return linePnt + lineDir * d;
    }

    //=====================
    void OnTouchDown()
    {
        mat.color = selectedColor;
    }
    void OnTouchUp()
    {
        mat.color = defaultColor;
    }
    void OnTouchStay()
    {
        mat.color = selectedColor;
    }
    void OnTouchExit()
    {
        mat.color = defaultColor;
    }

    //================
}
