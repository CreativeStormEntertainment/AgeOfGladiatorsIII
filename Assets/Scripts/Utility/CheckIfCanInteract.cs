using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CheckIfCanInteract
{
    // check for item conditions (map item)
    public static bool CheckForInteractableConditions(MapItem _Item)
    {
        bool _clear = true;

        // ---------------------------
        // combat activated
        if (Combat.instance.combatActivated)
            _clear = false;
        // ---------------------------

        // ---------------------------
        // perception click
        if (_Item.GetComponentInParent<TriggerPerception>() != null && !_Item.GetComponentInParent<TriggerPerception>().alreadyDiscovered)
            _clear = false;
        // ---------------------------

        // ---------------------------
        // door open
        if (_Item.GetComponent<OpenDoor>() != null && _Item.GetComponent<OpenDoor>().Doors.Any((MapDoor) => MapDoor.doorOpen))
            _clear = false;
        // ---------------------------

        return _clear;
    }

    // check for item conditions (npc)
    public static bool CheckForInteractableConditions(NPC _NPC)
    {
        bool _clear = true;

        // ---------------------------
        // combat activated
        //if (Combat.combatActivated)
        //    _clear = false;
        // ---------------------------

        // ---------------------------
        // npc
        if (_NPC.cannotApproach)
            _clear = false;
        // ---------------------------

        return _clear;
    }
}
