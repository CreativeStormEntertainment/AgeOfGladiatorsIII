using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapItemContainer : MonoBehaviour
{
    public List<Item> ContainedItems = new List<Item>();

    [Header("Container")]
    public CrateType CrateSupplyType;
    public bool disappearWhenPickedUp;
    public int specificItemNumber = 10000;



    public void Supply()
    {
        // random containers (supply is here instead of start)
        if (CrateSupplyType == CrateType.Random)
            SupplyContainer();
        else
            SupplyContainer(specificItemNumber);   
    }



    // supply container (random)
    public void SupplyContainer()
    {
        // ------------------------------
        // add weapons
        int _random = Random.Range(1, 4);
        for (int i = 0; i < _random; i++)
            ContainedItems.Add(GenerateItem.RandomGenerateWeapon());

        // add helmets
        _random = Random.Range(1, 4);
        for (int i = 0; i < _random; i++)
            ContainedItems.Add(GenerateItem.RandomGenerateHelmet());

        // add chest
        _random = Random.Range(1, 4);
        for (int i = 0; i < _random; i++)
            ContainedItems.Add(GenerateItem.RandomGenerateChest());

        // add legs
        _random = Random.Range(1, 4);
        for (int i = 0; i < _random; i++)
            ContainedItems.Add(GenerateItem.RandomGenerateLegs());

        // add gloves
        _random = Random.Range(1, 4);
        for (int i = 0; i < _random; i++)
            ContainedItems.Add(GenerateItem.RandomGenerateGloves());

        // add boots
        _random = Random.Range(1, 4);
        for (int i = 0; i < _random; i++)
            ContainedItems.Add(GenerateItem.RandomGenerateBoots());

        // add accessory
        //_random = Random.Range(2, 4);
        //for (int i = 0; i < _random; i++)
        //    ContainedItems.Add(new Accessory(0));
        // ------------------------------
    }

    // supply container (specific)
    public void SupplyContainer(int _itemNumber)
    {
        // mission type, mission number, and mission component (component to complete), item number
        switch (_itemNumber)
        {
            case 0:
                ContainedItems.Add(new MissionItem(QuestTriggers.QuestEntryAdvance, "You Bet Your Life", 1, _itemNumber));
                break;
            case 1:
                ContainedItems.Add(new MissionItem(QuestTriggers.QuestEntryAdvance, "Street Patrol", 0, _itemNumber));
                break;
            case 2:
                ContainedItems.Add(new MissionItem(QuestTriggers.QuestActivate, "Street Patrol", 0, _itemNumber));
                break;
        }
    }

    // supply container (npc loot)
    public void SupplyContainer(Character _npc)
    {
        if (_npc.EquippedWeapon != null)
            ContainedItems.Add(_npc.EquippedWeapon);

        if (_npc.EquippedHelmet != null)
            ContainedItems.Add(_npc.EquippedHelmet);

        if (_npc.EquippedAccessory != null)
            ContainedItems.Add(_npc.EquippedAccessory);

        // testing
        for (int i = 0; i < 1; i++)
            ContainedItems.Add(GenerateItem.RandomGenerateWeapon());
    }
}
