using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScene : MonoBehaviour
{
    public static PlayerScene instance;

    [Header("Scene Camera")]
    public Camera SceneCamera;

    [Header("Main Character")]
    public PlayerCharacter MainCharacter;

    [Header("Map Camera")]
    public Camera MapCamera;
    [Header("Minimap Camera")]
    public Camera MinimapCamera;

    [Header("Cinemachine Camera Target")]
    public CameraTarget CameraTarget;



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
        // transfer from new character creation
        // will eventually need to put somewhere else due to save/load
        MainCharacter.TransferSetting(Master.instance.NewCharacterTemporary);

        // activate proper model
        if (MainCharacter.male)
        {
            MainCharacter.GetComponent<MainCharacterModelSelector>().MaleModel.gameObject.SetActive(true);
            MainCharacter.GetComponent<MainCharacterModelSelector>().FemaleModel.gameObject.SetActive(false);
        }
        else
        {
            MainCharacter.GetComponent<MainCharacterModelSelector>().MaleModel.gameObject.SetActive(false);
            MainCharacter.GetComponent<MainCharacterModelSelector>().FemaleModel.gameObject.SetActive(true);
        }

        // equip main character
        MainCharacter.EquippedWeapon = new Weapon(WeaponTypes.Pistol, 0);
        MainCharacter.EquippedHelmet = new Helmet(ArmorTypes.Heavy, 0);
        MainCharacter.EquippedChest = new Chest(ArmorTypes.Heavy, 0);
        MainCharacter.EquippedLegs = new Legs(ArmorTypes.Heavy, 0);
        MainCharacter.EquippedGloves = new Gloves(ArmorTypes.Heavy, 0);
        MainCharacter.EquippedBoots = new Boots(ArmorTypes.Heavy, 0);

        MainCharacter.UpdateEquipmentOnModel();

        MainCharacter.GetComponent<CharacterAnimator>().AttachCharacter(); // here for reasons
    }



    // reset player on start
    public void ResetPlayer()
    {
        instance = null;
        Destroy(gameObject);
    }
}
