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



    // audio (hover)
    public void HoverAudio()
    {
        UISounds.instance.PlayMouseOn(0);
    }

    // audio (click)
    public void ClickAudio()
    {
        if (GetComponent<StandardUIResponseButton>().isClickable)
            UISounds.instance.PlaySmallButton();
    }



    // hover (graphic)
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
