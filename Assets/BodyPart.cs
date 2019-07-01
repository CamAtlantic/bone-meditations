using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public Vector2 RotationXMaxMin;
    public Vector2 RotationYMaxMin;
    public Vector2 RotationZMaxMin;

    public Color color = Color.white;

    [HideInInspector]
    
    //public bool selected = false;

    public Quaternion currentLocalRotation;

    private void Awake()
    {
        currentLocalRotation = transform.localRotation;
    }

    void OnTap()
    {
        //Go through all body parts and turn them normal material
        foreach (var part in Dance.danceScript.bodyParts)
        {
            part.GetComponent<Renderer>().material = Dance.danceScript.normalMaterial;
        }
        //set this body part to selected material
        GetComponent<Renderer>().material = Dance.danceScript.selectedMaterial;

      //  if(selected == false)
        {
            //Attach this to dial group
            FindObjectOfType<DialGroup>().SetBodyPart(this);
        }
    }
}
