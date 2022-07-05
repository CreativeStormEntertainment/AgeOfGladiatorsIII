using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombatActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image BackgroundImage;

    [Header("Action Type")]
    public CombatActions ActionType;



    // button press
    public void ButtonPress()
    {
        Combat.instance.ActionUse(ActionType);

        UpdateButton();
    }



    // update button status
    public void UpdateButton()
    {
        // stun toggle
        if (ActionType == CombatActions.Stun)
        {
            if (Combat.instance.stunSelected)
                BackgroundImage.sprite = ButtonList.instance.ButtonAction[1];
            else
                BackgroundImage.sprite = ButtonList.instance.ButtonAction[0];
        }

        // hit bonus toggle
        if (ActionType == CombatActions.HitBonus)
        {
            if (Combat.instance.hitBonusSelected)
                BackgroundImage.sprite = ButtonList.instance.ButtonAction[1];
            else
                BackgroundImage.sprite = ButtonList.instance.ButtonAction[0];
        }

        // critical bonus toggle
        if (ActionType == CombatActions.CriticalBonus)
        {
            if (Combat.instance.criticalBonusSelected)
                BackgroundImage.sprite = ButtonList.instance.ButtonAction[1];
            else
                BackgroundImage.sprite = ButtonList.instance.ButtonAction[0];
        }
    }

    // get description
    public string GetDescription()
    {
        string _description = "None";

        switch (ActionType)
        {
            case CombatActions.AttackRegular:
                break;
            case CombatActions.Heal:
                _description = "<color=#ffc149>Medic</color> \u25A0 Heals (<color=#ffc149>" + GameData.healAmount + "</color>) Hitpoints \u25A0 (<color=#ffc149>" + GameData.healCost + " AP</color>)";
                break;
            case CombatActions.Reload:
                _description = "<color=#ffc149>Reload</color> \u25A0 Reloads Ammunition";
                break;
            case CombatActions.Stun:
                _description = "<color=#ffc149>Concussive Round</color> \u25A0 Stuns Target For (<color=#ffc149>" + GameData.stunDuration + "</color>) Turns \u25A0 (<color=#ffc149>" + GameData.stunCost + " AP</color>)";
                break;
            case CombatActions.HitBonus:
                _description = "<color=#ffc149>Seeker</color> \u25A0 (<color=#ffc149>+" + GameData.hitPointBonus + "%</color>) Hit Point Bonus For Turn \u25A0 (<color=#ffc149>" + GameData.hitChanceCost+ " AP</color>)";
                break;
            case CombatActions.CriticalBonus:
                _description = "<color=#ffc149>Heart Shot</color> \u25A0 (<color=#ffc149>+" + GameData.criticalChanceBonus + "%</color>) Critical Chance \u25A0 Single Attack \u25A0 (<color=#ffc149>" + GameData.criticalChanceCost + " AP</color>)";
                break;
            case CombatActions.EndTurn:
                _description = "End Turn";
                break;
        }

        return _description;
    }



    // mouse over
    public void OnPointerEnter(PointerEventData eventData)
    {
        UISounds.instance.PlayMouseOn(0);

        UI.instance.CombatUI.ActionLabel.Label.text = GetDescription();
        UI.instance.CombatUI.ActionLabel.gameObject.SetActive(true);
    }

    // mouse exit
    public void OnPointerExit(PointerEventData eventData)
    {
        UI.instance.CombatUI.ActionLabel.gameObject.SetActive(false);
    }
}
