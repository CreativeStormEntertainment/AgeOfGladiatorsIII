using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using PixelCrushers.DialogueSystem.UnityGUI;

public class UI : MonoBehaviour
{
    public static UI instance;

    public GameObject canvasObj;
    public GameObject miscObj;
    private bool activeUI;

    [Header("Screens")]
    public MissionBox MissionBox;
    public LevelBox LevelBox;
    public UpdateBox UpdateBox;
    public UpdateBox CrimeBox;
    public UtilityBox UtilitySlider;
    public SmallBox SmallBox;

    public Menu MenuScreen;
    public InventoryWindow InventoryScreen;
    public VendorWindow VendorScreen;
    public LootWindow LootScreen;
    public MapWindow MapScreen;
    public TravelWindow TravelScreen;
    public CraftWindow CraftScreen;
    public CharacterWindow CharacterScreen;
    public CrimeTerminalWindow CrimeTerminalScreen;
    public ConclusionWindow ConclusionScreen;
    public CombatUI CombatUI;

    [Header("Quest System")]
    public MissionBox QuestBox;
    public PixelCrushers.DialogueSystem.QuestLogWindow QuestLogWindow;

    [Header("Crime")]
    public Crime ReportedCrime;
    public bool reportCrimeOnConversationEnd;

    [Header("Other")]
    public GameObject SpeechBubbles;
    public FadeBlack FadeScreen;
    public FadeBlack FadeInOutScreen;
    public FadeBlackTravel FadeInOutTravelScreen;
    public GameObject UtilityPanel;

    public bool availableToLoot = true;
    public bool reportTravelOnConversationEnd;
    public bool characterDidNotTravel;
    public bool waitingForConclusion;

    public TextMeshProUGUI DemoLabel;



    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        ResetAll();

        FadeInOutScreen.gameObject.SetActive(false);

        LevelBox.gameObject.SetActive(false);
        UpdateBox.gameObject.SetActive(false);
        CrimeBox.gameObject.SetActive(false);
        SmallBox.gameObject.SetActive(false);

        CombatUI.gameObject.SetActive(false);

        DemoLabel.gameObject.SetActive(false);

        StartCoroutine(ShowUtility());

