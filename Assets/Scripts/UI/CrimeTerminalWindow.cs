using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrimeTerminalWindow : MonoBehaviour
{
    [Header("Parent")]
    public GameObject CrimeGrid;

    [Header("Labels")]
    public TextMeshProUGUI NoneLabel;

    [Header("Stats")]
    public IconLabelBox FinesBox;
    public IconLabelBox ArrestsBox;
    public IconLabelBox ExecutionsBox;


    // populate
    public void Populate()
    {
        // audio
        GameSounds.instance.PlayAudioCue(AudioCues.Computer);

        BuildCrimeList();

        if (CrimeDatabase.CrimeList.Count == 0)
            NoneLabel.text = "None";
        else
            NoneLabel.text = "";

        FinesBox.Label.text = PlayerScene.instance.MainCharacter.fines.ToString();
        ArrestsBox.Label.text = PlayerScene.instance.MainCharacter.arrests.ToString();
        ExecutionsBox.Label.text = PlayerScene.instance.MainCharacter.executions.ToString();
    }



    // build list
    void BuildCrimeList()
    {
        // clear the grid and rebuild
        for (int i = 0; i < CrimeGrid.transform.childCount; i++)
            Destroy(CrimeGrid.transform.GetChild(i).gameObject);

        // list items
        foreach (Crime _Crime in CrimeDatabase.CrimeList)
        {
            // instantiate and set up prefab in grid
            GameObject _prefab = Instantiate(Resources.Load("UI-CrimeTerminal-Box")) as GameObject;
            _prefab.transform.SetParent(CrimeGrid.transform, false);
            _prefab.transform.localPosition = Vector3.zero;

            // populate prefab with info
            CrimeTerminalBox _Box = _prefab.GetComponent<CrimeTerminalBox>();
            _Box.Populate(_Crime);
        }
    }
}
