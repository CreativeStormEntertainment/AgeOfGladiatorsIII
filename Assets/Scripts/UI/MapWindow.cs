using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWindow : MonoBehaviour
{
    public List<KeyIcon> KeyIcons = new List<KeyIcon>();



    // populate map
    public void Populate()
    {
        ActivateMapIcons(true);
        //ActivateMapOverlay(true);

        KeyIcons[0].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Quest");
        KeyIcons[1].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Player");
        KeyIcons[2].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"NPC");
        KeyIcons[3].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Crate");
        KeyIcons[4].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Computer");
        KeyIcons[5].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Door");
    }

    // close map
    public void Close()
    {
        ActivateMapIcons(false);
        //ActivateMapOverlay(false);
    }



    // activate map overlay
    void ActivateMapOverlay(bool _activate)
    {
        var FoundObjects = FindObjectsOfType<Map>();

        foreach (Map _Map in FoundObjects)
        {
            if (_Map.MapOverlay != null)
                _Map.MapOverlay.gameObject.SetActive(_activate);
        }
    }

    // activate map icons
    public void ActivateMapIcons(bool _activate)
    {
        // -----------------------------
        var FoundObjects = FindObjectsOfType<MapIcon>();

        foreach (MapIcon _Icon in FoundObjects)
            _Icon.ActivateIcon(_activate);
        // -----------------------------
    }
}
