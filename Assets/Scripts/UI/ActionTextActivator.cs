using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTextActivator : MonoBehaviour
{
    [HideInInspector]
    public ActionText ActionTextFloating;
    [HideInInspector]
    public bool active;



    void Start()
    {
        Camera AttachedCamera = PlayerScene.instance.SceneCamera;
    }

    void Update()
    {
        if (active && ActionTextFloating != null)
        {
            Camera AttachedCamera = PlayerScene.instance.SceneCamera;
            ActionTextPosition(AttachedCamera);
        } 
    }



    // report (xp gain)
    public IEnumerator ReportXP(float _incoming)
    {
        yield return new WaitForSeconds(0.3f);
        ActivateActionText("+" + _incoming + " XP");
    }

    // report (damage)
    public IEnumerator ReportDamage(int _incoming)
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Defending.characterName + " Hit For " + _incoming + " (" + Combat.instance.Defending.combatHealth + ")");
        yield return new WaitForSeconds(0.1f);
        
        ActivateActionText("-" + _incoming);
    }

    // report (damage)
    public IEnumerator ReportCriticalDamage()
    {
        yield return new WaitForSeconds(0.3f);
        ActivateActionText("\nCritical Hit!");
    }

    // report (miss)
    public IEnumerator ReportMiss()
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Attacking.characterName + " Missed " + Combat.instance.Defending.characterName);
        yield return new WaitForSeconds(0.0f);
        ActivateActionText("Missed");
    }

    // report (evasion)
    public IEnumerator ReportEvasion()
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Defending.characterName + " Evaded Attack!");
        yield return new WaitForSeconds(0.0f);
        ActivateActionText("Evaded");
    }

    // report (cover)
    public IEnumerator ReportCover()
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Defending.characterName + " Missed (Cover)");
        yield return new WaitForSeconds(0.0f);
        ActivateActionText("Evaded (Cover)");
    }

    // report (other)
    public IEnumerator ReportOther(string _input)
    {
        yield return new WaitForSeconds(0.0f);
        ActivateActionText(_input);
    }



    // activate action text
    public void ActivateActionText(string _input)
    {
        GameObject _ActionText = Instantiate(Resources.Load("UI-ActionText")) as GameObject;
        _ActionText.transform.SetParent(UI.instance.SpeechBubbles.transform, false);

        ActionTextFloating = _ActionText.GetComponent<ActionText>();
        ActionTextFloating.ActionTextLabel.text = _input;
        StartCoroutine(ActionTextFloating.Close());

        active = true;

        ActionTextPosition(PlayerScene.instance.SceneCamera);
    }

    // action text position
    void ActionTextPosition(Camera _Camera)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "SpeechNode")
            {
                Vector3 _newPosition = child.transform.position;
                _newPosition = _Camera.WorldToScreenPoint(_newPosition);

                ActionTextFloating.transform.position = _newPosition;
            }
        }
    }
}
