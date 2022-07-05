using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class Character : MonoBehaviour
{
    [Header("Character Info")]
    public string characterName;
    public bool male = true;
    public CharacterSpecies Species;

    [Header("Portrait")]
    public CharacterPortraitType PortraitType;
    public int portraitNumber;

    [HideInInspector]
    public int level;
    [HideInInspector]
    public int experiencePoints;
    [HideInInspector]
    public int experiencePointsNextLevel;
    [HideInInspector]
    public int rank;
    [HideInInspector]
    public int levelCounter;

    [HideInInspector]
    public int skillPoints;
    [HideInInspector]
    public int attributePoints;

    [HideInInspector]
    public int strength;
    [HideInInspector]
    public int vision;
    [HideInInspector]
    public int coordination;
    [HideInInspector]
    public int speed;
    [HideInInspector]
    public int aggression;
    [HideInInspector]
    public int intelligence;

    [HideInInspector]
    public int brawling;
    [HideInInspector]
    public int shooting;
    [HideInInspector]
    public int specialAmmo;
    [HideInInspector]
    public int psionics;
    [HideInInspector]
    public int heavyWeapons;
    [HideInInspector]
    public int athletics;

    [HideInInspector]
    public int intimidation;
    [HideInInspector]
    public int persuasion;
    [HideInInspector]
    public int perception;
    [HideInInspector]
    public int lawKnowledge;
    [HideInInspector]
    public int streetsmarts;
    [HideInInspector]
    public int science;
    [HideInInspector]
    public int engineering;
    [HideInInspector]
    public int computers;
    [HideInInspector]
    public int medicine;
    [HideInInspector]
    public int lockpicking;
    [HideInInspector]
    public int demolitions;

    [HideInInspector]
    public int credits;

    [HideInInspector]
    public bool unableToTurn;
    [HideInInspector]
    public bool isDead;

    [Header("Equipment")]
    public Helmet EquippedHelmet;
    public Chest EquippedChest;
    public Legs EquippedLegs;
    public Boots EquippedBoots;
    public Gloves EquippedGloves;
    public Shield EquippedShield;
    public Accessory EquippedAccessory;
    public Weapon EquippedWeapon;

    public bool weaponDrawn;

    [HideInInspector]
    public bool playerControlledCombat;
    [HideInInspector]
    public int combatHealth;
    [HideInInspector]
    public int combatMovementPoints;
    [HideInInspector]
    public float combatActionPoints;

    [HideInInspector]
    public bool isStunned;
    [HideInInspector]
    public int stunCountdown;
    [HideInInspector]
    public bool inCover;

    public bool giveXP;

    [HideInInspector]
    public List<Action> Actions = new List<Action>();



    void Start()
    {
        credits = 20;

        experiencePointsNextLevel = GameData.experienceNeededForNext;

        levelCounter = 1;

        // change dialogue system actor portrait and name
        if (playerControlledCombat)
        {
            Actor conversant = DialogueManager.masterDatabase.GetActor("Player");
            conversant.spritePortrait = PortraitSelector.FindPortrait(this, 1);
            conversant.Name = characterName;
        }    
    }



    // skill check
    public bool SkillCheck(Skill _Skill, int _skillLevel)
    {
        int _stat = GetSkill(_Skill);

        if (_stat < _skillLevel)
            return false;
        else
            return true;
    }

    // get skill
    public ref int GetSkill(Skill _Input)
    {
        ref int _stat = ref strength; // figure out something else here for initialization

        switch (_Input)
        {
            case Skill.Intimidation:
                _stat = ref intimidation;
                break;
            case Skill.Persuasion:
                _stat = ref persuasion;
                break;
            case Skill.Law:
                _stat = ref lawKnowledge;
                break;
            case Skill.StreetSmarts:
                _stat = ref streetsmarts;
                break;
            case Skill.Perception:
                _stat = ref perception;
                break;
            case Skill.Science:
                _stat = ref science;
                break;
            case Skill.Engineering:
                _stat = ref engineering;
                break;
            case Skill.Computers:
                _stat = ref computers;
                break;
            case Skill.Medicine:
                _stat = ref medicine;
                break;
            case Skill.Demolitions:
                _stat = ref demolitions;
                break;
            case Skill.Lockpicking:
                _stat = ref lockpicking;
                break;

            case Skill.Psionics:
                _stat = ref psionics;
                break;
            case Skill.Brawling:
                _stat = ref brawling;
                break;
            case Skill.Shooting:
                _stat = ref shooting;
                break;
            case Skill.SpecialAmmo:
                _stat = ref specialAmmo;
                break;
            case Skill.HeavyWeapons:
                _stat = ref heavyWeapons;
                break;
            case Skill.Athletics:
                _stat = ref athletics;
                break;

        }

        return ref _stat;
    }

    // increase skill
    public void IncreaseSkill(Skill _Input)
    {
        ref int _skill = ref GetSkill(_Input);
        _skill++;

        skillPoints -= CalculateSkillPointCost(_Input);
    }

    // decrease skill
    public void DecreaseSkill(Skill _Input)
    {
        ref int _skill = ref GetSkill(_Input);
        _skill--;
    }



    // get attribute
    public ref int GetAttribute(Attribute _Input)
    {
        ref int _attribute = ref strength; // figure out something else here for initialization

        switch (_Input)
        {
            case Attribute.Strength:
                _attribute = ref strength;
                break;
            case Attribute.Vision:
                _attribute = ref vision;
                break;
            case Attribute.Coordination:
                _attribute = ref coordination;
                break;
            case Attribute.Speed:
                _attribute = ref speed;
                break;
            case Attribute.Aggression:
                _attribute = ref aggression;
                break;
            case Attribute.Intelligence:
                _attribute = ref intelligence;
                break;

        }

        return ref _attribute;
    }

    // increase attribute
    public void IncreaseAttribute(Attribute _Input)
    {
        ref int _attribute = ref GetAttribute(_Input);
        _attribute++;

        attributePoints--;
    }

    // decrease attribute
    public void DecreaseAttribute(Attribute _Input)
    {
        ref int _skill = ref GetAttribute(_Input);
        _skill--;
    }



    // calculate health
    public int GetInitiative()
    {
        int _stat = 0;

        _stat = vision * GameData.visionInitiative;

        return _stat;
    }

    // calculate armor
    public int GetArmor()
    {
        int _stat = 0;

        if (EquippedHelmet != null)
            _stat += EquippedHelmet.armor;

        if (EquippedChest != null)
            _stat += EquippedChest.armor;

        if (EquippedLegs != null)
            _stat += EquippedLegs.armor;

        if (EquippedBoots != null)
            _stat += EquippedBoots.armor;

        if (EquippedGloves != null)
            _stat += EquippedGloves.armor;

        return _stat;
    }

    // calculate health
    public int GetHealth()
    {
        int _stat = 0;

        // strength
        _stat = strength * GameData.strengthHealth;

        // athletics
        _stat += athletics * GameData.athleticsHealth;

        // level
        _stat += level * GameData.levelHealth;

        // player bonus (temporary)
        if (playerControlledCombat)
            _stat += 100;

        // adjust npc stat by difficulty
        _stat = DifficultyAdjustment(_stat);

        return _stat;
    }

    // calculate melee damage (bonus)
    public int GetMeleeDamageBonus()
    {
        int _stat = 0;

        // strength
        _stat = strength * GameData.strengthMeleeDamageBonus;

        // brawling
        _stat += brawling * GameData.brawlingMeleeDamageBonus;

        return _stat;
    }

    // calculate ranged damage (bonus)
    public int GetRangedDamageBonus()
    {
        int _stat = 0;

        // vision
        _stat = vision * GameData.visionRangedDamageBonus;

        // shooting
        _stat += shooting * GameData.shootingRangedDamageBonus;

        return _stat;
    }

    // calculate hit chance
    public int GetHitChance()
    {
        int _stat = 0;

        // vision
        _stat = coordination * GameData.coordinationHit;

        // weapon
        if (EquippedWeapon != null)
            _stat += EquippedWeapon.hitChance;

        // hit bonus (action)
        if (Combat.instance.combatActivated && Combat.instance.hitBonusSelected)
            _stat += GameData.hitPointBonus;

        // player bonus (for demo)
        if (playerControlledCombat)
            _stat += 15;

        // clamp
        _stat = Mathf.Clamp(_stat, 0, 101);

        return _stat;
    }

    // calculate critical chance
    public int GetCriticalChance()
    {
        int _stat = 0;

        // aggression
        _stat = aggression * GameData.aggressionCriticalChance;

        // weapon
        if (EquippedWeapon != null)
            _stat += EquippedWeapon.criticalChance;

        // adjust npc stat by difficulty
        _stat = DifficultyAdjustment(_stat);

        // clamp
        _stat = Mathf.Clamp(_stat, 0, 101);

        return _stat;
    }

    // calculate critical damage
    public float GetCriticalDamage()
    {
        float _stat = 1;

        // aggression
        _stat += aggression * GameData.aggressionCriticalDamage;

        // weapon
        if (EquippedWeapon != null)
            _stat += EquippedWeapon.criticalDamage;

        return _stat;
    }

    // calculate evasion chance
    public int GetEvasionChance()
    {
        int _stat = 0;

        // speed
        _stat = speed * GameData.speedEvasion;

        // evasion
        _stat += athletics * GameData.athleticsEvasion;

        // adjust npc stat by difficulty
        _stat = DifficultyAdjustment(_stat);

        // clamp
        _stat = Mathf.Clamp(_stat, 0, 101);

        return _stat;
    }

    // calculate penetration
    public int GetPenetration()
    {
        int _stat = 0;

        // coordination
        _stat = coordination * GameData.coordinationPeneration;

        // skill
        _stat += brawling * GameData.weaponPeneration;
        _stat += shooting * GameData.weaponPeneration;

        // weapon
        if (EquippedWeapon != null)
            _stat += EquippedWeapon.penetration;

        return _stat;
    }

    // calculate movement points
    public int GetMovementPoints()
    {
        int _stat = 3;

        int _attribute = intelligence;

        if (_attribute == 1 || _attribute == 2)
            _stat += 1;

        if (_attribute == 3 || _attribute == 4)
            _stat += 2;

        if (_attribute == 5 || _attribute == 6)
            _stat += 3;

        if (_attribute == 7 || _attribute == 8)
            _stat += 4;

        if (_attribute == 9 || _attribute == 10)
            _stat += 5;

        return _stat;
    }

    // calculate action points
    public int GetActionPoints()
    {
        int _stat = 2;

        int _attribute = speed;

        if (_attribute == 1 || _attribute == 2)
            _stat += 1;

        if (_attribute == 3 || _attribute == 4)
            _stat += 2;

        if (_attribute == 5 || _attribute == 6)
            _stat += 3;

        if (_attribute == 7 || _attribute == 8)
            _stat += 4;

        if (_attribute == 9 || _attribute == 10)
            _stat += 5;

        return _stat;
    }



    // calculate attack damage
    public int CalculateAttackDamage()
    {
        int _damage = 0;

        if (EquippedWeapon != null)
        {
            if (EquippedWeapon.WeaponDamageType == WeaponDamage.Melee)
                _damage = Random.Range(GetMeleeMinimumDamage(), GetMeleeMaximumDamage());
            else
                _damage = Random.Range(GetRangedMinimumDamage(), GetRangedMaximumDamage());
        }
        else
        {
            _damage = Random.Range(GetUnarmedMinimumDamage(), GetUnarmedMaximumDamage());
        }

        return _damage;
    }

    // calculate melee damage (min)
    public int GetMeleeMinimumDamage()
    {
        int _stat = 0;

        if (EquippedWeapon != null && EquippedWeapon.WeaponDamageType == WeaponDamage.Melee)
            _stat = EquippedWeapon.minDamage + GetMeleeDamageBonus();

        _stat = DifficultyAdjustment(_stat);

        return _stat;
    }

    // calculate melee damage (max)
    public int GetMeleeMaximumDamage()
    {
        int _stat = 0;

        if (EquippedWeapon != null && EquippedWeapon.WeaponDamageType == WeaponDamage.Melee)
            _stat = EquippedWeapon.maxDamage + GetMeleeDamageBonus();

        _stat = DifficultyAdjustment(_stat);

        return _stat;
    }

    // calculate ranged damage (min)
    public int GetRangedMinimumDamage()
    {
        int _stat = 0;

        if (EquippedWeapon != null && EquippedWeapon.WeaponDamageType == WeaponDamage.Ranged)
            _stat = EquippedWeapon.minDamage + GetRangedDamageBonus();

        _stat = DifficultyAdjustment(_stat);

        return _stat;
    }

    // calculate ranged damage (max)
    public int GetRangedMaximumDamage()
    {
        int _stat = 0;

        if (EquippedWeapon != null && EquippedWeapon.WeaponDamageType == WeaponDamage.Ranged)
            _stat = EquippedWeapon.maxDamage + GetRangedDamageBonus();

        _stat = DifficultyAdjustment(_stat);

        return _stat;
    }

    // calculate unarmed damage (min)
    public int GetUnarmedMinimumDamage()
    {
        int _stat = 30;

        _stat = DifficultyAdjustment(_stat);

        return _stat;
    }

    // calculate unarmed damage (max)
    public int GetUnarmedMaximumDamage()
    {
        int _stat = 40;

        _stat = DifficultyAdjustment(_stat);

        return _stat;
    }



    // calculate action points cost
    public int GetActionPointAttackCost()
    {
        int _cost = 2;

        if (EquippedWeapon != null)
            _cost = EquippedWeapon.GetActionPointsRequired();

        return _cost;
    }



    // stat difficulty adjustment
    public int DifficultyAdjustment(int _stat)
    {
        if (playerControlledCombat)
            return _stat;

        int _percentage = 0;

        switch (Dredd.instance.difficulty)
        {
            case GameDifficulty.Easy:
                _percentage = 40;
                break;
            case GameDifficulty.Medium:
                _percentage = 20;
                break;
            case GameDifficulty.Hard:
                _percentage = 0;
                break;
        }

        _stat -= (_stat * _percentage / 100);

        return _stat;
    }



    // gain experience
    public void GainExperience(int _incoming)
    {
        // intelligence bonus
        float _bonus = ((float)_incoming * ((float)(intelligence * GameData.intelligenceXP) / 100));
        _incoming += (int)_bonus;

        // add experience points
        experiencePoints += _incoming;

        if (GetComponent<PlayerCharacter>() != null && _incoming != 0)
            StartCoroutine(GetComponent<ActionTextActivator>().ReportXP(_incoming));

        if (experiencePoints >= experiencePointsNextLevel)
            IncreaseLevel();

        // sound
        if (!Combat.instance.combatActivated)
            UISounds.instance.PlaySkillSucceed();
    }

    // increase level
    public void IncreaseLevel()
    {
        level++;
        levelCounter++;

        experiencePointsNextLevel += GameData.experienceNeededForNext;

        if (GetComponent<PlayerCharacter>() != null)
            UI.instance.OpenLevel();
            
        IncreaseSkillPoints();
        IncreaseRank();
        IncreaseAttributePoints();

        if (levelCounter == 5)
            levelCounter = 0;
    }

    // calculate skill points per level
    public int GetSkillPointsPerLevel()
    {
        int _stat = intelligence;

        //int _attribute = intelligence;

        //if (_attribute == 1 || _attribute == 2)
        //    _stat += 1;

        //if (_attribute == 3 || _attribute == 4)
        //    _stat += 2;

        //if (_attribute == 5 || _attribute == 6)
        //    _stat += 3;

        //if (_attribute == 7 || _attribute == 8)
        //    _stat += 4;

        //if (_attribute == 9 || _attribute == 10)
        //    _stat += 5;

        return _stat;
    }

    // increase skillpoints
    void IncreaseSkillPoints()
    {
        skillPoints += GetSkillPointsPerLevel();
    }

    // increase attribute points
    void IncreaseAttributePoints()
    {
        if (levelCounter == 5)
            attributePoints += 1;
    }

    // calculate skill cost
    public int CalculateSkillPointCost(Skill _Input)
    {
        int _cost = GetSkill(_Input);

        return _cost;
    }



    // increase rank
    public void IncreaseRank()
    {
        if (GetComponent<PlayerCharacter>() != null)
        {
            if (levelCounter == 5)
            {
                rank++;
                UI.instance.OpenRank();
            }
        }
    }

    // get rank label
    public string GetRankLabel()
    {
        string _rank = "";

        switch (rank)
        {
            case 1:
                _rank = "Trainee";
                break;
            case 2:
                _rank = "Rookie";
                break;
            case 3:
                _rank = "Squadie";
                break;
            case 4:
                _rank = "Street Judge";
                break;
        }

        return _rank;
    }



    // get combat style
    public WeaponTypes CombatStyle()
    {
        WeaponTypes _CombatStyle = WeaponTypes.Unarmed; // unarmed

        // assign all these indexes to proper weapons later, this is temporarily assigning weapon styles
        if (EquippedWeapon != null)
        {
            switch (EquippedWeapon.WeaponType)
            {
                case WeaponTypes.Pistol:
                    _CombatStyle = WeaponTypes.Pistol;
                    break;
                case WeaponTypes.Shotgun:
                    _CombatStyle = WeaponTypes.Shotgun;
                    break;
                case WeaponTypes.Rifle:
                    _CombatStyle = WeaponTypes.Rifle;
                    break;
                case WeaponTypes.Blunt1H:
                    _CombatStyle = WeaponTypes.Blunt1H;
                    break;
                case WeaponTypes.Blade1H:
                    _CombatStyle = WeaponTypes.Blade1H;
                    break;
                case WeaponTypes.HeavyWeapon:
                    _CombatStyle = WeaponTypes.HeavyWeapon;
                    break;
            }
        }

        return _CombatStyle;
    }



    // reset combat stats (round)
    public void ResetCombatStatsEachRound()
    {
        combatActionPoints = GetActionPoints();
        combatMovementPoints = GetMovementPoints();
    }



    // update equipment on model
    public void UpdateEquipmentOnModel()
    {
        // -----------------------------
        // check equipment
        EquipmentChecker.instance.CheckEquipment(this, transform, false);
        // -----------------------------

        // -----------------------------
        // attach animator (portrait controller)
        Animator[] _ChildAnimators = this.GetComponentsInChildren<Animator>();

        foreach (Animator _Animator in _ChildAnimators)
            _Animator.runtimeAnimatorController = AnimationClips.instance.AnimatorControllers[0];
        // -----------------------------
    }



    // stun
    public void StunCharacter()
    {
        stunCountdown = GameData.stunDuration;
        isStunned = true;
    }

    // countdown stun
    public void CountdownStun()
    {
        if (stunCountdown > 0)
        {
            stunCountdown--;

            Debug.Log(Combat.instance.round + ": " + characterName + " stunned for " + stunCountdown);

            if (stunCountdown == 0)
                isStunned = false;
        }
    }



    // deduct credits
    public void DeductCredits(int _amount)
    {
        credits -= _amount;

        string _report = "Credits Lost (<color=#ffc149>-" + _amount.ToString() + "</color>)";
        UI.instance.OpenSmallBox(_report);
    }

    // earn credits
    public void EarnCredits(int _amount)
    {
        credits += _amount;

        string _report = "Credits Gained (<color=#ffc149>+" + _amount.ToString() + "</color>)";
        UI.instance.OpenSmallBox(_report);
    }



    // kill
    public void KillCharacter(int _deathType)
    {
        // do not kill if already dead
        if (isDead)
            return;

        // animation
        if (GetComponent<CharacterAnimator>() != null)
            GetComponent<CharacterAnimator>().deathType = _deathType;

        isDead = true;
        unableToTurn = true;

        // npc actions
        if (this != PlayerScene.instance.MainCharacter)
        {
            // here for now (eventually take out)
            PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().NPCTarget = null;

            // xp
            if (giveXP && !playerControlledCombat)
                PlayerScene.instance.MainCharacter.GainExperience(GameData.killXP * level);

            // add to stats
            PlayerScene.instance.MainCharacter.executions++;

            // add loot
            if (GetComponent<MapItemContainer>() == null)
            {
                MapItemContainer _Container;
                _Container = this.gameObject.AddComponent<MapItemContainer>();
                _Container.SupplyContainer(this);
                this.gameObject.GetComponent<AudioCue>().AudioCueOnTrigger = AudioCues.Loot;
            }

            // turn off crime box
            if (GetComponent<TriggerCrime>() != null)
            {
                if (GetComponent<TriggerCrime>().CrimeBox != null)
                    GetComponent<TriggerCrime>().CrimeBox.gameObject.SetActive(false);
            }

            // trigger mission kill
            if (GetComponent<TriggerMissionKill>() != null && GetComponent<TriggerMissionKill>().Type != QuestTriggers.None)
                GetComponent<TriggerMissionKill>().TriggerResolve();
        }
        else
        {
            PlayerScene.instance.MainCharacter.GetComponent<Movement>().restrictMovement = true;
            Debug.Log("Game Over");
        }
    }
}