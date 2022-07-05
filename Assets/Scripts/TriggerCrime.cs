using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCrime : MonoBehaviour
{
    [HideInInspector]
    public CrimeAlertBox CrimeBox;
    [HideInInspector]
    public bool alertTriggered;
    [HideInInspector]
    public Crime AttachedCrime;



    private void Start()
    {
        if (AttachedCrime == null)
            AttachedCrime = GetComponent<Crime>();
    }


    void Update()
    {
        if (Combat.instance.combatActivated || Dredd.instance == null)
            return;

        DetectCloseToCharacter();

        if (CrimeBox != null)
        {
            Camera _Camera = PlayerScene.instance.SceneCamera;

            AlertBoxPosition(_Camera);
        }
    }



    // detect if close to player
    public void DetectCloseToCharacter()
    {
        if (Vector3.Distance(PlayerScene.instance.MainCharacter.transform.position, transform.position) <= 6)
        {
            if (!alertTriggered && !AttachedCrime.crimeResolved && AttachedCrime.CrimeType != Crimes.None)
            {
                GameObject _crimeBoxPrefab = Instantiate(Resources.Load("UI-CrimeAlert")) as GameObject;
                _crimeBoxPrefab.transform.SetParent(UI.instance.SpeechBubbles.transform, false);

                if (!alertTriggered)
                    GameSounds.instance.PlayAudioCue(AudioCues.Crime);

                CrimeBox = _crimeBoxPrefab.GetComponent<CrimeAlertBox>();
                CrimeBox.TriggerPassed = GetComponent<TriggerCrime>();
                CrimeBox.TriggerPassed.alertTriggered = true;
                CrimeBox.Populate(GetComponent<NPC>(), AttachedCrime);
                StartCoroutine(CrimeBox.Close());
            }
        }
    }

    // alert box position
    void AlertBoxPosition(Camera _Camera)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "CrimeAlertNode")
            {
                Vector3 _newPosition = child.transform.position;
                _newPosition = _Camera.WorldToScreenPoint(_newPosition);

                CrimeBox.transform.position = _newPosition;
            }
        }
    }
}
