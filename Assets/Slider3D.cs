using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider3D : MonoBehaviour
{
    float deltaScale = 0.003f;
    void OnTouchMove(Vector2 delta)
    {
        
        transform.Translate(0, delta.y * deltaScale, 0);
    }
}
