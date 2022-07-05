using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorArray : MonoBehaviour
{
    public static VendorArray instance;

    [Header("Vendor")]
    public Vendor[] Vendors;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
