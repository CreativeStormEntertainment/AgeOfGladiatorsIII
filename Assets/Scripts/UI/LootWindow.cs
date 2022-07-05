using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootWindow : MonoBehaviour
{
    [Header("Inventory")]
    public GameObject ContainerGrid;

    [Header("Selected Item Panel")]
    public SelectedItemBox SelectedItemPanel;

    [Header("Container")]
    public MapItemContainer SelectedContainer;

    [Header("Button")]
    public Button LootAllButton;



    // populate inventory window
    public void Populate(MapItemContainer _Container)
    {
        SelectedContainer = _Container;

        SelectedItemPanel.gameObject.SetActive(false);

        if (SelectedContainer.ContainedItems.Count == 0)
            LootAllButton.gameObject.SetActive(false);
        else
            LootAllButton.gameObject.SetActive(true);

        PopulateContainer();
    }

    // populate container
    public void PopulateContainer()
    {
        // clear the grid and rebuild
        for (int i = 0; i < ContainerGrid.transform.childCount; i++)
            Destroy(ContainerGrid.transform.GetChild(i).gameObject);

        // list items
        foreach (Item _Item in SelectedContainer.ContainedItems)
        {
            // instantiate and set up prefab in grid
            GameObject _prefab = Instantiate(Resources.Load("UI-Equipment-Box")) as GameObject;
            _prefab.transform.SetParent(ContainerGrid.transform, false);
            _prefab.transform.localPosition = Vector3.zero;

            // populate prefab with info
            EquipBox _ItemBox = _prefab.GetComponent<EquipBox>();
            _ItemBox.AttachedItem = _Item;
            _ItemBox.Portrait.sprite = _Item.GetItemPortrait();

            _ItemBox.BoxButton.onClick.RemoveAllListeners();
            _ItemBox.BoxButton.onClick.AddListener(() => AddItem(_Item));

            _ItemBox.BoxButton.enabled = true;
        }
    }

    // populate selected item
    public void PopulateSelectedItem(Item _Item)
    {
        SelectedItemPanel.gameObject.SetActive(true);
        SelectedItemPanel.PopulateSelectedItem(_Item, false);
    }



    // add item
    void AddItem(Item _Item)
    {
        SelectedContainer.ContainedItems.Remove(_Item);
        Inventory.AddToInventory(_Item);

        GameSounds.instance.PlayAddItem(_Item.ItemClass);

        if (SelectedContainer.disappearWhenPickedUp && SelectedContainer.ContainedItems.Count == 0)
            SelectedContainer.gameObject.SetActive(false);

        if (SelectedContainer.ContainedItems.Count == 0)
            UI.instance.CloseLoot();
        else
            Populate(SelectedContainer);
    }

    // add all items
    public void AddAllItems()
    {
        foreach (Item _Item in SelectedContainer.ContainedItems)
            Inventory.AddToInventory(_Item);

        SelectedContainer.ContainedItems.Clear();

        if (SelectedContainer.disappearWhenPickedUp && SelectedContainer.ContainedItems.Count == 0)
            SelectedContainer.gameObject.SetActive(false);

        UI.instance.CloseLoot();
    }
}