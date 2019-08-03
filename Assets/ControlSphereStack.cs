using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSphereStack : MonoBehaviour
{
    public GameObject xAxisControl;
    public GameObject yAxisControl;
    public GameObject zAxisControl;
    [Space(10)]
    public LineRenderer XToObjectLine;
    public LineRenderer YToObjectLine;
    public LineRenderer ZToObjectLine;
    [Space(10)]

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        XToObjectLine.SetPositions(new Vector3[] { xAxisControl.transform.position, target.transform.position });
        YToObjectLine.SetPositions(new Vector3[] { yAxisControl.transform.position, target.transform.position });
        ZToObjectLine.SetPositions(new Vector3[] { zAxisControl.transform.position, target.transform.position });


    }

    public void MoveControl(Vector3 newPosition, GameObject control)
    {
        if (control == xAxisControl)
        {
        }
        if (control == yAxisControl)
        {

        }
        if (control == zAxisControl)
        {

        }
    }
}
