using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.AI;
using PixelCrushers.DialogueSystem;

public class PlayerCharacterMovement : Movement
{
    [Header("Conditions - Player Character")]
    public bool movingToInteractableTarget;

    [Header("Targets")]
    public NPC NPCTarget;
    public MapItem ItemForInteraction;
    [Tooltip("Layers that can contain obstacles to movement")]
    public LayerMask layerMask;
    private PlayerCharacter PlayerCharacterAttached;

    public bool conversationTriggered;



    private void OnEnable()
    {
        CombatGridCollider.OnClick += GridMoveToTarget;
        Combat.instance.CombatInitialized += CombatStartMoveToGrid;  // move to grid at start
    }

    private void OnDisable()
    {
        CombatGridCollider.OnClick -= GridMoveToTarget;
        Combat.instance.CombatInitialized -= CombatStartMoveToGrid;  // move to grid at start
    }



    void Awake()
    {
        PlayerCharacterAttached = GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        if (Dredd.instance.isLoading)
            return;

        DetectMouseTargetItem();

        Moving();

        DetectCloseToInteractableTarget();

        if (Combat.instance.combatActivated)
            LookAtCombatTarget();
    }



    // detect mouse click item
    public void DetectMouseTargetItem()
    {
        // ------------------------------------------- 
        // restrict movement if window open
        if (restrictMovement && !combatMovingToGrid)
        {
            if (isMoving)
                StopMoving();

            return;
        }
        // ------------------------------------------- 

        // ------------------------------------------- 
        // detect mouse click - left
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            // reset
            ItemForInteraction = null;
            NPCTarget = null;
            movingToInteractableTarget = false;
            conversationTriggered = false;

            // detect click
            RaycastHit _hit;
            Ray _ray = PlayerScene.instance.SceneCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                // -------------------------------
                // detect click on npc or dialogue click (conversation)
                if (_hit.collider.GetComponentInParent<NPC>() != null && !_hit.collider.GetComponentInParent<NPC>().isHostile && !_hit.collider.GetComponentInParent<NPC>().isDead && !Combat.instance.combatActivated)
                {
                    if (CheckIfCanInteract.CheckForInteractableConditions(_hit.collider.GetComponentInParent<NPC>()))
                        TargetNPCForConversation(_hit.collider.GetComponentInParent<NPC>());
                }
                // -------------------------------

                // -------------------------------
                // detect click on npc (loot)
                if (_hit.collider.GetComponentInParent<NPC>() != null && _hit.collider.GetComponentInParent<NPC>().isDead && !Combat.instance.combatActivated)
                    TargetNPCForLoot(_hit.collider.GetComponentInParent<NPC>());
                // -------------------------------

                // -------------------------------
                // detect click on world item
                if (_hit.collider.GetComponentInParent<MapItem>() != null)
                {
                    if (CheckIfCanInteract.CheckForInteractableConditions(_hit.collider.GetComponentInParent<MapItem>()) && !Combat.instance.combatActivated)
                        TargetWorldItemForInteraction(_hit.collider.GetComponentInParent<MapItem>());
                }
                // -------------------------------
            }
        }
        // ------------------------------------------- 
    }

    // target npc (conversation)
    public void TargetNPCForConversation(NPC _npc)
    {
        if (_npc.cannotApproach)
            return;

        if (_npc.GetComponentInParent<TriggerSkillRoll>() != null && _npc.GetComponentInParent<TriggerSkillRoll>().itemLocked)
            return;

        UISounds.instance.PlaySmallButton();

        MouseClickCircle.instance.MouseIcon.gameObject.SetActive(false);

        NPCTarget = _npc;
        Target = _npc.transform.position;

        NPCTarget.alreadyToldCantTalk = false;

        movingToInteractableTarget = true;
        DetectCloseToInteractableTarget();
    }

    // target npc (loot)
    public void TargetNPCForLoot(NPC _npc)
    {
        if (_npc.cannotApproach)
            return;

        UISounds.instance.PlaySmallButton();

        MouseClickCircle.instance.MouseIcon.gameObject.SetActive(false);

        NPCTarget = _npc;
        Target = _npc.transform.position;

        movingToInteractableTarget = true;
        DetectCloseToInteractableTarget();
    }

    // target world item
    public void TargetWorldItemForInteraction(MapItem _item)
    {
        UISounds.instance.PlaySmallButton();

        MouseClickCircle.instance.MouseIcon.gameObject.SetActive(false);

        ItemForInteraction = _item;
        Target = _item.transform.position;

        movingToInteractableTarget = true;
        DetectCloseToInteractableTarget();
    }



    // detect if close to interactable target
    public void DetectCloseToInteractableTarget()
    {
        // check NPCs
        NPCInteraction();

        // check map
        MapItemInteraction();

        // movement node
        MovementNodeInteraction();
    }

    // npc interaction
    void NPCInteraction()
    {
        // -------------------------------------------------------
        // npc - speak
        if (NPCTarget != null && movingToInteractableTarget)
        {
            if (Vector3.Distance(NPCTarget.transform.position, transform.position) <= 3)
            {
                // ----------------------------
                // stop and turn when reaching
                if (NPCTarget.GetComponent<NPCMovement>() != null)
                    NPCTarget.GetComponent<NPCMovement>().TurnTowardCharacter(this.transform.position);

                StopMoving();
                LookAtTarget(NPCTarget.transform.position);
                // ----------------------------
                // ----------------------------
                // conversation or loot
                if (!NPCTarget.isDead)
                {
                    // do not keep spamming comments
                    if (NPCTarget.alreadyToldCantTalk)
                        return;

                    // quest tracker
                    if (NPCTarget.GetComponent<TriggerMissionClick>() != null)
                        NPCTarget.GetComponent<TriggerMissionClick>().UpdateQuest();

                    // only speak if eligible
                    if (DialogueManager.ConversationHasValidEntry(NPCTarget.DialogueTrigger.conversation) && !conversationTriggered)
                    {
                        conversationTriggered = true;
                        restrictMovement = true;

                        NPCTarget.DialogueTrigger.DoConversationAction(NPCTarget.DialogueTrigger.conversationConversant);
                    }
                    else
                    {
                        NPCTarget.alreadyToldCantTalk = true;

                        NPCTarget.DialogueTrigger.DoBarkAction(NPCTarget.DialogueTrigger.conversationConversant);
                    }
                }
                else
                {
                    NPCTarget.LootNPC();
                }
                // ----------------------------
            }
        }
        // -------------------------------------------------------
    }

    // map item interaction
    void MapItemInteraction()
    {
        // -------------------------------------------------------
        // world item
        if (ItemForInteraction != null && movingToInteractableTarget)
        {
            float _stopDistance = 2.5f;

            if (ItemForInteraction.GetComponentInParent<MapDoor>() != null)
                _stopDistance = 4f;

            if (Vector3.Distance(ItemForInteraction.transform.position, transform.position) <= _stopDistance)
            {
                // --------------------------
                movingToInteractableTarget = false;
                LookAtTarget(ItemForInteraction.transform.position);
                // --------------------------

                // --------------------------
                // interact with item if unlocked
                if (ItemForInteraction.GetComponentInParent<TriggerSkillRoll>() != null && ItemForInteraction.GetComponent<TriggerSkillRoll>().itemLocked)
                {
                    StopMoving();
                    ItemForInteraction.GetComponentInParent<TriggerSkillRoll>().AttemptItemSkillRoll();
                }
                else
                {
                    // --------------------------
                    // item dialogue trigger
                    if (ItemForInteraction.GetComponentInParent<TriggerActions>() != null && ItemForInteraction.GetComponentInParent<MovementNode>() == null)
                    {
                        ItemForInteraction.GetComponentInParent<TriggerActions>().OnUse();
                        StopMoving();
                    }

                    // container
                    if (ItemForInteraction.GetComponentInParent<MapItemContainer>() != null)
                        UI.instance.OpenLoot(ItemForInteraction.GetComponentInParent<MapItemContainer>());

                    // crafting station
                    if (ItemForInteraction.GetComponentInParent<CraftingStation>() != null)
                        UI.instance.OpenCraft();

                    // crime terminal
                    if (ItemForInteraction.GetComponentInParent<CrimeTerminal>() != null)
                        UI.instance.OpenCrimeTerminal();

                    // viewable item
                    if (ItemForInteraction.GetComponentInParent<ViewItemActivator>() != null)
                    {
                        ItemForInteraction.GetComponentInParent<ViewItemActivator>().ViewBlurb();
                        StopMoving();
                    }

                    // door
                    if (ItemForInteraction.GetComponentInParent<MapDoor>() != null)
                    {
                        ItemForInteraction.GetComponentInParent<MapDoor>().OpenDoor();
                        StopMoving();
                    }
                    // --------------------------
                }
                // --------------------------
            }
        }
        // -------------------------------------------------------
    }

    // movement node interaction
    void MovementNodeInteraction()
    {
        // -------------------------------------------------------
        // movement node
        if (ItemForInteraction != null && !movingToInteractableTarget)
        {
            float _detectDistance = 0.5f;

            if (Vector3.Distance(ItemForInteraction.transform.position, transform.position) <= _detectDistance)
            {
                // -------------------------------------------------
                // on movement node
                if (ItemForInteraction.GetComponent<MovementNode>() != null && !UI.instance.TravelScreen.gameObject.activeSelf)
                {
                    // travel to destination or open travel map
                    if (ItemForInteraction.GetComponent<MovementNode>().destinationScene != "Travel-Map")
                    {
                        MovementNode _node = ItemForInteraction.GetComponent<MovementNode>();
                        _node.ActivateNode(false);
                    }
                    else
                    {
                        SceneSwitch.travelMapNode = ItemForInteraction.GetComponent<MovementNode>().destinationNode;

                        MovementNode _node = ItemForInteraction.GetComponent<MovementNode>();
                        _node.ActivateNode(true);

                        UI.instance.OpenTravel();
                    }

                    MouseClickCircle.instance.MouseIcon.gameObject.SetActive(false);
                    StopMoving();
                }
                // -------------------------------------------------
            }
        }
        // -------------------------------------------------------
    }



    // draw mouse circle on destination
    public void DrawMouseCircleDestination(Vector3 _Input)
    {
        MouseClickCircle.instance.MouseIcon.gameObject.SetActive(true);

        if (Combat.instance.combatActivated)
            MouseClickCircle.instance.transform.position = new Vector3(_Input.x, _Input.y + 0.02f, _Input.z);
        else
            MouseClickCircle.instance.transform.position = new Vector3(Target.x, Target.y + 0.02f, Target.z);
    }

    // select grid cell target
    private void GridMoveToTarget(int x, int y, int z, CombatGridCell _targetCell)
    {
        if (restrictMovement || EventSystem.current.IsPointerOverGameObject())
            return;

        if (Combat.instance.combatActivated && _targetCell.gCost != 0) // combat
        {
            ClearPath();
            var _start = CombatGrid.GetCellAtPosition(transform.position);

            if (!_targetCell.accessible || _targetCell.occupied || _start == null)
                return;

            Vector3 _targetCenter = _targetCell.boxCol.bounds.center;
            List<Vector3> _path = _start.grid.GetPath(_start, _targetCell, true, true);

            if (_path != null)
            {
                if (_targetCell.gCost / 10f <= PlayerCharacterAttached.combatActionPoints)
                {
                    Combat.instance.ClearHighlight();
                    path = _path;
                    Target = path[curPathIndex];
                    PlayerCharacterAttached.combatActionPoints -= _targetCell.gCost / 10f;
                    DrawMouseCircleDestination(_targetCenter);
                }
            }
        }
        else // non-combat
        {
            RaycastHit _hit;
            Ray _ray = PlayerScene.instance.SceneCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                Vector3 hitPosition = _hit.point;
                Target = hitPosition;
                DrawMouseCircleDestination(hitPosition);
            }

        }

        isMoving = true;
    }
}
