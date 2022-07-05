using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public static class Inventory
{
    public static List<Item> InventoryList = new List<Item>();



    // add to inventory
    public static void AddToInventory(Item _Item)
    {
        InventoryList.Add(_Item);

        // check if quest item
        if (_Item.ItemClass == ItemClasses.Mission)
            CheckIfMissionItem(_Item as MissionItem);
    }

    // remove from inventory
    public static void RemoveFromInventory(Item _Item)
    {
        InventoryList.Remove(_Item);
    }



    // check if mission item
    static void CheckIfMissionItem(MissionItem _Item)
    {
        // advance quest
        if (_Item.MissionTriggerType == QuestTriggers.QuestEntryAdvance)
            GameActions.instance.AdvanceQuest(_Item.questName, _Item.questEntry);

        // activate quest
        if (_Item.MissionTriggerType == QuestTriggers.QuestActivate)
            GameActions.instance.StartQuest(_Item.questName);
    }

    // check if inventory contains quest item
    public static bool CheckIfInventoryContainsQuestItem(string _questName, int _questEntry)
    {
        bool _itemPresent = false;

        foreach (Item _Item in InventoryList)
        {
            if (_Item.ItemClass == ItemClasses.Mission)
            {
                MissionItem _MissionItem = _Item as MissionItem;

                if ((_MissionItem.questName == _questName) && (_MissionItem.questEntry == _questEntry))
                    _itemPresent = true;
            }
        }

        return _itemPresent;
    }
}
