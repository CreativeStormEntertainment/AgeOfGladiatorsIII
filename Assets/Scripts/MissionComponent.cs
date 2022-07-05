using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionComponent
{
    //public int componentNumber;
    //public string componentDescription;
    //public int componentXP;

    //public bool componentLocked;
    //public bool componentComplete;

    //public int componentCountRequired;
    //public int componentCount;

    //public List<MissionComponentSub> SubComponents = new List<MissionComponentSub>();



    //public MissionComponent(int _componentNumber, bool _locked, int _xp, string _description, int _componentCount)
    //{
    //    componentNumber = _componentNumber;
    //    componentLocked = _locked;
    //    componentDescription = _description;
    //    componentXP = _xp;
    //    componentCountRequired = _componentCount;
    //}



    //// add sub-component
    //public void AddMissionSubComponent(int _number, int _xp, string _description)
    //{
    //    MissionComponentSub _SubComponent = new MissionComponentSub(_number, _xp, _description);

    //    SubComponents.Add(_SubComponent);
    //}



    //// resolve component
    //public void ResolveComponent(Mission _Mission)
    //{
    //    // trigger if eligible
    //    if (!CheckAllSubComponentsComplete())
    //        return;

    //    // gain experience
    //    PlayerScene.instance.MainCharacter.GainExperience(componentXP);

    //    // check component count
    //    if (componentCountRequired != 0)
    //        componentCount++;

    //    if (componentCount < componentCountRequired)
    //        return;

    //    // resolve component
    //    componentComplete = true;
    //    MissionList.instance.MissionSpecificEvents(_Mission, componentNumber);

    //    // check resolve mission
    //    _Mission.ResolveMission(componentNumber);

    //    // unlock next component in mission
    //    if (!_Mission.missionComplete)
    //        MissionList.instance.MatchMission(_Mission.MissionName).AttachedComponents[componentNumber + 1].componentLocked = false;

    //    // open mission box
    //    UI.instance.OpenMissionBox(_Mission, componentNumber);
    //}

    //// resolve sub-component
    //public void ResolveSubComponent(int _index)
    //{
    //    if (SubComponents.Count == 0 || _index == 5000)
    //        return;

    //    // gain experience
    //    PlayerScene.instance.MainCharacter.GainExperience(componentXP);

    //    // resolve sub-component
    //    SubComponents[_index].componentSubComplete = true;
    //}



    //// check if all sub-components complete
    //public bool CheckAllSubComponentsComplete()
    //{
    //    bool _complete = true;

    //    foreach (MissionComponentSub _Sub in SubComponents)
    //    {
    //        if (_Sub.componentSubComplete != true)
    //            _complete = false;
    //    }

    //    return _complete;
    //}
}
