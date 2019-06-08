using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGroup : MonoBehaviour
{
    public Text text;
    public Slider3D x;
    public Slider3D y;
    public Slider3D z;

    public GameObject affectedBodyPart;


    float scrollBarPositiveHeight = 0.8f;
    float ScrollBarTotalHeight = 1.6f;
    public Vector3 startingValues;
    Vector3 latestVector;

    [Header("limits")]
    [Space(10)]
    public Vector2 BodyPartXMaxMin;
    public Vector2 BodyPartYMaxMin;
    public Vector2 BodyPartZMaxMin;


    private void Start()
    {
        //startingValues = affectedBodyPart.transform.localEulerAngles;
        
        //normalize the values to 0-1 range then set them on the new scale
        SetPosition(x, ConvertToScrollBar(Mathf.InverseLerp(BodyPartXMaxMin.x,BodyPartXMaxMin.y,startingValues.x)));
        SetPosition(y, ConvertToScrollBar(Mathf.InverseLerp(BodyPartYMaxMin.x, BodyPartYMaxMin.y, startingValues.y)));
        SetPosition(z, ConvertToScrollBar(Mathf.InverseLerp(BodyPartZMaxMin.x, BodyPartZMaxMin.y, startingValues.z)));

    }


    // Update is called once per frame
    void Update()
    {
        float xValue = ConvertToRotation(
            Mathf.InverseLerp(-scrollBarPositiveHeight, scrollBarPositiveHeight, x.transform.localPosition.y),
            BodyPartXMaxMin);
        float yValue = ConvertToRotation(
            Mathf.InverseLerp(-scrollBarPositiveHeight, scrollBarPositiveHeight, y.transform.localPosition.y),
            BodyPartYMaxMin);
        float zValue = ConvertToRotation(
            Mathf.InverseLerp(-scrollBarPositiveHeight, scrollBarPositiveHeight, z.transform.localPosition.y), 
            BodyPartXMaxMin);

        latestVector = new Vector3(xValue, yValue, zValue);
        Dance.danceScript.SendMessage("SetTorsoRotation", latestVector, SendMessageOptions.DontRequireReceiver);

        //ShowText("x: " + Normalize(startingValues.x, BodyPartXMaxMin));

        //latestVector = new Vector3(Normalize(x.transform.localPosition.y), 0, 0);



        //Vector3 newHipAngle = new Vector3(torsoValue, 0, 0);
        //affectedBodyPart.transform.localRotation = Quaternion.Euler(newHipAngle);
    }

    float ConvertToScrollBar(float value)
    {    //newvalue= (max1-min1)/(max-min)*(value-max)+max1

        float scrollBarPosition = (value * ScrollBarTotalHeight) - scrollBarPositiveHeight;
        return scrollBarPosition;
    }

    float ConvertToRotation(float value, Vector2 bodyPartScale)
    {    //newvalue= (max1-min1)/(max-min)*(value-max)+max1

        float rotationEuler = (value * (bodyPartScale.x - bodyPartScale.y) + bodyPartScale.y);
        return rotationEuler;
    }
    void SetPosition(Slider3D slider, float newPosition)
    {
        slider.transform.localPosition = new Vector3(slider.transform.localPosition.x, newPosition, slider.transform.localPosition.z);
    }


    void ShowText(string value)
    {
        text.text = value;
    }
}
