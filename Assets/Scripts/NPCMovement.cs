using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPCMovement: Movement
{
    [HideInInspector]
    public NPC NonPlayerCharacter;

    [HideInInspector]
    public Quaternion StartRotation;
    [HideInInspector]
    public bool needsToTurnBack;



    void Start()
    {
        StartRotation = transform.rotation;

        NonPlayerCharacter = GetComponent<NPC>();

        // deactivate navmesh agent while standing still
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        // ignore collision with main character
        Physics.IgnoreCollision(PlayerScene.instance.MainCharacter.GetComponentInChildren<Collider>(), GetComponentInChildren<Collider>());

        StopMoving();
    }

    void Update()
    {
        // ------------------------------------------- 
        // return if npc is dead
        if (NonPlayerCharacter.isDead || !NonPlayerCharacter.gameObject.activeSelf)
            return;
        // ------------------------------------------- 

        // ------------------------------------------- 
        // turning
        if (needsToTurnBack && !Combat.instance.combatActivated)
            TurnTowardOriginalPosition();

        if (Combat.instance.combatActivated)
            LookAtCombatTarget();
        // -------------------------------------------

        // -------------------------------------------
        // moving    
        Moving();
        // -------------------------------------------
    }



    public void TurnTowardCharacter(Vector3 _turnTarget)
    {
        // do not turn if unable
        if (NonPlayerCharacter.unableToTurn)
            return;

        // turn toward player character
        var _lookPosition = _turnTarget - transform.position;
        _lookPosition.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookPosition), Time.deltaTime * 10);
    }

    public void TurnTowardOriginalPosition()
    {
        // turn back
        transform.rotation = Quaternion.Slerp(transform.rotation, StartRotation, Time.deltaTime * 5);

        // stop turning back
        if (transform.rotation == StartRotation)
            needsToTurnBack = false;
    }
}