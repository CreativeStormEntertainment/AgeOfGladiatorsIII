using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonList : MonoBehaviour
{
    public static ButtonList instance;

    public List<Sprite> ButtonsSmall = new List<Sprite>();
    public List<Sprite> ButtonsMedium = new List<Sprite>();
    public List<Sprite> ButtonsLarge = new List<Sprite>();
    public List<Sprite> ButtonsTab = new List<Sprite>();
    public List<Sprite> ButtonAction = new List<Sprite>();

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
