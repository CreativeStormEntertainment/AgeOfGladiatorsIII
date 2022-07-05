using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityPanel : MonoBehaviour
{
    public List<GameObject> Buttons = new List<GameObject>();



    private void Start()
    {
        Buttons[0].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Character <color=#ffc149>(C)</color>");
        Buttons[1].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Inventory <color=#ffc149>(I)</color>");
        Buttons[2].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Journal <color=#ffc149>(J)</color>");
        Buttons[3].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Map <color=#ffc149>(M)</color>");
        Buttons[4].GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"Menu <color=#ffc149>(ESc)</color>");
    }
}
