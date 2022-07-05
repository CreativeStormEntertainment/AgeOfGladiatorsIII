using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    public string mapItemID;
    public string mapItemName;



    public void TransferContainerSettings(MapItemContainer _incoming)
    {
        if (GetComponent<MapItemContainer>() == null)
            gameObject.AddComponent<MapItemContainer>();

        GetComponent<MapItemContainer>().CrateSupplyType = _incoming.CrateSupplyType;
        GetComponent<MapItemContainer>().disappearWhenPickedUp = _incoming.disappearWhenPickedUp;
        GetComponent<MapItemContainer>().specificItemNumber = _incoming.specificItemNumber;

        GetComponent<MapItemContainer>().ContainedItems = _incoming.ContainedItems;
    }

    public void TransferDoorSettings(MapDoor _incoming)
    {
        if (GetComponent<MapDoor>() == null)
            gameObject.AddComponent<MapDoor>();

        GetComponent<MapDoor>().doorOpen = _incoming.doorOpen;
    }

    public void TransferViewSettings(TriggerViewItem _incoming)
    {
        if (GetComponent<TriggerViewItem>() == null)
            gameObject.AddComponent<TriggerViewItem>();
    }

    public void TransferPerceptionSettings(TriggerPerception _incoming)
    {
        if (GetComponent<TriggerPerception>() == null)
            gameObject.AddComponent<TriggerPerception>();

        GetComponent<TriggerPerception>().alreadyDiscovered = _incoming.alreadyDiscovered;
    }

    public void TransferStatRollSettings(TriggerSkillRoll _incoming)
    {
        if (GetComponent<TriggerSkillRoll>() == null)
            gameObject.AddComponent<TriggerSkillRoll>();

        GetComponent<TriggerSkillRoll>().itemLocked = _incoming.itemLocked;
    }
}
