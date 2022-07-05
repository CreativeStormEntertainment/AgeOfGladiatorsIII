using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TravelWindow : MonoBehaviour
{
    [Header("UI")]
    public Button CloseButton;

    [Header("Key")]
    public Image KeyMissions;
    public Image KeyCrafting;

    [Header("Party")]
    public Image Party;

    [Header("Target Node")]
    public TravelWindowNode TargetNode;

    [Header("Travel Nodes Parent")]
    public GameObject TravelNodesParent;



    void Update()
    {
        MoveToNode();
    }



    // populate map
    public void PopulateMap()
    {
        // -----------------------------------------------------
        // make close button interactable
        CloseButton.interactable = true;
        // -----------------------------------------------------

        // -----------------------------------------------------
        // nodes interactable or not
        foreach (Transform child in TravelNodesParent.transform)
        {
            Button _TravelNode = child.GetComponent<Button>();

            if (WorldMap.instance.AreasUnlocked.Contains(_TravelNode.GetComponent<TravelWindowNode>().nodeNumber))
            {
                _TravelNode.GetComponent<TravelWindowNode>().LockedIcon.gameObject.SetActive(false);
                _TravelNode.interactable = true;
            }
            else
            {
                _TravelNode.GetComponent<TravelWindowNode>().LockedIcon.gameObject.SetActive(true);
                _TravelNode.interactable = false;
            }

            AssignTooltipToNode(_TravelNode.GetComponent<TravelWindowNode>());
        }
        // -----------------------------------------------------

        // -----------------------------------------------------
        // move party to last node visited
        foreach (Transform child in TravelNodesParent.transform)
        {
            TravelWindowNode _TravelNode = child.GetComponent<TravelWindowNode>();

            if (_TravelNode.nodeNumber == SceneSwitch.travelMapNode)
                Party.transform.position = _TravelNode.EntryNode.transform.position;
        }
        // -----------------------------------------------------

        // -----------------------------------------------------
        // assign tooltips
        foreach (Transform child in TravelNodesParent.transform)
            AssignTooltipToNode(child.GetComponent<Button>().GetComponent<TravelWindowNode>());
        // -----------------------------------------------------

        // -----------------------------------------------------
        // map keys
        // -----------------------------------------------------
    }



    // node click
    public void NodeClick()
    {
        // assign target node (and last node visited)
        TargetNode = EventSystem.current.currentSelectedGameObject.GetComponent<TravelWindowNode>();

        // store node number for next time window is opened
        SceneSwitch.travelMapNode = TargetNode.nodeNumber;

        // do not allow buttons or nodes to interact
        CloseButton.interactable = false;

        // sound
        UISounds.instance.PlayActionTravel();

        // do not reset character on node (for closing window without traveling)
        UI.instance.characterDidNotTravel = false;

        // no interaction for other nodes
        foreach (Transform child in TravelNodesParent.transform)
        {
            Button _TravelNode = child.GetComponent<Button>();
            _TravelNode.interactable = false;
        }

        // close tooltips
        foreach (Transform child in TravelNodesParent.transform)
        {
            if (child.GetComponent<ModelShark.TooltipTrigger>() != null)
                child.GetComponent<ModelShark.TooltipTrigger>().StopHover();
        }
    }

    // move to node
    public void MoveToNode()
    {
        if (TargetNode == null)
            return;

        // move toward target
        Party.transform.position = Vector2.MoveTowards(Party.transform.position, TargetNode.EntryNode.transform.position, (120 * Time.deltaTime));

        // transition to new location
        if (Vector3.Distance(Party.transform.position, TargetNode.EntryNode.transform.position) <= 0.5f)
        {
            TransitionLocation();
            TargetNode = null;
        }
    }

    // transition to new location
    void TransitionLocation()
    {
        switch (TargetNode.nodeNumber)
        {
            case 0:
                SceneSwitch.SceneSwitcherAsync("Map-Catwalk", 0);
                break;
            case 1:
                SceneSwitch.SceneSwitcherAsync("Map-Studio-Entrance", 0);
                break;
            case 2:
                SceneSwitch.SceneSwitcherAsync("Map-Sector-House", 0);
                break;
            case 3:
                break;
        }

        UI.instance.CloseTravel();
    }



    // assign tooltip to node
    void AssignTooltipToNode(TravelWindowNode _Node)
    {
        if (_Node.GetComponent<ModelShark.TooltipTrigger>() == null)
            return;

        // assign tooltip
        switch (_Node.nodeNumber)
        {
            case 0:
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("TitleText", "Mega-City One (Catwalks)");
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("Stats", "The restless and crowded catwalks of Mega-City One bustles with activity - both legal and illegal. Be on the lookout for any criminal activity.");
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("Description", "Danger Level: <color=#ffc149>Moderate</color>");
                break;
            case 1:
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("TitleText", "Mega-City One (Sewers)");
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("Stats", "Stinking, dangerous, and one of many entrances to the Mega-City One Sewer network. There are rumours of illegal broadcasts coming from this area. Travel with care.");
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("Description", "Danger Level: <color=#ffc149>High</color>");
                break;
            case 2:
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("TitleText", "Mega-City One (Sector House)");
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("Stats", "The restless and crowded street markets of Mega-City One bustles with activity - both legal and illegal. Be on the lookout for any criminal activity.");
                _Node.GetComponent<ModelShark.TooltipTrigger>().SetText("Description", "Danger Level: <color=#ffc149>None</color>");
                break;
        }
    }
}
