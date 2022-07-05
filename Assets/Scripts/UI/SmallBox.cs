using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SmallBox : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI Label;



    // populate
    public void Populate(string _label)
    {
        Label.text = _label;
    }
}
