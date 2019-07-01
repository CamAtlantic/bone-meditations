using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public Vector2 RotationXMaxMin;
    public Vector2 RotationYMaxMin;
    public Vector2 RotationZMaxMin;

    public Color color = Color.white;

    public Quaternion currentLocalRotation;

    void OnTap()
    {
        //getting the rotation here so it's available immediately
        currentLocalRotation = transform.localRotation;

        if (gameObject != Dance.danceScript.torso)
        {
            //Go through all body parts and turn them normal material
            foreach (var part in Dance.danceScript.bodyParts)
            {
                if (part.gameObject != Dance.danceScript.torso)
                {
                    part.GetComponent<Renderer>().material = Dance.danceScript.normalMaterial;
                }
            }
            //set this body part to selected material
            GetComponent<Renderer>().material = Dance.danceScript.selectedMaterial;

            //Attach this to dial group
            FindObjectOfType<DialGroup>().SetBodyPart(this);
        }
    }
}
