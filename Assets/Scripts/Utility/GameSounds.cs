using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour
{
    public static GameSounds instance;

    static AudioSource Audio;

    [Header("Equip")]
    public AudioClip[] Weapon;
    public AudioClip[] Armor;
    public AudioClip[] Shield;
    public AudioClip[] Mission;
    public AudioClip[] Accessory;

    [Header("Misc")]
    public AudioClip[] Computer;
    public AudioClip[] Door;
    public AudioClip[] Lock;
    public AudioClip[] Crate;
    public AudioClip[] Radio;
    public AudioClip[] Loot;
    public AudioClip[] Craft;
    public AudioClip[] Perception;
    public AudioClip[] SkillDoor;
    public AudioClip[] SkillCrate;
    public AudioClip[] SkillComputer;

    public AudioClip[] Other;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }



    public void PlayAudioCue(AudioCues _Cue)
    {
        if (_Cue == AudioCues.None)
            return;

        switch (_Cue)
        {
            case AudioCues.Computer:
                PlayComputer();
                break;
            case AudioCues.Lock:
                PlayLock();
                break;
            case AudioCues.Door:
                PlayDoor();
                break;
            case AudioCues.Crate:
                PlayCrate();
                break;
            case AudioCues.Radio:
                PlayRadio();
                break;
            case AudioCues.Loot:
                PlayLoot();
                break;
            case AudioCues.Craft:
                PlayCraft();
                break;
            case AudioCues.Perception:
                PlayPerception();
                break;
        }
    }

    public void PlayAudioCue(Skill _Skill)
    {
        if (_Skill == Skill.None)
            return;

        switch (_Skill)
        {
            case Skill.Computers:
                PlaySkillComputer();
                break;
            case Skill.Lockpicking:
                PlaySkillDoor();
                break;
        }
    }



    public void PlayAddItem(ItemClasses _Type)
    {
        switch (_Type)
        {
            case ItemClasses.Helmet:
                Audio.clip = Armor[Random.Range(0, Armor.Length)];
                break;
            case ItemClasses.Chest:
                Audio.clip = Armor[Random.Range(0, Armor.Length)];
                break;
            case ItemClasses.Legs:
                Audio.clip = Armor[Random.Range(0, Armor.Length)];
                break;
            case ItemClasses.Gloves:
                Audio.clip = Armor[Random.Range(0, Armor.Length)];
                break;
            case ItemClasses.Boots:
                Audio.clip = Armor[Random.Range(0, Armor.Length)];
                break;

            case ItemClasses.Weapon:
                Audio.clip = Weapon[Random.Range(0, Weapon.Length)];
                break;
            case ItemClasses.Shield:
                Audio.clip = Shield[Random.Range(0, Shield.Length)];
                break;
            case ItemClasses.Mission:
                Audio.clip = Mission[Random.Range(0, Accessory.Length)];
                break;
            case ItemClasses.Accessory:
                Audio.clip = Accessory[Random.Range(0, Accessory.Length)];
                break;
        }

        Audio.Play();
    }



    public void PlayComputer()
    {
        Audio.clip = Computer[0];
        Audio.Play();
    }

    public void PlayDoor()
    {
        Audio.clip = Door[0];
        Audio.Play();
    }

    public void PlayLock()
    {
        Audio.clip = Lock[0];
        Audio.Play();
    }

    public void PlayCrate()
    {
        Audio.clip = Crate[Random.Range(0, Crate.Length)];
        Audio.Play();
    }

    public void PlayRadio()
    {
        Audio.clip = Radio[0];
        Audio.Play();
    }

    public void PlayLoot()
    {
        Audio.clip = Loot[Random.Range(0, Loot.Length)];
        Audio.Play();
    }

    public void PlayCraft()
    {
        Audio.clip = Craft[0];
        Audio.Play();
    }

    public void PlayPerception()
    {
        Audio.clip = Perception[0];
        Audio.Play();
    }

    public void PlaySkillDoor()
    {
        Audio.clip = SkillDoor[0];
        Audio.Play();
    }

    public void PlaySkillCrate()
    {
        Audio.clip = SkillCrate[0];
        Audio.Play();
    }

    public void PlaySkillComputer()
    {
        Audio.clip = SkillComputer[0];
        Audio.Play();
    }



    public void PlayOther(int _index)
    {
        Audio.clip = Other[_index];
        Audio.Play();
    }
}