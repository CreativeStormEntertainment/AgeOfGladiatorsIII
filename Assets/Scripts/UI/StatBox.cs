using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBox : MonoBehaviour
{
    [Header("Combat Stat Attached")]
    public CombatStats CombatStat;


    // show rollover stat
    public void ShowRolloverStat(int _input)
    {
        GetComponent<IconLabelBox>().Label.text += " (<color=#ffc149>+" + _input + "</color>)";
    }

    // show rollover stat
    public void ShowRolloverStat(float _input)
    {
        GetComponent<IconLabelBox>().Label.text += " (+<color=#ffc149>" + _input.ToString("F1") + "</color>)";
    }

    // show rollover stat
    public void ShowRolloverStat(string _string)
    {
        GetComponent<IconLabelBox>().Label.text += " (<color=#ffc149>" + _string + "</color>)";
    }
}
