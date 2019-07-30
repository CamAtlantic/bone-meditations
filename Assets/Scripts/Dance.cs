using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dance : MonoBehaviour
{
    public static Dance danceScript;
    
    public Slider speedSlider;
   
    public Slider3D torsoSlider;
    [Space(10)]
    [Header("Head")]
   
    public Slider headSlider;

    [Space(10)]
    [Header("Leg")]
 
    public LegControl legControl;
    public Slider legYSlider;
    public Slider legZSlider;
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
    public List<BodyPart> bodyParts = new List<BodyPart>();
    [Space(10)]
    public Material normalMaterial;
    public Material selectedMaterial;


    private void Awake()
    {
        danceScript = this;
        bodyParts.Add(head.GetComponent<BodyPart>());
        bodyParts.Add(torso.GetComponent<BodyPart>());
        bodyParts.Add(upperLeg.GetComponent<BodyPart>());
        bodyParts.Add(lowerLeg.GetComponent<BodyPart>());
        bodyParts.Add(leftUpperArm.GetComponent<BodyPart>());
        bodyParts.Add(leftLowerArm.GetComponent<BodyPart>());
        bodyParts.Add(leftHand.GetComponent<BodyPart>());
        bodyParts.Add(rightUpperArm.GetComponent<BodyPart>());
        bodyParts.Add(rightLowerArm.GetComponent<BodyPart>());
        bodyParts.Add(rightHand.GetComponent<BodyPart>());
        //Contact Leg is not in this list because it's not interactable at all
    }
    
    [Space(10)]
    [Header("Physics")]
    //value passed in via SwipeRotate
    public float currentRotateSpeed = 0;
    public float friction = 0.7f;
    public float deadzone = 0.02f;
    public float maxSpeed = 15f;

    bool isSpinning = false;

    GameObject dialTarget;
    Quaternion dialTargetRotation;

    // Update is called once per frame
    void Update()
    {
        if (isSpinning)
        {
            currentRotateSpeed *= friction;
            currentRotateSpeed = Mathf.Clamp(currentRotateSpeed, -maxSpeed, maxSpeed);
            if (Mathf.Abs(currentRotateSpeed) <= deadzone)
            {
                currentRotateSpeed = 0;
                isSpinning = false;
            }
        }
        
        //Get the position of the leg that's down and rotate about that position
        Vector3 contactLegPosition = contactLeg.transform.position;
        transform.RotateAround(contactLegPosition, new Vector3(0, 1f, 0), -currentRotateSpeed);

        //coming in via the 3D sliders
        torso.transform.localRotation = Quaternion.Euler(torsoRotation);
        
        //if there is a dial target that is not the torso
        if(dialTarget & dialTarget != torso) dialTarget.transform.localRotation = dialTargetRotation;

    }


    float deltaScale = 0.1f;
    
    void DirectRotate(float delta)
    {
        isSpinning = false;
        float speed = Mathf.Clamp(delta, -maxSpeed, maxSpeed);

        currentRotateSpeed = speed;
        
    }

    //called via SendMessage TouchInputController
    void SwipeSpin(float swipeDelta)
    {
        isSpinning = true;
        swipeDelta *= deltaScale ;//cause it's really high values
        currentRotateSpeed += swipeDelta;//add it to current so we don't lose values
    }

    Vector3 torsoRotation;
    void SetTorsoRotation(Vector3 rotation)
    {
        torsoRotation = rotation;
    }

    Vector3 upperLegRotation;
    void SetUpperLegRotation(Vector3 rotation)
    {

        upperLegRotation = rotation;
    }

    public void SetDialTarget(GameObject target)
    {
        dialTarget = target;
    }
    public void SetDialTargetRotation(Quaternion rotation)
    {
        dialTargetRotation = rotation;
    }
}
