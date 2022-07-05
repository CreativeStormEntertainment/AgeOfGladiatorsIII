using UnityEngine;
using PixelCrushers.DialogueSystem;

public class GameActions : MonoBehaviour
{
    public static GameActions instance;

    public bool showEvent;
    public string eventName;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }



    // end conversation
    public void EndConversation()
    {
        // report travel (if applicable)
        if (UI.instance.reportTravelOnConversationEnd)
            UI.instance.OpenTravelUpdate();

        // open conclusion (if applicable)
        if (showEvent)
        {
            UI.instance.StartConclusionTimer(eventName);

            UI.instance.FadeInOutScreen.gameObject.SetActive(true);
            UI.instance.FadeInOutScreen.StartCoroutine(UI.instance.FadeInOutScreen.ActivateFadeInOut());
        }

        // check start combat
        CheckStartCombat();
    }
    
    public void CheckStartCombat()
    {
        // -----------------------------
        // find open map
        Map OpenMap = null;

        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject _object in allObjects)
        {
            if (_object.activeInHierarchy)
            {
                if (_object.GetComponent<Map>() != null)
                    OpenMap = _object.GetComponent<Map>();
            }
        }
        // -----------------------------

        // -----------------------------
        // check for combat if anyone has turned hostile
        bool _cueAmbient = true;

        // find if any hostile (eventually add distance check for hostile as well)
        foreach (Transform child in OpenMap.MapNPCS.transform)
        {
            if (child.GetComponent<NPC>().isHostile && !Combat.instance.combatActivated)
            {
                Combat.instance.combatActivated = true;
                StartCoroutine(Combat.instance.StartCombatCoroutine());

                _cueAmbient = false;
            }
        }
        // -----------------------------

        // -----------------------------
        // cue back to ambient (if not combat)
        if (GameMusic.Audio.clip == GameMusic.instance.EventMusic[0] && _cueAmbient)
            GameMusic.instance.TransitionToAmbient(2f);
        // -----------------------------
    }



    // event screen settings
    public void EventScreenTrigger(string _name)
    {
        showEvent = true;
        eventName = _name;
    }

    public void CleanAfterEventScreen(string _name)
    {
        PlayerScene.instance.MainCharacter.weaponDrawn = false;
        PlayerScene.instance.MainCharacter.GetComponentInChildren<BodyManager>().HideWeapon();

        switch (eventName)
        {
            case "PeaceApes":
            case "DiseaseApes":
            case "FleeApes":
                DeactivateNPC("Gangster Ape");
                DeactivateNPC("Ape Thug");
                DeactivateNPC("Ape Henchman");
                break;

            case "PeaceBabyBobNicely":
                ChangeAnimationNPC("Baby Bob Nicely");
                ChangeAnimationNPC("Camera Operator");
                ChangeAnimationNPC("Tech");
                ChangeAnimationNPC("Assistant");
                ChangeAnimationNPC("Sheldon Whitley");
                break;

            case "YouBetYourLifeTerminal":
                DeactivateNPC("Assistant");
                DeactivateNPC("Sheldon Whitley");
                ActivateHiddenNPC("Judge Johnson");
                ActivateHiddenNPC("Judge Torres");
                ActivateHiddenNPC("Judge Winston");
                ActivateHiddenNPC("Judge Lucas");
                ActivateHiddenNPC("Judge Butler");
                ActivateHiddenNPC("Judge Douglas");
                ActivateHiddenNPC("Judge Talbot");
                break;

            case "NomiSmith":
                DeactivateNPC("Nomi Smith");
                break;

            case "TimLunaDeadHostageHurt":
            case "TimLunaDeadHostageSafe":
            case "TimLunaDeadHostageDead":
            case "TimLunaAliveHostageHurt":
            case "TimLunaPeace":
                DeactivateNPC("Tim Luna");
                DeactivateNPC("Linda Dooley");
                break;
        }
    }



    // player
    public double GetSkill(string _Input)
    {
        double _stat = 0;

        switch (_Input)
        {
            case "Intimidation":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Intimidation);
                break;
            case "Persuasion":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Persuasion);
                break;
            case "Law":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Law);
                break;
            case "StreetSmarts":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.StreetSmarts);
                break;
            case "Perception":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Perception);
                break;
            case "Science":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Science);
                break;
            case "Engineering":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Engineering);
                break;
            case "Computers":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Computers);
                break;
            case "Medicine":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Medicine);
                break;
            case "Demolitions":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Demolitions);
                break;
            case "Lockpicking":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Lockpicking);
                break;

            case "Psionics":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Psionics);
                break;
            case "Brawling":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Brawling);
                break;
            case "Shooting":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Shooting);
                break;
            case "Special Ammo":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.SpecialAmmo);
                break;
            case "Heavy Weapons":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.HeavyWeapons);
                break;
            case "Athletics":
                _stat = (double)PlayerScene.instance.MainCharacter.GetSkill(Skill.Athletics);
                break;

        }

        return _stat;
    }

    public int GetCredits()
    {
        return PlayerScene.instance.MainCharacter.credits;
    }

    public void DeductCredits(double _amount)
    {
        PlayerScene.instance.MainCharacter.DeductMoney((int)_amount);
    }

    public void GainCredits(double _amount)
    {
        PlayerScene.instance.MainCharacter.EarnMoney((int)_amount);
    }

    public void ShowWeapon()
    {
        PlayerScene.instance.MainCharacter.weaponDrawn = true;
        PlayerScene.instance.MainCharacter.GetComponentInChildren<BodyManager>().ShowWeapon();
    }



    // npc
    public void KillNPC(string _name, double _deathType)
    {
        // -----------------------------
        // find open map
        Map OpenMap = LocateOpenMap.GetOpenMap();
        // -----------------------------

        // -----------------------------
        // locate applicable npcs
        foreach (Transform child in OpenMap.MapNPCS.transform)
        {
            if (child.GetComponent<NPC>() != null)
            {
                if (child.GetComponent<NPC>().characterName == _name && child.GetComponent<NPC>().name != "NPC-Instantiate(Clone)")
                {
                    if (!child.GetComponent<NPC>().GetComponent<NPC>().isDead)
                        child.GetComponent<NPC>().KillCharacter((int)_deathType);
                }
            }
        }
        // -----------------------------
    }

    public void AggroNPC(string _name)
    {
        // -----------------------------
        // find open map
        Map OpenMap = LocateOpenMap.GetOpenMap();
        // -----------------------------

        // -----------------------------
        // locate applicable npcs
        foreach (Transform child in OpenMap.MapNPCS.transform)
        {
            if (child.GetComponent<NPC>() != null)
            {
                if (child.GetComponent<NPC>().characterName == _name && child.GetComponent<NPC>().name != "NPC-Instantiate(Clone)")
                    child.GetComponent<NPC>().isHostile = true;
            }
        }
        // -----------------------------
    }

    public void ActivateHiddenNPC(string _name)
    {
        // -----------------------------
        // find open map
        Map OpenMap = LocateOpenMap.GetOpenMap();
        // -----------------------------

        // -----------------------------
        // locate applicable npcs
        foreach (Transform child in OpenMap.MapNPCS.transform)
        {
            if (child.GetComponent<NPC>() != null)
            {
                if (child.GetComponent<NPC>().characterName == _name && child.GetComponent<NPC>().name != "NPC-Instantiate(Clone)")
                    child.GetComponent<NPC>().gameObject.SetActive(true);
            }
        }
        // -----------------------------
    }

    public void DeactivateNPC(string _name)
    {
        // -----------------------------
        // find open map
        Map OpenMap = LocateOpenMap.GetOpenMap();
        // -----------------------------

        // -----------------------------
        // locate applicable npcs
        foreach (Transform child in OpenMap.MapNPCS.transform)
        {
            if (child.GetComponent<NPC>() != null)
            {
                if (child.GetComponent<NPC>().characterName == _name && child.GetComponent<NPC>().name != "NPC-Instantiate(Clone)")
                    child.GetComponent<NPC>().gameObject.SetActive(false);
            }
        }
        // -----------------------------
    }

    public void ChangeAnimationNPC(string _name)
    {
        // -----------------------------
        // find open map
        Map OpenMap = LocateOpenMap.GetOpenMap();
        // -----------------------------

        // -----------------------------
        // locate applicable npcs (refactor this to accomodate many animations)
        foreach (Transform child in OpenMap.MapNPCS.transform)
        {
            if (child.GetComponent<NPC>() != null)
            {
                if (child.GetComponent<NPC>().characterName == _name && child.GetComponent<NPC>().name != "NPC-Instantiate(Clone)")
                    child.GetComponent<NPC>().GetComponent<CharacterAnimator>().IdleAnimation = IdleAnimationType.IdleHostage;
            }
        }
        // -----------------------------
    }



    // utility
    public void OpenVendor()
    {
        int _vendorNumber = DialogueManager.currentConversant.GetComponent<NPC>().vendorNumber;
        UI.instance.OpenVendor(_vendorNumber);
    }

    public void OpenDoor()
    {
        MapItem _Item = DialogueManager.currentConversant.GetComponent<MapItem>();

        // open any attached doors
        if (_Item.GetComponent<OpenDoor>() != null)
            _Item.GetComponent<OpenDoor>().OpenAttachedDoors();

        // activate travel nodes
        if (_Item.GetComponent<ActivateTravelNode>() != null)
            _Item.GetComponent<ActivateTravelNode>().ActivateNode();
    }



    // quest
    public void StartQuest(string _questName)
    {
        QuestLog.SetQuestState(_questName, "active");

        UI.instance.OpenQuestBox(_questName, 1);
    }

    public void EndQuest(string _questName)
    {
        QuestLog.SetQuestState(_questName, "success");

        PlayerScene.instance.MainCharacter.GainExperience(GameData.largeMissionXP);

        OpenQuestBox(_questName, QuestLog.GetQuestEntryCount(_questName));
    }

    public void AdvanceQuest(string _questName, double _questEntry)
    {
        QuestLog.SetQuestEntryState(_questName, _questEntry, "success");

        //Debug.Log(_questName + " " + _questEntry + "/" + QuestLog.GetQuestEntryCount(_questName));

        if (_questEntry < QuestLog.GetQuestEntryCount(_questName))
        {
            PlayerScene.instance.MainCharacter.GainExperience(GameData.smallMissionXP);
            QuestLog.SetQuestEntryState(_questName, _questEntry + 1, "active");
            OpenQuestBox(_questName, _questEntry);
        }
        else
        {
            EndQuest(_questName);
        }
    }

    public void OpenQuestBox(string _questName, double _questEntry)
    {
        UI.instance.OpenQuestBox(_questName, _questEntry);
    }



    // movement node
    public void ActivateMovementNode(double _index)
    {
        // -----------------------------
        // find open map
        Map OpenMap = LocateOpenMap.GetOpenMap();
        // -----------------------------

        // -----------------------------
        // locate applicable movement node
        OpenMap.MovementNodes[(int)_index].disabled = false;
        // -----------------------------
    }

    public void ActivateTravelMapNode(double _index)
    {
        WorldMap.instance.AreasUnlocked.Add((int)_index);

        UI.instance.reportTravelOnConversationEnd = true;
    }



    // register/unregister methods with lua
    private void OnEnable()
    {
        Lua.RegisterFunction("EventScreenTrigger", this, SymbolExtensions.GetMethodInfo(() => EventScreenTrigger(string.Empty)));

        Lua.RegisterFunction("GetSkill", this, SymbolExtensions.GetMethodInfo(() => GetSkill(string.Empty)));
        Lua.RegisterFunction("GetCredits", this, SymbolExtensions.GetMethodInfo(() => GetCredits()));
        Lua.RegisterFunction("DeductCredits", this, SymbolExtensions.GetMethodInfo(() => GainCredits((double)0)));
        Lua.RegisterFunction("GainCredits", this, SymbolExtensions.GetMethodInfo(() => DeductCredits((double)0)));

        Lua.RegisterFunction("KillNPC", this, SymbolExtensions.GetMethodInfo(() => KillNPC((string.Empty), (double)0)));
        Lua.RegisterFunction("AggroNPC", this, SymbolExtensions.GetMethodInfo(() => AggroNPC(string.Empty)));
        Lua.RegisterFunction("ActivateHiddenNPC", this, SymbolExtensions.GetMethodInfo(() => ActivateHiddenNPC(string.Empty)));
        Lua.RegisterFunction("DeactivateNPC", this, SymbolExtensions.GetMethodInfo(() => DeactivateNPC(string.Empty)));

        Lua.RegisterFunction("OpenVendor", this, SymbolExtensions.GetMethodInfo(() => OpenVendor()));
        Lua.RegisterFunction("OpenDoor", this, SymbolExtensions.GetMethodInfo(() => OpenDoor()));

        Lua.RegisterFunction("StartQuest", this, SymbolExtensions.GetMethodInfo(() => StartQuest(string.Empty)));
        Lua.RegisterFunction("EndQuest", this, SymbolExtensions.GetMethodInfo(() => EndQuest(string.Empty)));
        Lua.RegisterFunction("AdvanceQuest", this, SymbolExtensions.GetMethodInfo(() => AdvanceQuest((string.Empty), (double)0)));
        Lua.RegisterFunction("OpenQuestBox", this, SymbolExtensions.GetMethodInfo(() => OpenQuestBox((string.Empty), (double)0)));

        Lua.RegisterFunction("ActivateMovementNode", this, SymbolExtensions.GetMethodInfo(() => ActivateMovementNode((double)0)));
        Lua.RegisterFunction("ActivateTravelMapNode", this, SymbolExtensions.GetMethodInfo(() => ActivateTravelMapNode((double)0)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("EventScreenTrigger");

        Lua.UnregisterFunction("GetSkill");
        Lua.UnregisterFunction("GetCredits");
        Lua.UnregisterFunction("DeductCredits");
        Lua.UnregisterFunction("GainCredits");

        Lua.UnregisterFunction("AggroNPC");
        Lua.UnregisterFunction("ActivateHiddenNPC");
        Lua.UnregisterFunction("DeactivateNPC");
        Lua.UnregisterFunction("OpenVendor");
        Lua.UnregisterFunction("OpenDoor");
        Lua.UnregisterFunction("Crime");

        Lua.UnregisterFunction("StartQuest");
        Lua.UnregisterFunction("EndQuest");
        Lua.UnregisterFunction("AdvanceQuest");
        Lua.UnregisterFunction("OpenQuestBox");

        Lua.UnregisterFunction("ActivateMovementNode");
        Lua.UnregisterFunction("ActivateTravelMapNode");

        Lua.UnregisterFunction("MaxNormal");
        Lua.UnregisterFunction("MaxNormalRadio");
        Lua.UnregisterFunction("GangsterApe");
    }
}
