using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    public static WorldMap instance;

    [Header("Areas Unlocked")]
    public List<int> AreasUnlocked = new List<int>();



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        UnlockStartingAreas();
    }



    // unlock starting areas
    public void UnlockStartingAreas()
    {
        AreasUnlocked.Add(0);
        //AreasUnlocked.Add(1);
        //AreasUnlocked.Add(2);
    }
}
