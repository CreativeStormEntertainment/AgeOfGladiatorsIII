using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour
{
    [Header("Outline")]
    public Outline Outliner;

    public bool mouseOver;



    void Start()
    {
        if (Outliner != null)
            Outliner.enabled = false;
    }



    // change cursor
    void ChangeCursor()
    {
        if (GetComponentInParent<NPC>() != null)
        {
            if (Combat.instance.combatActivated)
            {
                if (GetComponentInParent<NPC>().isHostile)
                    Cursor.SetCursor(Cursors.instance.AttackCursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                if (!GetComponentInParent<NPC>().isDead)
                    Cursor.SetCursor(Cursors.instance.SpeakCursor, Vector2.zero, CursorMode.Auto);
                else
                    Cursor.SetCursor(Cursors.instance.HandCursor, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            if (GetComponent<MovementNode>() != null)
            {
                if (!GetComponent<MovementNode>().disabled)
                    Cursor.SetCursor(Cursors.instance.MapCursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(Cursors.instance.HandCursor, Vector2.zero, CursorMode.Auto);
            } 
        }
    }



    // on mouse over
    private void OnMouseEnter()
    {
        // -----------------------
        // check if over ui
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        // -----------------------

        // -----------------------
        // check if player turn
        if (Combat.instance.combatActivated)
        {
            if (Combat.instance.Attacking != null && !Combat.instance.Attacking.playerControlledCombat)
                return;
        }
        // -----------------------
        
        // -----------------------
        // check if interactable
        if (GetComponentInParent<MapItem>() != null && !CheckIfCanInteract.CheckForInteractableConditions(GetComponentInParent<MapItem>()))
            return;

        if (GetComponentInParent<NPC>() != null && !CheckIfCanInteract.CheckForInteractableConditions(GetComponentInParent<NPC>()))
            return;

        if (GetComponent<MovementNode>() != null && GetComponent<MovementNode>().disabled)
            return;
        // -----------------------

        // -----------------------
        // enable outline
        if (Outliner != null)
            Outliner.enabled = true;
        // -----------------------

        // -----------------------
        // show nameplate
        if ((GetComponentInParent<NPC>() != null && !GetComponentInParent<NPC>().isDead) && GetComponent<NamePlateActivator>() != null)
            GetComponent<NamePlateActivator>().ActivateNamePlate(true);
        // -----------------------

        // sound
        UISounds.instance.PlayMouseOn(2);

        // -----------------------
        // change cursor
        ChangeCursor();
        // -----------------------

    }

    // on mouse exit
    private void OnMouseExit()
    {
        // -----------------------
        // disable outline
        if (Outliner != null)
            Outliner.enabled = false;
        // -----------------------

        // -----------------------
        // show nameplate
        if (GetComponentInParent<NPC>() != null && GetComponentInParent<NamePlateActivator>() != null)
            GetComponentInParent<NamePlateActivator>().DeactivateNamePlate();
        // -----------------------

        // -----------------------
        // change cursor
        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
        // -----------------------

        // sound
        //UISounds.instance.PlayMouseOff();

        mouseOver = false;
    }

    // on mouse over
    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        mouseOver = true;
    }
}
