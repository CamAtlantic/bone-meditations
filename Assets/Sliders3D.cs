using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliders3D : MonoBehaviour
{
    float deltaScale = 0.003f;
    void OnTouchMove(Vector2 delta)
    {
        
        transform.Translate(0, delta.y * deltaScale, 0);
    }
}
