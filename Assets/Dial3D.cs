using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis { X,Y,Z}

public class Dial3D : MonoBehaviour
{
    public Axis axis = Axis.X;
    float deltaScale = 0.3f;


    public float value = 0;

    void OnTouchMove(Vector2 delta)
    {
        switch (axis)
        {
            //i know they don't make sense, just go with it
            case Axis.X:
                transform.Rotate(0, -delta.y * deltaScale, 0);
                value += -delta.y * deltaScale;
                break;
            case Axis.Y:
                transform.Rotate(0, -delta.x * deltaScale, 0);
                value += -delta.x * deltaScale;

                break;
            case Axis.Z:
                transform.Rotate(0, -delta.x * deltaScale, 0);
                value += -delta.x * deltaScale;

                break;
            default:
                break;
        }
    }
}
