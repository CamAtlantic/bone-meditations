using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis { X,Y,Z}

public class Dial3D : MonoBehaviour
{
    public Axis axis = Axis.X;
    float deltaScale = 0.3f;


    public float Value { get; private set; }

    void OnTouchMove(Vector2 delta)
    {
        switch (axis)
        {
            

            //i know they don't make sense, just go with it.
            //works by changing the value a particular amount. it is not tied to absolute rotation
            case Axis.X:
                transform.Rotate(0, -delta.y * deltaScale, 0);
                Value += -delta.y * deltaScale;
                break;
            case Axis.Y:
                transform.Rotate(0, -delta.x * deltaScale, 0);
                Value += -delta.x * deltaScale;

                break;
            case Axis.Z:
                transform.Rotate(0, -delta.x * deltaScale, 0);
                Value += -delta.x * deltaScale;

                break;
            default:
                break;
        }
    }

    public void SetValue(float newValue)
    {
        Value = newValue;
    }
}
