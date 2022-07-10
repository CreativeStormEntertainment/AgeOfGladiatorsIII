using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : Item
{
    public Accessory(int _itemNumber)
    {
        ItemClass = ItemClasses.Accessory;
        itemNumber = _itemNumber;

        CalculateCost();
    }



    public override Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = PortraitImages.instance.AccessoryPortraits[itemNumber];

        return _ItemPortrait;
    }

    public override string GetItemName()
    {
        string _name = "Accessory";

        return _name;
    }

    public override string GetItemClassDescription()
    {
        string _name = ItemClass.ToString();

        return _name;
    }



    public override int CalculateCost()
    {
        int _cost = 1000;

        return _cost;
    }
}
