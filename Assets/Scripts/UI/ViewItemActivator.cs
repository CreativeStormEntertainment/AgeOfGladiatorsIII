using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewItemActivator : MonoBehaviour
{
    [HideInInspector]
    public ViewItemBubble Bubble;
    [HideInInspector]
    public Vector3 Position;
    [HideInInspector]
    public bool active;



    void Update()
    {
        if (Bubble != null)
        {
            Camera _Camera = PlayerScene.instance.SceneCamera;

            ViewItemPosition(_Camera);
        }
    }



    // view item blurb
    public void ViewBlurb()
    {
        ViewItemActivator _Activator = GetComponent<ViewItemActivator>();

        if (!_Activator.active)
        {
            // activate speech bubble
            GameObject _speechPrefab = Instantiate(Resources.Load("UI-ViewItemBubble")) as GameObject;
            _speechPrefab.transform.SetParent(UI.instance.SpeechBubbles.transform, false);

            _Activator.Bubble = _speechPrefab.GetComponent<ViewItemBubble>();

            _Activator.Bubble.ForumulateViewBlurb(GetComponent<TriggerViewItem>().viewContent, _Activator);
        }
    }



    // view item position to node
    void ViewItemPosition(Camera _Camera)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "SpeechNode")
            {
                Vector3 _newPosition = child.transform.position;
                _newPosition = _Camera.WorldToScreenPoint(_newPosition);

                Bubble.transform.position = _newPosition;
            }
        }
    }
}
