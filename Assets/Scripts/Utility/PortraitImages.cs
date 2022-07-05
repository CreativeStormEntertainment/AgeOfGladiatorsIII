using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitImages : MonoBehaviour
{
    public static PortraitImages instance;

    [Header("Heads - Small")]
    public Sprite[] JudgeMalePortraitsSmall;
    public Sprite[] JudgeFemalePortraitsSmall;
    public Sprite[] CitizenMalePortraitsSmall;
    public Sprite[] CitizenFemalePortraitsSmall;
    public Sprite[] CharacterPortraitsSmall;
    public Sprite[] ItemPortraitsSmall;

    [Header("Heads - Medium")]
    public Sprite[] JudgeMalePortraitsMedium;
    public Sprite[] JudgeFemalePortraitsMedium;
    public Sprite[] CitizenMalePortraitsMedium;
    public Sprite[] CitizenFemalePortraitsMedium;
    public Sprite[] CharacterPortraitsMedium;
    public Sprite[] ItemPortraitsMedium;

    [Header("Heads - Large")]
    public Sprite[] JudgeMalePortraits;
    public Sprite[] JudgeFemalePortraits;
    public Sprite[] CitizenMalePortraits;
    public Sprite[] CitizenFemalePortraits;
    public Sprite[] CharacterPortraits;
    public Sprite[] ItemPortraits;

    [Header("Weapons")]
    public Sprite[] PistolPortraits;
    public Sprite[] ShotgunPortraits;
    public Sprite[] RiflePortraits;
    public Sprite[] HeavyWeaponPortraits;
    public Sprite[] Blade1HPortraits;
    public Sprite[] Blunt1HPortraits;
    public Sprite[] Blade2HPortraits;
    public Sprite[] Blunt2HPortraits;

    [Header("Helmets")]
    public Sprite[] LightHelmetPortraits;
    public Sprite[] MediumHelmetPortraits;
    public Sprite[] HeavyHelmetPortraits;

    [Header("Chest")]
    public Sprite[] LightChestPortraits;
    public Sprite[] MediumChestPortraits;
    public Sprite[] HeavyChestPortraits;

    [Header("Legs")]
    public Sprite[] LightLegPortraits;
    public Sprite[] MediumLegPortraits;
    public Sprite[] HeavyLegPortraits;

    [Header("Boots")]
    public Sprite[] LightBootPortraits;
    public Sprite[] MediumBootPortraits;
    public Sprite[] HeavyBootPortraits;

    [Header("Gloves")]
    public Sprite[] LightGlovesPortraits;
    public Sprite[] MediumGlovesPortraits;
    public Sprite[] HeavyGlovesPortraits;

    [Header("Shields")]
    public Sprite[] ShieldPortraits;
    public Sprite[] AccessoryPortraits;
    public Sprite[] MissionItemPortraits;



    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
