using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSphere : MonoBehaviour
{
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

    public LineRenderer axis;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        normalScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 corner0 = axis.transform.TransformPoint( axis.GetPosition(0) );
        Vector3 corner1 = axis.transform.TransformPoint(axis.GetPosition(1));

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

    void Rotate(Vector2 rotationVector)
    {
        //had to reverse the X and Y and do -Y for some reason here but it kinda works!
        Vector2 newVector = new Vector2(rotationVector.y, -rotationVector.x);

        transform.parent.Rotate(newVector*rotationSpeed,Space.World);
    }

}
