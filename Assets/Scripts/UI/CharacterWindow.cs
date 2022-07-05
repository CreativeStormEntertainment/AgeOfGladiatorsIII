using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterWindow : MonoBehaviour
{
    [HideInInspector]
    public PlayerCharacter SelectedCharacter;

    [Header("Character Portrait")]
    public RawImage CharacterPortrait;
    public Image CharacterPortraitSmall;

    [Header("Labels")]
    public TextMeshProUGUI NameLabel;
    public TextMeshProUGUI RankLabel;
    public TextMeshProUGUI RankPanelLabel;
    public TextMeshProUGUI LevelLabel;
    public GameObject LevelPanel;
    public GameObject RankPanel;
    public TextMeshProUGUI SkillPointsLabel;
    public TextMeshProUGUI AttributePointsLabel;
    public TextMeshProUGUI ExperienceLabel;

    [Header("Buttons")]
    public Button AttributesButton;
    public Button PerksButton;
    public Button ReputationButton;

    [Header("Sections")]
    public GameObject AttributesSection;
    public GameObject PerksSection;
    public GameObject ReputationSection;

    [Header("Parents")]
    public GameObject AttributesParent;
    public GameObject CombatStatsParent;
    public GameObject CombatParent;
    public GameObject MentalParent;
    public GameObject TechnicalParent;

    [Header("Secondary Stats")]
    public IconLabelBox ArrestsBox;
    public IconLabelBox FinesBox;
    public IconLabelBox ExecutionsBox;

    [Header("Slider")]
    public Slider ExperienceSlider;

    [Header("Combat Stat Boxes")]
    public List<IconLabelBox> CombatStatBoxes = new List<IconLabelBox>();



    // populate
    public void Populate()
    {
        SelectedCharacter = PlayerScene.instance.MainCharacter;

        ShowBody();

        ToggleSection(0);
    }



    // populate info
    public void PopulateInfo()
    {
        NameLabel.text = SelectedCharacter.characterName;

        RankLabel.text = SelectedCharacter.GetRankLabel();

        LevelLabel.text = SelectedCharacter.level.ToString();
        LevelLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Level:</color> Current Level of character.");

        RankPanelLabel.text = "1".ToString();
        RankPanelLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Rank:</color> Current Rank of character.");

        ExperienceSlider.value = SelectedCharacter.experiencePoints;
        ExperienceSlider.maxValue = SelectedCharacter.experiencePointsNextLevel;

        ExperienceLabel.text = "<color=#ffc149>" + SelectedCharacter.experiencePoints + "</color>/" + SelectedCharacter.experiencePointsNextLevel;
        ExperienceLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Experience Points:</color> Experience Points required to reach next level.");

        CharacterPortraitSmall.sprite = PortraitSelector.FindPortrait(PlayerScene.instance.MainCharacter, 1);

        ArrestsBox.Label.text = SelectedCharacter.arrests.ToString();
        ArrestsBox.Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Arrests:</color> Amount of criminals you have arrested.");

        FinesBox.Label.text = SelectedCharacter.fines.ToString();
        FinesBox.Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Fines:</color> Amount of criminals you have fined.");

        ExecutionsBox.Label.text = SelectedCharacter.executions.ToString();
        ExecutionsBox.Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Executions:</color> Amount of criminals you have executed.");
    }

    // populate combat stats
    public void PopulateCombatStats()
    {
        // --------------------------------
        // populate list
        CombatStatBoxes.Clear();

        foreach (Transform child in CombatStatsParent.transform)
        {
            IconLabelBox _box = child.gameObject.GetComponent<IconLabelBox>();
            CombatStatBoxes.Add(_box);
        }
        // --------------------------------

        // --------------------------------
        // stats
        CombatStatBoxes[0].Label.text = SelectedCharacter.GetInitiative().ToString();
        CombatStatBoxes[0].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Initiative:</color> Determines turn order during combat \nBase Attribute: <color=#ffc149>Vision</color>");

        CombatStatBoxes[1].Label.text = SelectedCharacter.GetHealth().ToString();
        CombatStatBoxes[1].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Health:</color> Hitpoints during combat \nBase Attribute: <color=#ffc149>Strength</color>");

        CombatStatBoxes[2].Label.text = SelectedCharacter.GetArmor().ToString();
        CombatStatBoxes[2].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Armor:</color> Incoming Damage is reduced by Armor value \nBase Attribute: <color=#ffc149>None</color>");

        CombatStatBoxes[3].Label.text = SelectedCharacter.GetActionPoints().ToString();
        CombatStatBoxes[3].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Action Points:</color> Determines movement range and action uses during combat \nBase Attribute: <color=#ffc149>Speed</color>");

        CombatStatBoxes[4].Label.text = SelectedCharacter.GetMovementPoints().ToString();
        CombatStatBoxes[4].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Movement Points:</color> Determines movement range and action uses during combat \nBase Attribute: <color=#ffc149>Speed</color>");

        CombatStatBoxes[5].Label.text = SelectedCharacter.GetHitChance().ToString() + "%";
        CombatStatBoxes[5].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Hit Chance:</color> Chance of hitting a target during combat \nBase Attribute: <color=#ffc149>Coordination</color>");

        CombatStatBoxes[6].Label.text = SelectedCharacter.GetMeleeDamageBonus().ToString();
        CombatStatBoxes[6].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Melee Damage Bonus:</color> Bonus to Melee Damage \nBase Attribute: <color=#ffc149>Strength</color>");

        CombatStatBoxes[7].Label.text = SelectedCharacter.GetRangedDamageBonus().ToString();
        CombatStatBoxes[7].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Ranged Damage Bonus:</color> Bonus to Ranged Damage \nBase Attribute: <color=#ffc149>Vision</color>");

        CombatStatBoxes[8].Label.text = SelectedCharacter.GetCriticalChance().ToString() + "%";
        CombatStatBoxes[8].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Critical Chance:</color> Bonus to Melee Damage \nBase Attribute: <color=#ffc149>Aggression</color>");

        CombatStatBoxes[9].Label.text = SelectedCharacter.GetCriticalDamage().ToString("F1") + "x";
        CombatStatBoxes[9].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Critical Damage:</color> Multiplier to base damage if a successful critical attack is rolled \nBase Attribute: <color=#ffc149>Aggression</color>");

        CombatStatBoxes[10].Label.text = SelectedCharacter.GetEvasionChance().ToString() + "%";
        CombatStatBoxes[10].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Evasion Chance:</color> Chance of successfully evading an incoming attack \nBase Attribute: <color=#ffc149>Speed</color>");

        CombatStatBoxes[11].Label.text = SelectedCharacter.GetPenetration().ToString();
        CombatStatBoxes[11].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Armor Penetration:</color> Determines the amount of target Armor that an attack will bypass \nBase Attribute: <color=#ffc149>Coordination</color>");
        // --------------------------------
    }

    // populate attributes
    public void PopulateAttributes()
    {
        // --------------------------------
        // attribute points
        AttributePointsLabel.text = SelectedCharacter.attributePoints.ToString();
        AttributePointsLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Attribute Points:</color> Amount available to spend on Attributes.");
        // --------------------------------

        // --------------------------------
        // populate list
        foreach (Transform child in AttributesParent.transform)
        {
            AttributeBox _box = child.gameObject.GetComponent<AttributeBox>();
            _box.Populate(SelectedCharacter, false);
        }
        // --------------------------------
    }

    // populate skills
    public void PopulateSkills()
    {
        // --------------------------------
        // skill points
        SkillPointsLabel.text = SelectedCharacter.skillPoints.ToString();
        SkillPointsLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Skill Points:</color> Amount available to spend on Skills.");
        // --------------------------------

        // --------------------------------
        // populate list
        foreach (Transform child in CombatParent.transform)
        {
            AttributeBox _box = child.gameObject.GetComponent<AttributeBox>();
            _box.Populate(SelectedCharacter, false);
        }

        foreach (Transform child in MentalParent.transform)
        {
            AttributeBox _box = child.gameObject.GetComponent<AttributeBox>();
            _box.Populate(SelectedCharacter, false);
        }

        foreach (Transform child in TechnicalParent.transform)
        {
            AttributeBox _box = child.gameObject.GetComponent<AttributeBox>();
            _box.Populate(SelectedCharacter, false);
        }
        // --------------------------------
    }



    // populate portrait panel
    public void PopulatePortraitPanel()
    {
        Character3D.instance.PopulateModel(true, SelectedCharacter);
    }



    // show head
    public void ShowHead()
    {
        Character3D.instance.ShowHead();
    }

    // show body
    public void ShowBody()
    {
        Character3D.instance.ShowBody();
    }



    // toggle
    public void ToggleSection(int _input)
    {
        ResetAll();

        switch (_input)
        {
            case 0:
                ActivateAttributes();
                break;
            case 1:
                ActivatePerks();
                break;
            case 2:
                ActivateReputation();
                break;
        }

        CheckButtons();
    }

    // reset all
    void ResetAll()
    {
        AttributesSection.gameObject.SetActive(false);
        PerksSection.gameObject.SetActive(false);
        ReputationSection.gameObject.SetActive(false);
    }

    // check buttons
    void CheckButtons()
    {
        AttributesButton.interactable = true;
        PerksButton.interactable = true;
        ReputationButton.interactable = true;

        if (AttributesSection.activeSelf)
            AttributesButton.interactable = false;

        if (PerksSection.activeSelf)
            PerksButton.interactable = false;

        if (ReputationSection.activeSelf)
            ReputationButton.interactable = false;
    }

    // activate attributes
    void ActivateAttributes()
    {
        PopulatePortraitPanel();
        PopulateInfo();
        PopulateCombatStats();
        PopulateAttributes();
        PopulateSkills();

        AttributesSection.gameObject.SetActive(true);
    }

    // activate perks
    void ActivatePerks()
    {
        PerksSection.gameObject.SetActive(true);
    }

    // activate reputation
    void ActivateReputation()
    {
        ReputationSection.gameObject.SetActive(true);
    }
}
