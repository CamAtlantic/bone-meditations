using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGroup : MonoBehaviour
{
    public Text text;
    public Slider3D scrollBarX;
    public Slider3D scrollBarY;
    public Slider3D scrollBarZ;

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
        SetPosition(scrollBarX, ConvertToScrollBar(Mathf.InverseLerp(BodyPartXMaxMin.x,BodyPartXMaxMin.y,startingValues.x)));
        SetPosition(scrollBarY, ConvertToScrollBar(Mathf.InverseLerp(BodyPartYMaxMin.x, BodyPartYMaxMin.y, startingValues.y)));
        SetPosition(scrollBarZ, ConvertToScrollBar(Mathf.InverseLerp(BodyPartZMaxMin.x, BodyPartZMaxMin.y, startingValues.z)));

    }


    // Update is called once per frame
    void Update()
    {
        //clamping scroll bars here cause they're too dumb to do it on their own
        scrollBarX.transform.localPosition = new Vector3(
            scrollBarX.transform.localPosition.x,
            Mathf.Clamp( scrollBarX.transform.localPosition.y,-scrollBarPositiveHeight,scrollBarPositiveHeight),
            scrollBarX.transform.localPosition.z);

        scrollBarY.transform.localPosition = new Vector3(
            scrollBarY.transform.localPosition.x,
            Mathf.Clamp(scrollBarY.transform.localPosition.y, -scrollBarPositiveHeight, scrollBarPositiveHeight),
            scrollBarY.transform.localPosition.z);

        scrollBarZ.transform.localPosition = new Vector3(
            scrollBarZ.transform.localPosition.x,
            Mathf.Clamp(scrollBarZ.transform.localPosition.y, -scrollBarPositiveHeight, scrollBarPositiveHeight),
            scrollBarZ.transform.localPosition.z);

        //convert each slider value into a float
        float xValue = ConvertToRotation(
            Mathf.InverseLerp(-scrollBarPositiveHeight, scrollBarPositiveHeight, scrollBarX.transform.localPosition.y),
            BodyPartXMaxMin);
        float yValue = ConvertToRotation(
            Mathf.InverseLerp(-scrollBarPositiveHeight, scrollBarPositiveHeight, scrollBarY.transform.localPosition.y),
            BodyPartYMaxMin);
        float zValue = ConvertToRotation(
            Mathf.InverseLerp(-scrollBarPositiveHeight, scrollBarPositiveHeight, scrollBarZ.transform.localPosition.y), 
            BodyPartXMaxMin);

        //turn em all into a vector and send it to where it needs to go
        latestVector = new Vector3(xValue, yValue, zValue);
        Dance.danceScript.SendMessage("SetTorsoRotation", latestVector, SendMessageOptions.DontRequireReceiver);
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
