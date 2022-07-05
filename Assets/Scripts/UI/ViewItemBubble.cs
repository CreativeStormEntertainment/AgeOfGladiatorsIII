using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewItemBubble : MonoBehaviour
{
    [Header("View Item Text")]
    public Text ViewItemText;

    [Header("View Item Activator")]
    public ViewItemActivator ActivatorPassed;

    [Header("Background Rect")]
    private RectTransform BackgroundRectTransform;



    private void Awake()
    {
        BackgroundRectTransform = GetComponent<RectTransform>();
    }



    // formulate view blurb
    public void ForumulateViewBlurb(string _input, ViewItemActivator _activatorPassed)
    {
        ActivatorPassed = _activatorPassed;
        ActivatorPassed.active = true;

        ViewItemText.text = _input;

        ResizeBubble(2f);

        StartCoroutine(CloseBubble());
    }



    // resize bubble
    void ResizeBubble(float _amount)
    {
        BackgroundRectTransform = GetComponent<RectTransform>();
        float _paddingSize = 24f;
        Vector2 _backgroundSize = new Vector2(ViewItemText.preferredWidth + _paddingSize * _amount, ViewItemText.preferredHeight + _paddingSize * _amount);
        BackgroundRectTransform.sizeDelta = _backgroundSize;
    }

    // close bubble timer
    IEnumerator CloseBubble()
    {
        yield return new WaitForSeconds(2);
        ActivatorPassed.active = false;
        DestroyBubble();
    }

    // destroy bubble
    public void DestroyBubble()
    {
        Destroy(gameObject);
    }
}
