using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TravelWindowNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Number")]
    public int nodeNumber;

    [Header("Entry Node")]
    public GameObject EntryNode;

    [Header("Entry Node")]
    public GameObject LockedIcon;


    // on mouse enter (ui event)
    public void OnPointerEnter(PointerEventData eventData)
    {
        // change cursor
        Cursor.SetCursor(Cursors.instance.HandCursor, Vector2.zero, CursorMode.Auto);

        UISounds.instance.PlayMouseOnNode();
    }

    // on mouse exit (ui event)
    public void OnPointerExit(PointerEventData eventData)
    {
        // change cursor
        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
    }

    // on pointer down
    public void OnPointerDown(PointerEventData eventData)
    {
        UISounds.instance.PlayTabButton();
    }
}
