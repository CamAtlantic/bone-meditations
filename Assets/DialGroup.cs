using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialGroup : MonoBehaviour
{
    public Text text;
    public Dial3D dialX;
    public Dial3D dialY;
    public Dial3D dialZ;

    BodyPart affectedBodyPart;

    //the current value that the group will assign to the part
    Quaternion latestQuaternion;

    [Space(10)]
    Vector2 BodyPartXMaxMin;
    Vector2 BodyPartYMaxMin;
    Vector2 BodyPartZMaxMin;

    public GameObject light;


    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
        latestQuaternion = Quaternion.Euler(dialX.Value, dialY.Value, dialZ.Value);
        
        Dance.danceScript.SetDialTargetRotation(latestQuaternion);
    }

    public void SetBodyPart(BodyPart bodyPart)
    {
        affectedBodyPart = bodyPart;

        latestQuaternion = affectedBodyPart.currentLocalRotation;

        dialX.SetValue(latestQuaternion.eulerAngles.x);
        dialY.SetValue(latestQuaternion.eulerAngles.y);
        dialZ.SetValue(latestQuaternion.eulerAngles.z);

        BodyPartXMaxMin = affectedBodyPart.RotationXMaxMin;
        BodyPartYMaxMin = affectedBodyPart.RotationYMaxMin;
        BodyPartZMaxMin = affectedBodyPart.RotationZMaxMin;
        
        Dance.danceScript.SetDialTarget(bodyPart.gameObject);
        Dance.danceScript.SetDialTargetRotation(latestQuaternion);

        //feedback part
        light.GetComponent<Renderer>().material = Dance.danceScript.selectedMaterial;

    }

    void ShowText(string value)
    {
        text.text = value;
    }
    
}
