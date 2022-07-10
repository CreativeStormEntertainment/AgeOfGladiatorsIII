using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class DialogueButtonExtra : MonoBehaviour
{
    [Header("Images")]
    public Sprite Active;
    public Sprite NonActive;



    public void HoverAudio()
    {
        UISounds.instance.PlayMouseOn(0);
    }

    public void ClickAudio()
    {
        if (GetComponent<StandardUIResponseButton>().isClickable)
            UISounds.instance.PlaySmallButton();
    }



    public void HoverGraphic(bool _hover)
    {
        if (_hover)
        {
            GetComponent<Image>().sprite = Active;
        }
        else
        {
            GetComponent<Image>().sprite = NonActive;
        }
    }
}
