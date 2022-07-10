using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character3D : MonoBehaviour
{
    public static Character3D instance;

    [Header("Model Parent")]
    public GameObject ModelParent;
    [Header("Model Parent")]
    public Camera AttachedCamera;
    [Header("Selected Character")]
    public GameObject AttachedModel;

    public bool headView;

    static int counter;

    bool inventory;



    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        // find a way to remove all this
        if (UI.instance == null)
            return;

        if (!UI.instance.InventoryScreen.gameObject.activeSelf)
            return;

        UpdatePortraitBaseModel();
    }



    public void PopulateModel(bool _inventory, PlayerCharacter _SelectedCharacter)
    {
        inventory = _inventory;

        // -----------------------------
        // destroy previous
        if (AttachedModel != null)
            Destroy(AttachedModel);

        // instantiate model
        GameObject _Model = null;
        if (_SelectedCharacter.male)
            _Model = Instantiate(ModelArray.instance.BaseMaleModels[0]) as GameObject;
        else
            _Model = Instantiate(ModelArray.instance.BaseFemaleModels[0]) as GameObject;

        // set parent as prefab and position
        _Model.transform.SetParent(ModelParent.transform, true);
        _Model.transform.position = ModelParent.transform.position;
        _Model.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
        _Model.transform.localScale = new Vector3(20.0f, 20.0f, 20.0f);

        counter++;
        _Model.name = _Model.name + " " + counter;
        // -----------------------------

        // -----------------------------
        // attach model
        AttachedModel = _Model;

        if (ModelParent.GetComponent<Character>() == null)
            ModelParent.AddComponent<Character>();
        // -----------------------------

        // -----------------------------
        // check equipment
        if (inventory)
            EquipmentChecker.instance.CheckEquipment(_SelectedCharacter, ModelParent.transform, true);
        // -----------------------------

        // -----------------------------
        // change layers so potrait camera can see model
        _Model.layer = 6;

        Transform[] _Objects = GetComponentsInChildren<Transform>();

        foreach (Transform _Object in _Objects)
            _Object.gameObject.layer = 6;
        // -----------------------------

        // -----------------------------
        // attach animator (portrait controller)
        Animator[] _ChildAnimators = GetComponentsInChildren<Animator>();

        foreach (Animator _Animator in _ChildAnimators)
            _Animator.runtimeAnimatorController = AnimationClips.instance.AnimatorControllers[4];

        // different idle for character and inventory screens
        if (!inventory)
        {
            foreach (Animator _Animator in _ChildAnimators)
                _Animator.SetBool("isCharacterScreen", true);
        }
        // -----------------------------
    }

    void UpdatePortraitBaseModel()
    {
        // (not sure why this needs to be in update, but the base sections will go back to active (after being deactivate) right after)

        // hair
        if (PlayerScene.instance.MainCharacter.EquippedHelmet != null)
        {
            if (ModelParent.transform.GetComponentInChildren<BodyManager>().BaseHair != null)
                ModelParent.transform.GetComponentInChildren<BodyManager>().BaseHair.gameObject.SetActive(false);
        }
        else
        {
            if (ModelParent.transform.GetComponentInChildren<BodyManager>().BaseHair != null)
                ModelParent.transform.GetComponentInChildren<BodyManager>().BaseHair.gameObject.SetActive(true);
        }

        // chest
        if (PlayerScene.instance.MainCharacter.EquippedChest != null)
            ModelParent.transform.GetComponentInChildren<BodyManager>().BaseChest.gameObject.SetActive(false);
        else
            ModelParent.transform.GetComponentInChildren<BodyManager>().BaseChest.gameObject.SetActive(true);

        // legs
        if (PlayerScene.instance.MainCharacter.EquippedLegs != null)
            ModelParent.transform.GetComponentInChildren<BodyManager>().BaseLegs.gameObject.SetActive(false);
        else
            ModelParent.transform.GetComponentInChildren<BodyManager>().BaseLegs.gameObject.SetActive(true);

        // boots
        if (PlayerScene.instance.MainCharacter.EquippedBoots != null)
            ModelParent.transform.GetComponentInChildren<BodyManager>().BaseFeet.gameObject.SetActive(false);
        else
            ModelParent.transform.GetComponentInChildren<BodyManager>().BaseFeet.gameObject.SetActive(true);

        // gloves
        if (PlayerScene.instance.MainCharacter.EquippedGloves != null)
            ModelParent.transform.GetComponentInChildren<BodyManager>().BaseHands.gameObject.SetActive(false);
        else
            ModelParent.transform.GetComponentInChildren<BodyManager>().BaseHands.gameObject.SetActive(true);

        // show weapon
        ModelParent.transform.GetComponentInChildren<BodyManager>().ResetAll();

        if (inventory)
        {
            if (PlayerScene.instance.MainCharacter.EquippedWeapon != null)
            {
                if (PlayerScene.instance.MainCharacter.EquippedWeapon.WeaponType == WeaponTypes.Pistol)
                {
                    ModelParent.transform.GetComponentInChildren<BodyManager>().Pistol.gameObject.SetActive(true);
                    ModelParent.transform.GetComponentInChildren<BodyManager>().Pistol.gameObject.layer = 6;
                }
                    
                // other weapons here
            }  
        }
    }
    


    public void ShowHead()
    {
        if (headView)
            return;

        AttachedCamera.orthographicSize = 8;

        Vector3 _newPosition = AttachedCamera.transform.position;
        _newPosition.y += 7.7f;
        AttachedCamera.transform.position = _newPosition;

        headView = true;
    }

    public void ShowBody()
    {
        if (!headView)
            return;

        AttachedCamera.orthographicSize = 22;

        Vector3 _newPosition = AttachedCamera.transform.position;
        _newPosition.y -= 7.7f;
        AttachedCamera.transform.position = _newPosition;

        headView = false;
    }
}
