using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraZoom : MonoBehaviour
{
    [Tooltip("Multiplied by zoom scroll input")]
    public float zoomMultiplyer;
    [Tooltip("Max distance camera can zoom (Higher value is snappier)")]
    public float maxZoomIncrement;
    public AnimationCurve vcam1Blend;
    public AnimationCurve vcam2Blend;
    public CinemachineMixingCamera mixCam;
    private float zoomTarget;
    private float zoom;



    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            zoomTarget = Mathf.Clamp(zoom + (zoomMultiplyer * Input.GetAxis("Mouse ScrollWheel")), 0, 1);
        }

        if (zoom != zoomTarget)
        {
            zoom = Mathf.MoveTowards(zoom, zoomTarget, maxZoomIncrement * Time.deltaTime);
            CalcZoom(zoom);
        }
    }



    public void CalcZoom(float zoom)
    {
        mixCam.SetWeight(0, vcam1Blend.Evaluate(zoom));
        mixCam.SetWeight(1, vcam2Blend.Evaluate(zoom));
    }
}
