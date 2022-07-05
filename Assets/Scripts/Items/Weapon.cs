using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public WeaponTypes WeaponType;
    public WeaponDamage WeaponDamageType;

    public int minDamage;
    public int maxDamage;
    public int hitChance;
    public int criticalChance;
    public float criticalDamage;
    public int penetration;

    public int ammo;


    public Weapon(WeaponTypes _weaponType, int _itemNumber)
    {
        ItemClass = ItemClasses.Weapon;
        WeaponType = _weaponType;

        itemNumber = _itemNumber;

        CreateWeaponStats();
        CalculateCost();

        if (WeaponDamageType == WeaponDamage.Ranged)
            ammo = GetMaximumAmmo();
    }



    // create weapon stats
    void CreateWeaponStats()
    {
        // ----------------
        // move these population of stats into individual methods
        minDamage = Random.Range(20, 50);
        maxDamage = minDamage + Random.Range(20, 50);
        hitChance = Random.Range(10, 30);
        criticalChance = Random.Range(5, 30);
        criticalDamage = Random.Range(0.2f, 0.8f);
        // ----------------

        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                WeaponDamageType = WeaponDamage.Ranged;
                PistolStats();
                break;
            case WeaponTypes.Shotgun:
                WeaponDamageType = WeaponDamage.Ranged;
                ShotgunStats();
                break;
            case WeaponTypes.Rifle:
                WeaponDamageType = WeaponDamage.Ranged;
                RifleStats();
                break;
            case WeaponTypes.Blunt1H:
                WeaponDamageType = WeaponDamage.Melee;
                BluntStats();
                break;
            case WeaponTypes.Blade1H:
                WeaponDamageType = WeaponDamage.Melee;
                BladeStats();
                break;
            case WeaponTypes.Blunt2H:
                WeaponDamageType = WeaponDamage.Melee;
                BluntStats();
                break;
            case WeaponTypes.Blade2H:
                WeaponDamageType = WeaponDamage.Melee;
                BladeStats();
                break;
            case WeaponTypes.HeavyWeapon:
                WeaponDamageType = WeaponDamage.Ranged;
                HeavyWeaponStats();
                break;
        }
    }

    // pistol stats
    void PistolStats()
    {
        switch (itemNumber)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    // shotgun stats
    void ShotgunStats()
    {
    }

    // rifle stats
    void RifleStats()
    {
    }

    // blade stats
    void BladeStats()
    {
    }

    // blunt stats
    void BluntStats()
    {
    }

    // heavy weapon stats
    void HeavyWeaponStats()
    {
    }



    // get portrait (override)
    public override Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = null;

        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                _ItemPortrait = PortraitImages.instance.PistolPortraits[itemNumber];
                break;
            case WeaponTypes.Shotgun:
                _ItemPortrait = PortraitImages.instance.ShotgunPortraits[itemNumber];
                break;
            case WeaponTypes.Rifle:
                _ItemPortrait = PortraitImages.instance.RiflePortraits[itemNumber];
                break;
            case WeaponTypes.Blade1H:
                _ItemPortrait = PortraitImages.instance.Blade1HPortraits[itemNumber];
                break;
            case WeaponTypes.Blunt1H:
                _ItemPortrait = PortraitImages.instance.Blunt1HPortraits[itemNumber];
                break;
            case WeaponTypes.Blade2H:
                _ItemPortrait = PortraitImages.instance.Blade2HPortraits[itemNumber];
                break;
            case WeaponTypes.Blunt2H:
                _ItemPortrait = PortraitImages.instance.Blunt2HPortraits[itemNumber];
                break;
            case WeaponTypes.HeavyWeapon:
                _ItemPortrait = PortraitImages.instance.HeavyWeaponPortraits[itemNumber];
                break;
        }

        return _ItemPortrait;
    }



    // get item name (override)
    public override string GetItemName()
    {
        string _name = "";

        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                _name = PistolNames();
                break;
            case WeaponTypes.Shotgun:
                _name = ShotgunNames();
                break;
            case WeaponTypes.Rifle:
                _name = RifleNames();
                break;
            case WeaponTypes.Blunt1H:
                _name = Blunt1HNames();
                break;
            case WeaponTypes.Blade1H:
                _name = Blade1HNames();
                break;
            case WeaponTypes.Blunt2H:
                _name = Blunt2HNames();
                break;
            case WeaponTypes.Blade2H:
                _name = Blade2HNames();
                break;
            case WeaponTypes.HeavyWeapon:
                _name = HeavyWeaponNames();
                break;
        }

        return _name;
    }

    // pistol names
    string PistolNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Headhunter";
                break;
            case 1:
                _name = "Redeemer";
                break;
            case 2:
                _name = "Ayatollah";
                break;
        }

        return _name;
    }

    // shotgun names
    string ShotgunNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Shotgun";
                break;
            case 1:
                _name = "Shotgun";
                break;
            case 2:
                _name = "Shotgun";
                break;
        }

        return _name;
    }

    // rifle names
    string RifleNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Rifle";
                break;
            case 1:
                _name = "Rifle";
                break;
            case 2:
                _name = "Rifle";
                break;
        }

        return _name;
    }

    // heavy weapon names
    string HeavyWeaponNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Heavy Weapon";
                break;
            case 1:
                _name = "Heavy Weapon";
                break;
            case 2:
                _name = "Heavy Weapon";
                break;
        }

        return _name;
    }

    // blunt names
    string Blunt1HNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Blunt";
                break;
            case 1:
                _name = "Blunt";
                break;
            case 2:
                _name = "Blunt";
                break;
        }

        return _name;
    }

    // blade names
    string Blade1HNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Blade";
                break;
            case 1:
                _name = "Blade";
                break;
            case 2:
                _name = "Blade";
                break;
        }

        return _name;
    }

    // blunt names
    string Blunt2HNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Blunt";
                break;
            case 1:
                _name = "Blunt";
                break;
            case 2:
                _name = "Blunt";
                break;
        }

        return _name;
    }

    // blade names
    string Blade2HNames()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "Blade";
                break;
            case 1:
                _name = "Blade";
                break;
            case 2:
                _name = "Blade";
                break;
        }

        return _name;
    }



    // get item description (override)
    public override string GetItemDescription()
    {
        string _description = "";

        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                _description = PistolDescriptions();
                break;
            case WeaponTypes.Shotgun:
                break;
            case WeaponTypes.Rifle:
                break;
            case WeaponTypes.Blunt1H:
                break;
            case WeaponTypes.Blade1H:
                break;
            case WeaponTypes.Blunt2H:
                break;
            case WeaponTypes.Blade2H:
                break;
            case WeaponTypes.HeavyWeapon:
                break;
        }

        return _description;
    }

    // pistol descriptions
    string PistolDescriptions()
    {
        string _name = "";

        switch (itemNumber)
        {
            case 0:
                _name = "A pistol used for close combat. Never leave home without it.";
                break;
            case 1:
                _name = "Pistola with attitude. Punches holes through folk.";
                break;
            case 2:
                _name = "Close-combat weapon. Military grade.";
                break;
        }

        return _name;
    }



    // get item class description (override)
    public override string GetItemClassDescription()
    {
        string _name = WeaponType.ToString();

        return _name;
    }



    // get range
    public int GetRange()
    {
        int _value = 0;

        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                _value = 6;
                break;
            case WeaponTypes.Shotgun:
                _value = 4;
                break;
            case WeaponTypes.Rifle:
                _value = 8;
                break;
            case WeaponTypes.Blunt1H:
                _value = 2;
                break;
            case WeaponTypes.Blade1H:
                _value = 2;
                break;
            case WeaponTypes.Blunt2H:
                _value = 2;
                break;
            case WeaponTypes.Blade2H:
                _value = 2;
                break;
            case WeaponTypes.HeavyWeapon:
                _value = 6;
                break;
        }

        return _value;
    }

    // get action points
    public int GetActionPointsRequired()
    {
        int _value = 0;

        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                _value = 2;
                break;
            case WeaponTypes.Shotgun:
                _value = 3;
                break;
            case WeaponTypes.Rifle:
                _value = 4;
                break;
            case WeaponTypes.Blunt1H:
                _value = 2;
                break;
            case WeaponTypes.Blade1H:
                _value = 2;
                break;
            case WeaponTypes.HeavyWeapon:
                _value = 5;
                break;
        }

        return _value;
    }



    // get maximum ammo
    public int GetMaximumAmmo()
    {
        int _ammo = 0;

        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                _ammo = 7;
                break;
            case WeaponTypes.Shotgun:
                _ammo = 5;
                break;
            case WeaponTypes.Rifle:
                _ammo = 5;
                break;
            case WeaponTypes.HeavyWeapon:
                _ammo = 2;
                break;
        }

        return _ammo;
    }

    // check ammo
    public bool CheckIfAmmo()
    {
        bool _canAttack = true;

        if (WeaponDamageType == WeaponDamage.Ranged && ammo <= 0)
            _canAttack = false;

        return _canAttack;
    }

    // deduct ammo
    public void DeductAmmo()
    {
        if (WeaponDamageType == WeaponDamage.Ranged)
            ammo--;
    }

    // reload
    public void Reload()
    {
        ammo = GetMaximumAmmo();
    }



    // calculate cost (override)
    public override int CalculateCost()
    {
        int _cost = 0;

        _cost = maxDamage * 100;

        return _cost;
    }
}
