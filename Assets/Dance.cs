using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dance : MonoBehaviour
{
    //public float rotateSpeed =5f;

    public Slider speedSlider;
    public Slider torsoSlider;

    public float torsoRotationMax = 50;
    public float torsoRotationMin = -90;

    public Slider headSlider;
    public float headRotationMax = 50;
    public float headRotationMin = -90;

    // Start is called before the first frame update
    void Start()
    {
        torsoSlider.maxValue = torsoRotationMax;
        torsoSlider.minValue = torsoRotationMin;
        torsoSlider.value = 0;

        headSlider.maxValue = headRotationMax;
        headSlider.minValue = headRotationMin;
        headSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float rotateSpeed =speedSlider.value;
        float torsoValue = torsoSlider.value;
        float headValue = headSlider.value;

        Vector3 lowerLegPosition = GameObject.Find("LowerLeg").transform.position;
        transform.RotateAround(lowerLegPosition, new Vector3(0, 1f, 0), -rotateSpeed);

        Vector3 newHipAngle = new Vector3(torsoValue, 0, 0);
        GameObject.Find("Torso").transform.localRotation = Quaternion.Euler(newHipAngle);

        Vector3 newHeadAngle = new Vector3(headValue, 0, 0);
        GameObject.Find("Head").transform.localRotation = Quaternion.Euler(newHeadAngle);
    }
}
