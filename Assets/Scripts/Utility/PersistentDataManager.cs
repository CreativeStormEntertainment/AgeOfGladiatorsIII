using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PersistentDataManager : MonoBehaviour
{
    public static PersistentDataManager instance;

    [Header("Parents")]
    public GameObject NPCParent;
    public GameObject MapItemParent;

    [Header("Lists")]
    public List<NPC> NPCList = new List<NPC>();
    public List<MapItem> MapItemList = new List<MapItem>();



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }




    // inital storage/addition of npc data to here
    public void StoreNPCsOnAwake(Map _Map)
    {
        // add map npcs on start to persistent list (happens only once)
        int count = 0;

        foreach (Transform child in _Map.MapNPCS.transform)
        {
            NPC _npc = child.GetComponent<NPC>();
            _npc.npcID = SceneManager.GetActiveScene().name + "-" + count;

            if (!NPCList.Any((NPC) => NPC.npcID == _npc.npcID))
                InstantiateNPC(_npc);

            count++;
        }
    }

    // instantiate (npc)
    public void InstantiateNPC(NPC _Original)
    {
        // instantiate prefab
        GameObject _npcPrefab = Instantiate(Resources.Load("Utility-Instantiate-NPC")) as GameObject;
        _npcPrefab.transform.SetParent(NPCParent.transform, false);

        // copy over npc info
        NPC _New = _npcPrefab.GetComponent<NPC>();
        _New.TransferSettingsToNPC(_Original);

        // copy over crime
        TransferNPCOtherSettings(_New, _Original);

        NPCList.Add(_New);
    }

    // update npcs (enter)
    public void UpdateNPCsOnEnterMap(Map _Map)
    {
        // update map npcs with with persistent list data
        foreach (Transform child in _Map.MapNPCS.transform)
        {
            NPC _mapNPC = child.GetComponent<NPC>();
            NPC _listNPC = NPCList.Where(obj => obj.npcID == _mapNPC.npcID).SingleOrDefault();
            _mapNPC.TransferSettingsToNPC(_listNPC);
            TransferNPCOtherSettings(_mapNPC, _listNPC);
        }
    }

    // update npcs (exit)
    public void UpdateNPCsOnExitMap(Map _Map)
    {
        // update persistent list with map npc data
        foreach (Transform child in _Map.MapNPCS.transform)
        {
            NPC _mapNPC = child.GetComponent<NPC>();
            NPC _listNPC = NPCList.Where(obj => obj.npcID == _mapNPC.npcID).SingleOrDefault();
            _listNPC.TransferSettingsToNPC(_mapNPC);
            TransferNPCOtherSettings(_listNPC, _mapNPC);
        }
    }

    // transfer settings
    public void TransferNPCOtherSettings(NPC _Recieving, NPC _Incoming)
    {
        // ------------------------------
        // crime
        if (_Incoming.GetComponent<Crime>() != null)
            _Recieving.GetComponent<NPC>().TransferCrimeSettings(_Incoming);
        // ------------------------------
    }



    // inital storage/addition of map item data to here
    public void StoreMapItemsOnAwake(Map _Map)
    {
        int count = 0;

        foreach (Transform child in _Map.MapItems.transform)
        {
            if (child.GetComponent<MapItem>() != null)
            {
                MapItem _container = child.GetComponent<MapItem>();
                _container.mapItemID = _container.name + "-" + SceneManager.GetActiveScene().name + "-" + count;

                if (!MapItemList.Any((MapItemContainer) => MapItemContainer.mapItemID == _container.mapItemID))
                    InstantiateMapItem(_container);

                count++;
            }
        }
    }

    // instantiate (map item)
    public void InstantiateMapItem(MapItem _Original)
    {
        // -----------------------------------------
        // instantiate prefab
        GameObject _prefab = null;
        
        // create prefab (inventory)
        if (_Original.GetComponent<MapItemContainer>())
            _prefab = Instantiate(Resources.Load("Map-Inventory-Crate")) as GameObject;

        // create prefab (viewable)
        if (_Original.GetComponent<TriggerViewItem>())
            _prefab = Instantiate(Resources.Load("Map-Viewable-Item")) as GameObject;

        // create prefab (door)
        if (_Original.GetComponent<MapDoor>())
            _prefab = Instantiate(Resources.Load("Map-Door")) as GameObject;
        // -----------------------------------------

        // -----------------------------------------
        // other
        if (_prefab != null)
        {
            // set parent
            _prefab.transform.SetParent(MapItemParent.transform, false);

            // copy over info
            MapItem _New = _prefab.GetComponent<MapItem>();
            //_New.Supply();

            // transfer all settings
            TransferMapItemSettings(_New, _Original);

            // add to list
            MapItemList.Add(_New);
        }
        // -----------------------------------------
    }

    // update map (enter)
    public void UpdateMapItemsOnEnterMap(Map _Map)
    {
        foreach (Transform child in _Map.MapItems.transform)
        {
            if (child.GetComponent<MapItem>() != null)
            {
                MapItem _mapItem = child.GetComponent<MapItem>();
                MapItem _listMapItem = MapItemList.Where(obj => obj.mapItemID == _mapItem.mapItemID).SingleOrDefault();

                if (_mapItem != null && _listMapItem != null)
                    TransferMapItemSettings(_mapItem, _listMapItem);
            }
        }
    }

    // update map (exit)
    public void UpdateMapItemsOnExitMap(Map _Map)
    {
        // ----------------------------------------------------
        // containers
        foreach (Transform child in _Map.MapItems.transform)
        {
            if (child.GetComponent<MapItem>() != null)
            {
                MapItem _mapItem = child.GetComponent<MapItem>();
                MapItem _listMapItem = MapItemList.Where(obj => obj.mapItemID == _mapItem.mapItemID).SingleOrDefault();

                if (_mapItem != null && _listMapItem != null)
                    TransferMapItemSettings(_listMapItem, _mapItem);
            }
        }
        // ----------------------------------------------------
    }

    // transfer settings
    public void TransferMapItemSettings(MapItem _Recieving, MapItem _Incoming)
    {
        // active state
        _Recieving.gameObject.SetActive(_Incoming.gameObject.activeSelf);

        // map item id
        _Recieving.mapItemID = _Incoming.mapItemID;

        // ------------------------------
        // container
        if (_Incoming.GetComponent<MapItemContainer>() != null)
            _Recieving.GetComponent<MapItem>().TransferContainerSettings(_Incoming.GetComponent<MapItemContainer>());

        // door
        if (_Incoming.GetComponent<MapDoor>() != null)
            _Recieving.GetComponent<MapItem>().TransferDoorSettings(_Incoming.GetComponent<MapDoor>());

        // view
        if (_Incoming.GetComponent<TriggerViewItem>() != null)
            _Recieving.GetComponent<MapItem>().TransferViewSettings(_Incoming.GetComponent<TriggerViewItem>());

        // perception
        if (_Incoming.GetComponent<TriggerPerception>() != null)
            _Recieving.GetComponent<MapItem>().TransferPerceptionSettings(_Incoming.GetComponent<TriggerPerception>());

        // stat rolls
        if (_Incoming.GetComponent<TriggerSkillRoll>() != null)
            _Recieving.GetComponent<MapItem>().TransferStatRollSettings(_Incoming.GetComponent<TriggerSkillRoll>());
        // ------------------------------
    }
}
