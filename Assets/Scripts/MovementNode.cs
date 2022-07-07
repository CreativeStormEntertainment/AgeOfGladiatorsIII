using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using PixelCrushers.DialogueSystem;

public class MovementNode : MonoBehaviour
{
    [Header("Next Location")]
    public string destinationScene;
    public int destinationNode;

    [Header("Entry Node")]
    public GameObject EntryNodeAttached;
    public Vector3 cameraAngleOffset;

    [Header("Other")]
    public bool disabled;

    public DialogueSystemTrigger DialogueTrigger;



    private void Update()
    {
        if (DialogueTrigger.questName != "")
            CheckForMissionUpdate();
    }



    public void ActivateNode(bool _travelMapNode)
    {
        // do not activate if hidden
        if (disabled)
            return;

        // player - activate in transit between maps
        PlayerScene.instance.MainCharacter.GetComponent<Movement>().inTransitBetweenMaps = true;

        // player - deactive box collider temporarily
        PlayerScene.instance.MainCharacter.GetComponent<BoxCollider>().enabled = false;

        // player - deactivate navmesh agent
        PlayerScene.instance.MainCharacter.GetComponent<NavMeshAgent>().enabled = false;

        // update all terrain map npcs (must be here)
        if (GetComponentInParent<Map>() != null)
            PersistentDataManager.instance.UpdateNPCsOnExitMap(GetComponentInParent<Map>());

        // update all terrain map items (must be here)
        if (GetComponentInParent<Map>() != null)
            PersistentDataManager.instance.UpdateMapItemsOnExitMap(GetComponentInParent<Map>());

        // open map or location
        if (!_travelMapNode)
            SceneSwitch.SceneSwitcherAsync(destinationScene, destinationNode);
    }

    public void CheckForMissionUpdate()
    {
        if (Vector3.Distance(PlayerScene.instance.MainCharacter.transform.position, transform.position) <= 3)
            DialogueTrigger.OnUse();
    }
}
