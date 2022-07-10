using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public ButtonType ButtonType;
    public Button ButtonSprite;
    public bool noRollver;



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
            MouseOn();

        if (ButtonType == ButtonType.Small)
            UISounds.instance.PlayMouseOn(0);

        if (ButtonType == ButtonType.Regular)
            UISounds.instance.PlayMouseOn(1);

        if (ButtonType == ButtonType.Large)
            UISounds.instance.PlayMouseOn(2);

        if (ButtonType == ButtonType.Tab)
            UISounds.instance.PlayMouseOn(2);

        if (ButtonType == ButtonType.Close)
            UISounds.instance.PlayMouseOn(1);

        if (ButtonType == ButtonType.Journal)
            UISounds.instance.PlayMouseOn(0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
            MouseOff();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ButtonType == ButtonType.Small)
            UISounds.instance.PlaySmallButton();

        if (ButtonType == ButtonType.Regular)
            UISounds.instance.PlayRegularButton();

        if (ButtonType == ButtonType.Large)
            UISounds.instance.PlayLargeButton();

        if (ButtonType == ButtonType.Tab)
            UISounds.instance.PlayTabButton();

        if (ButtonType == ButtonType.Close)
            UISounds.instance.PlayCloseButton();

        if (ButtonType == ButtonType.Journal)
            UISounds.instance.PlaySmallButton();

        TurnOffRollOverOnPress();
    }



    void MouseOn()
    {
        if (noRollver)
            return;

        if (ButtonType == ButtonType.Small)
            ButtonSprite.GetComponent<Image>().sprite = ButtonList.instance.ButtonsSmall[1];

        if (ButtonType == ButtonType.Regular)
            ButtonSprite.GetComponent<Image>().sprite = ButtonList.instance.ButtonsMedium[1];

        if (ButtonType == ButtonType.Large)
            ButtonSprite.GetComponent<Image>().sprite = ButtonList.instance.ButtonsLarge[1];

        if (ButtonType == ButtonType.Tab)
            ButtonSprite.GetComponent<Image>().sprite = ButtonList.instance.ButtonsTab[1];
    }

    void MouseOff()
    {
        if (noRollver)
            return;

        if (ButtonType == ButtonType.Small)
            ButtonSprite.GetComponent<Image>().sprite = ButtonList.instance.ButtonsSmall[0];

        if (ButtonType == ButtonType.Regular)
            ButtonSprite.GetComponent<Image>().sprite = ButtonList.instance.ButtonsMedium[0];

        if (ButtonType == ButtonType.Large)
            ButtonSprite.GetComponent<Image>().sprite = ButtonList.instance.ButtonsLarge[0];

        if (ButtonType == ButtonType.Tab)
            ButtonSprite.GetComponent<Image>().sprite = ButtonList.instance.ButtonsTab[0];
    }



    public void TurnOffRollOverOnPress()
    {
        MouseOff();
    }
}
