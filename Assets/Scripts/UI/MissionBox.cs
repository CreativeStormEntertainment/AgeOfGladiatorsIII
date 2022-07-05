using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PixelCrushers.DialogueSystem;

public class MissionBox : MonoBehaviour
{
    public TextMeshProUGUI MissionHeaderLabel;
    public MissionComponentBox ComponentOne;
    public MissionComponentBox ComponentTwo;
    public TextMeshProUGUI MissionCompleteLabel;



    // populate
    public void Populate(string _questName, double _questEntry)
    {
        // ----------------------------------
        // quest name
        MissionHeaderLabel.text = QuestLog.GetQuestTitle(_questName);
        MissionCompleteLabel.gameObject.SetActive(false);
        // ----------------------------------

        // ----------------------------------
        // first entry
        ComponentOne.StatusIcon.gameObject.SetActive(false);
        ComponentOne.ComponentLabel.text = FormattedText.ParseCode(QuestLog.GetQuestEntry(_questName, (int)(_questEntry)));
        //ComponentOne.GetComponent<StandardUITextTemplate>().Assign(QuestLog.GetQuestEntry(_questName, (int)_questEntry));

        if (QuestLog.GetQuestEntryState(_questName, (int)_questEntry) == QuestState.Success)
            ComponentOne.StatusIcon.gameObject.SetActive(true);
        // ----------------------------------

        // ----------------------------------
        // second entry (do not show if mission is complete or entry is not active - usually at quest start)
        ComponentTwo.gameObject.SetActive(false);

        if (_questEntry < QuestLog.GetQuestEntryCount(_questName))
        {
            if (QuestLog.GetQuestEntryState(_questName, (int)(_questEntry + 1)) == QuestState.Active)
            {
                ComponentTwo.gameObject.SetActive(true);
                ComponentTwo.ComponentLabel.text = FormattedText.ParseCode(QuestLog.GetQuestEntry(_questName, (int)(_questEntry + 1)));
                ComponentTwo.StatusIcon.gameObject.SetActive(false);
            }
        }
        // ----------------------------------

        // ----------------------------------
        // mission complete
        if (QuestLog.GetQuestState(_questName) == QuestState.Success)
            MissionCompleteLabel.gameObject.SetActive(true);
        // ----------------------------------
    }
}