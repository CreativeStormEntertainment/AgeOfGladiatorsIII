using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ConclusionWindow : MonoBehaviour
{
    public Image ConclusionImage;
    public TextMeshProUGUI ConclusionLabel;
    public Button ConcludeButton;



    public void Populate(string _name)
    {
        GameMusicCues.instance.PlayMusicCue(MusicCues.MissionAdvance);

        ConclusionImage.sprite = EventImages.instance.EventSprites[1];

        ConclusionLabel.text = EventText(_name);

        ConcludeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Return To Duty";
        ConcludeButton.onClick.RemoveAllListeners();
        ConcludeButton.onClick.AddListener(() => UI.instance.CloseConclusion());
    }

    public void PopulateEndGame()
    {
        ConclusionImage.sprite = EventImages.instance.EventSprites[0];

        ConclusionLabel.text = "On this day in Mega City One, thirty-seven Judges will have perished via criminal attack, accidental negligence and various other forms of mortality.\n\nYou are one of those thirty-seven.";

        ConcludeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Return To Menu";
        ConcludeButton.onClick.RemoveAllListeners();
        ConcludeButton.onClick.AddListener(() => UI.instance.CloseEndGame());
    }



    public string EventText(string _name)
    {
        string _text = "";

        switch (_name)
        {
            case "NomiSmith":
                _text = "Nomi clutches her hair with grief as she is dragged to the arriving wagon, the Judges rolling their eyes as they load her in. \n\nThe wagon speeds off and the bystanders in the area continue once more with their night, well-used to seeing incarcerations take place at the skywalks.";
                break;


            case "FleeApes":
                _text = "The prospects of being sent back to the caged coldness of the zoo fills the monkeys with unbridled terror as they flee past you.\n\nYou are free to proceed - however, the gate to the underground broadcasting studio remains locked. You will need to find some way in...";
                break;
            case "PeaceApes":
                _text = "The monkeys give a quick wink and a toothy grin before walking away, leaving you alone at the sewer entrance.\n\nYou are free to proceed - however, the gate to the underground broadcasting studio remains locked. You will need to find some way in...";
                break;
            case "PayApes":
                _text = "The monkeys go rushing away with their newly earned credits, eager to spend them on whatever monkeys spend credits on.\n\nYou are free to proceed - however, the gate to the underground broadcasting studio remains locked. You will need to find some way in...";
                break;
            case "DiseaseApes":
                _text = "The unnerved monkeys quickly gather themselves and usher out of the sewer.\n\nYou are free to proceed - however, the gate to the underground broadcasting studio remains locked. You will need to find some way in...";
                break;


            case "YouBetYourLifeTerminal":
                _text = "Within seconds, a group of Judges barge into the studio, having come to assist after witnessing your actions as they were broadcast across the entire city. They are quick to secure the area.\n\nYour superior, Judge Talbot stands behind you, awaiting your final report...";
                break;


            case "PeaceBabyBobNicely":
                _text = "Everybody in the room raises their hands in surrender. After confiscating any weapons on their persons, you look over at the glowing terminal, your actions continuing to beam to all in Mega-City One watching the illegal broadcast.\nIt's time to deactive the terminal and end this for good...";
                break;


            case "TimLunaDeadHostageHurt":
                _text = "As the hostage squirms on the ground beside, you kneel down and check the vitals of Tim Luna: dead as a doornail. Before turning and walking away, you lift your radio: " + "\"" + "I need a meat wagon at the markets and medi-wagon for a wounded hostage..." + "\"" + " \n\nWithin minutes, the street is efficiently cleaned and activity continues as it did before.";
                break;
            case "TimLunaDeadHostageSafe":
                _text = "You kneel down and check the vitals of Tim Luna and confirm that he is dead as the unwounded hostage scrambles off down the street. Standing once more, you lift your radio to your mouth: " + "\"" + "I need a meat wagon at the markets..." + "\"" + " \n\nWithin minutes, the street is efficiently cleaned and activity continues as it did before.";
                break;
            case "TimLunaDeadHostageDead":
                _text = "You kneel down and check the vitals of Tim Luna, confirming that he is dead. Unfortunately, the hostage also looks pretty dead. Before turning and walking away, you lift your radio: " + "\"" + "I need a meat wagon at the markets..." + "\"" + " \n\nWithin minutes, the street is efficiently cleaned and activity continues as it did before.";
                break;
            case "TimLunaAliveHostageHurt":
                _text = "Tim Luna is led way by Judges that arrive on the scene while Medi-Judges attend to the wounded hostage. She trembles and glares at you with fearful eyes as she is loaded onto a stretcher. Within minutes, the street is efficiently cleaned and activity continues as it did before.";
                break;
            case "TimLunaPeace":
                _text = "As Tim Luna is led way by Med-Judges, the hostage runs over and gives you a firm hug: " + "\"" + "Oh Judge! You save my life! I learned my lesson, I'll never smile at anyone ever again! I promise!" + "\"" + " \n\nPeople emerge from cover and street activity continues as it did before.";
                break;
        }

        return _text;
    }



    public void Close()
    {
        GameActions.instance.CleanAfterEventScreen(GameActions.instance.eventName);
    }

    public void CloseEndGame()
    {
        GameMusic.instance.FadeOutMusic(1f);

        Master.instance.ResetMaster();
        UI.instance.ResetUI();
        PlayerScene.instance.ResetPlayer();

        SceneManager.LoadScene("Master");
    }
}
