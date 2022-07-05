using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public List<Item> VendorList = new List<Item>();

    public int credits;



    void Awake()
    {
        SupplyStore();
    }



    // supply store
    public void SupplyStore()
    {
        // ------------------------------
        // replenish coin
        credits = Random.Range(400, 800);
        // ------------------------------

        // ------------------------------
        // add weapons
        int _random = Random.Range(5, 20);
        for (int i = 0; i < _random; i++)
            VendorList.Add(GenerateItem.RandomGenerateWeapon());

        // add helmets
        _random = Random.Range(5, 20);
        for (int i = 0; i < _random; i++)
            VendorList.Add(GenerateItem.RandomGenerateHelmet());

        // add accessory
        _random = Random.Range(2, 4);
        for (int i = 0; i < _random; i++)
            VendorList.Add(new Accessory(0));
        // ------------------------------
    }
}
