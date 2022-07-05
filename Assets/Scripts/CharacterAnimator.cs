using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [HideInInspector]
    public Character AttachedCharacter;
    bool male;

    [HideInInspector]
    public Animator AttachedAnimator;

    [HideInInspector]
    public Movement CharacterMovement;

    [HideInInspector]
    public AnimatorOverrideController OverrideController;

    public IdleAnimationType IdleAnimation;
    public DeathAnimationType DeathAnimation;

    public int deathType;



    void Start()
    {
        AttachCharacter();

        //AttachedAnimator = GetComponentInChildren<Animator>();

        SetAllAnimators(AnimationClips.instance.AnimatorControllers[0]);

        // override controller allows for multiple instances of animation states to be added (without extra bools in animator)
        OverrideController = new AnimatorOverrideController(AttachedAnimator.runtimeAnimatorController);
        SetAllAnimators(OverrideController);
    }

    void Update()
    {
        CheckAnimationSpeed();

        if ((Combat.instance.combatActivated || AttachedCharacter.weaponDrawn) && !AttachedCharacter.isDead)
        {
            PlayCombatAnimation();
        }
        else
        {
            PlayMovementAnimations();
            PlayIdleAnimation();
            PlayDyingAnimations();
        }
    }



    // switch animator
    public void SwitchAnimator()
    {
        if (Combat.instance.combatActivated)
        {
            //AttachedAnimator = GetComponentInChildren<Animator>();
            //SetAllAnimators(AnimationClips.instance.AnimatorControllers[1] as RuntimeAnimatorController);
            OverrideController = new AnimatorOverrideController(AnimationClips.instance.AnimatorControllers[1]);
            SetAllAnimators(OverrideController);
        }
        else
        {
            //AttachedAnimator = GetComponentInChildren<Animator>();
            //SetAllAnimators(AnimationClips.instance.AnimatorControllers[0] as RuntimeAnimatorController);
            OverrideController = new AnimatorOverrideController(AnimationClips.instance.AnimatorControllers[0]);
            SetAllAnimators(OverrideController);
        }
    }



    // change animation speed
    public void CheckAnimationSpeed()
    {
        //if (PartyScene.instance.MainCharacter.GetComponent<Movement>().restrictMovement)
        //    AttachedAnimator.speed = 0;
        //else
        //    AttachedAnimator.speed = 1f;
    }



    // play idle animation (non-combat)
    void PlayIdleAnimation()
    {
        // -------------------------------------------------------
        // check if dead
        if (AttachedCharacter.isDead)
            return;
        // -------------------------------------------------------

        // -------------------------------------------------------
        // non-combat
        switch (IdleAnimation)
        {
            // idle (non-combat)
            case IdleAnimationType.Idle:
                AttachedCharacter.unableToTurn = false;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[0];
                break;
            case IdleAnimationType.IdleAlert:
                AttachedCharacter.unableToTurn = false;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[1];
                break;
            case IdleAnimationType.IdleLazy:
                AttachedCharacter.unableToTurn = false;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[2];
                break;
            case IdleAnimationType.IdleNeutral:
                AttachedCharacter.unableToTurn = false;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[3];
                break;
            case IdleAnimationType.IdleSad:
                AttachedCharacter.unableToTurn = false;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[4];
                break;
            case IdleAnimationType.IdleKneeling:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[5];
                break;
            case IdleAnimationType.IdleLeaning:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[6];
                break;
            case IdleAnimationType.IdleLookingAround:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[7];
                break;
            case IdleAnimationType.IdleTalking:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[8];
                break;
            case IdleAnimationType.IdleTalkingTwo:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[9];
                break;
            case IdleAnimationType.IdleTalkingThree:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[10];
                break;
            case IdleAnimationType.IdleCounting:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[11];
                break;
            case IdleAnimationType.IdleTexting:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[12];
                break;
            case IdleAnimationType.IdleHappy:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[13];
                break;
            case IdleAnimationType.IdleCheering:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.IdleAnimationClips[14];
                break;


            // special
            case IdleAnimationType.IdleHostage:
                AttachedCharacter.unableToTurn = true;
                OverrideController["Idle"] = AnimationClips.instance.HostageAnimationClips[0];
                break;


            // sitting (non-combat)
            case IdleAnimationType.Sitting:
                AttachedCharacter.unableToTurn = true;
                if (male)
                    OverrideController["Idle"] = AnimationClips.instance.SittingMaleAnimationClips[0];
                else
                    OverrideController["Idle"] = AnimationClips.instance.SittingFemaleAnimationClips[0];
                break;
            case IdleAnimationType.SittingGround:
                AttachedCharacter.unableToTurn = true;
                if (male)
                    OverrideController["Idle"] = AnimationClips.instance.SittingMaleAnimationClips[0];
                else
                    OverrideController["Idle"] = AnimationClips.instance.SittingFemaleAnimationClips[0];
                break;

            // laying (non-combat)
            case IdleAnimationType.Laying:
                AttachedCharacter.unableToTurn = true;
                if (male)
                    OverrideController["Idle"] = AnimationClips.instance.LayingMaleAnimationClips[0];
                else
                    OverrideController["Idle"] = AnimationClips.instance.LayingFemaleAnimationClips[0];
                break;
        }
        // -------------------------------------------------------

        // -------------------------------------------------------
        // reset all animators
        SetAllAnimators(OverrideController);
        // -------------------------------------------------------
    }

    // play movement animations (non-combat)
    void PlayMovementAnimations()
    {
        // -------------------------------------------------------
        // check if dead
        if (AttachedCharacter.isDead)
            return;
        // -------------------------------------------------------

        // -------------------------------------------------------
        // moving
        if (CharacterMovement.isMoving)
        {
            SetAllAnimators("isWalking", true);

            if (male)
                OverrideController["Walking"] = AnimationClips.instance.WalkingMaleAnimationClips[0];
            else
                OverrideController["Walking"] = AnimationClips.instance.WalkingFemaleAnimationClips[0];
        }  
        else
        {
            SetAllAnimators("isWalking", false);
        }
        // -------------------------------------------------------
    }

    // play combat animation
    void PlayCombatAnimation()
    {
        // -------------------------------------------------------
        // check if moving
        if (CharacterMovement.isMoving)
            SetAllAnimators("isWalking", true);
        else
            SetAllAnimators("isWalking", false);
        // -------------------------------------------------------

        // -------------------------------------------------------
        // combat (THE "" IS ESTABLISHED BY ADDING AN ANIMATION TO COMBAT ANIMATION CONTROLLER - IT WILL GET ADDED TO THE OVERRIDE)
        switch (AttachedCharacter.CombatStyle())
        {
            case WeaponTypes.Pistol: // weapon type (ranged) (pistol)
                OverrideController["Idle"] = AnimationClips.instance.IdleCombatAnimationClips[0];
                OverrideController["Running"] = AnimationClips.instance.MoveCombatAnimationClips[0];
                OverrideController["Attack"] = AnimationClips.instance.PistolAttackAnimationClips[Random.Range(0, AnimationClips.instance.PistolAttackAnimationClips.Count)];
                OverrideController["Damage"] = AnimationClips.instance.PistolHurtAnimationClips[Random.Range(0, AnimationClips.instance.PistolHurtAnimationClips.Count)];
                OverrideController["Reload"] = AnimationClips.instance.ReloadAnimationClips[0];

                if (AttachedCharacter.inCover)
                {
                    OverrideController["Idle"] = AnimationClips.instance.PistolCoverIdleAnimationClips[0];
                    OverrideController["Reload"] = AnimationClips.instance.ReloadCoverAnimationClips[0];
                }
                    
                break;
            case WeaponTypes.Rifle: // weapon type (ranged) (rifle)
                OverrideController["Idle"] = AnimationClips.instance.IdleCombatAnimationClips[1];
                OverrideController["Running"] = AnimationClips.instance.MoveCombatAnimationClips[1];
                OverrideController["Attack"] = AnimationClips.instance.RifleAttackAnimationClips[Random.Range(0, AnimationClips.instance.RifleAttackAnimationClips.Count)];
                OverrideController["Damage"] = AnimationClips.instance.RifleHurtAnimationClips[Random.Range(0, AnimationClips.instance.RifleHurtAnimationClips.Count)];
                OverrideController["Reload"] = AnimationClips.instance.ReloadAnimationClips[1];
                break;
            case WeaponTypes.HeavyWeapon: // weapon type (ranged) (heavy weapon)
                OverrideController["Idle"] = AnimationClips.instance.IdleCombatAnimationClips[2];
                OverrideController["Running"] = AnimationClips.instance.MoveCombatAnimationClips[2];
                OverrideController["Attack"] = AnimationClips.instance.HeavyAttackAnimationClips[Random.Range(0, AnimationClips.instance.HeavyAttackAnimationClips.Count)];
                OverrideController["Damage"] = AnimationClips.instance.HeavyHurtAnimationClips[Random.Range(0, AnimationClips.instance.HeavyHurtAnimationClips.Count)];
                break;
            case WeaponTypes.Blade1H: // weapon type (blade) (one-handed)
                OverrideController["Idle"] = AnimationClips.instance.IdleCombatAnimationClips[3];
                OverrideController["Running"] = AnimationClips.instance.MoveCombatAnimationClips[3];
                OverrideController["Attack"] = AnimationClips.instance.Blade1hAttackAnimationClips[Random.Range(0, AnimationClips.instance.Blade1hAttackAnimationClips.Count)];
                OverrideController["Damage"] = AnimationClips.instance.Blade1hHurtAnimationClips[Random.Range(0, AnimationClips.instance.Blade1hHurtAnimationClips.Count)];
                break;
            case WeaponTypes.Blunt1H: // weapon type (blunt) (one-handed)
                OverrideController["Idle"] = AnimationClips.instance.IdleCombatAnimationClips[3];
                OverrideController["Running"] = AnimationClips.instance.MoveCombatAnimationClips[3];
                OverrideController["Attack"] = AnimationClips.instance.Blunt1hAttackAnimationClips[Random.Range(0, AnimationClips.instance.Blunt1hAttackAnimationClips.Count)];
                OverrideController["Damage"] = AnimationClips.instance.Blunt1hHurtAnimationClips[Random.Range(0, AnimationClips.instance.Blunt1hHurtAnimationClips.Count)];
                break;
            case WeaponTypes.Blade2H: // weapon type (blade) (two-handed)
                OverrideController["Idle"] = AnimationClips.instance.IdleCombatAnimationClips[4];
                OverrideController["Running"] = AnimationClips.instance.MoveCombatAnimationClips[4];
                OverrideController["Attack"] = AnimationClips.instance.Blade2hAttackAnimationClips[Random.Range(0, AnimationClips.instance.Blade2hAttackAnimationClips.Count)];
                OverrideController["Damage"] = AnimationClips.instance.Blade2hHurtAnimationClips[Random.Range(0, AnimationClips.instance.Blade2hHurtAnimationClips.Count)];
                break;
            case WeaponTypes.Blunt2H: // weapon type (blunt) (two-handed)
                OverrideController["Idle"] = AnimationClips.instance.IdleCombatAnimationClips[4];
                OverrideController["Running"] = AnimationClips.instance.MoveCombatAnimationClips[4];
                OverrideController["Attack"] = AnimationClips.instance.Blunt2hAttackAnimationClips[Random.Range(0, AnimationClips.instance.Blunt2hAttackAnimationClips.Count)];
                OverrideController["Damage"] = AnimationClips.instance.Blunt2hHurtAnimationClips[Random.Range(0, AnimationClips.instance.Blunt2hHurtAnimationClips.Count)];
                break;

            case WeaponTypes.Unarmed: // unarmed (default)
                OverrideController["Idle"] = AnimationClips.instance.IdleCombatAnimationClips[5];
                OverrideController["Running"] = AnimationClips.instance.MoveCombatAnimationClips[5];
                OverrideController["Attack"] = AnimationClips.instance.UnarmedAttackAnimationClips[Random.Range(0, AnimationClips.instance.UnarmedAttackAnimationClips.Count)];
                OverrideController["Damage"] = AnimationClips.instance.UnarmedHurtAnimationClips[Random.Range(0, AnimationClips.instance.UnarmedHurtAnimationClips.Count)];
                break;
        }
        // -------------------------------------------------------

        // -------------------------------------------------------
        // check other conditions
        if (AttachedCharacter.isStunned)
            OverrideController["Idle"] = AnimationClips.instance.StunnedAnimationClips[0];
        // -------------------------------------------------------

        // -------------------------------------------------------
        // non-hostile
        if (AttachedCharacter.GetComponent<NPC>() != null && !AttachedCharacter.GetComponent<NPC>().isHostile && !AttachedCharacter.weaponDrawn)
            OverrideController["Idle"] = AnimationClips.instance.ScaredAnimationClips[0];
        // -------------------------------------------------------
    }

    // play dying animations
    void PlayDyingAnimations()
    {
        if (AttachedCharacter.isDead)
        {
            int _deathType;

            if (DeathAnimation == DeathAnimationType.Random)
                _deathType = Random.Range(0, AnimationClips.instance.DyingAnimationClips.Count);
            else
                _deathType = deathType;

            SetAllAnimators(AnimationClips.instance.AnimatorControllers[3]);
            SetAllAnimators("Dying", _deathType);
        }  
    }



    // attach character and animator
    public void AttachCharacter()
    {
        // attach NPC on runtime - done here due to NPC script being attached sometimes after game start
        if (AttachedCharacter == null && GetComponent<NPC>() != null)
        {
            AttachedCharacter = GetComponent<NPC>();
            male = AttachedCharacter.male;

            if (AttachedAnimator == null)
                AttachedAnimator = GetComponentInChildren<Animator>();
        }

        // attach PlayerCharacter on runtime (based on male or female model)
        if (GetComponent<PlayerCharacter>() != null)
        {
            AttachedCharacter = GetComponent<PlayerCharacter>();

            // attach model animator (male or female)
            if (GetComponent<AttachedAnimators>() != null)
            {
                male = AttachedCharacter.male;

                if (male)
                    AttachedAnimator = GetComponent<AttachedAnimators>().MaleJudgeAnimator;
                else
                    AttachedAnimator = GetComponent<AttachedAnimators>().FemaleJudgeAnimator;
            }
        }
    }



    // blood splatter
    public void ActivateBloodSplatter(Character _Attacking)
    {
        if (GetComponentInChildren<BloodActivator>() != null && _Attacking.CombatStyle() != WeaponTypes.Unarmed)
        {
            if (_Attacking.CombatStyle() == WeaponTypes.Blade1H || _Attacking.CombatStyle() == WeaponTypes.Blade2H)
                GetComponentInChildren<BloodActivator>().ActivateCutBloodSplatter();

            if (_Attacking.CombatStyle() == WeaponTypes.Pistol || _Attacking.CombatStyle() == WeaponTypes.Rifle)
                GetComponentInChildren<BloodActivator>().ActivateShotBloodSplatter();

            if (_Attacking.CombatStyle() == WeaponTypes.Shotgun)
                GetComponentInChildren<BloodActivator>().ActivateShotgunBloodSplatter();

            if (_Attacking.CombatStyle() == WeaponTypes.HeavyWeapon)
                GetComponentInChildren<BloodActivator>().ActivateHeavyBloodSplatter();
        }  
    }



    // set all animators (bool)
    public void SetAllAnimators(string _condition, bool _state)
    {
        Animator[] _ChildAnimators = GetComponentsInChildren<Animator>();

        foreach (Animator _Animator in _ChildAnimators)
            _Animator.SetBool(_condition, _state);
    }

    // set all animators (int)
    void SetAllAnimators(string _condition, int _index)
    {
        Animator[] _ChildAnimators = GetComponentsInChildren<Animator>();

        foreach (Animator _Animator in _ChildAnimators)
            _Animator.SetInteger(_condition, _index);
    }

    // set all animators (controller)
    void SetAllAnimators(RuntimeAnimatorController _Controller)
    {
        Animator[] _ChildAnimators = GetComponentsInChildren<Animator>();

        foreach (Animator _Animator in _ChildAnimators)
            _Animator.runtimeAnimatorController = _Controller as RuntimeAnimatorController;
    }



    // get current clip duration
    public float GetClipDuration()
    {
        AnimatorClipInfo[] m_CurrentClipInfo;
        string m_ClipName;
        float m_CurrentClipLength;

        // current animation clip information for the base layer
        m_CurrentClipInfo = AttachedAnimator.GetCurrentAnimatorClipInfo(0);
        // length of the clip
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        // name of clip
        m_ClipName = m_CurrentClipInfo[0].clip.name;

        //Debug.Log(Combat.instance.round + ": " + AttachedCharacter.characterName + " (" + m_ClipName + ", " + m_CurrentClipLength + ") ----------------------------");

        return m_CurrentClipLength;
    }
}

