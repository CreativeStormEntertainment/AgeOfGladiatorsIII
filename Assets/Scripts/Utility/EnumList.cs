using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -------------------------------------------
// mission names
public enum MissionNames
{
    None,
    YouBetYourLife,
    RobotDisturbance,

    FutureShock,
    ABountyList,
    StreetPatrol
}
// -------------------------------------------
// conversant names
public enum NPCConversantNames
{
    None,
    Vendor,
    MaxNormal,
    MaxNormalRadio,
    GangsterApe,
    BillBaxter,
    TimLuna,
    BabyBobNicely,
    CleverJacko,
    OpenDoorTerminal,
    OpenDoorWires,
    YouBetYourLifePartOne,
    YouBetYourLifePartTwo,
    YouBetYourLifeTerminal,
    JudgeTalbot,
    NomiSmith
}
// -------------------------------------------
// conversant names
public enum NPCSpecialNames
{
    None,
    LindaDooley,
    Sheldon,
    Penny,
    BabyBobNicely, // for television
}
// -------------------------------------------
// character species
public enum CharacterSpecies
{
    Human,
    Ape,
}
// -------------------------------------------
// attributes
public enum Attribute
{
    None,
    Strength,
    Vision,
    Coordination,
    Speed,
    Aggression,
    Intelligence
}
// -------------------------------------------
// skills
public enum Skill
{
    None,
    Intimidation,
    Persuasion,
    Law,
    StreetSmarts,
    Perception,
    Science,
    Engineering,
    Computers,
    Medicine,
    Demolitions,
    Lockpicking,

    Psionics,
    Brawling,
    Shooting,
    HeavyWeapons,
    SpecialAmmo,
    Athletics
}
// -------------------------------------------
// combat stats
public enum CombatStats
{
    None,
    Initiative,
    Health,
    Armor,
    MovementSpeed,
    ActionPoints,
    HitChance,
    MeleeDamageBonus,
    RangedDamageBonus,
    CriticalChance,
    CriticalDamage,
    Evasion,
    Penetration,
    WeaponDamageTotal
}


// -------------------------------------------
// item class
public enum ItemClasses
{
    Helmet,
    Chest,
    Legs,
    Boots,
    Gloves,
    Shield,
    Accessory,
    Weapon,
    Mission
}
// -------------------------------------------
// weapon damage
public enum WeaponDamage
{
    Melee,
    Ranged
}
// -------------------------------------------
// weapon types
public enum WeaponTypes
{
    Unarmed,
    Blade1H,
    Blunt1H,
    Blade2H,
    Blunt2H,
    Pistol,
    Shotgun,
    Rifle,
    HeavyWeapon
}
// -------------------------------------------
// weapon damage
public enum ArmorTypes
{
    Light,
    Medium,
    Heavy
}
// -------------------------------------------
// quest trigger types
public enum QuestTriggers
{
    None,
    QuestActivate,
    QuestComplete,
    QuestEntryAdvance,
    QuestEntrySuccess,
}
// -------------------------------------------
// quest clicker types
public enum QuestClickers
{
    None,
    Increment,
}
// -------------------------------------------
// attribute box types
public enum AttributeBoxType
{
    Attribute,
    Skill
}
// -------------------------------------------
// music cues
public enum MusicCues
{
    None,
    Combat,
    Event,
    LevelUp,
    MissionAdvance,
    GameShow,
    CombatIn,
    CombatOut
}
// -------------------------------------------
// audio cues
public enum AudioCues
{
    None,
    Computer,
    Lock,
    Crate,
    Radio,
    Door,
    Loot,
    Craft,
    Perception,
    SkillDoor,
    SkillCrate,
    SkillComputer
}
// -------------------------------------------
// idle animations
public enum IdleAnimationType
{
    Idle,
    IdleSad,
    IdleWarrior,
    IdleAlert,
    IdleLazy,
    IdleNeutral,
    IdleLeaning,
    IdleKneeling,
    IdleHostage,
    IdleDrawGun,
    IdleTalking,
    IdleTalkingTwo,
    IdleTalkingThree,
    IdleCounting,
    IdleTexting,
    IdleLookingAround,
    IdleHappy,
    IdleCheering,

    Sitting,
    SittingGround,
    Laying,

    WalkingInCircle,
}
// -------------------------------------------
// death animations
public enum DeathAnimationType
{
    Random,
    Specific
}
// -------------------------------------------
// crate type
public enum CrateType
{
    Random,
    Specific
}
// -------------------------------------------
// character portraits
public enum CharacterPortraitType
{
    JudgeMale,
    JudgeFemale,
    CitizenMale,
    CitizenFemale,
    Item,
    Character,
}
// -------------------------------------------
// combat actions
public enum CombatActions
{
    None,
    EndTurn,
    AttackRegular,
    Heal,
    Reload,
    Stun,
    HitBonus,
    CriticalBonus,
    
}
// -------------------------------------------
// button type
public enum ButtonType
{
    Large,
    Regular,
    Small,
    Tab,
    Close, 
    Journal
}
// -------------------------------------------
// game difficulty
public enum GameDifficulty
{
    Easy, 
    Medium,
    Hard,
}
// -------------------------------------------
// speech blurb type
public enum SpeechBlurbType
{
    Citizen,
    Judge,
}
// -------------------------------------------





public class EnumList
{
    // get random enum
    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
}
