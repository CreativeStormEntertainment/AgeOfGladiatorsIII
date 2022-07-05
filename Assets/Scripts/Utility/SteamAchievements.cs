using UnityEngine;
using System.Collections;
using System.ComponentModel;
using Steamworks;


public class SteamAchievements : MonoBehaviour
{
    public static SteamAchievements instance;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }



    // activate achievement
    public void ActivateAchievement(string _achievement)
    {
        if (!SteamManager.Initialized)
            return;

        SteamUserStats.SetAchievement(_achievement);
        SteamUserStats.StoreStats();
    }

    // reset achievements
    public void ResetAchievements()
    {
        Steamworks.SteamUserStats.ResetAllStats(true);
        Steamworks.SteamUserStats.RequestCurrentStats();
    }
}