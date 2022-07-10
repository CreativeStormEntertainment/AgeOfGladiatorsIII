using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTarget : MonoBehaviour
{
    public Camera cam;
    [Tooltip("Multiplied by distance to determine movement speed to target")]
    public float moveMultiplier;
    private float moveSpd = 10;
    public Transform target;

    private Vector3 clickPos;
    private bool checkDoubleClick;
    private float elapsedTime;
    private float doubleClickTime = 0.4f;



    void Update()
    {
        // -------------------------------------------------
        // camera target
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeTarget(PlayerScene.instance.MainCharacter.transform);
        }
        // -------------------------------------------------

        //move to target
        if (target != null)
        {
            //Debug.Log(target.name);

            if (transform.position != target.position)
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpd * Time.deltaTime);
        }

        //Check for double clicks
        if (Input.GetMouseButtonUp(1))
        {
            if (checkDoubleClick)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                if (Vector3.Distance(clickPos, Input.mousePosition) < 1f && elapsedTime < doubleClickTime)
                {
                    checkDoubleClick = false;
                    GetTarget(Input.mousePosition);
                }
            }
            else
            {
                clickPos = Input.mousePosition;
                checkDoubleClick = true;
            }
        }

        if (checkDoubleClick)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > doubleClickTime)
            {
                elapsedTime = 0;
                checkDoubleClick = false;
            }  
        }

    }



    public void ChangeTarget(Transform tar)
    {
        target = tar;
        var dis = Vector3.Distance(transform.position, tar.position);
        moveSpd = dis * moveMultiplier;
    }

    public void GetTarget(Vector3 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Add filtering for anything that shouldn't be snapped to

            ChangeTarget(hit.transform);
        }
    }

    public void AlignToTarget(Vector3 offset)
    {
        target = PlayerScene.instance.MainCharacter.transform;
        transform.position = target.position;
        transform.localEulerAngles = target.localEulerAngles + offset;
    }
}