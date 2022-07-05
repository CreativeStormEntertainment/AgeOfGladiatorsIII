using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedIndicator : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
    }
}
