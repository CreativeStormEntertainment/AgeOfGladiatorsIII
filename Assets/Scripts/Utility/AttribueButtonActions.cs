using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttribueButtonActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        UISounds.instance.PlayPipButton();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        

        if (GetComponent<Button>().interactable)
        {
            GetComponentInParent<AttributeBox>().ButtonOver();
            UISounds.instance.PlayMouseOn(3);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
            GetComponentInParent<AttributeBox>().ButtonExit();
    }
}
