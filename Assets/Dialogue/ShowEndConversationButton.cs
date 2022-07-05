using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using TMPro;

public class ShowEndConversationButton : MonoBehaviour
{
    public Button continueButton; // Assign in inspector, or get via code. In this example, assume inspector assignment.



    private void Start()
    {
        // Configure the button to send a sequencer message "End" in addition to its regular activity:
        continueButton.onClick.AddListener(() => { Sequencer.Message("End"); });
    }



    void OnConversationLine(Subtitle subtitle)
    {
        if (!DialogueManager.currentConversationState.hasAnyResponses)
        {
            // If we're at the end of the conversation, force the continue button to be visible with
            // the text END CONVERSATION. (assuming Text here, but you can switch to TextMeshProUGUI)
            continueButton.GetComponentInChildren<TextMeshProUGUI>().text = DialogueManager.GetLocalizedText("END CONVERSATION");
            continueButton.gameObject.SetActive(true);

            // Tell this line to wait for the sequencer message "End", which is sent by the continue button:
            subtitle.sequence = "WaitForMessage(End); " + subtitle.sequence;
        }
    }

    void OnConversationEnd(Transform actor)
    {
        GameActions.instance.EndConversation();

        PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().restrictMovement = false;

        // Set the continue button text back to CONTINUE:
        continueButton.GetComponentInChildren<TextMeshProUGUI>().text = DialogueManager.GetLocalizedText("CONTINUE");
        continueButton.gameObject.SetActive(false);
    }
}