        //if (QuestLogWindow == null) 
        //    QuestLogWindow = FindObjectOfType<PixelCrushers.DialogueSystem.QuestLogWindow>();
    }

    void Update()
    {
        if (Dredd.instance.isLoading)
            return;

        if (activeUI)
            KeyPresses();
    }



    //  enable/disable UI
    public void ActivateUI(bool _active)
    {
        activeUI = _active;
        canvasObj.SetActive(_active);
        miscObj.SetActive(_active);
    }

    // reset ui on new game
    public void ResetUI()
    {
        instance = null;
        Destroy(gameObject);
    }



    // check keypresses
    void KeyPresses()
    {
        // -------------------------------------------------
        // menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuScreen.gameObject.activeSelf)
                CloseMenu();
            else
                OpenMenu();
        }
        // -------------------------------------------------

        // -------------------------------------------------
        // inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryScreen.gameObject.activeSelf)
                CloseInventory();
            else
                OpenInventory();
        }
        // -------------------------------------------------

        // -------------------------------------------------
        // journal
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (QuestLogWindow.gameObject.activeSelf)
                CloseJournal();
            else
                OpenJournal();
        }
        // -------------------------------------------------

        // -------------------------------------------------
        // character
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CharacterScreen.gameObject.activeSelf)
                CloseCharacter();
            else
                OpenCharacter();
        }
        // -------------------------------------------------

        // -------------------------------------------------
        // map
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MapScreen.gameObject.activeSelf)
                CloseMap();
            else
                OpenMap();
        }
        // -------------------------------------------------

        // -------------------------------------------------
        // reload
        if (Input.GetKeyDown(KeyCode.R) && !Combat.instance.combatActivated)
        {
            Action _action = new Action(CombatActions.Reload);
            StartCoroutine(_action.ReloadAnimationOutOfCombat());

        }
        // -------------------------------------------------

        // -------------------------------------------------
        // highlight all
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            HighlightAll();
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            // outlines
            var FoundObjects = FindObjectsOfType<Outline>();

            foreach (Outline _Outline in FoundObjects)
                _Outline.enabled = false;

            // nameplates
            var FoundObjectsTwo = FindObjectsOfType<NamePlateActivator>();

            foreach (NamePlateActivator _NamePlate in FoundObjectsTwo)
                _NamePlate.GetComponent<NamePlateActivator>().DeactivateNamePlate();
        }
        // -------------------------------------------------
    }

    // reset all
    void ResetAll()
    {
        UtilitySlider.gameObject.SetActive(false);
        MenuScreen.gameObject.SetActive(false);
        InventoryScreen.gameObject.SetActive(false);
        VendorScreen.gameObject.SetActive(false);
        QuestLogWindow.gameObject.SetActive(false);
        LootScreen.gameObject.SetActive(false);
        MapScreen.gameObject.SetActive(false);
        TravelScreen.gameObject.SetActive(false);
        CraftScreen.gameObject.SetActive(false);
        CharacterScreen.gameObject.SetActive(false);
        CrimeTerminalScreen.gameObject.SetActive(false);
        ConclusionScreen.gameObject.SetActive(false);
    }



    // show utility
    IEnumerator ShowUtility()
    {
        while (LoadingScreen.instance.LoadingImage.gameObject.activeSelf)
            yield return null;

        UtilityPanel.gameObject.SetActive(true);
    }



    // open window
    void OpenWindow()
    {
        CloseTooltips();

        ResetAll();

        PlayerScene.instance.MainCharacter.GetComponent<Movement>().restrictMovement = true;

        UISounds.instance.PlayWindowOpen();
    }

    // close window
    void CloseWindow()
    {
        CloseTooltips();

        MapScreen.ActivateMapIcons(false);

        ResetAll();

        PlayerScene.instance.MainCharacter.GetComponent<Movement>().restrictMovement = false;

        if (!UISounds.Audio.isPlaying)
            UISounds.instance.PlayCloseButton();
    }



    // open inventory
    public void OpenInventory()
    {
        OpenWindow();
        InventoryScreen.SelectedItemPanel.gameObject.SetActive(false);
        InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
        InventoryScreen.Populate();
        InventoryScreen.gameObject.SetActive(true);
    }

    // close inventory
    public void CloseInventory()
    {
        CloseWindow();
        InventoryScreen.gameObject.SetActive(false);
    }



    // open inventory
    public void OpenJournal()
    {
        OpenWindow();

        QuestLogWindow.gameObject.SetActive(true);
        QuestLogWindow.Open();
    }

    // close inventory
    public void CloseJournal()
    {
        CloseWindow();

        QuestLogWindow.gameObject.SetActive(false);
        QuestLogWindow.Close();
    }



    // open map
    public void OpenMap()
    {
        OpenWindow();
        MapScreen.Populate();
        MapScreen.gameObject.SetActive(true);
    }

    // close map
    public void CloseMap()
    {
        CloseWindow();
        MapScreen.Close();
        MapScreen.gameObject.SetActive(false);
    }



    // open travel
    public void OpenTravel()
    {
        characterDidNotTravel = true;

        OpenWindow();

        TravelScreen.PopulateMap();
        TravelScreen.gameObject.SetActive(true);
    }

    // close travel
    public void CloseTravel()
    {
        if (characterDidNotTravel)
            ResetCharacter();

        FadeScreen.gameObject.SetActive(true);
        FadeScreen.StartCoroutine(FadeScreen.ActivateFade());

        CloseWindow();
        TravelScreen.gameObject.SetActive(false);
    }



    // open craft
    public void OpenCraft()
    {
        OpenWindow();
        CraftScreen.Populate();
        CraftScreen.gameObject.SetActive(true);
    }

    // close craft
    public void CloseCraft()
    {
        CloseWindow();
        CraftScreen.gameObject.SetActive(false);
    }



    // open crime terminal
    public void OpenCrimeTerminal()
    {
        OpenWindow();
        CrimeTerminalScreen.Populate();
        CrimeTerminalScreen.gameObject.SetActive(true);
    }

    // close crime terminal
    public void CloseCrimeTerminal()
    {
        CloseWindow();
        CrimeTerminalScreen.gameObject.SetActive(false);
    }



    // open character
    public void OpenCharacter()
    {
        OpenWindow();
        CharacterScreen.Populate();
        CharacterScreen.gameObject.SetActive(true);
    }

    // close craft
    public void CloseCharacter()
    {
        CloseWindow();
        CharacterScreen.gameObject.SetActive(false);
    }




    // open vendor
    public void OpenVendor(int _vendorNumber)
    {
        OpenWindow();
        VendorScreen.SelectedItemPanel.gameObject.SetActive(false);
        VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
        VendorScreen.Populate(_vendorNumber);
        VendorScreen.gameObject.SetActive(true);
    }

    // close vendor
    public void CloseVendor()
    {
        CloseWindow();
        VendorScreen.gameObject.SetActive(false);
    }



    // open loot (container)
    public void OpenLoot(MapItemContainer _Container)
    {
        if (_Container == null)
            return;

        if (_Container.GetComponent<AudioCue>() != null)
            _Container.GetComponent<AudioCue>().PlayCue();

        OpenWindow();
        LootScreen.SelectedItemPanel.gameObject.SetActive(false);
        LootScreen.Populate(_Container);
        LootScreen.gameObject.SetActive(true);
    }

    // close loot
    public void CloseLoot()
    {
        CloseWindow();
        PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().movingToInteractableTarget = false;
        LootScreen.gameObject.SetActive(false);
    }



    // open mission box
    public void OpenQuestBox(string _quest, double _questEntry)
    {
        StartCoroutine(WaitToOpenQuestBox(_quest, _questEntry));
    }

    // wait to open level box
    public IEnumerator WaitToOpenQuestBox(string _quest, double _questEntry)
    {
        while (Combat.instance.combatActivated || waitingForConclusion)
            yield return null;

        QuestBox.gameObject.SetActive(true);
        QuestBox.Populate(_quest, _questEntry);
        StartCoroutine("CloseQuestnBoxTimer");
    }

    // closing timer
    IEnumerator CloseQuestnBoxTimer()
    {
        yield return new WaitForSeconds(4);
        CloseQuestBox();
    }

    // close mission box
    public void CloseQuestBox()
    {
        QuestBox.gameObject.SetActive(false);
    }



    // open menu
    public void OpenMenu()
    {
        OpenWindow();
        MenuScreen.gameObject.SetActive(true);
    }

    // close menu
    public void CloseMenu()
    {
        CloseWindow();
        MenuScreen.gameObject.SetActive(false);
    }



    // start conclusion timer
    public void StartConclusionTimer(string _name)
    {
        waitingForConclusion = true;
        GameMusic.instance.FadeOutMusic(1f);
        StartCoroutine(OpenConclusionTimer(_name));
    }

    // conclusion timer
    public IEnumerator OpenConclusionTimer(string _name)
    {
        yield return new WaitForSeconds(1f);
        OpenConclusion(_name);
    }

    // open conclusion
    public void OpenConclusion(string _name)
    {
        OpenWindow();

        ConclusionScreen.Populate(_name);
        ConclusionScreen.gameObject.SetActive(true);
    }

    // close conclusion
    public void CloseConclusion()
    {
        waitingForConclusion = false;

        CloseWindow();
        ConclusionScreen.Close();
        ConclusionScreen.gameObject.SetActive(false);

        FadeScreen.gameObject.SetActive(true);
        FadeScreen.StartCoroutine(FadeScreen.ActivateFade());

        GameActions.instance.showEvent = false;
        GameActions.instance.eventName = "";
    }



    // start end game timer
    public void StartEndGameTimer()
    {
        GameMusic.instance.FadeOutMusic(1f);
        StartCoroutine(OpenEndGameTimer());
    }

    // end game timer
    public IEnumerator OpenEndGameTimer()
    {
        yield return new WaitForSeconds(1f);
        OpenEndGame();
    }

    // open end game
    public void OpenEndGame()
    {
        OpenWindow();

        ConclusionScreen.PopulateEndGame();
        ConclusionScreen.gameObject.SetActive(true);
    }

    // close end game
    public void CloseEndGame()
    {
        CloseWindow();
        ConclusionScreen.CloseEndGame();
        ConclusionScreen.gameObject.SetActive(false);
    }



    // open level
    public void OpenLevel()
    {
        StartCoroutine(WaitToOpenLevelBox());
    }

    // wait to open level box
    public IEnumerator WaitToOpenLevelBox()
    {
        yield return new WaitForSeconds(2.5f);

        while (MissionBox.gameObject.activeSelf || waitingForConclusion) // need waiting for conclusion rather than conclusion screen active self
            yield return null;

        GameMusic.instance.FadeOutMusic(1f);
        GameMusicCues.instance.PlayMusicCue(MusicCues.LevelUp);

        LevelBox.gameObject.SetActive(false);
        LevelBox.PopulateLevel();
        LevelBox.gameObject.SetActive(true);

        StartCoroutine(LevelBox.CloseLevelBox());
    }

    // open rank
    public void OpenRank()
    {
        UpdateBox.gameObject.SetActive(false);
        UpdateBox.PopulateRank();
        UpdateBox.gameObject.SetActive(true);
    }



    // open rank (box)
    public void OpenTravelUpdate()
    {
        UpdateBox.gameObject.SetActive(false);
        UpdateBox.PopulateTravel();
        UpdateBox.gameObject.SetActive(true);
    }

    // open crime solved (box)
    public void OpenCrimeSolved(Crime _Crime)
    {
        CrimeBox.gameObject.SetActive(false);
        CrimeBox.PopulateCrime(_Crime);
        CrimeBox.gameObject.SetActive(true);
    }

    // open crime solved (box)
    public void OpenSmallBox(string _label)
    {
        SmallBox.gameObject.SetActive(false);
        SmallBox.Populate(_label);
        SmallBox.gameObject.SetActive(true);
    }



    // open utility
    public void OpenUtility()
    {
        UtilitySlider.gameObject.SetActive(false);
        UtilitySlider.Populate();
        UtilitySlider.gameObject.SetActive(true);
    }



    // open end demo message
    public void OpenEndDemoMessage()
    {
        DemoLabel.gameObject.SetActive(true);
    }



    // close all tooltips
    void CloseTooltips()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject _object in allObjects)
        {
            if (_object.activeInHierarchy)
            {
                if (_object.GetComponent<ModelShark.TooltipTrigger>() != null)
                    _object.GetComponent<ModelShark.TooltipTrigger>().StopHover();
            }  
        }
    }

    // reset character from travel window close (without travelling)
    public void ResetCharacter()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject _object in allObjects)
        {
            if (_object.activeInHierarchy)
            {
                if (_object.GetComponent<Map>() != null)
                    _object.GetComponent<Map>().PlaceMainCharacterOnEntryNode();
            }
        }
    }



    // hightlight from alt
    void HighlightAll()
    {
        // --------------------------
        // outline
        var FoundObjects = FindObjectsOfType<Outline>();

        foreach (Outline _Outline in FoundObjects)
        {
            _Outline.highlightAvailable = true;


            // --------------------------
            // check
            if (_Outline.gameObject.GetComponent<MapItem>() != null)
            {
                if (!CheckIfCanInteract.CheckForInteractableConditions(_Outline.gameObject.GetComponent<MapItem>()))
                    _Outline.highlightAvailable = false;
            }

            if (_Outline.gameObject.GetComponent<NPC>() != null)
            {
                if (!CheckIfCanInteract.CheckForInteractableConditions(_Outline.gameObject.GetComponent<NPC>()))
                    _Outline.highlightAvailable = false;
            }
            // --------------------------

            // --------------------------
            // enable
            if (_Outline.highlightAvailable)
                _Outline.enabled = true;
            // --------------------------
        }
        // --------------------------


        // --------------------------
        // nameplates
        var FoundObjectsTwo = FindObjectsOfType<NamePlateActivator>();

        foreach (NamePlateActivator _NamePlate in FoundObjectsTwo)
        {
            if (_NamePlate.GetComponent<PlayerCharacter>() == null)
                _NamePlate.GetComponent<NamePlateActivator>().ActivateNamePlate(false);
        }
        // --------------------------
    }
}
