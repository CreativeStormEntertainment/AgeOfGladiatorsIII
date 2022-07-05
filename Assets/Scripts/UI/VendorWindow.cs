using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VendorWindow : MonoBehaviour
{
    [Header("Inventory")]
    public GameObject InventoryGrid;
    public GameObject VendorGrid;

    [Header("Labels")]
    public LineIcon PartyCoinLine;
    public LineIcon VendorCoinLine;

    [Header("Selected Item Panel")]
    public SelectedItemBox SelectedItemPanel;
    public SelectedItemBox EquippedItemPanel;

    [Header("Selected Character")]
    Character SelectedCharacter;

    [HideInInspector]
    public Vendor SelectedVendor;
    [HideInInspector]
    public string vendorCointExtra;
    [HideInInspector]
    public string partyCoinExtra;
    [HideInInspector]
    public int vendorNumber;



    // populate inventory window
    public void Populate(int _vendorNumber)
    {
        vendorNumber = _vendorNumber;
        SelectedVendor = VendorArray.instance.Vendors[vendorNumber];

        PopulateCoinLabels();

        PopulateInventory();
        PopulateVendor();
    }



    // populate inventory
    public void PopulateInventory()
    {
        // clear the grid and rebuild
        for (int i = 0; i < InventoryGrid.transform.childCount; i++)
            Destroy(InventoryGrid.transform.GetChild(i).gameObject);

        // list items
        foreach (Item _Item in Inventory.InventoryList)
        {

            if (_Item.ItemClass != ItemClasses.Mission)
            {
                // instantiate and set up prefab in grid
                GameObject _prefab = Instantiate(Resources.Load("UI-Equipment-Box")) as GameObject;
                _prefab.transform.SetParent(InventoryGrid.transform, false);
                _prefab.transform.localPosition = Vector3.zero;

                // populate prefab with info
                EquipBox _ItemBox = _prefab.GetComponent<EquipBox>();
                _ItemBox.AttachedItem = _Item;
                _ItemBox.Portrait.sprite = _Item.GetItemPortrait();

                _ItemBox.BoxButton.onClick.RemoveAllListeners();
                _ItemBox.BoxButton.onClick.AddListener(() => SellItem(_Item));

                // do not allow selling if not enough coin
                if (_Item.CalculateCost() > SelectedVendor.credits)
                    _ItemBox.BoxButton.enabled = false;
                else
                    _ItemBox.BoxButton.enabled = true;
            }
        }
    }

    // populate vendor
    public void PopulateVendor()
    {
        // clear the grid and rebuild
        for (int i = 0; i < VendorGrid.transform.childCount; i++)
            Destroy(VendorGrid.transform.GetChild(i).gameObject);

        // list items
        foreach (Item _Item in SelectedVendor.VendorList)
        {
            // instantiate and set up prefab in grid
            GameObject _prefab = Instantiate(Resources.Load("UI-Equipment-Box")) as GameObject;
            _prefab.transform.SetParent(VendorGrid.transform, false);
            _prefab.transform.localPosition = Vector3.zero;

            // populate prefab with info
            EquipBox _ItemBox = _prefab.GetComponent<EquipBox>();
            _ItemBox.AttachedItem = _Item;
            _ItemBox.Portrait.sprite = _Item.GetItemPortrait();

            _ItemBox.BoxButton.onClick.RemoveAllListeners();
            _ItemBox.BoxButton.onClick.AddListener(() => BuyItem(_Item));

            // do not allow buying if not enough coin
            if (_Item.CalculateCost() > PlayerScene.instance.MainCharacter.credits)
                _ItemBox.BoxButton.enabled = false;
            else
                _ItemBox.BoxButton.enabled = true;
        }
    }



    // populate coin labels
    public void PopulateCoinLabels()
    {
        PartyCoinLine.Label.text = PlayerScene.instance.MainCharacter.credits.ToString("n0");
        VendorCoinLine.Label.text = SelectedVendor.credits.ToString("n0");

        PartyCoinLine.Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Player Credits</color>");
        VendorCoinLine.Icon.GetComponent<ModelShark.TooltipTrigger>().SetText("BodyText", $"<color=#ffc149>Vendor Credits</color>");
    }



    // populate selected item
    public void PopulateSelectedItem(Item _Item)
    {
        SelectedItemPanel.gameObject.SetActive(true);
        SelectedItemPanel.PopulateSelectedItem(_Item, false);

        // reset coin strings
        PopulateCoinLabels();

        // if item from inventory
        if (Inventory.InventoryList.Contains(_Item))
        {
            partyCoinExtra = (" (+" + _Item.CalculateCost().ToString("n0") + ")");
            vendorCointExtra = (" (-" + _Item.CalculateCost().ToString("n0") + ")");

            UI.instance.VendorScreen.PartyCoinLine.Label.text += partyCoinExtra;
            UI.instance.VendorScreen.VendorCoinLine.Label.text += vendorCointExtra;
        }

        // if item from vendor
        if (UI.instance.VendorScreen.SelectedVendor.VendorList.Contains(_Item))
        {
            partyCoinExtra = (" (-" + _Item.CalculateCost().ToString("n0") + ")");
            vendorCointExtra = (" (+" + _Item.CalculateCost().ToString("n0") + ")");

            UI.instance.VendorScreen.PartyCoinLine.Label.text += partyCoinExtra;
            UI.instance.VendorScreen.VendorCoinLine.Label.text += vendorCointExtra;
        }
    }

    // populate selected item panel
    public void PopulateEquippedItem(Item _Item)
    {
        EquippedItemPanel.gameObject.SetActive(true);

        EquippedItemPanel.PopulateSelectedItem(_Item, true);
    }


    // buy item
    void BuyItem(Item _Item)
    {
        SelectedVendor.VendorList.Remove(_Item);
        Inventory.AddToInventory(_Item); ;

        SelectedVendor.credits += _Item.CalculateCost();
        PlayerScene.instance.MainCharacter.credits -= _Item.CalculateCost();

        Populate(vendorNumber);
    }

    // sell item
    void SellItem(Item _Item)
    {
        SelectedVendor.VendorList.Add(_Item);
        Inventory.RemoveFromInventory(_Item);

        SelectedVendor.credits -= _Item.CalculateCost();
        PlayerScene.instance.MainCharacter.credits += _Item.CalculateCost();

        Populate(vendorNumber);
    }
}
