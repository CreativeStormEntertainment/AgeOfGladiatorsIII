using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public CombatActions ActionType;
    public int cooldown;

    public bool stunAction;



    public Action(CombatActions _ActionType)
    {
        ActionType = _ActionType;

        AddSpecialConditions();
    }



    public void AddSpecialConditions()
    {
        switch (ActionType)
        {
            case CombatActions.Stun:
                stunAction = true;
                break;
        }
    }

    public void CooldownCountdown()
    {
        if (cooldown > 0)
            cooldown--;
    }



    // reload
    public bool CheckReload()
    {
        bool _available = true;

        // check weapon
        if (Combat.instance.Attacking.EquippedWeapon == null || Combat.instance.Attacking.EquippedWeapon.ammo == Combat.instance.Attacking.EquippedWeapon.GetMaximumAmmo() || Combat.instance.Attacking.EquippedWeapon.WeaponDamageType == WeaponDamage.Melee)
            _available = false;

        // check if action points
        if (!Combat.instance.CheckIfEnoughActionPoints(GameData.actionPointsReload) && _available)
            _available = false;

        return _available;
    }

    public IEnumerator ReloadAnimation()
    {
        // report
        Combat.instance.Attacking.StartCoroutine(Combat.instance.Attacking.GetComponent<ActionTextActivator>().ReportOther("Reloading"));

        // turn on attack animation state and wait to finish
        Combat.instance.Attacking.GetComponent<CharacterAnimator>().SetAllAnimators("isReloading", true);
        Combat.instance.waitingForActionToFinish = true;

        // wait until proper clip is triggered (otherwise it gets clip duration of idle for whatever reason)
        while (!Combat.instance.Attacking.GetComponent<CharacterAnimator>().AttachedAnimator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            yield return null;

        // sound 
        CombatSounds.instance.PlayReload();

        // wait until animation is complete
        yield return new WaitForSeconds(Combat.instance.Attacking.GetComponent<CharacterAnimator>().GetClipDuration() / Combat.instance.speedDivisor);

        // switch off attack animation state
        Combat.instance.Attacking.GetComponent<CharacterAnimator>().SetAllAnimators("isReloading", false);

        // reload
       Reload();
    }

    void Reload()
    {
        // reload
        Combat.instance.Attacking.combatActionPoints -= GameData.actionPointsReload;
        Combat.instance.Attacking.EquippedWeapon.Reload();

        // human refresh
        Combat.instance.Refresh();

        // action finished
        Combat.instance.waitingForActionToFinish = false;
    }

    public IEnumerator ReloadAnimationOutOfCombat()
    {
        // report
        PlayerScene.instance.MainCharacter.StartCoroutine(PlayerScene.instance.MainCharacter.GetComponent<ActionTextActivator>().ReportOther("Reloading"));

        // turn on attack animation state and wait to finish
        PlayerScene.instance.MainCharacter.GetComponent<CharacterAnimator>().SetAllAnimators("isReloading", true);

        //// wait until proper clip is triggered (otherwise it gets clip duration of idle for whatever reason)
        while (!PlayerScene.instance.MainCharacter.GetComponent<CharacterAnimator>().AttachedAnimator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            yield return null;

        // sound 
        CombatSounds.instance.PlayReload();

        // wait until animation is complete
        yield return new WaitForSeconds(PlayerScene.instance.MainCharacter.GetComponent<CharacterAnimator>().GetClipDuration());

        // switch off attack animation state
        PlayerScene.instance.MainCharacter.GetComponent<CharacterAnimator>().SetAllAnimators("isReloading", false);

        // reload
        ReloadOutCombat();
    }

    void ReloadOutCombat()
    {
        // reload
        PlayerScene.instance.MainCharacter.EquippedWeapon.Reload();
    }



    // heal
    public bool CheckHeal()
    {
        bool _available = true;

        // check cooldown
        if (cooldown != 0)
            _available = false;

        // check health
        if (Combat.instance.Attacking.combatHealth >= Combat.instance.Attacking.GetHealth() && _available)
            _available = false;

        // check if action points
        if (!Combat.instance.CheckIfEnoughActionPoints(GameData.healCost) && _available)
            _available = false;

        return _available;
    }

    public IEnumerator HealAnimation()
    {
        CombatSounds.instance.PlayHeal();

        Combat.instance.waitingForActionToFinish = true;

        yield return new WaitForSeconds(0.5f);

        Heal();
    }

    void Heal()
    {
        // report
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Attacking.characterName + " (" + Combat.instance.Attacking.combatActionPoints + ") Healing");
        Combat.instance.Attacking.StartCoroutine(Combat.instance.Attacking.GetComponent<ActionTextActivator>().ReportOther("Healing"));

        // heal
        Combat.instance.Attacking.combatActionPoints -= GameData.healCost;
        Combat.instance.Attacking.combatHealth += GameData.healAmount;

        // cooldown
        cooldown = 3;

        // human refresh
        Combat.instance.Refresh();

        // action finished
        Combat.instance.waitingForActionToFinish = false;
    }



    // stun
    public bool CheckStun()
    {
        bool _available = true;

        // check cooldown
        if (cooldown != 0)
            _available = false;

        // check if action points
        if (!Combat.instance.CheckIfEnoughActionPoints(GameData.stunCost) && _available)
            _available = false;

        // check ammo
        if (Combat.instance.Attacking.EquippedWeapon.WeaponDamageType == WeaponDamage.Ranged && Combat.instance.Attacking.EquippedWeapon.ammo == 0)
            _available = false;

        return _available;
    }

    public void ActivateStun()
    {
        CombatSounds.instance.PlayReload();

        Combat.instance.stunSelected = !Combat.instance.stunSelected;

        if (Combat.instance.stunSelected)
            Combat.instance.Attacking.StartCoroutine(Combat.instance.Attacking.GetComponent<ActionTextActivator>().ReportOther("Concussion Round Activated"));
        else
            Combat.instance.Attacking.StartCoroutine(Combat.instance.Attacking.GetComponent<ActionTextActivator>().ReportOther("Concussion Round Deactivated"));
    }

    public void Stun()
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Defending.characterName + " Stunned");

        // deduct ammo
        if (Combat.instance.Attacking.EquippedWeapon != null)
            Combat.instance.Attacking.EquippedWeapon.DeductAmmo();

        // stun
        Combat.instance.Attacking.combatActionPoints -= GameData.actionPointsReload;
        Combat.instance.Defending.StunCharacter();
        Combat.instance.Defending.StartCoroutine(Combat.instance.Defending.GetComponent<ActionTextActivator>().ReportOther("Stunned"));

        // cooldown
        cooldown = 3;

        // remove stun selected
        Combat.instance.stunSelected = false;

        // human refresh
        Combat.instance.Refresh();

        // action finished
        Combat.instance.waitingForActionToFinish = false;
    }



    // hit bonus
    public bool CheckHitBonus()
    {
        bool _available = true;

        // check cooldown
        if (cooldown != 0)
            _available = false;

        // check if action points
        if (!Combat.instance.CheckIfEnoughActionPoints(GameData.hitChanceCost + Combat.instance.Attacking.GetActionPointAttackCost()) && _available)
            _available = false;

        // check ammo
        if (Combat.instance.Attacking.EquippedWeapon.WeaponDamageType == WeaponDamage.Ranged && Combat.instance.Attacking.EquippedWeapon.ammo == 0)
            _available = false;

        return _available;
    }

    public void ActivateHitBonus()
    {
        CombatSounds.instance.PlayReload();

        Combat.instance.Attacking.combatActionPoints -= GameData.hitChanceCost;
        Combat.instance.hitBonusSelected = true;

        cooldown = 3;

        Combat.instance.Attacking.StartCoroutine(Combat.instance.Attacking.GetComponent<ActionTextActivator>().ReportOther("Hit Bonus"));

        Combat.instance.Refresh();
    }



    // critical bonus
    public bool CheckCriticalBonus()
    {
        bool _available = true;

        // check cooldown
        if (cooldown != 0)
            _available = false;

        // check if action points
        if (!Combat.instance.CheckIfEnoughActionPoints(GameData.criticalChanceCost + Combat.instance.Attacking.GetActionPointAttackCost()) && _available)
            _available = false;

        // check ammo
        if (Combat.instance.Attacking.EquippedWeapon.WeaponDamageType == WeaponDamage.Ranged && Combat.instance.Attacking.EquippedWeapon.ammo == 0)
            _available = false;

        return _available;
    }

    public void ActivateCriticalBonus()
    {
        CombatSounds.instance.PlayReload();

        Combat.instance.Attacking.combatActionPoints -= GameData.criticalChanceCost;
        Combat.instance.criticalBonusSelected = true;

        cooldown = 3;

        Combat.instance.Attacking.StartCoroutine(Combat.instance.Attacking.GetComponent<ActionTextActivator>().ReportOther("Critical Bonus"));

        Combat.instance.Refresh();
    }
}
