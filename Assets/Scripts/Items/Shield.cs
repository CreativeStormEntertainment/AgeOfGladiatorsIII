using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{



    // get shield portrait
    public override Sprite GetItemPortrait()
    {
        Sprite _ItemPortrait = null;

        _ItemPortrait = PortraitImages.instance.ShieldPortraits[itemNumber];

        return _ItemPortrait;
    }
}
