using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PopulateCombatStatsRollover
{
    // populate combat stat change during rollover
    public static void Rollover(Attribute _Attribute, List<IconLabelBox> _List)
    {
        // ---------------------------------------
        foreach (IconLabelBox _Box in _List)
        {
            // --------------------
            // strength
            if ((_Attribute == Attribute.Strength) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Health))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.strengthHealth);

            if ((_Attribute == Attribute.Strength) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.MeleeDamageBonus))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.strengthMeleeDamageBonus);
            // --------------------
            // --------------------
            // vision
            if ((_Attribute == Attribute.Vision) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Initiative))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.visionInitiative);

            if ((_Attribute == Attribute.Vision) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.RangedDamageBonus))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.visionRangedDamageBonus);
            // --------------------
            // --------------------
            // coordination
            if ((_Attribute == Attribute.Coordination) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.HitChance))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.coordinationHit);

            if ((_Attribute == Attribute.Coordination) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Penetration))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.coordinationPeneration);
            // --------------------
            // --------------------
            // speed
            if ((_Attribute == Attribute.Speed) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.ActionPoints))
                _Box.GetComponent<StatBox>().ShowRolloverStat(1); // fix

            if ((_Attribute == Attribute.Speed) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Evasion))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.speedEvasion);
            // --------------------
            // --------------------
            // aggression
            if ((_Attribute == Attribute.Aggression) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.CriticalChance))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.aggressionCriticalChance);

            if ((_Attribute == Attribute.Aggression) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.CriticalDamage))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.aggressionCriticalDamage);
            // --------------------
            // --------------------
            // intelligence

            // --------------------
        }
        // ---------------------------------------
    }

    // populate combat stat change during rollover
    public static void Rollover(Skill _Skill, List<IconLabelBox> _List)
    {
        // ---------------------------------------
        foreach (IconLabelBox _Box in _List)
        {
            // --------------------
            // brawling
            if ((_Skill == Skill.Brawling) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.MeleeDamageBonus))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.brawlingMeleeDamageBonus);

            if ((_Skill == Skill.Brawling) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Penetration))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.weaponPeneration);
            // --------------------
            // --------------------
            // shooting
            if ((_Skill == Skill.Shooting) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.RangedDamageBonus))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.shootingRangedDamageBonus);

            if ((_Skill == Skill.Shooting) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Penetration))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.weaponPeneration);
            // --------------------
            // --------------------
            // athletics
            if ((_Skill == Skill.Athletics) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Health))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.athleticsHealth);

            if ((_Skill == Skill.Athletics) && (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Evasion))
                _Box.GetComponent<StatBox>().ShowRolloverStat(GameData.athleticsEvasion);
            // --------------------

        }
        // ---------------------------------------
    }

    // populate combat stat change during rollover
    public static void Rollover(Item _Item, List<IconLabelBox> _List)
    {
        // ---------------------------------------
        foreach (IconLabelBox _Box in _List)
        {
            // weapon
            if (_Item.ItemClass == ItemClasses.Weapon)
            {
                Weapon _Weapon = _Item as Weapon;

                if (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Penetration)
                    _Box.GetComponent<StatBox>().ShowRolloverStat(CompareItem(_Item, _Box.GetComponent<StatBox>().CombatStat));

                if (_Box.GetComponent<StatBox>().CombatStat == CombatStats.CriticalChance)
                    _Box.GetComponent<StatBox>().ShowRolloverStat(CompareItem(_Item, _Box.GetComponent<StatBox>().CombatStat));

                if (_Box.GetComponent<StatBox>().CombatStat == CombatStats.CriticalDamage)
                    _Box.GetComponent<StatBox>().ShowRolloverStat(CompareItem(_Item, _Box.GetComponent<StatBox>().CombatStat));

                if (_Box.GetComponent<StatBox>().CombatStat == CombatStats.HitChance)
                    _Box.GetComponent<StatBox>().ShowRolloverStat(CompareItem(_Item, _Box.GetComponent<StatBox>().CombatStat));
            }

            // armor
            if (_Item.ItemClass == ItemClasses.Helmet || _Item.ItemClass == ItemClasses.Chest || _Item.ItemClass == ItemClasses.Legs || _Item.ItemClass == ItemClasses.Boots || _Item.ItemClass == ItemClasses.Gloves)
            {
                if (_Box.GetComponent<StatBox>().CombatStat == CombatStats.Armor)
                    _Box.GetComponent<StatBox>().ShowRolloverStat(CompareItem(_Item, _Box.GetComponent<StatBox>().CombatStat));
            }
        }
        // ---------------------------------------
    }



    // compare item
    public static string CompareItem(Item _Item, CombatStats _Stat)
    {
        // -----------------------------------
        // set up
        PlayerCharacter SelectedCharacter = PlayerScene.instance.MainCharacter;

        string _comparison = "--";
        int _amount = 0;
        // -----------------------------------

        // -----------------------------------
        // weapon (hit chance)
        if (_Item.ItemClass == ItemClasses.Weapon && _Stat == CombatStats.HitChance)
        {
            Weapon _Weapon = _Item as Weapon;

            if (SelectedCharacter.EquippedWeapon != null)
                _amount = _Weapon.hitChance - SelectedCharacter.EquippedWeapon.hitChance;
            else
                _amount = _Weapon.hitChance;
        }
        // -----------------------------------
        // -----------------------------------
        // weapon (penetration)
        if (_Item.ItemClass == ItemClasses.Weapon && _Stat == CombatStats.Penetration)
        {
            Weapon _Weapon = _Item as Weapon;

            if (SelectedCharacter.EquippedWeapon != null)
                _amount = _Weapon.penetration - SelectedCharacter.EquippedWeapon.penetration;
            else
                _amount = _Weapon.penetration;
        }
        // -----------------------------------
        // -----------------------------------
        // weapon (critical chance)
        if (_Item.ItemClass == ItemClasses.Weapon && _Stat == CombatStats.CriticalChance)
        {
            Weapon _Weapon = _Item as Weapon;

            if (SelectedCharacter.EquippedWeapon != null)
                _amount = _Weapon.criticalChance - SelectedCharacter.EquippedWeapon.criticalChance;
            else
                _amount = _Weapon.criticalChance;
        }
        // -----------------------------------
        // -----------------------------------
        // helmet (armor)
        if (_Item.ItemClass == ItemClasses.Helmet)
        {
            Helmet _Armor = _Item as Helmet;

            if (SelectedCharacter.EquippedHelmet != null)
                _amount = _Armor.armor - SelectedCharacter.EquippedHelmet.armor;
            else
                _amount = _Armor.armor;
        }
        // -----------------------------------
        // -----------------------------------
        // chest (armor)
        if (_Item.ItemClass == ItemClasses.Chest)
        {
            Chest _Armor = _Item as Chest;

            if (SelectedCharacter.EquippedChest != null)
                _amount = _Armor.armor - SelectedCharacter.EquippedChest.armor;
            else
                _amount = _Armor.armor;
        }
        // -----------------------------------
        // -----------------------------------
        // legs (armor)
        if (_Item.ItemClass == ItemClasses.Legs)
        {
            Legs _Armor = _Item as Legs;

            if (SelectedCharacter.EquippedLegs != null)
                _amount = _Armor.armor - SelectedCharacter.EquippedLegs.armor;
            else
                _amount = _Armor.armor;
        }
        // -----------------------------------
        // -----------------------------------
        // boots (armor)
        if (_Item.ItemClass == ItemClasses.Boots)
        {
            Boots _Armor = _Item as Boots;

            if (SelectedCharacter.EquippedBoots != null)
                _amount = _Armor.armor - SelectedCharacter.EquippedBoots.armor;
            else
                _amount = _Armor.armor;
        }
        // -----------------------------------
        // -----------------------------------
        // gloves (armor)
        if (_Item.ItemClass == ItemClasses.Gloves)
        {
            Gloves _Armor = _Item as Gloves;

            if (SelectedCharacter.EquippedGloves != null)
                _amount = _Armor.armor - SelectedCharacter.EquippedGloves.armor;
            else
                _amount = _Armor.armor;
        }
        // -----------------------------------

        // -----------------------------------
        // create string
        _comparison = PlusMinus(_amount);

        return _comparison;
        // -----------------------------------
    }



    // add plus or minus in front
    static string PlusMinus(int _amount)
    {
        string _prefix;

        // create string
        if (_amount > 0)
            _prefix = "+";
        else
            _prefix = "";

        // return
        if (_amount != 0)
            return _prefix + _amount;
        else
            return _prefix = "--";
    }

    // add plus or minus in front - invert
    static string PlusMinusInvert(int _amount)
    {
        string _prefix;

        // create string
        if (_amount > 0)
            _prefix = "-";
        else
            _prefix = "+";

        // remove sign if negative
        if (_amount < 0)
            _amount = Mathf.Abs(_amount); // makes absolute number

        // return
        if (_amount != 0)
            return _prefix + _amount;
        else
            return _prefix = "--";
    }
}
