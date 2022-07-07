using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System;

public class Map : MonoBehaviour
{
    public static event Action<bool> OnLoad;

    [Header("Movement Nodes")]
    public MovementNode[] MovementNodes;

    [Header("Travel Nodes Parent")]
    public GameObject MovementNodesParent;

    [Header("Map NPCs")]
    public GameObject MapNPCS;

    [Header("Map Containers")]
    public GameObject MapItems;

    [Header("Map Overlay")]
    public GameObject MapOverlay;

    [Header("Combat Grid")]
    public CombatGrid AttachedGrid;

    [Header("Ambient Sounds")]
    public int ambientIndex;



    void Awake()
    {
        SupplyAllContainers();

        PersistentDataManager.instance.StoreNPCsOnAwake(this);
        PersistentDataManager.instance.StoreMapItemsOnAwake(this);

        // make grid invisible
        foreach (Transform child in AttachedGrid.transform)
        {
            CombatGridCell _Cell = child.GetComponent<CombatGridCell>();
            _Cell.cellBorder.SetActive(false);
        }
    }

    void Start()
    {
        PlayerScene.instance.SceneCamera.gameObject.SetActive(true);
        PlayerScene.instance.MainCharacter.gameObject.SetActive(true);

        // update grid
        AttachedGrid.boxCol.enabled = false;
        AttachedGrid.ClearAP(); // prevent ap box from showing during load

        // set cursor
        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);

        // place character by entry node
        PlaceMainCharacterOnEntryNode();
        PersistentDataManager.instance.UpdateNPCsOnEnterMap(this);
        PersistentDataManager.instance.UpdateMapItemsOnEnterMap(this);

        // update map items
        UpdateAllDoors();
        UpdateAllNPCEquipment();

        // update player animator (male or female)
        UpdatePlayerAnimator();

        // sounds 
        AmbientSounds.instance.TransitionToAmbient(1f, ambientIndex);

        // close travel window in case switching from travel
        //UI.instance.CloseTravel();

    }



    public void PlaceMainCharacterOnEntryNode()
    {
        // determine node
        MovementNode _movementNode = MovementNodes[SceneSwitch.entryNode];

        // move character to entry node attached to movement node
        PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().Target = _movementNode.EntryNodeAttached.transform.position;
        PlayerScene.instance.MainCharacter.transform.position = _movementNode.EntryNodeAttached.transform.position;

        // look opposite direction of movement node
        //if ((_movementNode.EntryNodeAttached.transform.position - PlayerScene.instance.MainCharacter.transform.position) != Vector3.zero)
        //PlayerScene.instance.MainCharacter.transform.rotation = Quaternion.LookRotation(_movementNode.EntryNodeAttached.transform.position - PlayerScene.instance.MainCharacter.transform.position);

        // rotate direction movement node is facing
        PlayerScene.instance.MainCharacter.transform.rotation = _movementNode.transform.rotation;

        // reactivate character box collider
        PlayerScene.instance.MainCharacter.GetComponentInChildren<BoxCollider>().enabled = true;

        // reactivate characte nav mesh agent
        PlayerScene.instance.MainCharacter.GetComponent<NavMeshAgent>().enabled = true;

        // player - activate in transit between maps
        PlayerScene.instance.MainCharacter.GetComponent<Movement>().inTransitBetweenMaps = false;

        //Reset camera rotation
        PlayerScene.instance.CameraTarget.AlignToTarget(_movementNode.cameraAngleOffset);

        OnLoad?.Invoke(true);
    }

    public void SupplyAllContainers()
    {
        // supply all containers (here so not to supply twice due to persistent saving)

        foreach (Transform child in MapItems.transform)
        {
            if (child.gameObject.GetComponent<MapItemContainer>() != null)
                child.gameObject.GetComponent<MapItemContainer>().Supply();
        }
    }



    public void UpdateAllDoors()
    {
        foreach (Transform child in MapItems.transform)
        {
            if (child.gameObject.GetComponent<MapDoor>() != null && child.gameObject.GetComponent<MapDoor>().doorOpen)
                child.gameObject.GetComponent<MapDoor>().PlayAnimation();
        }
    }

    public void UpdateAllNPCEquipment()
    {
        foreach (Transform child in MapNPCS.transform)
        {
            if (child.gameObject.GetComponent<NPC>() != null)
            {
                NPC _NPC = child.gameObject.GetComponent<NPC>();

                _NPC.GetComponent<EquipNPC>().EquipWeaponOnStart();
                _NPC.UpdateEquipmentOnModel();
            }
        }
    }

    public void UpdatePlayerAnimator()
    {
        // this needs to be here to attach male or female animator
        PlayerScene.instance.MainCharacter.GetComponent<CharacterAnimator>().AttachCharacter();
    }
}
