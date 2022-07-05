using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursors : MonoBehaviour
{
    public static Cursors instance;

    [Header("Cursors")]
    public Texture2D WaitCursor;
    public Texture2D SpeakCursor;
    public Texture2D HandCursor;
    public Texture2D MapCursor;
    public Texture2D AttackCursor;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (EventSystem.current == null)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
    }
}