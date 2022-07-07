using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWindow : MonoBehaviour
{
    public List<KeyIcon> KeyIcons = new List<KeyIcon>();



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



    void ActivateMapOverlay(bool _activate)
    {
        var FoundObjects = FindObjectsOfType<Map>();

        foreach (Map _Map in FoundObjects)
        {
            if (_Map.MapOverlay != null)
                _Map.MapOverlay.gameObject.SetActive(_activate);
        }
    }

    public void ActivateMapIcons(bool _activate)
    {
        // -----------------------------
        var FoundObjects = FindObjectsOfType<MapIcon>();

        foreach (MapIcon _Icon in FoundObjects)
            _Icon.ActivateIcon(_activate);
        // -----------------------------
    }



    public void Close()
    {
        ActivateMapIcons(false);
        //ActivateMapOverlay(false);
    }
}
