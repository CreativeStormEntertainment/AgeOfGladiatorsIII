using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item AttachedItem;

    public Image Portrait;
    public Button BoxButton;
    public Image Panel;



    public void OnPointerEnter(PointerEventData eventData)
    {
        Panel.sprite = UISprites.instance.Item[1];

        UISounds.instance.PlayMouseOn(0);

        if (AttachedItem != null)
        {
            // --------------------------------------
            // inventory rollover
            if (UI.instance.InventoryScreen.gameObject.activeSelf)
            {
                // populate selected items
                UI.instance.InventoryScreen.PopulateSelectedItem(AttachedItem);

                // weapon
                if (AttachedItem.ItemClass == ItemClasses.Weapon)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedWeapon != null)
                        UI.instance.InventoryScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedWeapon);
                    else
                        UI.instance.InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // helmet
                if (AttachedItem.ItemClass == ItemClasses.Helmet)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedHelmet != null)
                        UI.instance.InventoryScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedHelmet);
                    else
                        UI.instance.InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // chest
                if (AttachedItem.ItemClass == ItemClasses.Chest)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedChest != null)
                        UI.instance.InventoryScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedChest);
                    else
                        UI.instance.InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // legs
                if (AttachedItem.ItemClass == ItemClasses.Legs)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedLegs != null)
                        UI.instance.InventoryScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedLegs);
                    else
                        UI.instance.InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // boots
                if (AttachedItem.ItemClass == ItemClasses.Boots)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedBoots != null)
                        UI.instance.InventoryScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedBoots);
                    else
                        UI.instance.InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // gloves
                if (AttachedItem.ItemClass == ItemClasses.Gloves)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedGloves != null)
                        UI.instance.InventoryScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedGloves);
                    else
                        UI.instance.InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // accessory
                if (AttachedItem.ItemClass == ItemClasses.Accessory)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedAccessory != null)
                        UI.instance.InventoryScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedAccessory);
                    else
                        UI.instance.InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // populate rollovers
                PopulateCombatStatsRollover.Rollover(AttachedItem, UI.instance.InventoryScreen.CombatStatMainBoxes);
                PopulateCombatStatsRollover.Rollover(AttachedItem, UI.instance.InventoryScreen.CombatStatBoxes);
            }
            // --------------------------------------

            // --------------------------------------
            // vendor rollover
            if (UI.instance.VendorScreen.gameObject.activeSelf)
            {
                // populate selected items
                UI.instance.VendorScreen.PopulateSelectedItem(AttachedItem);

                // weapon
                if (AttachedItem.ItemClass == ItemClasses.Weapon)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedWeapon != null)
                        UI.instance.VendorScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedWeapon);
                    else
                        UI.instance.VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // helmet
                if (AttachedItem.ItemClass == ItemClasses.Helmet)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedHelmet != null)
                        UI.instance.VendorScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedHelmet);
                    else
                        UI.instance.VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // chest
                if (AttachedItem.ItemClass == ItemClasses.Chest)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedChest != null)
                        UI.instance.VendorScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedChest);
                    else
                        UI.instance.VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // legs
                if (AttachedItem.ItemClass == ItemClasses.Legs)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedLegs != null)
                        UI.instance.VendorScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedLegs);
                    else
                        UI.instance.VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // boots
                if (AttachedItem.ItemClass == ItemClasses.Boots)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedBoots != null)
                        UI.instance.VendorScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedBoots);
                    else
                        UI.instance.VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // gloves
                if (AttachedItem.ItemClass == ItemClasses.Gloves)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedGloves != null)
                        UI.instance.VendorScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedGloves);
                    else
                        UI.instance.VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
                }

                // accessory
                if (AttachedItem.ItemClass == ItemClasses.Accessory)
                {
                    if (PlayerScene.instance.MainCharacter.EquippedAccessory != null)
                        UI.instance.VendorScreen.PopulateEquippedItem(PlayerScene.instance.MainCharacter.EquippedAccessory);
                    else
                        UI.instance.VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
                }
            }
            // --------------------------------------

            // --------------------------------------
            // loot rollover
            if (UI.instance.LootScreen.gameObject.activeSelf)
                UI.instance.LootScreen.PopulateSelectedItem(AttachedItem);
            // --------------------------------------
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Panel.sprite = UISprites.instance.Item[0];

        // inventory
        if (UI.instance.InventoryScreen.gameObject.activeSelf)
            UI.instance.InventoryScreen.PopulateCombatStats();

        // vendor rollover
        if (UI.instance.VendorScreen.gameObject.activeSelf)
            UI.instance.VendorScreen.PopulateCoinLabels();

        // loot rollover
        if (UI.instance.LootScreen.gameObject.activeSelf)
            UI.instance.LootScreen.SelectedItemPanel.gameObject.SetActive(false);
    }
}
