using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float panSpd;
    public CameraTarget camTar;



    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            CalcPanning();

    }



    public void CalcPanning()
    {
        if (camTar.target != null)
            camTar.target = null;

        var y = transform.position.y;

        if (Input.GetKey(KeyCode.W))
            transform.localPosition += transform.forward * panSpd * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            transform.localPosition += -transform.right * panSpd * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.localPosition += -transform.forward * panSpd * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.localPosition += transform.right * panSpd * Time.deltaTime;

        //Prevent height from changing to due camera tilt
        var pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }
}
