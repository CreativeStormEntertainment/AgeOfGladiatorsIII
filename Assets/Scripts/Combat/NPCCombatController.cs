using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCCombatController : MonoBehaviour
{
    NPC NPCAttached;

    NPCMovement NPCMovementAttached;

    [HideInInspector]
    public bool hasMoved;



    private void Start()
    {
        NPCAttached = GetComponent<NPC>();
        NPCMovementAttached = GetComponent<NPCMovement>();
    }



    // combat actions
    public void StartActions()
    {
        StartCoroutine(ActionsCoroutine());
    }

    // combat action timer
    public IEnumerator ActionsCoroutine()
    {
        // wait for action to end
        while (Combat.instance.waitingForActionToFinish)
            yield return null;

        // select target and move
        if (!hasMoved) // for now, to prevent moving again after attacking
        {
            SelectTarget();
            MoveToTarget();
        }

        // wait to stop moving
        while (NPCMovementAttached.isMoving)
            yield return null;

        // check again waiting for action to finish
        while (Combat.instance.waitingForActionToFinish)
            yield return null;

        // attack or other action
        if (Combat.instance.CheckCanAttackDefender())
            StartCoroutine(Combat.instance.AttackAnimation());
        else
            CheckOtherActions();
    }

    // select target
    void SelectTarget()
    {
        // -------------------------------------------
        // reset
        Combat.instance.Defending = null;
        // -------------------------------------------

        // -------------------------------------------
        // create list and sort by nearest
        List<Character> _Nearest = new List<Character>();

        foreach (Character _Character in Combat.instance.CombatList)
            _Nearest.Add(_Character);

        _Nearest = _Nearest.OrderBy(x => Vector2.Distance(this.transform.position, x.transform.position)).ToList();
        // -------------------------------------------

        // -------------------------------------------
        // select nearest player controlled character
        while (Combat.instance.Defending == null)
        {
            foreach (Character _Character in _Nearest)
            {
                if (_Character.playerControlledCombat)
                    Combat.instance.Defending = _Character;
            }
        }
        // -------------------------------------------

        Debug.Log(Combat.instance.round + ": " + Combat.instance.Attacking.characterName + " is targeting " + Combat.instance.Defending.characterName);
    }

    // move to target
    void MoveToTarget()
    {
        if (NPCAttached.combatActionPoints < 1)
            return;

        // ---------------------------
        NPCMovementAttached.ClearPath();
        CombatGridCell _start = CombatGrid.GetCellAtPosition(transform.position);
        CombatGridCell _target = CombatGrid.GetCellAtPosition(Combat.instance.Defending.transform.position);
        List<Vector3> _path;
        // ---------------------------


        // ---------------------------
        // check attack range
        float _attackRange = 1.4f;
        float _dist = 0;

        if (NPCAttached.EquippedWeapon != null)
            _attackRange = NPCAttached.EquippedWeapon.GetRange();

        if (_attackRange == 1.4f) // melee
        {
            if (Combat.instance.CheckDefenderInRange()) // already in melee range
            {

            }
            else
            {
                _target = CombatGrid.GetNearestAvailableCell(_start, _target);
                _dist = _target.grid.CalcDistanceCost(_start, _target) / 10f;

                if (_dist /* + base melee atk cost */ <= NPCAttached.combatActionPoints) // move and melee target
                {
                    _path = _start.grid.GetPath(_start, _target, true, true);
                }
                else // move closer to target
                {
                    _target = CombatGrid.GetNearestCellToTarget(_start, _target, NPCAttached.combatActionPoints);
                    _path = _start.grid.GetPath(_start, _target, true, true);
                }
                NPCMovementAttached.path = _path;
            }
        }
        else // ranged
        {
            if (Combat.instance.CheckDefenderInRange()) // already in shooting range
            {

            }
            else
            {
                _target = CombatGrid.GetNearestCellInShootingRange(_start, _target, _attackRange);
                _dist = _target.grid.CalcDistanceCost(_start, _target) / 10f;

                if (_dist /* + base ranged atk cost */ <= NPCAttached.combatActionPoints) // move and shoot target
                {
                    _path = _start.grid.GetPath(_start, _target, true, true);
                }
                else // move closer to target
                {
                    _target = CombatGrid.GetNearestCellToTarget(_start, _target, NPCAttached.combatActionPoints);
                    _path = _start.grid.GetPath(_start, _target, true, true);
                }
                NPCMovementAttached.path = _path;
            }
        }
        // ---------------------------


        // ---------------------------
        // move to target
        if (NPCMovementAttached.path.Count != 0)
        {
            Debug.Log(Combat.instance.round + ": " + Combat.instance.Attacking.characterName + " (" + Combat.instance.Attacking.combatActionPoints + ") Moving!");

            NPCAttached.combatActionPoints -= NPCMovementAttached.path.Count;
            NPCMovementAttached.Target = NPCMovementAttached.path[0];
            NPCMovementAttached.isMoving = true;

            hasMoved = true;
        }
        // ---------------------------
    }



    // other actions
    public void CheckOtherActions()
    {

        // reload
        Combat.instance.ActionUse(CombatActions.Reload);

        // heal
        //Combat.instance.ActionUse(CombatActions.Heal);

        StartCoroutine(WaitForActionToEnd());
    }

    // waiting for actions to end
    public IEnumerator WaitForActionToEnd()
    {
        while (Combat.instance.waitingForActionToFinish)
            yield return null;

        yield return new WaitForSeconds(0.5f);

        CheckEndTurn();
    }

    // check end turn
    public void CheckEndTurn()
    {
        //// check again if can attack (after reloading, ap recharging, etc...)
        //if (!Actions.instance.CheckCanAttackDefender())
        //    Combat.instance.EndTurn();
        //else
        //    CombatActions();

        StartCoroutine(Combat.instance.EndTurnTimer());
    }



    // on mouse over (select as target)
    private void OnMouseOver()
    {
        if (!CheckValidNPCForAttack())
            return;

        Combat.instance.Defending = NPCAttached;

        if (Input.GetMouseButtonDown(0))
        {
            if (Combat.instance.CheckCanAttackDefender())
                StartCoroutine(Combat.instance.AttackAnimation());
        } 
    }

    // on mouse over (unselect as target)
    private void OnMouseExit()
    {
        if (!CheckValidNPCForAttack())
            return;

        Combat.instance.Defending = null;
    }

    // check if valid npc target
    bool CheckValidNPCForAttack()
    {
        if (!Combat.instance.combatActivated || NPCAttached.isDead || !Combat.instance.Attacking.playerControlledCombat || !NPCAttached.isHostile || Combat.instance.waitingForActionToFinish)
            return false;
        else
            return true;
    }
}
