using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class CombatSounds : MonoBehaviour
{
    public static CombatSounds instance;

    static AudioSource Audio;



    [Header("Swings")]
    public AudioClip[] Swings;

    [Header("Attacks")]
    public AudioClip[] SingleFirePistol;
    public AudioClip[] SuccessiveFirePistol;
    public AudioClip[] SingleFireRifle;
    public AudioClip[] SuccessiveFireRifle;
    public AudioClip[] SingleFireAuto;
    public AudioClip[] SuccessiveFireAuto;
    public AudioClip[] SingleFireShotgun;

    [Header("Hit")]
    public AudioClip[] MeleeImpact;
    public AudioClip[] RangedImpact;

    [Header("Miss")]
    public AudioClip[] MeleeMiss;
    public AudioClip[] RangedMiss;

    [Header("Pain")]
    public AudioClip[] PainMale;
    public AudioClip[] PainFemale;
    public AudioClip[] PainApe;

    [Header("Grunt")]
    public AudioClip[] GruntMale;
    public AudioClip[] GruntFemale;
    public AudioClip[] GruntApe;

    [Header("Death")]
    public AudioClip[] DeathMale;
    public AudioClip[] DeathFemale;
    public AudioClip[] DeathApe;

    [Header("Other")]
    public AudioClip[] Reload;
    public AudioClip[] SelectAmmo;
    public AudioClip[] Heal;



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



    public void PlayAttack(WeaponTypes _Type)
    {
        switch (_Type)
        {
            case WeaponTypes.Pistol:
                PlayPistol();
                break;
            case WeaponTypes.Rifle:
                PlayRifle();
                break;
            case WeaponTypes.Shotgun:
                PlayShotgun();
                break;
            case WeaponTypes.HeavyWeapon:
                break;

            case WeaponTypes.Blade1H:
                PlaySwing();
                break;
            case WeaponTypes.Blade2H:
                PlaySwing();
                break;
            case WeaponTypes.Blunt1H:
                PlaySwing();
                break;
            case WeaponTypes.Blunt2H:
                PlaySwing();
                break;
            case WeaponTypes.Unarmed:
                PlaySwing();
                break;
        }
    }

    public void PlayAttackPlayer()
    {
        switch (PlayerScene.instance.MainCharacter.EquippedWeapon.WeaponType)
        {
            case WeaponTypes.Pistol:
                PlayPistol();
                break;
            case WeaponTypes.Rifle:
                PlayRifle();
                break;
            case WeaponTypes.Shotgun:
                PlayShotgun();
                break;
            case WeaponTypes.HeavyWeapon:
                break;

            case WeaponTypes.Blade1H:
                PlaySwing();
                break;
            case WeaponTypes.Blade2H:
                PlaySwing();
                break;
            case WeaponTypes.Blunt1H:
                PlaySwing();
                break;
            case WeaponTypes.Blunt2H:
                PlaySwing();
                break;
            case WeaponTypes.Unarmed:
                PlaySwing();
                break;
        }
    }



    public void PlayPistol()
    {
        int _random = Random.Range(0, SingleFirePistol.Length);
        Audio.PlayOneShot(SingleFirePistol[_random]);
    }

    public void PlayPistolSuccessive()
    {
        int _random = Random.Range(0, SuccessiveFirePistol.Length);
        Audio.PlayOneShot(SuccessiveFirePistol[_random]);
    }

    public void PlayRifle()
    {
        int _random = Random.Range(0, SingleFireRifle.Length);
        Audio.PlayOneShot(SingleFireRifle[_random]);
    }

    public void PlayRifleSuccessive()
    {
        int _random = Random.Range(0, SuccessiveFireRifle.Length);
        Audio.PlayOneShot(SuccessiveFireRifle[_random]);
    }

    public void PlayAuto()
    {
        int _random = Random.Range(0, SingleFireAuto.Length);
        Audio.PlayOneShot(SingleFireAuto[_random]);
    }

    public void PlayAutoSuccessive()
    {
        int _random = Random.Range(0, SuccessiveFireAuto.Length);
        Audio.PlayOneShot(SuccessiveFireAuto[_random]);
    }

    public void PlayShotgun()
    {
        int _random = Random.Range(0, SingleFireShotgun.Length);
        Audio.PlayOneShot(SingleFireShotgun[_random]);
    }

    public void PlayShotgunSuccessive()
    {
        Audio.PlayOneShot(SingleFireShotgun[0]);
        StartCoroutine(WaitBetweenShots());
    }

    public IEnumerator WaitBetweenShots()
    {
        yield return new WaitForSeconds(0.4f);
        PlayShotgunSuccessiveTwo();
    }

    public void PlayShotgunSuccessiveTwo()
    {
        Audio.PlayOneShot(SingleFireShotgun[1]);
    }

    public void PlaySwing()
    {
        int _random = Random.Range(0, Swings.Length);
        Audio.PlayOneShot(Swings[_random]);
    }



    public void PlayDamage(WeaponTypes _Type)
    {
        switch (_Type)
        {
            case WeaponTypes.Pistol:
                PlayRangedImpact();
                break;
            case WeaponTypes.Rifle:
                PlayRangedImpact();
                break;
            case WeaponTypes.Shotgun:
                PlayRangedImpact();
                break;
            case WeaponTypes.HeavyWeapon:
                break;

            case WeaponTypes.Blade1H:
                PlayMeleeImpact();
                break;
            case WeaponTypes.Blade2H:
                PlayMeleeImpact();
                break;
            case WeaponTypes.Blunt1H:
                PlayMeleeImpact();
                break;
            case WeaponTypes.Blunt2H:
                PlayMeleeImpact();
                break;
            case WeaponTypes.Unarmed:
                PlayMeleeImpact();
                break;
        }
    }

    public void PlayMeleeImpact()
    {
        int _random = Random.Range(0, MeleeImpact.Length);
        Audio.PlayOneShot(MeleeImpact[_random]);
    }

    public void PlayRangedImpact()
    {
        int _random = Random.Range(0, RangedImpact.Length);
        Audio.PlayOneShot(RangedImpact[_random]);
    }



    public void PlayMiss(WeaponTypes _Type)
    {
        switch (_Type)
        {
            case WeaponTypes.Pistol:
                PlayRangedMiss();
                break;
            case WeaponTypes.Rifle:
                PlayRangedMiss();
                break;
            case WeaponTypes.Shotgun:
                PlayRangedMiss();
                break;
            case WeaponTypes.HeavyWeapon:
                break;

            case WeaponTypes.Blade1H:
                break;
            case WeaponTypes.Blade2H:
                break;
            case WeaponTypes.Blunt1H:
                break;
            case WeaponTypes.Blunt2H:
                break;
            case WeaponTypes.Unarmed:
                break;
        }
    }

    public void PlayMeleeMiss()
    {
        int _random = Random.Range(0, MeleeMiss.Length);
        Audio.PlayOneShot(MeleeMiss[_random]);
    }

    public void PlayRangedMiss()
    {
        int _random = Random.Range(0, RangedMiss.Length);
        Audio.PlayOneShot(RangedMiss[_random]);
    }



    public void PlayGrunt(CharacterSpecies _Type, bool _male)
    {
        switch (_Type)
        {
            case CharacterSpecies.Human:
                if (_male)
                    PlayGruntMale();
                else
                    PlayGruntFemale();
                break;
            case CharacterSpecies.Ape:
                PlayGruntApe();
                break;
        }
    }

    public void PlayGruntMale()
    {
        int _random = Random.Range(0, GruntMale.Length);
        Audio.PlayOneShot(GruntMale[_random]);
    }

    public void PlayGruntFemale()
    {
        int _random = Random.Range(0, GruntFemale.Length);
        Audio.PlayOneShot(GruntFemale[_random]);
    }

    public void PlayGruntApe()
    {
        int _random = Random.Range(0, GruntApe.Length);
        Audio.PlayOneShot(GruntApe[_random]);
    }



    public void PlayPain(CharacterSpecies _Type, bool _male)
    {
        switch (_Type)
        {
            case CharacterSpecies.Human:
                if (_male)
                    PlayPainMale();
                else
                    PlayPainFemale();
                break;
            case CharacterSpecies.Ape:
                PlayPainApe();
                break;
        }
    }

    public void PlayPainMale()
    {
        int _random = Random.Range(0, PainMale.Length);
        Audio.PlayOneShot(PainMale[_random]);
    }

    public void PlayPainFemale()
    {
        int _random = Random.Range(0, PainFemale.Length);
        Audio.PlayOneShot(PainFemale[_random]);
    }

    public void PlayPainApe()
    {
        int _random = Random.Range(0, PainApe.Length);
        Audio.PlayOneShot(PainApe[_random]);
    }



    public void PlayDeath(CharacterSpecies _Type, bool _male)
    {
        switch (_Type)
        {
            case CharacterSpecies.Human:
                if (_male)
                    PlayDeathMale();
                else
                    PlayDeathFemale();
                break;
            case CharacterSpecies.Ape:
                PlayDeathApe();
                break;
        }
    }

    public void PlayDeathMale()
    {
        int _random = Random.Range(0, DeathMale.Length);
        Audio.PlayOneShot(DeathMale[_random]);
    }

    public void PlayDeathFemale()
    {
        int _random = Random.Range(0, DeathFemale.Length);
        Audio.PlayOneShot(DeathFemale[_random]);
    }

    public void PlayDeathApe()
    {
        int _random = Random.Range(0, DeathApe.Length);
        Audio.PlayOneShot(DeathApe[_random]);
    }



    public void PlayReload()
    {
        Audio.PlayOneShot(Reload[0]);
    }

    public void PlaySelectAmmo()
    {
        Audio.PlayOneShot(SelectAmmo[0]);
    }

    public void PlayHeal()
    {
        Audio.PlayOneShot(Heal[0]);
    }




    private void OnEnable()
    {
        Lua.RegisterFunction("PlayAttackPlayer", this, SymbolExtensions.GetMethodInfo(() => PlayAttackPlayer()));
        Lua.RegisterFunction("PlayDeathMale", this, SymbolExtensions.GetMethodInfo(() => PlayDeathMale()));
        Lua.RegisterFunction("PlayDeathFemale", this, SymbolExtensions.GetMethodInfo(() => PlayDeathFemale()));
    }

    private void OnDisable()
    {
        //Lua.UnregisterFunction("EventScreenTrigger");
    }
}