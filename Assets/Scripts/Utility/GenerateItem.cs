using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateItem
{
    // generate random weapon
    public static Weapon RandomGenerateWeapon()
    {
        //WeaponTypes _Type = EnumList.GetRandomEnum<WeaponTypes>();

        WeaponTypes _Type = WeaponTypes.Pistol;

        Weapon _Item = new Weapon(_Type, Random.Range(0, 3));

        return _Item;
    }

    // generate random helmet
    public static Helmet RandomGenerateHelmet()
    {
        ArmorTypes _Type = EnumList.GetRandomEnum<ArmorTypes>();

        Helmet _Item = new Helmet(_Type, 0);

        return _Item;
    }

    // generate random chest
    public static Chest RandomGenerateChest()
    {
        ArmorTypes _Type = EnumList.GetRandomEnum<ArmorTypes>();

        Chest _Item = new Chest(_Type, 0);

        return _Item;
    }

    // generate random legs
    public static Legs RandomGenerateLegs()
    {
        ArmorTypes _Type = EnumList.GetRandomEnum<ArmorTypes>();

        Legs _Item = new Legs(_Type, 0);

        return _Item;
    }

    // generate random legs
    public static Boots RandomGenerateBoots()
    {
        ArmorTypes _Type = EnumList.GetRandomEnum<ArmorTypes>();

        Boots _Item = new Boots(_Type, 0);

        return _Item;
    }

    // generate random hands
    public static Gloves RandomGenerateGloves()
    {
        ArmorTypes _Type = EnumList.GetRandomEnum<ArmorTypes>();

        Gloves _Item = new Gloves(_Type, 0);

        return _Item;
    }
}
