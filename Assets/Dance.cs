using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dance : MonoBehaviour
{
    //public float rotateSpeed =5f;
    //[Space(10)]
    public Slider speedSlider;
   
    public Slider torsoSlider;
    public float torsoRotationMax = 50;
    public float torsoRotationMin = -90;
    [Space(10)]
    [Header("Head")]
   
    public Slider headSlider;
    public float headRotationMax = 50;
    public float headRotationMin = -90;

    [Space(10)]
    [Header("Leg")]
 
    public LegControl legControl;
    public Slider legYSlider;
    public float legYMax = 87;
    public float legYMin = 0;
    [Space(10)]
    [Header("Body Parts")]
    
    public GameObject head;
    public GameObject torso;
    public GameObject upperLeg;
    public GameObject lowerLeg;
    public GameObject contactLeg;


    // Start is called before the first frame update
    void Start()
    {
        torsoSlider.maxValue = torsoRotationMax;
        torsoSlider.minValue = torsoRotationMin;
        torsoSlider.value = 0;

        headSlider.maxValue = headRotationMax;
        headSlider.minValue = headRotationMin;
        headSlider.value = 0;

        legYSlider.maxValue = legYMax;
        legYSlider.minValue = legYMin;
        legYSlider.value = 0;

    }

    // Update is called once per frame
    void Update()
    {
        float rotateSpeed = speedSlider.value;
        float torsoValue = torsoSlider.value;
        float headValue = headSlider.value;
        float legYValue = legYSlider.value;


        Vector3 contactLegPosition = contactLeg.transform.position;
        transform.RotateAround(contactLegPosition, new Vector3(0, 1f, 0), -rotateSpeed);

        Vector3 newHipAngle = new Vector3(torsoValue, 0, 0);
        torso.transform.localRotation = Quaternion.Euler(newHipAngle);

        Vector3 newHeadAngle = new Vector3(headValue, 0, 0);
        head.transform.localRotation = Quaternion.Euler(newHeadAngle);

        Vector3 newUpperLegAngle = new Vector3(legControl.upperLegAngle, legYValue, 0);
        upperLeg.transform.localRotation = Quaternion.Euler(newUpperLegAngle);

        Vector3 newLowerLegAngle = new Vector3(legControl.lowerLegAngle, 0, 0);
        lowerLeg.transform.localRotation = Quaternion.Euler(newLowerLegAngle);

    }
}
