using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public static PlayerCharacter instance;

    [HideInInspector]
    public int arrests;
    [HideInInspector]
    public int executions;
    [HideInInspector]
    public int fines;

    [HideInInspector]
    public int lawReputation;

    public GameObject occupied;



    // start character stats
    public void CharacterStart()
    {
        characterName = "Judge Diablo";

        rank = 1;
        level = 1;

        male = true;

        if (male)
            PortraitType = CharacterPortraitType.JudgeMale;
        else
            PortraitType = CharacterPortraitType.JudgeFemale;

        portraitNumber = 0;

        intimidation = 0;
        persuasion = 0;
        perception = 0;
        lawKnowledge = 0;
        streetsmarts = 0;
        science = 0;
        engineering = 0;
        computers = 0;
        medicine = 0;
        lockpicking = 0;
        demolitions = 0;

        strength = 5;
        vision = 5;
        coordination = 5;
        speed = 5;
        aggression = 5;
        intelligence = 5;

        brawling = 0;
        shooting = 0;
        specialAmmo = 0;
        psionics = 0;
        heavyWeapons = 0;
        athletics = 0;

        skillPoints = 40;
        attributePoints = 10;

        playerControlledCombat = true;
    }



    // transfer settings
    public void TransferSetting(PlayerCharacter i)
    {
        characterName = i.characterName;
        male = i.male;
        PortraitType = i.PortraitType;
        portraitNumber = i.portraitNumber;

        level = i.level;
        rank = i.rank;

        skillPoints = i.skillPoints;
        attributePoints = i.attributePoints;

        strength = i.strength;
        vision = i.vision;
        coordination = i.coordination;
        speed = i.speed;
        aggression = i.aggression;
        intelligence = i.intelligence;

        brawling = i.brawling;
        shooting = i.shooting;
        specialAmmo = i.specialAmmo;
        psionics = i.psionics;
        heavyWeapons = i.heavyWeapons;
        athletics = i.athletics;

        intimidation = i.intimidation;
        persuasion = i.persuasion;
        perception = i.perception;
        lawKnowledge = i.lawKnowledge;
        streetsmarts = i.streetsmarts;
        science = i.science;
        engineering = i.engineering;
        computers = i.computers;
        medicine = i.medicine;
        lockpicking = i.lockpicking;
        demolitions = i.demolitions;

        playerControlledCombat = i.playerControlledCombat;

        EquippedWeapon = i.EquippedWeapon;
        EquippedHelmet = i.EquippedHelmet;
        EquippedChest = i.EquippedChest;
        EquippedLegs = i.EquippedLegs;
        EquippedGloves = i.EquippedGloves;
        EquippedBoots = i.EquippedBoots;

        PlayerScene.instance.MainCharacter.UpdateEquipmentOnModel();
    }



    // premade character (temporary)
    public void PremadeCharacter()
    {
        characterName = "Judge Diablo";

        level = 1;
        rank = 1;

        male = false;

        if (male)
            PortraitType = CharacterPortraitType.JudgeMale;
        else
            PortraitType = CharacterPortraitType.JudgeFemale;

        intimidation = 8;
        persuasion = 7;
        perception = 7;
        lawKnowledge = 2;
        streetsmarts = 2;
        science = 5;
        engineering = 3;
        computers = 6;
        medicine = 10;
        lockpicking = 2;
        demolitions = 2;

        strength = 3;
        vision = 5;
        coordination = 6;
        speed = 7;
        aggression = 8;
        intelligence = 3;

        brawling = 2;
        shooting = 5;
        specialAmmo = 2;
        psionics = 4;
        heavyWeapons = 2;
        athletics = 1;

        skillPoints = 20;
        attributePoints = 5;

        playerControlledCombat = true;
    }

    // toggle occupied image
    public void ToggleOccupied(bool _show)
    {
        occupied.SetActive(_show);
    }
}
