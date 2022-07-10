using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Item
{
    public ArmorTypes ArmorType;
    public int armor;



    public Chest(ArmorTypes _type, int _itemNumber)
    {
        ItemClass = ItemClasses.Chest;
        itemNumber = _itemNumber;
        ArmorType = _type;

        CreateArmorStat(ref armor);
        CalculateCost();
    }



    public void CreateArmorStat(ref int _stat)
    {
        switch (ArmorType)
        {
            case ArmorTypes.Light:
                _stat = Random.Range(1, GameData.chestLight);
                break;
            case ArmorTypes.Medium:
                _stat = Random.Range(1, GameData.chestMedium);
                break;
            case ArmorTypes.Heavy:
                _stat = Random.Range(1, GameData.chestHeavy);
                break;
        }
    }



    public override Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = null;

        switch (ArmorType)
        {
            case ArmorTypes.Light:
                _ItemPortrait = PortraitImages.instance.LightChestPortraits[itemNumber];
                break;
            case ArmorTypes.Medium:
                _ItemPortrait = PortraitImages.instance.MediumChestPortraits[itemNumber];
                break;
            case ArmorTypes.Heavy:
                _ItemPortrait = PortraitImages.instance.HeavyChestPortraits[itemNumber];
                break;
        }

        return _ItemPortrait;
    }

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

    string LightNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Padded Jacket";
                break;
            case 1:
                _name = "Padded Jacket";
                break;
            case 2:
                _name = "Padded Jacket";
                break;
        }

        return _name;
    }

    string MediumNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Reinforced Jacket";
                break;
            case 1:
                _name = "Reinforced Jacket";
                break;
            case 2:
                _name = "Reinforced Jacket";
                break;
        }

        return _name;
    }

    string HeavyNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Armored Jacket";
                break;
            case 1:
                _name = "Armored Jacket";
                break;
            case 2:
                _name = "Armored Jacket";
                break;
        }

        return _name;
    }

    public override string GetItemDescription()
    {
        string _name = "Judge class chest armor designed to protect vital organs including the heart and liver.";

        return _name;
    }

    public override string GetItemClassDescription()
    {
        string _name = ArmorType.ToString();

        return _name;
    }



    public override int CalculateCost()
    {
        int _cost = 0;

        _cost = armor * 1000;

        return _cost;
    }
}
