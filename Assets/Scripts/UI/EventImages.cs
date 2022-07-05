using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventImages : MonoBehaviour
{
    public static EventImages instance;

    public List<Sprite> EventSprites;



    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
