using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using PixelCrushers.DialogueSystem;

public class NPC : Character
{
    [Header("Other")]
    public bool isHostile;
    public bool cannotApproach;

    [Header("Vendor")]
    public int vendorNumber;

    [HideInInspector]
    public bool alreadyToldCantTalk;
    [HideInInspector]
    public string npcID;
    [HideInInspector]
    public GameObject NamePlate;

    [HideInInspector]
    public DialogueSystemTrigger DialogueTrigger;



    void Start()
    {
        level = 1;
        SetAttributes();

        playerControlledCombat = false;

        // add dialogue trigger
        DialogueTrigger = GetComponent<DialogueSystemTrigger>();

        // add player to dialogue conversant actor
        if (DialogueTrigger != null)
        {
            DialogueTrigger.conversationActor = PlayerScene.instance.MainCharacter.transform;
            DialogueTrigger.barkTarget = PlayerScene.instance.MainCharacter.transform;
        }
    }



    public void LootNPC()
    {
        if (!UI.instance.LootScreen.isActiveAndEnabled)
            UI.instance.OpenLoot(GetComponent<MapItemContainer>());
    }



    public void TransferSettingsToNPC(NPC _incoming)
    {
        // active state
        this.gameObject.SetActive(_incoming.gameObject.activeSelf);

        // add data
        npcID = _incoming.npcID;
        characterName = _incoming.characterName;

        male = _incoming.male;

        isHostile = _incoming.isHostile;
        isDead = _incoming.isDead;

        // add container if dead
        if (_incoming.GetComponent<MapItemContainer>())
        {
            this.gameObject.AddComponent<MapItemContainer>();
            this.gameObject.GetComponent<MapItemContainer>().ContainedItems = _incoming.GetComponent<MapItemContainer>().ContainedItems;

            if (GetComponent<AudioCue>() != null)
                this.gameObject.GetComponent<AudioCue>().AudioCueOnTrigger = AudioCues.Loot;
        }
    }

    public void SetAttributes()
    {
        int _stat = 4;

        // eventually put different classes, levels and enemies in here
        strength = _stat;
        vision = _stat;
        coordination = _stat;
        speed = _stat;
        aggression = _stat;
        intelligence = _stat;
    }
}
