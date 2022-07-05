using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public Transform Player;



    private void LateUpdate()
    {
        Vector3 newPosition = Player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // optional rotate
        //transform.rotation = Quaternion.Euler(90f, Player.eulerAngles.y, 0f);
    }
}
