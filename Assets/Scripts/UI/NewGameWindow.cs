using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameWindow : MonoBehaviour
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
    public TextMeshProUGUI PointsLabel;

    [Header("Buttons")]
    public Button StartButton;
    public Button AttributesButton;
    public Button CombatButton;
    public Button MentalButton;
    public Button TechnicalButton;
    public Button MaleButton;
    public Button FemaleButton;

    [Header("Sections")]
    public GameObject Attributes;
    public GameObject Combat;
    public GameObject Mental;
    public GameObject Technical;

    [Header("Combat Stat")]
    public GameObject CombatStatsParent;

    [Header("Parents")]
    public GameObject AttributesParent;
    public GameObject CombatParent;
    public GameObject MentalParent;
    public GameObject TechnicalParent;

    [Header("Combat Stat Boxes")]
    public List<IconLabelBox> CombatStatBoxes = new List<IconLabelBox>();

    

    public void Populate()
    {
        SelectedCharacter = Master.instance.NewCharacterTemporary;
        SelectedCharacter.CharacterStart();

        PopulatePortraitPanel();
        PopulateInformation();
        ResetAll();
        ToggleSection(0);
    }



    public void PopulateInformation()
    {
        if (SelectedCharacter.male)
        {
            MaleButton.interactable = false;
            FemaleButton.interactable = true;
        }
        else
        {
            MaleButton.interactable = true;
            FemaleButton.interactable = false;
        }
    }

    public void PopulateAttributes()
    {
        // --------------------------------
        // attribute points
        PointsLabel.text = SelectedCharacter.attributePoints.ToString();
        PointsLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Attribute Points:</color> Amount available to spend on Attributes.");
        // --------------------------------

        // --------------------------------
        // info
        NameLabel.text = SelectedCharacter.characterName;

        RankLabel.text = SelectedCharacter.GetRankLabel();
        RankPanelLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Rank:</color> Current Rank of character.");

        LevelLabel.text = SelectedCharacter.level.ToString();
        LevelLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Level:</color> Current Level of character.");
        // --------------------------------

        // --------------------------------
        // populate list
        foreach (Transform child in AttributesParent.transform)
        {
            AttributeBox _box = child.gameObject.GetComponent<AttributeBox>();
            _box.Populate(SelectedCharacter, true);
        }
        // --------------------------------

        // --------------------------------
        // activate screen
        Attributes.gameObject.SetActive(true);
        CheckButtons();
        PopulateCombatStats();
        // --------------------------------
    }

    public void PopulateCombat()
    {
        // --------------------------------
        // skill points 
        PointsLabel.text = SelectedCharacter.skillPoints.ToString();
        PointsLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Skill Points:</color> Amount available to spend on Skills.");
        // --------------------------------

        // --------------------------------
        // populate list
        foreach (Transform child in CombatParent.transform)
        {
            AttributeBox _box = child.gameObject.GetComponent<AttributeBox>();
            _box.Populate(SelectedCharacter, true);
        }
        // --------------------------------

        // --------------------------------
        // activate section
        Combat.gameObject.SetActive(true);
        CheckButtons();
        PopulateCombatStats();
        // --------------------------------
    }

    public void PopulateMental()
    {
        // --------------------------------
        // skill points 
        PointsLabel.text = SelectedCharacter.skillPoints.ToString();
        PointsLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Skill Points:</color> Amount available to spend on Skills.");
        // --------------------------------

        // -----------------------------
        // populate list
        foreach (Transform child in MentalParent.transform)
        {
            AttributeBox _box = child.gameObject.GetComponent<AttributeBox>();
            _box.Populate(SelectedCharacter, true);
        }
        // ----------------------------

        // -----------------------------
        // activate section
        Mental.gameObject.SetActive(true);
        CheckButtons();
        // -----------------------------
    }

    public void PopulateTechnical()
    {
        // --------------------------------
        // skill points 
        PointsLabel.text = SelectedCharacter.skillPoints.ToString();
        PointsLabel.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Skill Points:</color> Amount available to spend on Skills.");
        // --------------------------------

        // -----------------------------
        // populate list
        foreach (Transform child in TechnicalParent.transform)
        {
            AttributeBox _box = child.gameObject.GetComponent<AttributeBox>();
            _box.Populate(SelectedCharacter, true);
        }
        // ----------------------------

        // -----------------------------
        // activate section
        Technical.gameObject.SetActive(true);
        CheckButtons();
        // -----------------------------
    }



    void CheckButtons()
    {
        AttributesButton.interactable = true;
        CombatButton.interactable = true;
        MentalButton.interactable = true;
        TechnicalButton.interactable = true;

        if (Attributes.activeSelf)
            AttributesButton.interactable = false;

        if (Combat.activeSelf)
            CombatButton.interactable = false;

        if (Mental.activeSelf)
            MentalButton.interactable = false;

        if (Technical.activeSelf)
            TechnicalButton.interactable = false;
    }



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



    public void ChangeSex(bool _male)
    {
        SelectedCharacter.male = _male;

        if (SelectedCharacter.male)
            SelectedCharacter.PortraitType = CharacterPortraitType.JudgeMale;
        else
            SelectedCharacter.PortraitType = CharacterPortraitType.JudgeFemale;

        PopulateInformation();
        PopulatePortraitPanel();
    }

    public void ToggleSection(int _input)
    {
        ResetAll();

        switch (_input)
        {
            case 0:
                PopulateAttributes();
                break;
            case 1:
                PopulateCombat();
                break;
            case 2:
                PopulateMental();
                break;
            case 3:
                PopulateTechnical();
                break;
        }
    }

    void ResetAll()
    {
        Attributes.gameObject.SetActive(false);
        Combat.gameObject.SetActive(false);
        Mental.gameObject.SetActive(false);
        Technical.gameObject.SetActive(false);
    }



    public void PopulatePortraitPanel()
    {
        CharacterPortraitSmall.sprite = PortraitSelector.FindPortrait(SelectedCharacter, 2);

        Character3D.instance.PopulateModel(false, SelectedCharacter);
    }

    public void NextPortrait()
    {
        SelectedCharacter.portraitNumber++;

        Sprite[] _TemporaryArray;
        if (SelectedCharacter.male)
            _TemporaryArray = PortraitImages.instance.JudgeMalePortraits;
        else
            _TemporaryArray = PortraitImages.instance.JudgeFemalePortraits;

        if (SelectedCharacter.portraitNumber == _TemporaryArray.Length)
            SelectedCharacter.portraitNumber = 0;

        PopulatePortraitPanel();
    }

    public void PreviousPortrait()
    {
        SelectedCharacter.portraitNumber--;

        Sprite[] _TemporaryArray;
        if (SelectedCharacter.male)
            _TemporaryArray = PortraitImages.instance.JudgeMalePortraits;
        else
            _TemporaryArray = PortraitImages.instance.JudgeFemalePortraits;

        if (SelectedCharacter.portraitNumber == -1)
            SelectedCharacter.portraitNumber = _TemporaryArray.Length - 1;

        PopulatePortraitPanel();
    }

    public void ShowHead()
    {
        Character3D.instance.ShowHead();
    }

    public void ShowBody()
    {
        Character3D.instance.ShowBody();
    }



    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
