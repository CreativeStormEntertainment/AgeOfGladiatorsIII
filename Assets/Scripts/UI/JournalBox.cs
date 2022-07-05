using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalBox : MonoBehaviour
{
    public TextMeshProUGUI MissionNameLabel;
    public Button BoxButton;
    public Image Panel;
    public Image MissionStatusIcon;


    // populate
    public void Populate(Mission _Mission)
    {
        // component status image will change

        // mission label
        //MissionNameLabel.text = _Mission.missionNameForLabel;

        //if (_Mission.missionComplete)
        //    MissionStatusIcon.gameObject.SetActive(true);
        //else
        //    MissionStatusIcon.gameObject.SetActive(false);
    }
}
