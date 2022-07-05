using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatGridCollider : MonoBehaviour
{
    public static event Action<int,int,int, CombatGridCell> OnClick;
    private CombatGridCell cell;
    private List<GameObject> characters = new List<GameObject>();

    private void Awake()
    {
        cell = transform.parent.GetComponent<CombatGridCell>();
    }

    private void OnMouseDown()
    {
        if (Application.isPlaying && !EventSystem.current.IsPointerOverGameObject())
        {
            OnClick?.Invoke(cell.gridPosX, cell.gridPosY, cell.gridPosZ, cell);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Character")
        {
            characters.Add(col.gameObject);
            cell.occupied = true;
        } 
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Character")
        {
            characters.Remove(col.gameObject);

            if (characters.Count == 0)
                cell.occupied = false;
        }
    }

    private void OnMouseEnter()
    {
        if (Combat.instance.combatActivated && Combat.instance.Attacking != null && Combat.instance.Attacking.playerControlledCombat)
        {
            if (Combat.instance.Attacking.GetComponent<Movement>().isMoving || cell.occupied || !cell.accessible)
                return;

            cell.GetMovementAPCost(CombatGrid.GetCellAtPosition(Combat.instance.Attacking.transform.position));
            Combat.instance.highlighted.Add(cell.hoverHighlight);

            if (cell.canReach)
            {
                cell.hoverHighlight.SetActive(true);

                if (cell.FindAdjacentCellsCover())
                    cell.CoverIcon.gameObject.SetActive(true);
                else
                    cell.CoverIcon.gameObject.SetActive(false);
            }
        }  
    }

    private void OnMouseExit()
    {
        cell.hoverHighlight.SetActive(false);
        Combat.instance.highlighted.Remove(cell.hoverHighlight);
        cell.grid.APCost.rt.gameObject.SetActive(false);
        cell.grid.APCostLine.gameObject.SetActive(false);
        cell.CoverIcon.gameObject.SetActive(false);
    }
}
