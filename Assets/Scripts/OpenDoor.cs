using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public List<MapDoor> Doors = new List<MapDoor>();



    public void OpenAttachedDoors()
    {
        foreach (MapDoor _Door in Doors)
        {
            if (_Door.GetComponentInParent<TriggerSkillRoll>() != null)
                _Door.GetComponentInParent<TriggerSkillRoll>().itemLocked = false;

            _Door.OpenDoor();
        }
    }
}
