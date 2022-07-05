using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrimeTerminalBox : MonoBehaviour
{
    [Header("Portrait")]
    public Image Portrait;

    [Header("Labels")]
    public TextMeshProUGUI NameLabel;
    public TextMeshProUGUI ArrestingJudgeLabel;
    public TextMeshProUGUI CrimeNameLabel;
    public TextMeshProUGUI CrimeDescriptionLabel;
    public TextMeshProUGUI ReputationLabel;

    [Header("Buttons")]
    public Button CrimeButton;



    // populate box
    public void Populate(Crime _Crime)
    {
        // ----------------------------
        // status 
        string _status = "Upheld";
        if (!_Crime.crimeSuccess)
            _status = "Appealed";
        // ----------------------------
        // ----------------------------
        // info
        Portrait.sprite = PortraitSelector.FindPortrait(_Crime.Criminal, 0);
        NameLabel.text = _Crime.criminalName.ToUpper();
        ArrestingJudgeLabel.text = ("Arresting: " + PlayerScene.instance.MainCharacter.characterName).ToUpper();
        CrimeNameLabel.text = _Crime.crimeName.ToUpper();
        CrimeDescriptionLabel.text = (_Crime.crimeDescription + "\nPunishment: " + _Crime.GetPunishmentType() + "\nStatus: " + _status).ToUpper();
        // ----------------------------
        // ----------------------------
        // reputation
        if (_Crime.crimeSuccess && _Crime.crimeReported)
        {
            ReputationLabel.text = ("Reputation Awarded (" + _Crime.crimeReputationGain + ")").ToUpper();
            ReputationLabel.gameObject.SetActive(true);
        }
        else
        {
            ReputationLabel.gameObject.SetActive(false);
        }
        // ----------------------------
        // ----------------------------
        // button
        if (!_Crime.crimeSuccess || _Crime.crimeReported)
            CrimeButton.gameObject.SetActive(false);
        else
            CrimeButton.gameObject.SetActive(true);

        CrimeButton.onClick.RemoveAllListeners();
        CrimeButton.onClick.AddListener(() => ReportCrime(_Crime));
        // ----------------------------
    }

    // report crime
    public void ReportCrime(Crime _Crime)
    {
        _Crime.crimeReported = true;

        Populate(_Crime);

        PlayerScene.instance.MainCharacter.lawReputation += _Crime.crimeReputationGain;
    }
}
