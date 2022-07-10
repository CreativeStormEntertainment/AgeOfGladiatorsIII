using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipNPC : MonoBehaviour
{
    [Header("Weapon Type")]
    public WeaponTypes EquipTypeOfWeapon;



    public void EquipWeaponOnStart()
    {
        if (GetComponent<Character>().EquippedWeapon == null && EquipTypeOfWeapon != WeaponTypes.Unarmed)
            GetComponent<Character>().EquippedWeapon = new Weapon(EquipTypeOfWeapon, 0);
    }
}
