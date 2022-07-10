using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Item
{
    public ArmorTypes ArmorType;
    public int armor;



    public Helmet(ArmorTypes _type, int _itemNumber)
    {
        ItemClass = ItemClasses.Helmet;
        itemNumber = _itemNumber;
        ArmorType = _type;

        CreateArmorStat(ref armor);
    }



    public void CreateArmorStat(ref int _stat)
    {
        switch (ArmorType)
        {
            case ArmorTypes.Light:
                _stat = Random.Range(1, GameData.helmetLight);
                break;
            case ArmorTypes.Medium:
                _stat = Random.Range(1, GameData.helmetMedium);
                break;
            case ArmorTypes.Heavy:
                _stat = Random.Range(1, GameData.helmetHeavy);
                break;
        }
    }



    public override Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = null;

        switch (ArmorType)
        {
            case ArmorTypes.Light:
                _ItemPortrait = PortraitImages.instance.LightHelmetPortraits[itemNumber];
                break;
            case ArmorTypes.Medium:
                _ItemPortrait = PortraitImages.instance.MediumHelmetPortraits[itemNumber];
                break;
            case ArmorTypes.Heavy:
                _ItemPortrait = PortraitImages.instance.HeavyHelmetPortraits[itemNumber];
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
                _name = "Light Helmet";
                break;
            case 1:
                _name = "Light Helmet";
                break;
            case 2:
                _name = "Light Helmet";
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
                _name = "Medium Helmet";
                break;
            case 1:
                _name = "Medium Helmet";
                break;
            case 2:
                _name = "Medium Helmet";
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
                _name = "Heavy Helmet";
                break;
            case 1:
                _name = "Heavy Helmet";
                break;
            case 2:
                _name = "Heavy Helmet";
                break;
        }

        return _name;
    }
    
    public override string GetItemDescription()
    {
        string _name = "Judge class helmet designed for maximum head protection.";

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
