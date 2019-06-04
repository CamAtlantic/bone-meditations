using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputController : MonoBehaviour
{
    public GameObject touchIndicator;
    public Canvas canvas;
    public Text text;
    [Space(10)]
    public LayerMask touchInputMask;


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Vector3 touchScreenPos = Input.touches[i].position;
            Debug.DrawLine(Vector3.zero, touchWorldPos, Color.red);
            RayCast(touchScreenPos);
        }

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseScreenPos = Input.mousePosition;
            Debug.DrawLine(Vector3.zero, mouseWorldPos, Color.red);
            RayCast(mouseScreenPos);
        }
#endif
    }


    void RayCast(Vector3 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        if (hit.collider)
        {
            ShowText(hit.collider.name);
        }
        else
        {
            ShowText("");
        }
    }

    void ShowText(string value)
    {
        text.text = value;
    }
}