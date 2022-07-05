using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateBox : MonoBehaviour
{
    public Image Portrait;
    public TextMeshProUGUI Header;
    public TextMeshProUGUI Label;
    public TextMeshProUGUI LevelLabel;



    // populate
    public void Populate(string _header, string _label)
    {
        Header.text = _header;
        Label.text = _label;
        LevelLabel.gameObject.SetActive(false);
    }

    // populate (rank)
    public void PopulateRank()
    {
        Header.text = ("New rank achieved!\n" + PlayerScene.instance.MainCharacter.GetRankLabel()).ToUpper();
        Label.text = "Report back to sector house".ToUpper();
    }

    // populate (rank)
    public void PopulateCrime(Crime _Crime)
    {
        Portrait.sprite = PortraitSelector.FindPortrait(_Crime.Criminal, 1);
        Header.text = ("Crime Prosecuted!\n" + _Crime.crimeName).ToUpper();
        Label.text = "Report back to crime terminal".ToUpper();

        UI.instance.reportCrimeOnConversationEnd = false;
    }

    // populate (travel)
    public void PopulateTravel()
    {
        Header.text = ("Travel Location \n<color=#ffc149>Unlocked</color>").ToUpper();
        Label.text = "Mega-City One (<color=#ffc149>Sewers</color>)".ToUpper();

        UI.instance.reportTravelOnConversationEnd = false;
    }
}
