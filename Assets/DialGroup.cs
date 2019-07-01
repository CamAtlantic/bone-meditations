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


    Vector3 startingValues;
    Vector3 latestVector;

    [Space(10)]
    Vector2 BodyPartXMaxMin;
    Vector2 BodyPartYMaxMin;
    Vector2 BodyPartZMaxMin;

    public GameObject light;


    private void Start()
    {
        //startingValues = affectedBodyPart.transform.localEulerAngles;
        
        //normalize the values to 0-1 range then set them on the new scale
        //SetPosition(dialX, ConvertToScrollBar(Mathf.InverseLerp(BodyPartXMaxMin.x,BodyPartXMaxMin.y,startingValues.x)));
        //SetPosition(dialY, ConvertToScrollBar(Mathf.InverseLerp(BodyPartYMaxMin.x, BodyPartYMaxMin.y, startingValues.y)));
        //SetPosition(dialZ, ConvertToScrollBar(Mathf.InverseLerp(BodyPartZMaxMin.x, BodyPartZMaxMin.y, startingValues.z)));

    }


    // Update is called once per frame
    void Update()
    {
        //TODO: fix this for rotation
        /*
        dialX.transform.localRotation = new Vector3(
            dialX.transform.localPosition.x,
            Mathf.Clamp( dialX.transform.localPosition.y,BodyPartXMaxMin.y,BodyPartXMaxMin.x),
            dialX.transform.localPosition.z);

        dialY.transform.localPosition = new Vector3(
            dialY.transform.localPosition.x,
            Mathf.Clamp(dialY.transform.localPosition.y, BodyPartYMaxMin.y, BodyPartYMaxMin.x),
            dialY.transform.localPosition.z);

        dialZ.transform.localPosition = new Vector3(
            dialZ.transform.localPosition.x,
            Mathf.Clamp(dialZ.transform.localPosition.y, BodyPartZMaxMin.y, BodyPartZMaxMin.x),
            dialZ.transform.localPosition.z);
          */  

        //convert each slider value into a float
        float xValue = dialX.value;
        float yValue = dialY.value;
        float zValue = dialZ.value;

        //turn em all into a vector and send it to where it needs to go
        latestVector = new Vector3(xValue, yValue, zValue);
        //TODO: some of these messages could just be public method calls
        Dance.danceScript.SendMessage("SetDialTargetRotation", latestVector, SendMessageOptions.DontRequireReceiver);
    }

    public void SetBodyPart(BodyPart bodyPart)
    {
        affectedBodyPart = bodyPart;

        BodyPartXMaxMin = bodyPart.RotationXMaxMin;
        BodyPartYMaxMin = bodyPart.RotationYMaxMin;
        BodyPartZMaxMin = bodyPart.RotationZMaxMin;

        //is this resetting the dials to 0 properly?
        dialX.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        dialX.value = latestVector.x;
        dialY.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        dialX.value = latestVector.y;
        dialZ.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        dialX.value = latestVector.z;

        latestVector = bodyPart.currentLocalRotation.eulerAngles;

        light.GetComponent<Renderer>().material = Dance.danceScript.selectedMaterial;
        Dance.danceScript.SendMessage("SetDialTarget", bodyPart.gameObject, SendMessageOptions.DontRequireReceiver);
    }

    void ShowText(string value)
    {
        text.text = value;
    }
}
