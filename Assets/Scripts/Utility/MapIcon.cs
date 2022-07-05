using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIcon : MonoBehaviour
{
    public SpriteRenderer RegularIcon;
    public SpriteRenderer QuestIcon;


    public void ActivateIcon(bool _activate)
    {
        // -----------------------------
        // activate regular icon
        RegularIcon.gameObject.SetActive(_activate);
        QuestIcon.gameObject.SetActive(false);
        // -----------------------------

        // -----------------------------
        // activate quest icon (mission click)
        //if (GetComponent<TriggerMissionClick>() != null)
        //{
        //    if (MissionList.instance.CheckMission(GetComponent<TriggerMissionClick>().MissionName, GetComponent<TriggerMissionClick>().resolveComponent))
        //    {
        //        RegularIcon.gameObject.SetActive(false);
        //        QuestIcon.gameObject.SetActive(_activate);
        //    }  
        //}
        // -----------------------------
        // -----------------------------
        // activate quest icon (conversation)

        //RegularIcon.gameObject.SetActive(false);
        //QuestIcon.gameObject.SetActive(_activate);

        // -----------------------------
    }
}
