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
    public Slider legZSlider;
    public float legZMax = 87;
    public float legZMin = 0;
    [Space(10)]
    [Header("Body Parts")]
    
    public GameObject head;
    public GameObject torso;
    public GameObject upperLeg;
    public GameObject lowerLeg;
    public GameObject contactLeg;
    public GameObject leftUpperArm;
    public GameObject leftLowerArm;
    public GameObject leftHand;
    public GameObject rightUpperArm;
    public GameObject rightLowerArm;
    public GameObject rightHand;

    [HideInInspector]
    public List<GameObject> bodyParts = new List<GameObject>();

    private void Awake()
    {
        bodyParts.Add(head);
        bodyParts.Add(torso);
        bodyParts.Add(upperLeg);
        bodyParts.Add(lowerLeg);
        bodyParts.Add(contactLeg);
        bodyParts.Add(leftUpperArm);
        bodyParts.Add(leftLowerArm);
        bodyParts.Add(leftHand);
        bodyParts.Add(rightUpperArm);
        bodyParts.Add(rightLowerArm);
        bodyParts.Add(rightHand);
    }
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

        legZSlider.maxValue = legZMax;
        legZSlider.minValue = legZMin;
        legZSlider.value = 0;

    }

    //value passed in via SwipeRotate
    float currentRotateSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        //float rotateSpeed = speedSlider.value;
        float torsoValue = torsoSlider.value;
        float headValue = headSlider.value;
        float legYValue = legYSlider.value;
        float legZValue = legZSlider.value;


        Vector3 contactLegPosition = contactLeg.transform.position;
        transform.RotateAround(contactLegPosition, new Vector3(0, 1f, 0), -currentRotateSpeed);

        Vector3 newHipAngle = new Vector3(torsoValue, 0, 0);
        torso.transform.localRotation = Quaternion.Euler(newHipAngle);

        Vector3 newHeadAngle = new Vector3(headValue, 0, 0);
        head.transform.localRotation = Quaternion.Euler(newHeadAngle);

        Vector3 newUpperLegAngle = new Vector3(legControl.upperLegAngle, legYValue, legZValue);
        upperLeg.transform.localRotation = Quaternion.Euler(newUpperLegAngle);

        Vector3 newLowerLegAngle = new Vector3(legControl.lowerLegAngle, 0, 0);
        lowerLeg.transform.localRotation = Quaternion.Euler(newLowerLegAngle);

    }


    float deltaScale = 0.1f;
    float friction = 0.7f;
    float deadzone = 0.02f;

    //called via SendMessage TouchInputController
    void SwipeRotate(float swipeDelta)
    {
        swipeDelta *= deltaScale ;//cause it's really high values

        currentRotateSpeed += swipeDelta;
        currentRotateSpeed = Mathf.Clamp(currentRotateSpeed, -5, 5);
        currentRotateSpeed *= 0.7f;
        if(Mathf.Abs( currentRotateSpeed )<= 0.02f)
        {
            currentRotateSpeed = 0;

        }
    }
}
