using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Combat : MonoBehaviour
{
    public static Combat instance;

    public event System.Action CombatInitialized;

    [HideInInspector]
    public bool combatActivated;

    [HideInInspector]
    public List<Character> CombatList = new List<Character>();
    [HideInInspector]
    Map CombatMap;

    [HideInInspector]
    public int round;

    [HideInInspector]
    public Character Attacking;
    [HideInInspector]
    public Character Defending;

    [HideInInspector]
    public int attackerIndex;

    [HideInInspector]
    public List<GameObject> highlighted = new List<GameObject>();
    [HideInInspector]
    public int movedToGrid;

    public bool waitingForActionToFinish;

    public Action SelectedAction;
    public bool stunSelected;
    public bool hitBonusSelected;
    public bool criticalBonusSelected;

    public float speedDivisor = 1.5f;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }



    // start combat time
    public IEnumerator StartCombatCoroutine()
    {
        GameMusicCues.instance.PlayMusicCue(MusicCues.CombatIn);

        PrepareCombat();

        // wait for character list to be populated
        while (CombatList.Count == 0) 
            yield return null;

        yield return new WaitForSeconds(1f);

        // wait while moving to grid
        //while (movedToGrid != CombatList.Count)
        //    yield return null;

        StartCombat();
    }

    // prepare combat
    public void PrepareCombat()
    {
        CombatInitialized?.Invoke();
        combatActivated = true;

        UI.instance.CombatUI.ShowCombatUI();

        AssignMap();
        
        AssignPlayerPartyToList();
        ActivateCombatGrid();
        AddAllEnemiesToList();
        SwitchAnimators();
        ShowWeapons();
        SortByInitiative();
        SetCombatStats();
        AddActions();
    }

    // start combat
    public void StartCombat()
    {
        round = 0;
        Round();

        waitingForActionToFinish = false;
    }

    // end combat
    public void EndCombat()
    {
        GameMusicCues.instance.PlayMusicCue(MusicCues.CombatOut);

        combatActivated = false;
        
        RemoveHostile();

        SwitchAnimators();
        HideWeapons();

        DeactivateCombatGrid();

        PlayerScene.instance.MainCharacter.GetComponent<Movement>().StopMoving();
        PlayerScene.instance.MainCharacter.GetComponent<Movement>().Agent.enabled = true;

        UI.instance.CombatUI.HideCombatUI();

        if (!CheckIfPlayerPartyLiving())
            UI.instance.StartEndGameTimer();

        CombatList.Clear();
    }

    // check end combat
    public bool CheckEndCombat()
    {
        bool _end = false;

        if (!CheckIfPlayerPartyLiving())
            _end = true;

        if (!CheckIfEnemyPartyLiving())
            _end = true;

        return _end;
    }



    // new round
   void Round()
    {
        round++;

        // action cooldown
        CooldownActions();

        // reset attacker index
        attackerIndex = 0;

        // reset action and movement points
        foreach (Character _C in CombatList)
            _C.ResetCombatStatsEachRound();

        // added this 
        if (!CheckEndCombat())
            Turn();
        else
            EndCombat();
    }

    // turn
    void Turn()
    {
        ClearHighlight();

        // populate initiative panel
        UI.instance.CombatUI.InitiativePanel.Populate();

        // set next attacker
        if (!CombatList[attackerIndex].isDead)
        {
            Attacking = CombatList[attackerIndex];

            // countdowns
            Attacking.CountdownStun();

            // restrict movement of all except attacker
            foreach (Character _C in CombatList)
                _C.GetComponent<Movement>().restrictMovement = true;

            Attacking.GetComponent<Movement>().restrictMovement = false;

            // update combat ui
            UI.instance.CombatUI.UpdateCombatPanel();

            // player or npc action
            if (Attacking.playerControlledCombat)
                PlayerAction();
            else
                NPCAction();
        }
        else
        {
            StartCoroutine(EndTurnTimer());
        }
    }

    // player actions
     void PlayerAction()
    {
        HighlightMovableCells();

        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);

        UI.instance.CombatUI.UpdateCombatPanel();

        if (Attacking.isStunned)
            StartCoroutine(StunnedAnimation());
    }

    // npc actions
     void NPCAction()
    {
        UI.instance.CombatUI.HideActionsPanel();

        Attacking.GetComponent<NPCCombatController>().hasMoved = false;

        if (Attacking.isStunned)
            StartCoroutine(StunnedAnimation());
        else
            Attacking.GetComponent<NPCCombatController>().StartActions();
    }

    // check end human turn
    public  void CheckEndHumanTurn()
    {
        UI.instance.CombatUI.UpdateCombatPanel();

        if (Attacking.playerControlledCombat && Attacking.combatActionPoints < 1)
            StartCoroutine(EndTurnTimer());
    }

    // end turn timer
    public IEnumerator EndTurnTimer()
    {
        //Attacking.StartCoroutine(Attacking.GetComponent<ActionTextActivator>().ReportOther("Ending Turn"));

        while (waitingForActionToFinish)
            yield return null;

        yield return new WaitForSeconds(0.5f);

        EndTurn();
    }

    // end turn
    public  void EndTurn()
    {
        ResetActionBools();
        
        Defending = null;

        attackerIndex++;

        Debug.Log(round + ": " + Attacking.characterName + " Ending Turn");
            
        // new turn or new round
        if (attackerIndex >= CombatList.Count)
            Round();
        else
            Turn();
    }



    // use action
    public void ActionUse(CombatActions _ActionType)
    {
        // ---------------------------------
        // locate action
        SelectedAction = null;

        foreach (Action _Action in Attacking.Actions)
        {
            if (_Action.ActionType == _ActionType)
                SelectedAction = _Action;
        }
        // ---------------------------------

        // ---------------------------------
        // trigger action
        switch (SelectedAction.ActionType)
        {
            case CombatActions.AttackRegular:
                // not activated here
                break;
            case CombatActions.Heal:
                ResetActionBools();
                if (SelectedAction.CheckHeal())
                    StartCoroutine(SelectedAction.HealAnimation());
                break;
            case CombatActions.Reload:
                ResetActionBools();
                if (SelectedAction.CheckReload())
                    StartCoroutine(SelectedAction.ReloadAnimation());
                break;
            case CombatActions.Stun:
                SelectedAction.ActivateStun();
                break;
            case CombatActions.HitBonus:
                SelectedAction.ActivateHitBonus();
                break;
            case CombatActions.CriticalBonus:
                SelectedAction.ActivateCriticalBonus();
                break;
            case CombatActions.EndTurn:
                StartCoroutine(EndTurnTimer());
                UISounds.instance.PlaySmallButton();
                break;
        }
        // ---------------------------------

        // update portrait panel
        UI.instance.CombatUI.UpdateCombatPanel();
    }

    // reset action bools
    void ResetActionBools()
    {
        stunSelected = false;
        hitBonusSelected = false;
        criticalBonusSelected = false;
    }



    // attack animation
    public IEnumerator AttackAnimation()
    {
        // turn on attack animation state and wait to finish
        Attacking.GetComponent<CharacterAnimator>().SetAllAnimators("isAttacking", true);
        waitingForActionToFinish = true;

        // speech blurb
        //Attacking.GetComponentInParent<SpeechBubbleActivator>().SpeechBlurb(SpeechBubbleTriggerTypes.CannotTalk, 0);

        // wait until proper clip is triggered (otherwise it gets clip duration of idle for whatever reason)
        while (!Attacking.GetComponent<CharacterAnimator>().AttachedAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            yield return null;

        // grunt sound
        if (Attacking.CombatStyle() == WeaponTypes.Blade1H || Attacking.CombatStyle() == WeaponTypes.Blade2H || Attacking.CombatStyle() == WeaponTypes.Blunt1H || Attacking.CombatStyle() == WeaponTypes.Blunt2H || Attacking.CombatStyle() == WeaponTypes.Unarmed)
            CombatSounds.instance.PlayGrunt(Attacking.Species, Attacking.male);

        // attack sound
        CombatSounds.instance.PlayAttack(Attacking.CombatStyle());

        // muzzle flash
        if (Attacking.CombatStyle() == WeaponTypes.Pistol || Attacking.CombatStyle() == WeaponTypes.Rifle || Attacking.CombatStyle() == WeaponTypes.Shotgun || Attacking.CombatStyle() == WeaponTypes.HeavyWeapon)
            Attacking.GetComponentInChildren<BodyManager>().Pistol.GetComponent<MuzzleFlash>().ActivateMuzzleFlash();

        // wait until animation is complete
        yield return new WaitForSeconds(Attacking.GetComponent<CharacterAnimator>().GetClipDuration() / speedDivisor);

        // switch off attack animation state
        Attacking.GetComponent<CharacterAnimator>().SetAllAnimators("isAttacking", false);

        // attack target
        if (stunSelected)
            SelectedAction.Stun();
        else
            AttackTarget();
    }

    // attack
    public void AttackTarget()
    {
        // this here for now due to null error (NEED TO FIGURE OUT WHY THIS IS AND REMOVE)
        if (Defending == null)
            Debug.Log("---------------------------------------------------------- DEFENDING NULL -----------------------------------------------------------------");

        Debug.Log(round + ": " + Attacking.characterName + " (" + Attacking.combatActionPoints + ") Attacking " + Defending.characterName);

        // deduct action points
        Attacking.combatActionPoints -= Attacking.GetActionPointAttackCost();

        // deduct ammo
        if (Attacking.EquippedWeapon != null)
            Attacking.EquippedWeapon.DeductAmmo();

        // damage animation or end attack
        if (RollAttack())
            DamageTarget();
        else
            StartCoroutine(MissAnimation());
    }

    // roll attack
    public bool RollAttack()
    {
        bool _hit = false;

        // --------------------------
        // base hit chance
        int _attack = Attacking.GetHitChance();
        // --------------------------

        // --------------------------
        // roll attack
        int _roll = (Random.Range(0, 100));

        // deduct cover penalty from attack chance
        if (Defending.inCover)
            _attack -= GameData.coverPenalty;

        // determine if attack hits
        if (_attack >= _roll)
            _hit = true;
        // --------------------------

        // --------------------------
        // calculate evasion (separated from above for reporting) (no evasion if stunned)
        bool _evaded = false;
        if (_hit && !Attacking.isStunned && _attack < (_roll + Defending.GetEvasionChance()))
        {
            Defending.StartCoroutine(Defending.GetComponent<ActionTextActivator>().ReportEvasion());

            _hit = false;
            _evaded = true;
        }
        // --------------------------

        // --------------------------
        // report miss (only if not evaded)
        if (!_hit && !_evaded)
            Defending.StartCoroutine(Defending.GetComponent<ActionTextActivator>().ReportMiss());
        // --------------------------    

        // -------------------------- 
        // sound
        if (!_hit)
            CombatSounds.instance.PlayMiss(Attacking.CombatStyle());
        // -------------------------- 

        return _hit;
    }



    // damage target
    void DamageTarget()
    {
        // --------------------------
        // calculate damage
        float _damage = Attacking.CalculateAttackDamage();
        // --------------------------

        // --------------------------
        // armor vs penetration
        if (Defending.GetArmor() > Attacking.GetPenetration())
            _damage -= (Defending.GetArmor() - Attacking.GetPenetration());

        if (_damage <= 0)
            _damage = 1;
        // --------------------------

        // --------------------------
        // critical damage
        _damage *= RollCritical();
        // --------------------------

        // --------------------------
        // deal damage
        Defending.combatHealth -= (int)_damage;
        Mathf.Clamp(Defending.combatHealth, 0, Defending.combatHealth);
        Defending.StartCoroutine(Defending.GetComponent<ActionTextActivator>().ReportDamage((int)_damage));
        // --------------------------

        // --------------------------
        // kill unit
        if (Defending.combatHealth <= 0)
            KillTarget();
        else
            StartCoroutine(DamageAnimation());
        // --------------------------
    }

    // roll critical
    float RollCritical()
    {
        float _damageIncrease = 1;

        int _chance = Attacking.GetCriticalChance();

        if (criticalBonusSelected)
        {
            _chance += GameData.criticalChanceBonus;
            criticalBonusSelected = false;
        }    

        if (_chance >= Random.Range(0, 101))
        {
            _damageIncrease *= Attacking.GetCriticalDamage();
            Defending.StartCoroutine(Defending.GetComponent<ActionTextActivator>().ReportCriticalDamage());
        }

        return _damageIncrease;
    }

    // kill target
    public void KillTarget()
    {
        Debug.Log(round + ": " + Attacking.characterName + " (" + Attacking.combatActionPoints + ") Killed " + Defending.characterName);

        // temporarily turn off outline (otherwise blood is outlined)
        if (Defending.GetComponent<Outline>() != null)
            Defending.GetComponent<Outline>().enabled = false;

        // sound
        CombatSounds.instance.PlayDamage(Attacking.CombatStyle());
        CombatSounds.instance.PlayDeath(Defending.Species, Defending.male);

        // blood splatter
        if (Defending.GetComponentInChildren<BloodActivator>() != null)
            Defending.GetComponentInChildren<BloodActivator>().ActivateKillBloodSplatter();

        // kill character
        Defending.KillCharacter(0);

        // end combat or end attack
        if (CheckEndCombat())
        {
            EndCombat();
            return;
        }
        else
        {
            EndAttack();
        }
    }

    // damage animation
    public IEnumerator DamageAnimation()
    {
        // turn on attack animation state and wait to finish
        Defending.GetComponent<CharacterAnimator>().SetAllAnimators("isHit", true);
        waitingForActionToFinish = true;

        // sound
        CombatSounds.instance.PlayDamage(Attacking.CombatStyle());
        CombatSounds.instance.PlayPain(Defending.Species, Defending.male);

        // wait until attack clip is triggered (otherwise it gets clip duration of idle for whatever reason)
        while (!Defending.GetComponent<CharacterAnimator>().AttachedAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            yield return null;

        // temporarily turn off outline (otherwise blood is outlined)
        if (Defending.GetComponent<Outline>() != null)
            Defending.GetComponent<Outline>().enabled = false;

        // blood splatter
        Defending.GetComponent<CharacterAnimator>().ActivateBloodSplatter(Attacking);

        // switch off attack animation state
        Defending.GetComponent<CharacterAnimator>().SetAllAnimators("isHit", false);

        // wait until animation is complete
        yield return new WaitForSeconds(Defending.GetComponent<CharacterAnimator>().GetClipDuration());

        // end attack
        EndAttack();
    }

    // miss animation
    public IEnumerator MissAnimation()
    {
        // wait to finish
        waitingForActionToFinish = true;

        // wait until animation is complete
        yield return new WaitForSeconds(1f);

        // end attack
        EndAttack();
    }

    // end attack
    public void EndAttack()
    {
        // refresh
        waitingForActionToFinish = false;

        // player or npc
        if (Attacking.playerControlledCombat)
            Refresh();
        else
            Attacking.GetComponent<NPCCombatController>().StartActions();
    }



    // stunned animation
    public IEnumerator StunnedAnimation()
    {
        // wait to finish
        waitingForActionToFinish = true;

        // report
        Attacking.StartCoroutine(Attacking.GetComponent<ActionTextActivator>().ReportOther("Stunned"));

        // wait
        yield return new WaitForSeconds(0.5f);

        // wait to finish
        waitingForActionToFinish = false;

        // end end turn
        StartCoroutine(EndTurnTimer());
    }



    // check player can attack
    public  bool CheckCanAttackDefender()
    {
        bool _canAttack = true;

        // conditions only if defender selected
        if (Defending == null)
        {
            _canAttack = false;
        }
        else
        {
            // check ammo
            if (Attacking.EquippedWeapon != null && !Attacking.EquippedWeapon.CheckIfAmmo())
            {
                _canAttack = false;

                if (Attacking.playerControlledCombat)
                {
                    UISounds.instance.PlayActionNotPossible();
                    Attacking.StartCoroutine(Attacking.GetComponent<ActionTextActivator>().ReportOther("Out Of Ammo"));
                }  
            }

            // check action points
            if (!CheckIfEnoughActionPoints(Attacking.GetActionPointAttackCost()) && _canAttack)
            {
                _canAttack = false;

                if (Attacking.playerControlledCombat)
                {
                    UISounds.instance.PlayActionNotPossible();
                    Attacking.StartCoroutine(Attacking.GetComponent<ActionTextActivator>().ReportOther("Not Enough Action Points"));
                }  
            }

            // check in range (only for player)
            if (Attacking.playerControlledCombat && !CheckDefenderInRange() && _canAttack)
            {
                _canAttack = false;

                if (Attacking.playerControlledCombat)
                {
                    UISounds.instance.PlayActionNotPossible();
                    Attacking.StartCoroutine(Attacking.GetComponent<ActionTextActivator>().ReportOther("Out Of Range"));
                }   
            }

            //// check if too close (ranged)
            //if ((Attacking.CombatStyle() == WeaponTypes.Pistol || Attacking.CombatStyle() == WeaponTypes.Shotgun || Attacking.CombatStyle() == WeaponTypes.Rifle || Attacking.CombatStyle() == WeaponTypes.HeavyWeapon) && _canAttack)
            //{
            //    CombatGridCell _start = CombatGrid.GetCellAtPosition(Attacking.transform.position);
            //    CombatGridCell _target = CombatGrid.GetCellAtPosition(Defending.transform.position);

            //    float _dist = _start.grid.CalcDistanceCost(_start, _target) / 10f;

            //    if (_dist < 2f)
            //    {
            //        _canAttack = false;
            //        UISounds.instance.PlayActionNotPossible();
            //        Attacking.StartCoroutine(Attacking.GetComponent<ActionTextActivator>().ReportOther("Too Close!"));
            //    }  
            //}
        }

        return _canAttack;
    }

    // check if defender in range
    public  bool CheckDefenderInRange()
    {
        bool _inRange = false;

        // -----------------------------
        // default range for unarmed
        float _attackRange = 1.4f;

        // range if armed with weapon
        if (Attacking.EquippedWeapon != null)
            _attackRange = Attacking.EquippedWeapon.GetRange();
        // -----------------------------

        // -----------------------------
        // calculate defending unit spot
        CombatGridCell _start = CombatGrid.GetCellAtPosition(Attacking.transform.position);
        CombatGridCell _target = CombatGrid.GetCellAtPosition(Defending.transform.position);

        float _dist = _start.grid.CalcDistanceCost(_start, _target) / 10f;

        //Debug.Log(_attackRange + " " + _dist);

        if (_dist <= _attackRange) // already in shooting range
            _inRange = true;
        // -----------------------------

        return _inRange;
    }

    // check if enough action points
    public  bool CheckIfEnoughActionPoints(int _ap)
    {
        bool _enough = true;

        if (Attacking.combatActionPoints < _ap)
            _enough = false;

        return _enough;
    }

    // check if beside cover
    public void CheckIfBesideCover()
    {
        if (Attacking != null)
        {
            Attacking.inCover = false;

            if (CombatGrid.CheckForCover(CombatGrid.GetCellAtPosition(Attacking.transform.position)))
            {
                Attacking.inCover = true;
                Attacking.StartCoroutine(Attacking.GetComponent<ActionTextActivator>().ReportOther("Cover!"));
            }
        }
    }



    // assign map
     void AssignMap()
    {
        CombatMap = null;

        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject _object in allObjects)
        {
            if (_object.activeInHierarchy)
            {
                if (_object.GetComponent<Map>() != null)
                    CombatMap = _object.GetComponent<Map>();
            }
        }
    }

    // activate combat grid
     void ActivateCombatGrid()
    {
        //foreach (Transform child in CombatMap.AttachedGrid.transform)
        //{
        //    CombatGridCell _Cell = child.GetComponent<CombatGridCell>();
        //    _Cell.cellBorder.SetActive(true);
        //} 

        CombatMap.MovementNodesParent.SetActive(false);

        foreach (Character _char in CombatList)
        {
            if (_char.playerControlledCombat)
            {
                PlayerCharacter _player = (PlayerCharacter)_char;
                _player.ToggleOccupied(true);
            }  
        }
    }

    // deactivate combat grid
     void DeactivateCombatGrid()
    {
        //foreach (Transform child in CombatMap.AttachedGrid.transform)
        //{
        //    CombatGridCell _Cell = child.GetComponent<CombatGridCell>();
        //    _Cell.cellBorder.SetActive(false);
        //}

        ClearHighlight();
        CombatMap.MovementNodesParent.SetActive(true);

        foreach (Character _char in CombatList)
        {
            if (_char.playerControlledCombat)
            {
                PlayerCharacter _player = (PlayerCharacter)_char;
                _player.ToggleOccupied(false);
            }
        }
    }

    // add player and companions to list
     void AssignPlayerPartyToList()
    {
        CombatList.Add(PlayerScene.instance.MainCharacter);
    }

    // add enemies to list
     void AddAllEnemiesToList()
    {
        foreach (Transform child in CombatMap.MapNPCS.transform)
        {
            if (child.GetComponent<NPC>().isHostile)
                CombatList.Add(child.GetComponent<NPC>()); // change to hostile after   
        }
    }



    // show weapons
    void ShowWeapons()
    {
        foreach (Character _Character in CombatList)
            _Character.GetComponentInChildren<BodyManager>().ShowWeapon();
    }

    // hide weapons
    void HideWeapons()
    {
        foreach (Character _Character in CombatList)
        {
            if (!_Character.isDead)
                _Character.GetComponentInChildren<BodyManager>().HideWeapon();
        }   
    }

    // switch combatant animators to combat
    void SwitchAnimators()
    {
        foreach (Character _Character in CombatList)
        {
            if (!_Character.isDead)
                _Character.GetComponent<CharacterAnimator>().SwitchAnimator();
        }  
    }

    // add actions
    void AddActions()
    {
        foreach (Character _Character in CombatList)
        {
            _Character.Actions.Clear();

            _Character.Actions.Add(new Action(CombatActions.AttackRegular));
            _Character.Actions.Add(new Action(CombatActions.Stun));
            _Character.Actions.Add(new Action(CombatActions.Heal));
            _Character.Actions.Add(new Action(CombatActions.Reload));
            _Character.Actions.Add(new Action(CombatActions.HitBonus));
            _Character.Actions.Add(new Action(CombatActions.CriticalBonus));

            _Character.Actions.Add(new Action(CombatActions.EndTurn));
        }
    }

    // cooldown actions
    void CooldownActions()
    {
        foreach (Character _Character in CombatList)
        {
            foreach (Action _Action in _Character.Actions)
                _Action.CooldownCountdown();
        }
    }

    // sort list
     void SortByInitiative()
    {
        CombatList.Sort((y, x) => x.GetInitiative().CompareTo(y.GetInitiative()));
    }

    // set stats
     void SetCombatStats()
    {
        foreach (Character _Character in CombatList)
        {
            _Character.combatHealth = _Character.GetHealth();
            Debug.Log(_Character.characterName + ": " + _Character.combatHealth);
            _Character.combatMovementPoints = _Character.GetMovementPoints();
            _Character.combatActionPoints = _Character.GetActionPoints();
        }
    }

    // check if player party still living
    public  bool CheckIfPlayerPartyLiving()
    {
        if (PlayerScene.instance.MainCharacter.isDead)
            return false;
        else
            return true;
    }

    // check if enemy party still living
    public  bool CheckIfEnemyPartyLiving()
    {
        if (CombatList.Count(element => !element.isDead && !element.playerControlledCombat) == 0)
            return false;
        else
            return true;
    }

    // remove hostile
     void RemoveHostile()
    {
        foreach (Character _Character in CombatList)
        {
            if (_Character.gameObject.GetComponent<NPC>() != false)
                _Character.gameObject.GetComponent<NPC>().isHostile = false;
        }
    }



    // refresh
    public void Refresh()
    {
        if (Attacking.playerControlledCombat)
        {
            ClearHighlight();
            HighlightMovableCells();
            CheckEndHumanTurn();
        }
    }

    // highlight moveable cells
    public  void HighlightMovableCells()
    {
        if (!combatActivated)
            return;

        if (Attacking == null || Attacking.combatActionPoints <= 0)
            return;

        // default range for unarmed
        float _attackRange = 1.4f + Attacking.combatActionPoints;

        // range if armed with weapon
        if (Attacking.EquippedWeapon != null)
            _attackRange = Attacking.EquippedWeapon.GetRange() + Attacking.combatActionPoints;
        
        CombatGridCell _startingCell = CombatGrid.GetCellAtPosition(Attacking.transform.position);
        //_startingCell.grid.DrawOutlines(_startingCell, _attackRange, false);
        _startingCell.grid.DrawOutlines(_startingCell, Attacking.combatActionPoints, true);
    }

    // clear highlighted cells
    public  void ClearHighlight()
    {
        foreach (GameObject obj in highlighted)
        {
            obj.SetActive(false);
        }
        highlighted.Clear();

        CombatGrid.ClearOutlines();

        CombatGridCell _Cell = CombatGrid.GetCellAtPosition(PlayerScene.instance.MainCharacter.transform.position);
        _Cell.grid.ClearAP();
    }
}
