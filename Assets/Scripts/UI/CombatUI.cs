using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CombatUI : MonoBehaviour
{
    [Header("Buttons")]
    public List<CombatActionButton> ActionButtons = new List<CombatActionButton>();

    [Header("Panels")]
    public GameObject ActionsPanel;
    public ActionDescriptionLabel ActionLabel;
    public CombatInitiativePanel InitiativePanel;
    public CharacterPanel AttackingPanel;



    private void Update()
    {
        // end turn
        if (Input.GetKeyDown(KeyCode.Space) && Combat.instance.combatActivated && Combat.instance.Attacking.playerControlledCombat && !Combat.instance.waitingForActionToFinish)
            StartCoroutine(Combat.instance.EndTurnTimer());

        // reload
        if (Input.GetKeyDown(KeyCode.R) && Combat.instance.combatActivated && Combat.instance.Attacking.playerControlledCombat)
        {
            Combat.instance.ActionUse(CombatActions.Reload);

            if (Combat.instance.Attacking.combatActionPoints < GameData.actionPointsReload)
                Combat.instance.Attacking.StartCoroutine(Combat.instance.Attacking.GetComponent<ActionTextActivator>().ReportOther("Not Enough Action Points"));
        }

        // clear special attack
        if (Input.GetMouseButtonDown(1))
        {
            Combat.instance.stunSelected = false;
        }  
    }



    public void UpdateCombatPanel()
    {
        if (Combat.instance.Attacking.playerControlledCombat)
            ShowActionsPanel();
        else
            HideActionsPanel();

        AttackingPanel.Populate(Combat.instance.Attacking);
        InitiativePanel.Populate();

        ActionLabel.gameObject.SetActive(false);

        foreach (CombatActionButton _Button in ActionButtons)
            _Button.UpdateButton();
    }

    public void ShowCombatUI()
    {
        //Debug.Log(GameMusic.Audio.clip.name);

        UI.instance.FadeScreen.gameObject.SetActive(true);
        UI.instance.FadeScreen.StartCoroutine(UI.instance.FadeScreen.ActivateFade());

        if (GameMusic.instance.AmbientMusic.Contains(GameMusic.Audio.clip))
            GameMusic.instance.TransitionToCombatMusic(1f);

        ActionLabel.gameObject.SetActive(false);

        this.gameObject.SetActive(true);
    }

    public void HideCombatUI()
    {
        UI.instance.FadeInOutScreen.gameObject.SetActive(true);
        UI.instance.FadeInOutScreen.StartCoroutine(UI.instance.FadeInOutScreen.ActivateFadeInOut());

        GameMusic.instance.TransitionToAmbient(1f);

        ActionLabel.gameObject.SetActive(false);

        this.gameObject.SetActive(false);
    }



    public void ShowActionsPanel()
    {
        // show panel
        UI.instance.CombatUI.ActionsPanel.gameObject.SetActive(true);

        // action buttons
        foreach (CombatActionButton _ActionButton in ActionButtons)
        {
            if (_ActionButton.ActionType == CombatActions.None)
                continue;

            // interactable by default
            _ActionButton.GetComponent<Button>().interactable = true;

            // assign action
            Action _Action = Combat.instance.Attacking.Actions.Where(obj => obj.ActionType == _ActionButton.ActionType).SingleOrDefault();

            // check action specific
            switch (_ActionButton.ActionType)
            {
                case CombatActions.AttackRegular:
                    break;
                case CombatActions.Stun:
                    if (!_Action.CheckStun())
                        _ActionButton.GetComponent<Button>().interactable = false;
                    break;
                case CombatActions.Heal:
                    if (!_Action.CheckHeal())
                        _ActionButton.GetComponent<Button>().interactable = false;
                    break;
                case CombatActions.Reload:
                    if (!_Action.CheckReload())
                        _ActionButton.GetComponent<Button>().interactable = false;
                    break;
                case CombatActions.HitBonus:
                    if (!_Action.CheckHitBonus())
                        _ActionButton.GetComponent<Button>().interactable = false;
                    break;
                case CombatActions.CriticalBonus:
                    if (!_Action.CheckCriticalBonus())
                        _ActionButton.GetComponent<Button>().interactable = false;
                    break;
            }
        }
    }

    public void HideActionsPanel()
    {
        // hide panel
        UI.instance.CombatUI.ActionsPanel.gameObject.SetActive(false);
    }
}
