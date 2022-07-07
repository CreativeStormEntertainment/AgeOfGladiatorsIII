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



    public IEnumerator ReportXP(float _incoming)
    {
        yield return new WaitForSeconds(0.3f);
        ActivateActionText("+" + _incoming + " XP");
    }

    public IEnumerator ReportDamage(int _incoming)
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Defending.characterName + " Hit For " + _incoming + " (" + Combat.instance.Defending.combatHealth + ")");
        yield return new WaitForSeconds(0.1f);
        
        ActivateActionText("-" + _incoming);
    }

    public IEnumerator ReportCriticalDamage()
    {
        yield return new WaitForSeconds(0.3f);
        ActivateActionText("\nCritical Hit!");
    }

    public IEnumerator ReportMiss()
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Attacking.characterName + " Missed " + Combat.instance.Defending.characterName);
        yield return new WaitForSeconds(0.0f);
        ActivateActionText("Missed");
    }

    public IEnumerator ReportEvasion()
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Defending.characterName + " Evaded Attack!");
        yield return new WaitForSeconds(0.0f);
        ActivateActionText("Evaded");
    }

    public IEnumerator ReportCover()
    {
        Debug.Log(Combat.instance.round + ": " + Combat.instance.Defending.characterName + " Missed (Cover)");
        yield return new WaitForSeconds(0.0f);
        ActivateActionText("Evaded (Cover)");
    }

    public IEnumerator ReportOther(string _input)
    {
        yield return new WaitForSeconds(0.0f);
        ActivateActionText(_input);
    }



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
