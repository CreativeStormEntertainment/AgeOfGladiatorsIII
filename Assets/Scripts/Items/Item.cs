using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemClasses ItemClass;

    public int itemNumber;



    // get portrait (virtual)
    public virtual Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = null;

        return _ItemPortrait;
    }

    // get item name (virtual)
    public virtual string GetItemName()
    {
        string _name = "";

        return _name;
    }

    // get item description (virtual)
    public virtual string GetItemDescription()
    {
        string _description = "";

        return _description;
    }

    // get item class description (virtual)
    public virtual string GetItemClassDescription()
    {
        string _description = "";

        return _description;
    }

    // calculate cost (virtual)
    public virtual int CalculateCost()
    {
        return 100;
    }
}
