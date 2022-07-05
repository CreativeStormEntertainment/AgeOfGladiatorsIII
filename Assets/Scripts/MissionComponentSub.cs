using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionComponentSub
{
    public int componentSubNumber;
    public string componentSubDescription;
    public int componentSubXP;

    public bool componentSubComplete;



    public MissionComponentSub(int _componentNumber, int _xp, string _description)
    {
        componentSubNumber = _componentNumber;
        componentSubDescription = _description;
        componentSubXP = _xp;
    }
}
