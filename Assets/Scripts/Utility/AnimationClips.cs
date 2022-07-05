using System.Collections.Generic;
using UnityEngine;

public class AnimationClips : MonoBehaviour
{
    public static AnimationClips instance;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }



    [Header("Animation Controllers")]
    public RuntimeAnimatorController[] AnimatorControllers;



    [Header("Idle Animations")]
    public List<AnimationClip> IdleAnimationClips;

    [Header("Sitting Animations")]
    public List<AnimationClip> SittingMaleAnimationClips;
    public List<AnimationClip> SittingFemaleAnimationClips;

    [Header("Walking Animations")]
    public List<AnimationClip> WalkingMaleAnimationClips;
    public List<AnimationClip> WalkingFemaleAnimationClips;

    [Header("Laying Animations")]
    public List<AnimationClip> LayingMaleAnimationClips;
    public List<AnimationClip> LayingFemaleAnimationClips;

    [Header("Special Animations")]
    public List<AnimationClip> HostageAnimationClips;
    public List<AnimationClip> ScaredAnimationClips;


    [Header("Combat - Idle Animations")]
    public List<AnimationClip> IdleCombatAnimationClips;
    public List<AnimationClip> StunnedAnimationClips;
    


    [Header("Combat - Move Animations")]
    public List<AnimationClip> MoveCombatAnimationClips;



    [Header("Combat - Attack Animations")]
    public List<AnimationClip> UnarmedAttackAnimationClips;
    public List<AnimationClip> Blade1hAttackAnimationClips;
    public List<AnimationClip> Blunt1hAttackAnimationClips;
    public List<AnimationClip> Blade2hAttackAnimationClips;
    public List<AnimationClip> Blunt2hAttackAnimationClips;
    public List<AnimationClip> PistolAttackAnimationClips;
    public List<AnimationClip> RifleAttackAnimationClips;
    public List<AnimationClip> HeavyAttackAnimationClips;

    [Header("Combat - Hurt Animations")]
    public List<AnimationClip> UnarmedHurtAnimationClips;
    public List<AnimationClip> Blade1hHurtAnimationClips;
    public List<AnimationClip> Blunt1hHurtAnimationClips;
    public List<AnimationClip> Blade2hHurtAnimationClips;
    public List<AnimationClip> Blunt2hHurtAnimationClips;
    public List<AnimationClip> PistolHurtAnimationClips;
    public List<AnimationClip> RifleHurtAnimationClips;
    public List<AnimationClip> HeavyHurtAnimationClips;



    [Header("Combat - (Cover) Idle Animations")]
    public List<AnimationClip> UnarmedCoverIdleAnimationClips;
    public List<AnimationClip> Blade1hCoverIdleAnimationClips;
    public List<AnimationClip> Blunt1hCoverIdleAnimationClips;
    public List<AnimationClip> Blade2hCoverIdleAnimationClips;
    public List<AnimationClip> Blunt2hCoverIdleAnimationClips;
    public List<AnimationClip> PistolCoverIdleAnimationClips;
    public List<AnimationClip> RifleCoverIdleAnimationClips;
    public List<AnimationClip> HeavyCoverIdleAnimationClips;

    [Header("Combat - (Cover) Attack Animations")]
    public List<AnimationClip> UnarmedCoverAttackAnimationClips;
    public List<AnimationClip> Blade1hCoverAttackAnimationClips;
    public List<AnimationClip> Blunt1hCoverAttackAnimationClips;
    public List<AnimationClip> Blade2hCoverAttackAnimationClips;
    public List<AnimationClip> Blunt2hCoverAttackAnimationClips;
    public List<AnimationClip> PistolCoverAttackAnimationClips;
    public List<AnimationClip> RifleCoverAttackAnimationClips;
    public List<AnimationClip> HeavyCoverAttackAnimationClips;

    [Header("Combat - (Cover) Hurt Animations")]
    public List<AnimationClip> UnarmedCoverHurtAnimationClips;
    public List<AnimationClip> Blade1hCoverHurtAnimationClips;
    public List<AnimationClip> Blunt1hCoverHurtAnimationClips;
    public List<AnimationClip> Blade2hCoverHurtAnimationClips;
    public List<AnimationClip> Blunt2hCoverHurtAnimationClips;
    public List<AnimationClip> PistolCoverHurtAnimationClips;
    public List<AnimationClip> RifleCoverHurtAnimationClips;
    public List<AnimationClip> HeavyCoverHurtAnimationClips;



    [Header("Combat - Reload Animations")]
    public List<AnimationClip> ReloadAnimationClips;
    public List<AnimationClip> ReloadCoverAnimationClips;

    [Header("Dying Animations")]
    public List<AnimationClip> DyingAnimationClips;
}
