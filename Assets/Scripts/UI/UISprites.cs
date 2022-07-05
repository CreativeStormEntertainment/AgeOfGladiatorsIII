using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISprites : MonoBehaviour
{
    public static UISprites instance;

    public List<Sprite> MissionComponents = new List<Sprite>();
    public List<Sprite> Missions = new List<Sprite>();
    public List<Sprite> Item = new List<Sprite>();

    public List<Sprite> Icons = new List<Sprite>();

    public List<Sprite> LoadingImages = new List<Sprite>();


    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
