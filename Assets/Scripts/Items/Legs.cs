using UnityEngine;

public class Legs : Item
{
    public ArmorTypes ArmorType;

    public int armor;



    public Legs(ArmorTypes _type, int _itemNumber)
    {
        ItemClass = ItemClasses.Legs;
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
                _stat = Random.Range(1, GameData.legsLight);
                break;
            case ArmorTypes.Medium:
                _stat = Random.Range(1, GameData.legsMedium);
                break;
            case ArmorTypes.Heavy:
                _stat = Random.Range(1, GameData.legsHeavy);
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
                _ItemPortrait = PortraitImages.instance.LightLegPortraits[itemNumber];
                break;
            case ArmorTypes.Medium:
                _ItemPortrait = PortraitImages.instance.MediumLegPortraits[itemNumber];
                break;
            case ArmorTypes.Heavy:
                _ItemPortrait = PortraitImages.instance.HeavyLegPortraits[itemNumber];
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
                _name = "Padded Pants";
                break;
            case 1:
                _name = "Padded Pants";
                break;
            case 2:
                _name = "Padded Pants";
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
                _name = "Reinforced Pants";
                break;
            case 1:
                _name = "Reinforced Pants";
                break;
            case 2:
                _name = "Reinforced Pants";
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
                _name = "Armored Pants";
                break;
            case 1:
                _name = "Armored Pants";
                break;
            case 2:
                _name = "Armored Pants";
                break;
        }

        return _name;
    }



    // get item description (override)
    public override string GetItemDescription()
    {
        string _name = "Judge class leg armor designed to protect the groin and lower extremities.";

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
