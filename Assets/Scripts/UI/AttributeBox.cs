using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributeBox : MonoBehaviour
{
    [HideInInspector]
    public PlayerCharacter SelectedCharacter;

    [Header("Attribute Box Type")]
    public AttributeBoxType Type;

    [Header("Skill Type")]
    public Skill SkillType;
    [Header("Attribute Type")]
    public Attribute AttributeType;

    [Header("Buttons Parent")]
    public GameObject ButtonParent;
    [Header("Buttons List")]
    public List<Button> ButtonList = new List<Button>();

    [Header("Label")]
    public TextMeshProUGUI Label;
    public Image Icon;

    public bool newCharacterScreen;



    public void Populate(PlayerCharacter _Character, bool _newCharacter)
    {
        // ---------------------------------
        SelectedCharacter = _Character;

        newCharacterScreen = _newCharacter;
        // ---------------------------------

        // ---------------------------------
        // create list
        ButtonList.Clear();

        foreach (Transform child in ButtonParent.transform)
            ButtonList.Add(child.gameObject.GetComponent<Button>());
        // ---------------------------------

        // ---------------------------------
        // populate by attribute or skill
        if (Type == AttributeBoxType.Attribute)
            PopulateAttribute();
        else
            PopulateSkill();
        // ---------------------------------

        // ---------------------------------
        // populate tooltip
        PopulateTooltip();
        // ---------------------------------
    }

    void PopulateAttribute()
    {
        // ---------------------------------
        // label
        Label.text = AttributeType.ToString() + " (" + SelectedCharacter.GetAttribute(AttributeType) + ")";
        // ---------------------------------

        // ---------------------------------
        // configure buttons
        int _level = SelectedCharacter.GetAttribute(AttributeType);

        foreach (Button _button in ButtonList)
        {
            _button.interactable = true;
            _button.gameObject.SetActive(true);

            // ---------------------------------
            // only interactable if proper level
            if (ButtonList.IndexOf(_button) < _level)
                _button.interactable = false;

            if (ButtonList.IndexOf(_button) > _level)
                //_button.interactable = false;
                _button.gameObject.SetActive(false);
            // ---------------------------------

            // ---------------------------------
            // only interactable if enough attribute points
            if (SelectedCharacter.attributePoints == 0)
                _button.interactable = false;
            // ---------------------------------

            // ---------------------------------
            // tool tip - only show if on correct button
            if (_button.IsInteractable())
            {
                _button.GetComponent<ModelShark.TooltipTrigger>().doNotOpen = false;
                _button.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", "Attribute Point Cost (<color=#ffc149>1</color>)");
            }
            else
            {
                _button.GetComponent<ModelShark.TooltipTrigger>().doNotOpen = true;
            }
            // ---------------------------------
        }
        // ---------------------------------
    }

    void PopulateSkill()
    {
        // ---------------------------------
        // label
        Label.text = SkillType.ToString() + " (" + SelectedCharacter.GetSkill(SkillType) + ")";
        // ---------------------------------

        // ---------------------------------
        // configure buttons
        int _level = SelectedCharacter.GetSkill(SkillType);

        foreach (Button _button in ButtonList)
        {
            _button.interactable = true;
            _button.gameObject.SetActive(true);

            // ---------------------------------
            // only interactable if proper level
            if (ButtonList.IndexOf(_button) < _level)
                _button.interactable = false;

            if (ButtonList.IndexOf(_button) > _level)
                //_button.interactable = false;
                _button.gameObject.SetActive(false);
            // ---------------------------------

            // ---------------------------------
            // only interactable if enough skillpoints
            if ((SelectedCharacter.CalculateSkillPointCost(SkillType) + 1) > SelectedCharacter.skillPoints)
                _button.interactable = false;
            // ---------------------------------

            // ---------------------------------
            // tool tip - only show if on correct button
            if (_button.IsInteractable())
            {
                _button.GetComponent<ModelShark.TooltipTrigger>().doNotOpen = false;
                _button.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", "Skill Point Cost (<color=#ffc149>" + (SelectedCharacter.CalculateSkillPointCost(SkillType) + 1) + "</color>)");
            }
            else
            {
                _button.GetComponent<ModelShark.TooltipTrigger>().doNotOpen = true;
            }
            // ---------------------------------
        }
        // ---------------------------------
    }



    public void ButtonOver()
    {
        // ---------------------------------------
        // select correct list
        List<IconLabelBox> _List;

        if (newCharacterScreen)
            _List = StartScreen.instance.NewGameScreen.CombatStatBoxes;
        else
            _List = UI.instance.CharacterScreen.CombatStatBoxes;

        // rollover by type
        if (Type == AttributeBoxType.Attribute)
            PopulateCombatStatsRollover.Rollover(AttributeType, _List);
        else
            PopulateCombatStatsRollover.Rollover(SkillType, _List);
        // ---------------------------------------
    }

    public void ButtonExit()
    {
        RefreshCombatStatBoxes();
    }



    public void ButtonPress()
    {
        if (Type == AttributeBoxType.Attribute)
        {
            SelectedCharacter.IncreaseAttribute(AttributeType);
            RefreshAttributes();
        }
        else
        {
            SelectedCharacter.IncreaseSkill(SkillType);
            RefreshSkills();
        }

        RefreshCombatStatBoxes();
    }

    public void RefreshAttributes()
    {
        if (newCharacterScreen)
        {
            StartScreen.instance.NewGameScreen.PopulateAttributes();
            StartScreen.instance.NewGameScreen.PopulateCombatStats();
        }
        else
        {
            UI.instance.CharacterScreen.PopulateAttributes();
            UI.instance.CharacterScreen.PopulateCombatStats();
        }
    }

    public void RefreshSkills()
    {
        if (newCharacterScreen)
        {
            if (StartScreen.instance.NewGameScreen.Combat.activeSelf)
                StartScreen.instance.NewGameScreen.PopulateCombat();

            if (StartScreen.instance.NewGameScreen.Mental.activeSelf)
                StartScreen.instance.NewGameScreen.PopulateMental();

            if (StartScreen.instance.NewGameScreen.Technical.activeSelf)
                StartScreen.instance.NewGameScreen.PopulateTechnical();
        }
        else
        {
            UI.instance.CharacterScreen.PopulateSkills();
            UI.instance.CharacterScreen.PopulateCombatStats();
        }
    }

    public void RefreshCombatStatBoxes()
    {
        if (newCharacterScreen)
            StartScreen.instance.NewGameScreen.PopulateCombatStats();
        else
            UI.instance.CharacterScreen.PopulateCombatStats();
    }



    public void PopulateTooltip()
    {
        // attributes
        switch (AttributeType)
        {
            case Attribute.Strength:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Strength:</color> Determines <color=#ffc149>Health</color> and <color=#ffc149>Melee Damage</color>");
                break;
            case Attribute.Coordination:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Coordination:</color> Determines <color=#ffc149>Hit Chance</color> and <color=#ffc149>Armor Penetration</color>");
                break;
            case Attribute.Speed:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Speed:</color> Determines <color=#ffc149>Action Points</color> and <color=#ffc149>Evasion</color>");
                break;
            case Attribute.Vision:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Vision:</color> Determines <color=#ffc149>Initiative</color> and <color=#ffc149>Ranged Damage</color>");
                break;
            case Attribute.Aggression:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Aggression:</color> Determines <color=#ffc149>Critical Chance</color> and <color=#ffc149>Critical Damage</color>");
                break;
            case Attribute.Intelligence:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Intelligence:</color> Determines <color=#ffc149>Attribute Points Per Level</color> and <color=#ffc149>Skill Points Per Level</color>");
                break;
        }

        // skill
        switch (SkillType)
        {
            case Skill.Intimidation:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Intimidation:</color> Ability to intimidate people, machines or animals into doing what you want.");
                break;
            case Skill.Persuasion:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Persuasion:</color> Ability to persuade people, machines or animals into doing what you want.");
                break;
            case Skill.Law:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Law:</color> Your knowledge of the law which can be used to diffuse or solve certain situations.");
                break;
            case Skill.StreetSmarts:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Street Smarts:</color> Your knowledge of the streets which can be used to diffuse or solve certain situations.");
                break;
            case Skill.Perception:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Perception:</color> Your ability to notice clues or hidden objects.");
                break;
            case Skill.Science:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Science:</color> Your knowledge of science which can be used in situations requiring a more subtle and scientific solution.");
                break;
            case Skill.Engineering:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Engineering:</color> Your knowledge of engineering which can be used in situations requiring a more subtle and mathematic solution.");
                break;
            case Skill.Computers:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Computers:</color> Your ability to crack into computers and databases.");
                break;
            case Skill.Medicine:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Medicine:</color> Your knowledge of medicine which can be used to identify both physiological and psychological problems in others.");
                break;
            case Skill.Demolitions:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Demolitions:</color> Your knowledge of explosive devices in either diffusing or applying them.");
                break;
            case Skill.Lockpicking:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Lockpicking:</color> Your ability to successfully pick open locked doors and crates.");
                break;

            case Skill.Psionics:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Psionics:</color> Each points spent applies <color=#ffc149>Damage</color> and <color=#ffc149>Resistance</color> bonuses to all <color=#ffc149>Psionic</color> attacks.");
                break;
            case Skill.Brawling:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Brawling:</color> Each points spent applies <color=#ffc149>Damage</color> and <color=#ffc149>Armor Penetration</color> bonuses to all <color=#ffc149>Unarmed</color> attacks.");
                break;
            case Skill.Shooting:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Shooting:</color> Each points spent applies <color=#ffc149>Damage</color> and <color=#ffc149>Armor Penetration</color> bonuses to all <color=#ffc149>Ranged</color> attacks.");
                break;
            case Skill.HeavyWeapons:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Heavy Weapons:</color> Each points spent applies <color=#ffc149>Damage</color> and <color=#ffc149>Armor Penetration</color> bonuses to all <color=#ffc149>Heavy Weapon</color> attacks.");
                break;
            case Skill.SpecialAmmo:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Special Ammo:</color> Each points spent applies <color=#ffc149>Damage</color> bonuses to all attacks using <color=#ffc149>Special Ammo</color> rounds.");
                break;
            case Skill.Athletics:
                Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Athletics:</color> Each points spent applies <color=#ffc149>Health</color> and <color=#ffc149>Evasion</color> bonuses.");
                break;
        }
    }
}
