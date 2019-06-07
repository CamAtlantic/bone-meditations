using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliders3D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTouchMove(Vector2 delta)
    {
        //Vector3 vector = new Vector3(transform.position.x, worldPos.y, transform.position.y);
        //transform.position = vector;
        transform.Translate(0, delta.y * 0.01f, 0);
    }
}
