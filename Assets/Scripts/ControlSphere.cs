using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSphere : MonoBehaviour
{

    public Color defaultColor;
    public Color selectedColor;
    Material mat;

    float rotationSpeed = 0.1f;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

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
