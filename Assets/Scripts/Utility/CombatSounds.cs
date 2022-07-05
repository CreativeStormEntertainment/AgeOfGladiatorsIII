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
    public AudioClip[] PainRobot;

    [Header("Grunt")]
    public AudioClip[] GruntMale;
    public AudioClip[] GruntFemale;
    public AudioClip[] GruntApe;
    public AudioClip[] GruntRobot;

    [Header("Death")]
    public AudioClip[] DeathMale;
    public AudioClip[] DeathFemale;
    public AudioClip[] DeathApe;
    public AudioClip[] DeathRobot;

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



    // attack
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



    // pistol - single
    public void PlayPistol()
    {
        int _random = Random.Range(0, SingleFirePistol.Length);
        Audio.PlayOneShot(SingleFirePistol[_random]);
    }

    // pistol - automatic
    public void PlayPistolSuccessive()
    {
        int _random = Random.Range(0, SuccessiveFirePistol.Length);
        Audio.PlayOneShot(SuccessiveFirePistol[_random]);
    }

    // rifle - single
    public void PlayRifle()
    {
        int _random = Random.Range(0, SingleFireRifle.Length);
        Audio.PlayOneShot(SingleFireRifle[_random]);
    }

    // rifle - automatic
    public void PlayRifleSuccessive()
    {
        int _random = Random.Range(0, SuccessiveFireRifle.Length);
        Audio.PlayOneShot(SuccessiveFireRifle[_random]);
    }

    // semi - single
    public void PlayAuto()
    {
        int _random = Random.Range(0, SingleFireAuto.Length);
        Audio.PlayOneShot(SingleFireAuto[_random]);
    }

    // semi - burst
    public void PlayAutoSuccessive()
    {
        int _random = Random.Range(0, SuccessiveFireAuto.Length);
        Audio.PlayOneShot(SuccessiveFireAuto[_random]);
    }

    // shotgun - single
    public void PlayShotgun()
    {
        int _random = Random.Range(0, SingleFireShotgun.Length);
        Audio.PlayOneShot(SingleFireShotgun[_random]);
    }

    // shotgun - successive
    public void PlayShotgunSuccessive()
    {
        Audio.PlayOneShot(SingleFireShotgun[0]);
        StartCoroutine(WaitBetweenShots());
    }

    // shotgun pause
    public IEnumerator WaitBetweenShots()
    {
        yield return new WaitForSeconds(0.4f);
        PlayShotgunSuccessiveTwo();
    }

    // shotgun - successive two
    public void PlayShotgunSuccessiveTwo()
    {
        Audio.PlayOneShot(SingleFireShotgun[1]);
    }

    // swing
    public void PlaySwing()
    {
        int _random = Random.Range(0, Swings.Length);
        Audio.PlayOneShot(Swings[_random]);
    }



    // damage
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

    // melee impact
    public void PlayMeleeImpact()
    {
        int _random = Random.Range(0, MeleeImpact.Length);
        Audio.PlayOneShot(MeleeImpact[_random]);
    }

    // ranged impact
    public void PlayRangedImpact()
    {
        int _random = Random.Range(0, RangedImpact.Length);
        Audio.PlayOneShot(RangedImpact[_random]);
    }



    // miss
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

    // miss - melee
    public void PlayMeleeMiss()
    {
        int _random = Random.Range(0, MeleeMiss.Length);
        Audio.PlayOneShot(MeleeMiss[_random]);
    }

    // miss - ranged
    public void PlayRangedMiss()
    {
        int _random = Random.Range(0, RangedMiss.Length);
        Audio.PlayOneShot(RangedMiss[_random]);
    }



    // grunt
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
            case CharacterSpecies.Robot:
                PlayGruntRobot();
                break;
        }
    }

    // grunt - male
    public void PlayGruntMale()
    {
        int _random = Random.Range(0, GruntMale.Length);
        Audio.PlayOneShot(GruntMale[_random]);
    }

    // grunt - female
    public void PlayGruntFemale()
    {
        int _random = Random.Range(0, GruntFemale.Length);
        Audio.PlayOneShot(GruntFemale[_random]);
    }

    // grunt - ape
    public void PlayGruntApe()
    {
        int _random = Random.Range(0, GruntApe.Length);
        Audio.PlayOneShot(GruntApe[_random]);
    }

    // grunt - robot
    public void PlayGruntRobot()
    {
        int _random = Random.Range(0, GruntRobot.Length);
        Audio.PlayOneShot(GruntRobot[_random]);
    }



    // pain
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
            case CharacterSpecies.Robot:
                PlayPainRobot();
                break;
        }
    }

    // pain - male
    public void PlayPainMale()
    {
        int _random = Random.Range(0, PainMale.Length);
        Audio.PlayOneShot(PainMale[_random]);
    }

    // pain - female
    public void PlayPainFemale()
    {
        int _random = Random.Range(0, PainFemale.Length);
        Audio.PlayOneShot(PainFemale[_random]);
    }

    // pain - ape
    public void PlayPainApe()
    {
        int _random = Random.Range(0, PainApe.Length);
        Audio.PlayOneShot(PainApe[_random]);
    }

    // pain - robot
    public void PlayPainRobot()
    {
        int _random = Random.Range(0, PainRobot.Length);
        Audio.PlayOneShot(PainRobot[_random]);
    }



    // death
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
            case CharacterSpecies.Robot:
                PlayDeathRobot();
                break;
        }
    }

    // death - male
    public void PlayDeathMale()
    {
        int _random = Random.Range(0, DeathMale.Length);
        Audio.PlayOneShot(DeathMale[_random]);
    }

    // pain - female
    public void PlayDeathFemale()
    {
        int _random = Random.Range(0, DeathFemale.Length);
        Audio.PlayOneShot(DeathFemale[_random]);
    }

    // death - ape
    public void PlayDeathApe()
    {
        int _random = Random.Range(0, DeathApe.Length);
        Audio.PlayOneShot(DeathApe[_random]);
    }

    // pain - female
    public void PlayDeathRobot()
    {
        int _random = Random.Range(0, DeathRobot.Length);
        Audio.PlayOneShot(DeathRobot[_random]);
    }


    // reload
    public void PlayReload()
    {
        Audio.PlayOneShot(Reload[0]);
    }

    // select ammo
    public void PlaySelectAmmo()
    {
        Audio.PlayOneShot(SelectAmmo[0]);
    }

    // heal
    public void PlayHeal()
    {
        Audio.PlayOneShot(Heal[0]);
    }




    // register/unregister methods with lua
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