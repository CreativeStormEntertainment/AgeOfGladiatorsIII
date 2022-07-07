using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryWindow : MonoBehaviour
{
    [Header("Inventory")]
    public GameObject InventoryGrid;

    [Header("Labels")]
    public TextMeshProUGUI PlayerNameLabel;
    public LineIcon CoinLine;

    [Header("Dropdown")]
    public TMP_Dropdown FilterDropdown;

    [Header("Equipped Boxes")]
    public EquipBox[] ArmorBoxes;
    public EquipBox[] ItemBoxes;

    [Header("Selected Item Panel")]
    public SelectedItemBox SelectedItemPanel;
    public SelectedItemBox EquippedItemPanel;

    [Header("Selected Character")]
    PlayerCharacter SelectedCharacter;

    [Header("Parents")]
    public GameObject CombatStatsMainParent;
    public GameObject CombatStatsParent;

    [Header("Combat Stat Boxes")]
    public List<IconLabelBox> CombatStatMainBoxes = new List<IconLabelBox>();
    public List<IconLabelBox> CombatStatBoxes = new List<IconLabelBox>();



    public void Populate()
    {
        SelectedCharacter = PlayerScene.instance.MainCharacter;

        PlayerNameLabel.text = SelectedCharacter.characterName;
        CoinLine.Label.text = PlayerScene.instance.MainCharacter.credits.ToString();
        CoinLine.Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Credits</color>");


        PopulateCharacterEquipment();
        PopulateInventory();
        PopulatePortrait();

        PopulateCombatStats();
    }



    public void PopulateCharacterEquipment()
    {
        // -------------------------------------
        // reset all
        ItemBoxes[0].Portrait.gameObject.SetActive(false);
        ItemBoxes[1].Portrait.gameObject.SetActive(false);
        ItemBoxes[2].Portrait.gameObject.SetActive(false);
        ItemBoxes[3].Portrait.gameObject.SetActive(false);

        ArmorBoxes[0].Portrait.gameObject.SetActive(false);
        ArmorBoxes[1].Portrait.gameObject.SetActive(false);
        ArmorBoxes[2].Portrait.gameObject.SetActive(false);
        ArmorBoxes[3].Portrait.gameObject.SetActive(false);
        ArmorBoxes[4].Portrait.gameObject.SetActive(false);
        // -------------------------------------


        // -------------------------------------
        // weapon
        if (SelectedCharacter.EquippedWeapon != null)
        {
            ItemBoxes[0].Portrait.gameObject.SetActive(true);
            ItemBoxes[0].AttachedItem = SelectedCharacter.EquippedWeapon;
            ItemBoxes[0].Portrait.sprite = SelectedCharacter.EquippedWeapon.GetItemPortrait();

            ItemBoxes[0].BoxButton.onClick.RemoveAllListeners();
            ItemBoxes[0].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedWeapon));
        }
        // -------------------------------------

        // -------------------------------------
        // weapon(second)
        if (SelectedCharacter.EquippedWeapon != null)
        {
            //ItemBoxes[1].Portrait.gameObject.SetActive(true);
            //ItemBoxes[1].AttachedItem = SelectedCharacter.EquippedWeapon;
            //ItemBoxes[1].Portrait.sprite = SelectedCharacter.EquippedWeapon.GetItemPortrait();

            //ItemBoxes[1].BoxButton.onClick.RemoveAllListeners();
            //ItemBoxes[1].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedWeapon));
        }
        // -------------------------------------

        // -------------------------------------
        // shield
        if (SelectedCharacter.EquippedShield != null)
        {
            ItemBoxes[2].Portrait.gameObject.SetActive(true);
            ItemBoxes[2].Portrait.sprite = SelectedCharacter.EquippedShield.GetItemPortrait();

            ItemBoxes[2].BoxButton.onClick.RemoveAllListeners();
            ItemBoxes[2].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedShield));
        }
        // -------------------------------------

        // -------------------------------------
        // accessory
        if (SelectedCharacter.EquippedAccessory != null)
        {
            ItemBoxes[3].Portrait.gameObject.SetActive(true);
            ItemBoxes[3].Portrait.sprite = SelectedCharacter.EquippedAccessory.GetItemPortrait();

            ItemBoxes[3].BoxButton.onClick.RemoveAllListeners();
            ItemBoxes[3].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedAccessory));
        }
        // -------------------------------------



        // -------------------------------------
        // armor
        if (SelectedCharacter.EquippedHelmet != null)
        {
            ArmorBoxes[0].Portrait.gameObject.SetActive(true);
            ArmorBoxes[0].Portrait.sprite = SelectedCharacter.EquippedHelmet.GetItemPortrait();

            ArmorBoxes[0].BoxButton.onClick.RemoveAllListeners();
            ArmorBoxes[0].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedHelmet));
        }
            
        if (SelectedCharacter.EquippedChest != null)
        {
            ArmorBoxes[1].Portrait.gameObject.SetActive(true);
            ArmorBoxes[1].Portrait.sprite = SelectedCharacter.EquippedChest.GetItemPortrait();

            ArmorBoxes[1].BoxButton.onClick.RemoveAllListeners();
            ArmorBoxes[1].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedChest));
        }

        if (SelectedCharacter.EquippedLegs != null)
        {
            ArmorBoxes[2].Portrait.gameObject.SetActive(true);
            ArmorBoxes[2].Portrait.sprite = SelectedCharacter.EquippedLegs.GetItemPortrait();

            ArmorBoxes[2].BoxButton.onClick.RemoveAllListeners();
            ArmorBoxes[2].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedLegs));
        }

        if (SelectedCharacter.EquippedBoots != null)
        {
            ArmorBoxes[3].Portrait.gameObject.SetActive(true);
            ArmorBoxes[3].Portrait.sprite = SelectedCharacter.EquippedBoots.GetItemPortrait();

            ArmorBoxes[3].BoxButton.onClick.RemoveAllListeners();
            ArmorBoxes[3].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedBoots));
        }

        if (SelectedCharacter.EquippedGloves != null)
        {
            ArmorBoxes[4].Portrait.gameObject.SetActive(true);
            ArmorBoxes[4].Portrait.sprite = SelectedCharacter.EquippedGloves.GetItemPortrait();

            ArmorBoxes[4].BoxButton.onClick.RemoveAllListeners();
            ArmorBoxes[4].BoxButton.onClick.AddListener(() => RemoveItemFromCharacter(SelectedCharacter.EquippedGloves));
        }
        // -------------------------------------
    }

    public void PopulateInventory()
    {
        // clear the grid and rebuild
        for (int i = 0; i < InventoryGrid.transform.childCount; i++)
            Destroy(InventoryGrid.transform.GetChild(i).gameObject);

        // list items
        foreach (Item _Item in Inventory.InventoryList)
        {
            // instantiate and set up prefab in grid
            GameObject _prefab = Instantiate(Resources.Load("UI-Equipment-Box")) as GameObject;
            _prefab.transform.SetParent(InventoryGrid.transform, false);
            _prefab.transform.localPosition = Vector3.zero;

            // populate prefab with info
            EquipBox _ItemBox = _prefab.GetComponent<EquipBox>();
            _ItemBox.AttachedItem = _Item;
            _ItemBox.Portrait.sprite = _Item.GetItemPortrait();

            if (_Item.ItemClass != ItemClasses.Mission)
            {
                _ItemBox.BoxButton.onClick.RemoveAllListeners();
                _ItemBox.BoxButton.onClick.AddListener(() => AddItemToCharacter(_Item));
            }
        }
    }

    public void PopulateSelectedItem(Item _Item)
    {
        SelectedItemPanel.gameObject.SetActive(true);

        SelectedItemPanel.PopulateSelectedItem(_Item, false);
    }

    public void PopulateEquippedItem(Item _Item)
    {
        EquippedItemPanel.gameObject.SetActive(true);

        EquippedItemPanel.PopulateSelectedItem(_Item, true);
    }

    public void PopulatePortrait()
    {
        // update character portrait on screen
        Character3D.instance.PopulateModel(true, SelectedCharacter);

        // update character in world
        PlayerScene.instance.MainCharacter.UpdateEquipmentOnModel();
    }

    public void PopulateCombatStats()
    {
        // --------------------------------
        // populate list
        CombatStatMainBoxes.Clear();

        foreach (Transform child in CombatStatsMainParent.transform)
        {
            IconLabelBox _box = child.gameObject.GetComponent<IconLabelBox>();
            CombatStatMainBoxes.Add(_box);
        }

        CombatStatBoxes.Clear();

        foreach (Transform child in CombatStatsParent.transform)
        {
            IconLabelBox _box = child.gameObject.GetComponent<IconLabelBox>();
            CombatStatBoxes.Add(_box);
        }
        // --------------------------------

        // --------------------------------
        // stats
        CombatStatMainBoxes[0].Label.text = SelectedCharacter.GetHealth().ToString();
        CombatStatMainBoxes[0].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Health:</color> Hitpoints during combat");

        if (SelectedCharacter.EquippedWeapon != null)
        {
            if (SelectedCharacter.EquippedWeapon.WeaponDamageType == WeaponDamage.Melee)
                CombatStatMainBoxes[1].Label.text = SelectedCharacter.GetMeleeMinimumDamage() + "-" + SelectedCharacter.GetMeleeMaximumDamage();
            else
                CombatStatMainBoxes[1].Label.text = SelectedCharacter.GetRangedMinimumDamage() + "-" + SelectedCharacter.GetRangedMaximumDamage();
        }
        else
        {
            CombatStatMainBoxes[1].Label.text = SelectedCharacter.GetUnarmedMinimumDamage() + "-" + SelectedCharacter.GetUnarmedMaximumDamage();
        }

        CombatStatMainBoxes[1].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Damage:</color> Total Damage");

        CombatStatMainBoxes[2].Label.text = SelectedCharacter.GetArmor().ToString();
        CombatStatMainBoxes[2].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Armor:</color> Incoming Damage is reduced by Armor value");

        CombatStatMainBoxes[3].Label.text = SelectedCharacter.GetActionPoints().ToString();
        CombatStatMainBoxes[3].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Action Points:</color> Determines movement range and action uses during combat");

        // secondary
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



    public void ShowHead()
    {
        Character3D.instance.ShowHead();
    }

    public void ShowBody()
    {
        Character3D.instance.ShowBody();
    }



    public void AddItemToCharacter(Item _Item)
    {
        GameSounds.instance.PlayAddItem(_Item.ItemClass);

        switch (_Item.ItemClass)
        {
            // weapon
            case ItemClasses.Weapon:
                if (SelectedCharacter.EquippedWeapon != null)
                    RemoveItemFromCharacter(SelectedCharacter.EquippedWeapon);

                SelectedCharacter.EquippedWeapon = _Item as Weapon;
                break;
            // helmet
            case ItemClasses.Helmet:
                if (SelectedCharacter.EquippedHelmet != null)
                    RemoveItemFromCharacter(SelectedCharacter.EquippedHelmet);

                SelectedCharacter.EquippedHelmet = _Item as Helmet;
                break;
            // chest
            case ItemClasses.Chest:
                if (SelectedCharacter.EquippedChest != null)
                    RemoveItemFromCharacter(SelectedCharacter.EquippedChest);

                SelectedCharacter.EquippedChest = _Item as Chest;
                break;
            // leg
            case ItemClasses.Legs:
                if (SelectedCharacter.EquippedLegs != null)
                    RemoveItemFromCharacter(SelectedCharacter.EquippedLegs);

                SelectedCharacter.EquippedLegs = _Item as Legs;
                break;
            // boot
            case ItemClasses.Boots:
                if (SelectedCharacter.EquippedBoots != null)
                    RemoveItemFromCharacter(SelectedCharacter.EquippedBoots);

                SelectedCharacter.EquippedBoots = _Item as Boots;
                break;
            // gloves
            case ItemClasses.Gloves:
                if (SelectedCharacter.EquippedGloves != null)
                    RemoveItemFromCharacter(SelectedCharacter.EquippedGloves);

                SelectedCharacter.EquippedGloves = _Item as Gloves;
                break;
            // shield
            case ItemClasses.Shield:
                if (SelectedCharacter.EquippedShield != null)
                    RemoveItemFromCharacter(SelectedCharacter.EquippedShield);

                SelectedCharacter.EquippedShield = _Item as Shield;
                break;
            // accessory
            case ItemClasses.Accessory:
                if (SelectedCharacter.EquippedAccessory != null)
                    RemoveItemFromCharacter(SelectedCharacter.EquippedAccessory);

                SelectedCharacter.EquippedAccessory = _Item as Accessory;
                break;
        }

        Inventory.RemoveFromInventory(_Item);
        Populate();
        EquippedItemPanel.gameObject.SetActive(false);
    }

    public void RemoveItemFromCharacter(Item _Item)
    {
        GameSounds.instance.PlayAddItem(_Item.ItemClass);

        Inventory.AddToInventory(_Item);

        switch (_Item.ItemClass)
        {
            // weapon
            case ItemClasses.Weapon:
                SelectedCharacter.EquippedWeapon = null;
                break;
            // helmet
            case ItemClasses.Helmet:
                SelectedCharacter.EquippedHelmet = null;
                break;
            // chest
            case ItemClasses.Chest:
                SelectedCharacter.EquippedChest = null;
                break;
            // leg
            case ItemClasses.Legs:
                SelectedCharacter.EquippedLegs = null;
                break;
            // boot
            case ItemClasses.Boots:
                SelectedCharacter.EquippedBoots = null;
                break;
            // boot
            case ItemClasses.Gloves:
                SelectedCharacter.EquippedGloves = null;
                break;
            // shield
            case ItemClasses.Shield:
                SelectedCharacter.EquippedShield = null;
                break;
            // accessory
            case ItemClasses.Accessory:
                SelectedCharacter.EquippedAccessory = null;
                break;
        }

        Populate();
        EquippedItemPanel.gameObject.SetActive(false);
    }
}
