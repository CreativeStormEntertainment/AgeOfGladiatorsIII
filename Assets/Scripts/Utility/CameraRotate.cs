using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotSpd;
    public bool inverted;
    [Tooltip("Max Y rotation\nStarting at 0 from horizontal")]
    public float maxY;
    [Tooltip("Min Y rotation\nStarts at 360 from horizontal")]
    public float minY;
    private Vector3 mouseCurPos;
    private Vector3 mousePrePos;
    private Vector3 mouseDelta;



    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            //KeyRotateLeft();
            RotateRight();

        if (Input.GetKey(KeyCode.E))
            //KeyRotateRight();
            RotateLeft();


        if (Input.GetMouseButtonDown(2))
        {
            mousePrePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            CalcRotation();
        }
    }



    public void RotateLeft()
    {
        Vector3 _Jig = this.transform.position;
        if (GetComponent<CameraTarget>().target != null)
            _Jig = GetComponent<CameraTarget>().target.position;

        transform.RotateAround(_Jig, -Vector3.up, 40 * Time.deltaTime);
    }

    public void RotateRight()
    {
        Vector3 _Jig = this.transform.position;
        if (GetComponent<CameraTarget>().target != null)
            _Jig = GetComponent<CameraTarget>().target.position;

        transform.RotateAround(_Jig, Vector3.up, 40 * Time.deltaTime);
    }



    public void CalcRotation()
    {
        mouseCurPos = Input.mousePosition;
        mouseDelta = mousePrePos - mouseCurPos;
        mousePrePos = mouseCurPos;

        var inv = inverted ? -1 : 1;
        var x = mouseDelta.y * rotSpd * inv;
        var y = mouseDelta.x * -rotSpd;

        transform.rotation *= Quaternion.AngleAxis(x, Vector3.right);
        transform.rotation *= Quaternion.AngleAxis(y, Vector3.up);

        //clamp vertical rotation
        var angles = transform.localEulerAngles;
        angles.z = 0;
        var angle = transform.localEulerAngles.x;

        if (angle > 180 && angle < minY)
            angles.x = minY;
        else if (angle < 180 && angle > maxY)
            angles.x = maxY;

        transform.localEulerAngles = angles;
    }

}
