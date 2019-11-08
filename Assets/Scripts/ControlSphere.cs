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

    float rotationSpeed = 0.1f;

    bool held = false;

    Vector3 normalScale;
    public float selectedScale = 1.2f;

    float selectionAnimSeconds;
    public float selectionAnimSecondsMax = 0.2f;

    public LineRenderer axisLine;
    public GameObject sphere;

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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(name + " " + corner0 + " " + corner1);

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

    }

    public void StartInteraction()//Called by TouchInputController
    {
        held = true;
    }
    public void EndInteraction()
    {
        held = false;
    }

    public void OnDrag(Vector3 delta)
    {
        switch (axis)
        {
            case ControlAxis.X:
                sphere.transform.localPosition = new Vector3(-delta.x * 0.01f, 0, 0);
                break;
            case ControlAxis.Y:
                break;
            case ControlAxis.Z:
                break;
            default:
                break;
        }
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
