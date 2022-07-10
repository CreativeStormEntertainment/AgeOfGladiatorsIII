using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickCircle : MonoBehaviour
{
    public static MouseClickCircle instance;

    [Header("Mouse Click Icon")]
    public SpriteRenderer MouseIcon;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}