using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionItem : Item
{
    public string questName;
    public int questEntry;
    public QuestTriggers MissionTriggerType;



    public MissionItem(QuestTriggers _QuestType, string _questName, int _missionComponent, int _itemNumber)
    {
        ItemClass = ItemClasses.Mission;
        itemNumber = _itemNumber;

        MissionTriggerType = _QuestType;
        questName = _questName;
        questEntry = _missionComponent;
    }



    // get portrait (override)
    public override Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = PortraitImages.instance.MissionItemPortraits[itemNumber];

        return _ItemPortrait;
    }

    // get item name (override)
    public override string GetItemName()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Pendant";
                break;
            case 1:
                _name = "Book";
                break;
            case 2:
                _name = "Poster";
                break;
        }

        return _name;
    }

    // calculate cost (override)
    public override int CalculateCost()
    {
        int _cost = 1000;

        return _cost;
    }
}
