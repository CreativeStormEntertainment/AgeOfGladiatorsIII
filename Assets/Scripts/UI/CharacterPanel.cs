using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    [Header("Portrait")]
    public Image Portrait;

    [Header("Labels")]
    public TextMeshProUGUI NameLabel;
    public LineIcon HealthLabel;
    public LineIcon AmmoLabel;
    public LineIcon ActionPointsLabel;
    public LineIcon ArmorLabel;
    public LineIcon DamageLabel;
    public TextMeshProUGUI LevelLabel;

    [Header("Status Icons")]
    public List<GameObject> StatusIcons;



    public void Populate(Character _Character)
    {
        Portrait.sprite = PortraitSelector.FindPortrait(_Character, 2);

        NameLabel.text = _Character.characterName;
        LevelLabel.text = _Character.level.ToString();

        HealthLabel.Label.text = _Character.combatHealth.ToString();
        ActionPointsLabel.Label.text = _Character.combatActionPoints.ToString("F0");
        ArmorLabel.Label.text = _Character.GetArmor().ToString();

        if (_Character.EquippedWeapon != null)
        {
            if (_Character.EquippedWeapon.WeaponDamageType == WeaponDamage.Melee)
                DamageLabel.Label.text = _Character.GetMeleeMinimumDamage() + "-" + _Character.GetMeleeMaximumDamage();
            else
                DamageLabel.Label.text = _Character.GetRangedMinimumDamage() + "-" + _Character.GetRangedMaximumDamage();

            if (_Character.EquippedWeapon.WeaponDamageType == WeaponDamage.Ranged)
            {
                AmmoLabel.gameObject.SetActive(true);
                AmmoLabel.Label.text = _Character.EquippedWeapon.ammo.ToString();
            }
            else
            {
                AmmoLabel.gameObject.SetActive(false);
            }  
        }
        else
        {
            DamageLabel.Label.text = _Character.GetUnarmedMinimumDamage() + "-" + _Character.GetUnarmedMaximumDamage();
            AmmoLabel.gameObject.SetActive(false);
        }

        PopulateStatusIcons(_Character);
    }

    void PopulateStatusIcons(Character _Character)
    {
        if (_Character.inCover)
            StatusIcons[0].gameObject.SetActive(true);
        else
            StatusIcons[0].gameObject.SetActive(false);

        if (_Character.isStunned)
            StatusIcons[1].gameObject.SetActive(true);
        else
            StatusIcons[1].gameObject.SetActive(false);

        if (Combat.instance.stunSelected)
            StatusIcons[2].gameObject.SetActive(true);
        else
            StatusIcons[2].gameObject.SetActive(false);

        if (Combat.instance.hitBonusSelected)
            StatusIcons[3].gameObject.SetActive(true);
        else
            StatusIcons[3].gameObject.SetActive(false);

        if (Combat.instance.criticalBonusSelected)
            StatusIcons[4].gameObject.SetActive(true);
        else
            StatusIcons[4].gameObject.SetActive(false);
    }
}
