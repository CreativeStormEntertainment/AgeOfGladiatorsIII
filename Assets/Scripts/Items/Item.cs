using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemClasses ItemClass;

    public int itemNumber;



    public virtual Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = null;

        return _ItemPortrait;
    }

    public virtual string GetItemName()
    {
        string _name = "";

        return _name;
    }

    public virtual string GetItemDescription()
    {
        string _description = "";

        return _description;
    }

    public virtual string GetItemClassDescription()
    {
        string _description = "";

        return _description;
    }

    public virtual int CalculateCost()
    {
        return 100;
    }
}
