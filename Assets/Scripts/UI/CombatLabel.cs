using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatLabel : MonoBehaviour
{
    [Header("Character")]
    public Character AttachedCharacter;

    [Header("Labels")]
    public TextMeshProUGUI NameLabel;
    public TextMeshProUGUI ArmorLabel;
    public TextMeshProUGUI HitChanceLabel;
    public TextMeshProUGUI HitPointsLabel;

    [Header("Slider")]
    public Slider HealthSlider;

    [Header("Other")]
    public GameObject ArmorPanel;


    private void Update()
    {
        if (AttachedCharacter != null)
            HealthBar();
    }



    // populate
    public void Populate(Character _character, bool _combat, bool _mouseOver)
    {
        // main labels
        AttachedCharacter = _character;
        NameLabel.text = AttachedCharacter.characterName.ToUpper();

        // armor
        if (_combat)
            Armor();
        else
            ArmorPanel.gameObject.SetActive(false);

        // hit chance
        if (_combat && _mouseOver && Combat.instance.Attacking.playerControlledCombat)
            HitChance();
        else
            HitChanceLabel.gameObject.SetActive(false);

        // health bar
        HealthBar();
    }



    // health bar
    void HealthBar()
    {
        if (AttachedCharacter.isDead)
        {
            HitPointsLabel.text = 0 + "/" + AttachedCharacter.GetHealth();
            HealthSlider.value = 0;
            HealthSlider.maxValue = AttachedCharacter.GetHealth();
        }
        else
        {
            if (Combat.instance.combatActivated)
            {
                HitPointsLabel.text = AttachedCharacter.combatHealth + "/" + AttachedCharacter.GetHealth();
                HealthSlider.value = AttachedCharacter.combatHealth;
                HealthSlider.maxValue = AttachedCharacter.GetHealth();
            }
            else
            {
                HitPointsLabel.text = AttachedCharacter.GetHealth() + "/" + AttachedCharacter.GetHealth();
                HealthSlider.value = AttachedCharacter.GetHealth();
                HealthSlider.maxValue = AttachedCharacter.GetHealth();
            }
        }
    }

    // hit chance
    void HitChance()
    {
        HitChanceLabel.gameObject.SetActive(true);

        int _chance = Combat.instance.Attacking.GetHitChance();

        if (AttachedCharacter.inCover)
            _chance -= GameData.coverPenalty;

        HitChanceLabel.text = _chance + "%";
    }

    // armor
    void Armor()
    {
        ArmorPanel.gameObject.SetActive(true);
        ArmorLabel.text = AttachedCharacter.GetArmor().ToString();
    }
}
