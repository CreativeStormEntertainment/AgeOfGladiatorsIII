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
    public ConclusionWindow ConclusionScreen;
    public CombatUI CombatUI;

    [Header("Quest System")]
    public MissionBox QuestBox;
    public PixelCrushers.DialogueSystem.QuestLogWindow QuestLogWindow;

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
        SmallBox.gameObject.SetActive(false);

        CombatUI.gameObject.SetActive(false);

        DemoLabel.gameObject.SetActive(false);

        StartCoroutine(ShowUtility());
    }

    void Update()
    {
        if (Master.instance.isLoading)
            return;

        if (activeUI)
            KeyPresses();
    }



    public void ActivateUI(bool _active)
    {
        activeUI = _active;
        canvasObj.SetActive(_active);
        miscObj.SetActive(_active);
    }

    public void ResetUI()
    {
        instance = null;
        Destroy(gameObject);
    }



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
        ConclusionScreen.gameObject.SetActive(false);
    }



    IEnumerator ShowUtility()
    {
        while (LoadingScreen.instance.LoadingImage.gameObject.activeSelf)
            yield return null;

        UtilityPanel.gameObject.SetActive(true);
    }



    void OpenWindow()
    {
        CloseTooltips();

        ResetAll();

        PlayerScene.instance.MainCharacter.GetComponent<Movement>().restrictMovement = true;

        UISounds.instance.PlayWindowOpen();
    }

    void CloseWindow()
    {
        CloseTooltips();

        MapScreen.ActivateMapIcons(false);

        ResetAll();

        PlayerScene.instance.MainCharacter.GetComponent<Movement>().restrictMovement = false;

        if (!UISounds.Audio.isPlaying)
            UISounds.instance.PlayCloseButton();
    }



    public void OpenInventory()
    {
        OpenWindow();
        InventoryScreen.SelectedItemPanel.gameObject.SetActive(false);
        InventoryScreen.EquippedItemPanel.gameObject.SetActive(false);
        InventoryScreen.Populate();
        InventoryScreen.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        CloseWindow();
        InventoryScreen.gameObject.SetActive(false);
    }

    public void OpenJournal()
    {
        OpenWindow();

        QuestLogWindow.gameObject.SetActive(true);
        QuestLogWindow.Open();
    }

    public void CloseJournal()
    {
        CloseWindow();

        QuestLogWindow.gameObject.SetActive(false);
        QuestLogWindow.Close();
    }

    public void OpenMap()
    {
        OpenWindow();
        MapScreen.Populate();
        MapScreen.gameObject.SetActive(true);
    }

    public void CloseMap()
    {
        CloseWindow();
        MapScreen.Close();
        MapScreen.gameObject.SetActive(false);
    }

    public void OpenTravel()
    {
        characterDidNotTravel = true;

        OpenWindow();

        TravelScreen.PopulateMap();
        TravelScreen.gameObject.SetActive(true);
    }

    public void CloseTravel()
    {
        if (characterDidNotTravel)
            ResetCharacter();

        FadeScreen.gameObject.SetActive(true);
        FadeScreen.StartCoroutine(FadeScreen.ActivateFade());

        CloseWindow();
        TravelScreen.gameObject.SetActive(false);
    }

    public void OpenCraft()
    {
        OpenWindow();
        CraftScreen.Populate();
        CraftScreen.gameObject.SetActive(true);
    }

    public void CloseCraft()
    {
        CloseWindow();
        CraftScreen.gameObject.SetActive(false);
    }

    public void OpenCharacter()
    {
        OpenWindow();
        CharacterScreen.Populate();
        CharacterScreen.gameObject.SetActive(true);
    }

    public void CloseCharacter()
    {
        CloseWindow();
        CharacterScreen.gameObject.SetActive(false);
    }

    public void OpenVendor(int _vendorNumber)
    {
        OpenWindow();
        VendorScreen.SelectedItemPanel.gameObject.SetActive(false);
        VendorScreen.EquippedItemPanel.gameObject.SetActive(false);
        VendorScreen.Populate(_vendorNumber);
        VendorScreen.gameObject.SetActive(true);
    }

    public void CloseVendor()
    {
        CloseWindow();
        VendorScreen.gameObject.SetActive(false);
    }



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

    public void CloseLoot()
    {
        CloseWindow();
        PlayerScene.instance.MainCharacter.GetComponent<PlayerCharacterMovement>().movingToInteractableTarget = false;
        LootScreen.gameObject.SetActive(false);
    }



    public void OpenQuestBox(string _quest, double _questEntry)
    {
        StartCoroutine(WaitToOpenQuestBox(_quest, _questEntry));
    }

    public IEnumerator WaitToOpenQuestBox(string _quest, double _questEntry)
    {
        while (Combat.instance.combatActivated || waitingForConclusion)
            yield return null;

        QuestBox.gameObject.SetActive(true);
        QuestBox.Populate(_quest, _questEntry);
        StartCoroutine(CloseQuestBoxTimer());
    }

    IEnumerator CloseQuestBoxTimer()
    {
        yield return new WaitForSeconds(4);
        CloseQuestBox();
    }

    public void CloseQuestBox()
    {
        QuestBox.gameObject.SetActive(false);
    }



    public void OpenMenu()
    {
        OpenWindow();
        MenuScreen.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        CloseWindow();
        MenuScreen.gameObject.SetActive(false);
    }



    public void StartConclusionTimer(string _name)
    {
        waitingForConclusion = true;
        GameMusic.instance.FadeOutMusic(1f);
        StartCoroutine(OpenConclusionTimer(_name));
    }

    public IEnumerator OpenConclusionTimer(string _name)
    {
        yield return new WaitForSeconds(1f);
        OpenConclusion(_name);
    }

    public void OpenConclusion(string _name)
    {
        OpenWindow();

        ConclusionScreen.Populate(_name);
        ConclusionScreen.gameObject.SetActive(true);
    }

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



    public void StartEndGameTimer()
    {
        GameMusic.instance.FadeOutMusic(1f);
        StartCoroutine(OpenEndGameTimer());
    }

    public IEnumerator OpenEndGameTimer()
    {
        yield return new WaitForSeconds(1f);
        OpenEndGame();
    }

    public void OpenEndGame()
    {
        OpenWindow();

        ConclusionScreen.PopulateEndGame();
        ConclusionScreen.gameObject.SetActive(true);
    }

    public void CloseEndGame()
    {
        CloseWindow();
        ConclusionScreen.CloseEndGame();
        ConclusionScreen.gameObject.SetActive(false);
    }



    public void OpenLevel()
    {
        StartCoroutine(WaitToOpenLevelBox());
    }

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



    public void OpenRank()
    {
        UpdateBox.gameObject.SetActive(false);
        UpdateBox.PopulateRank();
        UpdateBox.gameObject.SetActive(true);
    }



    public void OpenTravelUpdate()
    {
        UpdateBox.gameObject.SetActive(false);
        UpdateBox.PopulateTravel();
        UpdateBox.gameObject.SetActive(true);
    }

    public void OpenSmallBox(string _label)
    {
        SmallBox.gameObject.SetActive(false);
        SmallBox.Populate(_label);
        SmallBox.gameObject.SetActive(true);
    }



    public void OpenUtility()
    {
        UtilitySlider.gameObject.SetActive(false);
        UtilitySlider.Populate();
        UtilitySlider.gameObject.SetActive(true);
    }



    public void OpenEndDemoMessage()
    {
        DemoLabel.gameObject.SetActive(true);
    }



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

    public void ResetCharacter()
    {
        // reset character from travel window close (without travelling)

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
