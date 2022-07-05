using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    [Header("Body Parts")]
    public GameObject BaseHat;
    public GameObject BaseHead;
    public GameObject BaseHair;
    public GameObject BaseChest;
    public GameObject BaseLegs;
    public GameObject BaseHands;
    public GameObject BaseFeet;
    

    [Header("Weapons")]
    public GameObject Pistol;
    public GameObject Melee1H;


    // show weapon
    public void ShowWeapon()
    {
        ResetAll();

        if (GetComponentInParent<Character>() == null)
            return;

        if (GetComponentInParent<Character>().EquippedWeapon != null && GetComponentInParent<Character>().CombatStyle() != WeaponTypes.Unarmed)
        {
            if (GetComponentInParent<Character>().EquippedWeapon.WeaponType == WeaponTypes.Pistol)
                Pistol.gameObject.SetActive(true);

            if (GetComponentInParent<Character>().EquippedWeapon.WeaponType == WeaponTypes.Blunt1H)
                Melee1H.gameObject.SetActive(true);
        }
    }

    // hide weapon
    public void HideWeapon()
    {
        ResetAll();
    }



    // reset all
    public void ResetAll()
    {
        Pistol.gameObject.SetActive(false);
        Melee1H.gameObject.SetActive(false);
    }
}
