using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionList : MonoBehaviour
{
    //public static MissionList instance;

    //[Header("Missions")]
    //public List<Mission> MainMissions = new List<Mission>();
    //public List<Mission> SideMissions = new List<Mission>();
    //public List<Mission> CompanionMissions = new List<Mission>();



    //void Awake()
    //{
    //    if (instance == null)
    //        instance = this;
    //    else if (instance != this)
    //        Destroy(gameObject);
    //}

    //void Start()
    //{
    //    CreateMainMissions();
    //    CreateSideMissions();
    //    CreateCompanionMissions();
    //}



    //// create missions (main)
    //public void CreateMainMissions()
    //{
    //    // assign mission list
    //    List<Mission> _MissionListToAdd = MainMissions;

    //    // mission type
    //    int _type = 0;

    //    // create missions
    //    int _index = 0;
    //    _MissionListToAdd.Add(new Mission(_type));
    //    _MissionListToAdd[_index].ActivateMission(); // first one activates here
    //    _MissionListToAdd[_index].MissionName = MissionNames.YouBetYourLife;
    //    _MissionListToAdd[_index].missionNameForLabel = "You Bet Your Life";
    //    _MissionListToAdd[_index].missionDesciptionForLabel = "An illegal gameshow broadcast called You Bet Your Life has been detected, an insidious production where contestants put their lives on the line for grand prizes. After investigating the broadcast, you have been tipped off that a potential informant at the annex skywalk may have more information...";
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(0, false, GameData.smallMissionXP, "Go to the skywalk.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(1, true, GameData.smallMissionXP, "Find the dead-drop radio to contact the informant.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(2, true, GameData.smallMissionXP, "Locate the informant.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(3, true, GameData.smallMissionXP, "Speak with the informant.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(4, true, GameData.smallMissionXP, "Travel to the sewers.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(5, true, GameData.smallMissionXP, "Find a way into the studio.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(6, true, GameData.smallMissionXP, "Arrest or execute the game show host.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(7, true, GameData.smallMissionXP, "Deactivate the transimission.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(8, true, GameData.smallMissionXP, "Report to Judge Talbot.", 0));

    //    _index = 1;
    //    _MissionListToAdd.Add(new Mission(_type));
    //    _MissionListToAdd[_index].MissionName = MissionNames.RobotDisturbance;
    //    _MissionListToAdd[_index].missionNameForLabel = "A Robot Disturbance";
    //    _MissionListToAdd[_index].missionDesciptionForLabel = "You have now found your boat. It is now time to sail the savage coasts...";
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(0, false, GameData.smallMissionXP, "Set sail to the next island.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(1, true, GameData.smallMissionXP, "Find someone who can help outfit your boat.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(2, true, GameData.smallMissionXP, "", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(3, true, GameData.smallMissionXP, "", 0));
    //}

    //// create missions (side)
    //public void CreateSideMissions()
    //{
    //    // assign mission list
    //    List<Mission> _MissionListToAdd = SideMissions;

    //    // mission type
    //    int _type = 1;

    //    // create side missions
    //    int _index = 0;
    //    _MissionListToAdd.Add(new Mission(_type));
    //    _MissionListToAdd[_index].missionNameForLabel = "Future Shock";
    //    _MissionListToAdd[_index].MissionName = MissionNames.FutureShock;
    //    _MissionListToAdd[_index].missionDesciptionForLabel = "You have come across a hostage situation that requires immediate diffusing.";
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(0, false, GameData.smallMissionXP, "Speak with the Tim Luna.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(1, true, GameData.smallMissionXP, "Take care of the situation.", 0));

    //    _index = 1;
    //    _MissionListToAdd.Add(new Mission(_type));
    //    _MissionListToAdd[_index].missionNameForLabel = "A Bounty List...";
    //    _MissionListToAdd[_index].MissionName = MissionNames.ABountyList;
    //    _MissionListToAdd[_index].missionDesciptionForLabel = "You have found a message with a list of kill names on it...";
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(0, false, GameData.smallMissionXP, "You have found a message.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(1, true, GameData.smallMissionXP, "Find the targets on the list.", 0));
    //    _MissionListToAdd[_index].AttachedComponents[1].AddMissionSubComponent(0, GameData.smallMissionXP, "Kill The Eliminator");
    //    _MissionListToAdd[_index].AttachedComponents[1].AddMissionSubComponent(1, GameData.smallMissionXP, "Kill The Dreadnaught");
    //    _MissionListToAdd[_index].AttachedComponents[1].AddMissionSubComponent(2, GameData.smallMissionXP, "Kill The Brutician");
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(2, true, GameData.smallMissionXP, "Return and collect bounty.", 0));

    //    _index = 2;
    //    _MissionListToAdd.Add(new Mission(_type));
    //    _MissionListToAdd[_index].missionNameForLabel = "Street Patrol";
    //    _MissionListToAdd[_index].MissionName = MissionNames.StreetPatrol;
    //    _MissionListToAdd[_index].missionDesciptionForLabel = "You have been instructed to check on three citizens.";
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(0, false, GameData.smallMissionXP, "Receive your instructions.", 0));
    //    _MissionListToAdd[_index].AttachedComponents.Add(new MissionComponent(1, true, GameData.smallMissionXP, "Check or speak with three citizens.", 3));
    //}

    //// create missions (companion)
    //public void CreateCompanionMissions()
    //{
    //    // assign mission list
    //    List<Mission> _MissionListToAdd = CompanionMissions;
    //}



    //// match mission
    //public Mission MatchMission(MissionNames _MissionName)
    //{
    //    Mission _MatchedMission = null;

    //    // ----------------------------------
    //    // main mission (default)
    //    if (MainMissions.Any((Mission) => Mission.MissionName == _MissionName))
    //    {
    //        foreach (Mission _Mission in MainMissions)
    //        {
    //            if (_Mission.MissionName == _MissionName)
    //                _MatchedMission = _Mission;
    //        }
    //    }
    //    // ----------------------------------

    //    // ----------------------------------
    //    // side mission
    //    if (SideMissions.Any((Mission) => Mission.MissionName == _MissionName))
    //    {
    //        foreach (Mission _Mission in SideMissions)
    //        {
    //            if (_Mission.MissionName == _MissionName)
    //                _MatchedMission = _Mission;
    //        }
    //    }
    //    // ----------------------------------

    //    // ----------------------------------
    //    // companion mission
    //    if (CompanionMissions.Any((Mission) => Mission.MissionName == _MissionName))
    //    {
    //        foreach (Mission _Mission in CompanionMissions)
    //        {
    //            if (_Mission.MissionName == _MissionName)
    //                _MatchedMission = _Mission;
    //        }
    //    }
    //    // ----------------------------------

    //    return _MatchedMission;
    //}

    //// activate mission
    //public void ActivateMission(MissionNames _MissionName)
    //{
    //    // do not trigger if label is none
    //    if (_MissionName == MissionNames.None)
    //        return;

    //    // reference mission from proper list (main, side, companion)
    //    Mission _Mission = MatchMission(_MissionName);

    //    // trigger if eligible
    //    if (_Mission.missionComplete || _Mission.missionActive)
    //        return;

    //    // activate mission
    //    _Mission.ActivateMission();

    //    // advance first component of mission
    //    AdvanceMission(_Mission.MissionName, 0, 5000);

    //    // open mission box
    //    UI.instance.OpenMissionBox(_Mission, 0);
    //}

    //// advance mission
    //public void AdvanceMission(MissionNames _MissionName, int _missionComponent, int _missionSubComponent)
    //{
    //    // do not trigger if label is none
    //    if (_MissionName == MissionNames.None)
    //        return;

    //    // match mission with input mission name
    //    Mission _Mission = MatchMission(_MissionName);

    //    // trigger if eligible
    //    if (_Mission.missionComplete || !_Mission.missionActive || _Mission.AttachedComponents[_missionComponent].componentComplete || _Mission.AttachedComponents[_missionComponent].componentLocked)
    //        return;

    //    // resolve sub-component
    //    _Mission.AttachedComponents[_missionComponent].ResolveSubComponent(_missionSubComponent);

    //    // resolve component
    //    _Mission.AttachedComponents[_missionComponent].ResolveComponent(_Mission);
    //}



    //// check mission trigger valid
    //public bool CheckMission(MissionNames _MissionName, int _missionComponent)
    //{
    //    // return true if mission name set to none
    //    if (_MissionName == MissionNames.None)
    //        return false;

    //    // match mission with input mission name
    //    Mission _Mission = MatchMission(_MissionName);

    //    // trigger if eligible
    //    if (_Mission.missionComplete || !_Mission.missionActive || _Mission.AttachedComponents[_missionComponent].componentComplete || _Mission.AttachedComponents[_missionComponent].componentLocked)
    //        return false;
    //    else
    //        return true;
    //}

    //// check if mission can be activated
    //public bool CheckIfMissionCanBeActivated(MissionNames _MissionName)
    //{
    //    bool _clear = true;

    //    // do not trigger if label is none
    //    if (_MissionName == MissionNames.None)
    //        _clear = false;

    //    // reference mission from proper list (main, side, companion)
    //    Mission _Mission = MatchMission(_MissionName);

    //    // trigger if eligible
    //    if (_Mission.missionComplete || _Mission.missionActive)
    //        _clear = false;

    //    return _clear;
    //}



    //// mission specific events
    //public void MissionSpecificEvents(Mission _Mission, int _missionComponent)
    //{
    //    // -------------------------------------
    //    // main missions
    //    switch (_Mission.MissionName)
    //    {
    //    }
    //    // -------------------------------------

    //    // -------------------------------------
    //    // side missions
    //    switch (_Mission.MissionName)
    //    {
    //    }
    //    // -------------------------------------
    //}
}
