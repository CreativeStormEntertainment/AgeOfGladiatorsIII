using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrimeAlertBox : MonoBehaviour
{
    [Header("Speech Activator")]
    public TriggerCrime TriggerPassed;

    [Header("Portrait")]
    public Image Portrait;

    [Header("Labels")]
    public TextMeshProUGUI HeaderLabel;
    public TextMeshProUGUI CrimeLabel;

    NPC AttachedNPC;



    // populate box
    public void Populate(NPC _NPC, Crime _Crime)
    {
        AttachedNPC = _NPC;

        Portrait.sprite = PortraitSelector.FindPortrait(AttachedNPC, 0);
        CrimeLabel.text = _Crime.crimeDescription.ToUpper() + "\n" + _Crime.crimeName.ToUpper();
    }



    // close timer
    public IEnumerator Close()
    {
        yield return new WaitForSeconds(6);
        //TriggerPassed.alertTriggered = false; // uncomment this if want to keep appearing
        Destroy();
    }

    // destroy
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
