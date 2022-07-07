using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NamePlateActivator : MonoBehaviour
{
    [HideInInspector]
    public CombatLabel NamePlate;
    [HideInInspector]
    public bool active;



    void Update()
    {
        if (Master.instance == null)
            return;

        Camera _Camera = PlayerScene.instance.SceneCamera;

        if (NamePlate != null)
            NamePlatePosition(_Camera);
    }



    public void ActivateNamePlate(bool _mouseover)
    {
        // do not show non-hostile name plates during combat
        if (Combat.instance.combatActivated && !GetComponent<NPC>().isHostile)
            return;

        // do not show if already active
        if (!active)
        {
            GameObject _platePrefab = Instantiate(Resources.Load("UI-Combat-Label")) as GameObject;
            _platePrefab.transform.SetParent(UI.instance.SpeechBubbles.transform, false);
            
            NamePlate = _platePrefab.GetComponent<CombatLabel>();
            NamePlate.Populate(GetComponent<Character>(), Combat.instance.combatActivated, _mouseover);

            active = true;

            NamePlatePosition(PlayerScene.instance.SceneCamera);
        }
    }

    public void DeactivateNamePlate()
    {
        if (NamePlate != null)
        {
            Destroy(NamePlate.gameObject);
            NamePlate = null;
        }

        active = false;
    }



    void NamePlatePosition(Camera _Camera)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "NameNode")
            {
                Vector3 _newPosition = child.transform.position;
                _newPosition = _Camera.WorldToScreenPoint(_newPosition);

                NamePlate.transform.position = _newPosition;
            }
        }
    }
}
