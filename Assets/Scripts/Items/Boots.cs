using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Item
{
    public ArmorTypes ArmorType;

    public int armor;



    public Boots(ArmorTypes _type, int _itemNumber)
    {
        ItemClass = ItemClasses.Boots;
        itemNumber = _itemNumber;
        ArmorType = _type;

        CreateArmorStat(ref armor);
        CalculateCost();
    }



    // create armor stat
    public void CreateArmorStat(ref int _stat)
    {
        switch (ArmorType)
        {
            case ArmorTypes.Light:
                _stat = Random.Range(1, GameData.bootsLight);
                break;
            case ArmorTypes.Medium:
                _stat = Random.Range(1, GameData.bootsMedium);
                break;
            case ArmorTypes.Heavy:
                _stat = Random.Range(1, GameData.bootsHeavy);
                break;
        }
    }



    // get portrait (override)
    public override Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = null;

        switch (ArmorType)
        {
            case ArmorTypes.Light:
                _ItemPortrait = PortraitImages.instance.LightBootPortraits[itemNumber];
                break;
            case ArmorTypes.Medium:
                _ItemPortrait = PortraitImages.instance.MediumBootPortraits[itemNumber];
                break;
            case ArmorTypes.Heavy:
                _ItemPortrait = PortraitImages.instance.HeavyBootPortraits[itemNumber];
                break;
        }

        return _ItemPortrait;
    }



    // get item name (override)
    public override string GetItemName()
    {
        string _name = "";

        switch (ArmorType)
        {
            case ArmorTypes.Light:
                _name = LightNames();
                break;
            case ArmorTypes.Medium:
                _name = MediumNames();
                break;
            case ArmorTypes.Heavy:
                _name = HeavyNames();
                break;
        }

        return _name;
    }

    // light names
    string LightNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Padded Boots";
                break;
            case 1:
                _name = "Padded Boots";
                break;
            case 2:
                _name = "Padded Boots";
                break;
        }

        return _name;
    }

    // medium names
    string MediumNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Reinforced Boots";
                break;
            case 1:
                _name = "Reinforced Boots";
                break;
            case 2:
                _name = "Reinforced Boots";
                break;
        }

        return _name;
    }

    // heavy names
    string HeavyNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Military-Grade Boots";
                break;
            case 1:
                _name = "Military-Grade Boots";
                break;
            case 2:
                _name = "Military-Grade Boots";
                break;
        }

        return _name;
    }



    // get item description (override)
    public override string GetItemDescription()
    {
        string _name = "Judge class foot armor designed to protect the feet. Small pouches for extra storage capacity.";

        return _name;
    }



    // get item class description (override)
    public override string GetItemClassDescription()
    {
        string _name = ArmorType.ToString();

        return _name;
    }



    // calculate cost (override)
    public override int CalculateCost()
    {
        int _cost = 0;

        _cost = armor * 1000;

        return _cost;
    }
}
