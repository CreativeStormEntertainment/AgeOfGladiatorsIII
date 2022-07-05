using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [HideInInspector]
    public Vector3 Target;
    [HideInInspector]
    public float speed;

    [HideInInspector]
    public bool isMoving;
    [HideInInspector]
    public bool restrictMovement;
    [HideInInspector]
    public bool inTransitBetweenMaps;

    //[Header("Grid Movement")]
    [HideInInspector]
    public List<Vector3> path = new List<Vector3>();
    [HideInInspector]
    public int curPathIndex;
    [HideInInspector]
    public Vector3 pathOffsetY = new Vector3(0, 0.5f, 0);

    [Header("NavMesh Agent")]
    public NavMeshAgent Agent;

    [HideInInspector]
    public Vector3 LookCombatTarget;

    [HideInInspector]
    public bool combatMovingToGrid;



    private void OnEnable()
    {
        Combat.instance.CombatInitialized += CombatStartMoveToGrid; // move to grid at start
    }

    private void OnDisable()
    {
        Combat.instance.CombatInitialized -= CombatStartMoveToGrid;  // move to grid at start
    }



    private void Update()
    {
        if (Combat.instance.combatActivated)
            speed = 5f;
        else
            speed = 3f;
    }



    // moving
    public void Moving()
    {
        // movement mode
        if (Combat.instance.combatActivated)
            CombatMovement();
        else
            NonCombatMovement();
    }

    // combat movement
    public void CombatMovement()
    {
        if (!isMoving)
            return;

        Agent.enabled = false;

        speed = 5f;

        if (Vector3.Distance(Target, transform.position) <= 0.15f && path.Count != 0 && curPathIndex != path.Count - 1)
        {
            curPathIndex++;
            Target = path[curPathIndex];
        }

        if (Vector3.Distance(Target, transform.position) >= 0.05f)
        {
            Vector3 _Adjusted = new Vector3(Target.x, this.transform.position.y, Target.z); // lock y axis for now

            LookAtTarget(Target);
            transform.position = Vector3.MoveTowards(transform.position, _Adjusted, (speed * Time.deltaTime));
            isMoving = true;
        }
        else
        {
            StopMoving();

            // check automatically end turn
            if (Combat.instance.combatActivated && Combat.instance.Attacking != null && Combat.instance.Attacking.playerControlledCombat)
                Combat.instance.CheckEndHumanTurn();
        }
    }

    // non-combat movement
    public void NonCombatMovement()
    {
        // ------------------------------------
        // move toward target (using NavMesh Agent)
        if (!inTransitBetweenMaps)
        {
            if (gameObject.GetComponent<NavMeshAgent>().enabled)
            {
                speed = 3f;
                Agent.speed = speed;
                Agent.SetDestination(Target);
                isMoving = true;
            }
        }
        else
        {
            StopMoving();
        }
        // ------------------------------------

        // ------------------------------------
        // stop if close to movement target
        float _distance = 0.5f;

        if (Vector3.Distance(Target, transform.position) <= _distance)
            StopMoving();
        // ------------------------------------
    }



    // stop moving
    public void StopMoving()
    {
        // --------------------------------
        // stop moving
        isMoving = false;

        Target = transform.position;

        GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
        GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;
        // --------------------------------

        // --------------------------------
        // player character
        if (GetComponent<PlayerCharacter>() != null && MouseClickCircle.instance.MouseIcon != null)
        {
            MouseClickCircle.instance.MouseIcon.gameObject.SetActive(false);
            if (Combat.instance.combatActivated)
                Combat.instance.HighlightMovableCells();
        }

        // --------------------------------

        // --------------------------------
        // check cover
        if (GetComponent<PlayerCharacter>() != null && Combat.instance.combatActivated)
            Combat.instance.CheckIfBesideCover();
        // --------------------------------
    }

    // look at target
    public void LookAtTarget(Vector3 _target)
    {
        var _lookPosition = _target - transform.position;
        _lookPosition.y = 0;

        if (_lookPosition != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookPosition), Time.deltaTime * 10);
    }

    // look at combat target
    public void LookAtCombatTarget()
    {
        if (Combat.instance.Attacking == GetComponent<Character>() && Combat.instance.Defending != null)
        {
            var _lookPosition = Combat.instance.Defending.transform.position - transform.position;
            _lookPosition.y = 0;

            if (_lookPosition != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookPosition), Time.deltaTime * 10);
        } 
    }

    // clear path
    public void ClearPath()
    {
        path.Clear();
        curPathIndex = 0;
    }



    // move to grid (combat start)
    public void CombatStartMoveToGrid()
    {
        if (GetComponent<NPC>() != null && GetComponent<NPC>().isHostile || GetComponent<PlayerCharacter>() != null)
        {
            combatMovingToGrid = true;
            StartCoroutine(MoveToGrid());
        }
    }

    // move to grid (combat start)
    IEnumerator MoveToGrid()
    {
        isMoving = true;
        Target = CombatGrid.GetCellAtPosition(transform.position).boxCol.bounds.center;
        LookAtTarget(Target);

        speed = 5f;

        while (Vector3.Distance(Target, transform.position) >= 0.05f)
        {
            Vector3 _Adjusted = new Vector3(Target.x, this.transform.position.y, Target.z); // lock y axis for now
            transform.position = Vector3.MoveTowards(transform.position, _Adjusted, (speed * Time.deltaTime));
            yield return null;
        }

        combatMovingToGrid = false;
        Combat.instance.movedToGrid++;
        //Debug.Log(GetComponent<Character>().characterName + " " + Combat.instance.movedToGrid);
        StopMoving();

        if (CombatGrid.CheckForCover(CombatGrid.GetCellAtPosition(transform.position)))
        {
            GetComponent<Character>().inCover = true;
            GetComponent<Character>().StartCoroutine(GetComponent<Character>().GetComponent<ActionTextActivator>().ReportOther("Cover!"));
        }
    }
}
