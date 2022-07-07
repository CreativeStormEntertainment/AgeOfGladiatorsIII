using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedItemBox : MonoBehaviour
{
    [Header("Portrait")]
    public Image Portrait;

    [Header("Labels")]
    public TextMeshProUGUI ItemNameLabel;
    public TextMeshProUGUI ItemTypeLabel;
    public TextMeshProUGUI ItemDescriptionLabel;

    [Header("Line Icons")]
    public LineIcon[] LineIcons;

    [HideInInspector]
    public Item SelectedItem;



    public void PopulateSelectedItem(Item _Item, bool _equipped)
    {
        SelectedItem = _Item;

        Portrait.sprite = _Item.GetItemPortrait();

        ItemNameLabel.text = _Item.GetItemName();
        ItemDescriptionLabel.text = _Item.GetItemDescription();
        ItemTypeLabel.text = _Item.GetItemClassDescription();

        if (_equipped)
            ItemNameLabel.text += " (Equipped)";

        LineIcons[0].gameObject.SetActive(true);
        LineIcons[1].gameObject.SetActive(true);
        LineIcons[2].gameObject.SetActive(true);
        LineIcons[3].gameObject.SetActive(true);

        // weapon
        if (_Item.ItemClass == ItemClasses.Weapon)
        {
            Weapon _New = _Item as Weapon;
            LineIcons[0].Label.text = _New.minDamage + "-" + _New.maxDamage;
            LineIcons[0].Icon.sprite = UISprites.instance.Icons[1];
            LineIcons[0].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Damage:</color> Amount of damage inflicted by this weapon.");

            LineIcons[1].Label.text = _New.hitChance + "%";
            LineIcons[1].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Hit Chance:</color> Hit Chance bonus from equipping this weapon.");

            LineIcons[2].Label.text = _New.criticalChance + "%";
            LineIcons[2].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Hit Chance:</color> Critical Chance bonus from equipping this weapon.");

            LineIcons[3].Label.text = ((_New.criticalDamage + 1).ToString("F1") + "x");
            LineIcons[3].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Critical Damage:</color> Critical Damage inflicted by this weapon.");
        }

        // accessory
        if (_Item.ItemClass == ItemClasses.Accessory)
        {
            Accessory _New = _Item as Accessory;
            LineIcons[0].gameObject.SetActive(false);
            LineIcons[1].gameObject.SetActive(false);
            LineIcons[2].gameObject.SetActive(false);
            LineIcons[3].gameObject.SetActive(false);
        }

        // mission item
        if (_Item.ItemClass == ItemClasses.Mission)
        {
            MissionItem _New = _Item as MissionItem;
            LineIcons[0].gameObject.SetActive(false);
            LineIcons[1].gameObject.SetActive(false);
            LineIcons[2].gameObject.SetActive(false);
            LineIcons[3].gameObject.SetActive(false);
        }

        // helmet
        if (_Item.ItemClass == ItemClasses.Helmet)
        {
            Helmet _New = _Item as Helmet;
            LineIcons[0].Label.text = _New.armor.ToString();
            LineIcons[0].Icon.sprite = UISprites.instance.Icons[0];
            LineIcons[0].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Armor:</color> Armor protection provided by this item.");

            LineIcons[1].gameObject.SetActive(false);
            LineIcons[2].gameObject.SetActive(false);
            LineIcons[3].gameObject.SetActive(false);
        }

        // chest
        if (_Item.ItemClass == ItemClasses.Chest)
        {
            Chest _New = _Item as Chest;
            LineIcons[0].Label.text = _New.armor.ToString();
            LineIcons[0].Icon.sprite = UISprites.instance.Icons[0];
            LineIcons[0].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Armor:</color> Armor protection provided by this item.");

            LineIcons[1].gameObject.SetActive(false);
            LineIcons[2].gameObject.SetActive(false);
            LineIcons[3].gameObject.SetActive(false);
        }

        // legs
        if (_Item.ItemClass == ItemClasses.Legs)
        {
            Legs _New = _Item as Legs;
            LineIcons[0].Label.text = _New.armor.ToString();
            LineIcons[0].Icon.sprite = UISprites.instance.Icons[0];
            LineIcons[0].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Armor:</color> Armor protection provided by this item.");

            LineIcons[1].gameObject.SetActive(false);
            LineIcons[2].gameObject.SetActive(false);
            LineIcons[3].gameObject.SetActive(false);
        }

        // boots
        if (_Item.ItemClass == ItemClasses.Boots)
        {
            Boots _New = _Item as Boots;
            LineIcons[0].Label.text = _New.armor.ToString();
            LineIcons[0].Icon.sprite = UISprites.instance.Icons[0];
            LineIcons[0].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Armor:</color> Armor protection provided by this item.");

            LineIcons[1].gameObject.SetActive(false);
            LineIcons[2].gameObject.SetActive(false);
            LineIcons[3].gameObject.SetActive(false);
        }

        // gloves
        if (_Item.ItemClass == ItemClasses.Gloves)
        {
            Gloves _New = _Item as Gloves;
            LineIcons[0].Label.text = _New.armor.ToString();
            LineIcons[0].Icon.sprite = UISprites.instance.Icons[0];
            LineIcons[0].Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Armor:</color> Armor protection provided by this item.");

            LineIcons[1].gameObject.SetActive(false);
            LineIcons[2].gameObject.SetActive(false);
            LineIcons[3].gameObject.SetActive(false);
        }
    }
}
