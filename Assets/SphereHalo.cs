using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHalo : MonoBehaviour
{
    public GameObject xAxisControl;
    public GameObject yAxisControl;
    public GameObject zAxisControl;
    [Space(10)]
    public LineRenderer XToObjectLine;
    public LineRenderer YToObjectLine;
    public LineRenderer ZToObjectLine;

    public GameObject Target { get; private set; }

    public bool debugLinesOn = false;

    //TEST FOR DYNAMISM



    void Update()
    {
        XToObjectLine.enabled = debugLinesOn;
        YToObjectLine.enabled = debugLinesOn;
        ZToObjectLine.enabled = debugLinesOn;

        if (Target)
        {
            XToObjectLine.SetPositions(new Vector3[] { xAxisControl.transform.position, Target.transform.position });
            YToObjectLine.SetPositions(new Vector3[] { yAxisControl.transform.position, Target.transform.position });
            ZToObjectLine.SetPositions(new Vector3[] { zAxisControl.transform.position, Target.transform.position });

            xAxisControl.transform.localRotation = Target.transform.rotation;
            yAxisControl.transform.localRotation = Target.transform.rotation;
            zAxisControl.transform.localRotation = Target.transform.rotation;
        }
    }

    public void SetControlTarget(GameObject newTarget)
    {
        Target = newTarget;
    }


}
