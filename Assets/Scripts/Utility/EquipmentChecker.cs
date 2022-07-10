using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentChecker : MonoBehaviour
{
    public static EquipmentChecker instance;



    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }



    public void CheckEquipment(Character _Character, Transform _Parent, bool _characterScreen)
    {
        RemoveItem(_Parent, "Helmet", _Parent.transform.GetComponentInChildren<BodyManager>().BaseHair);
        RemoveItem(_Parent, "Chest", _Parent.transform.GetComponentInChildren<BodyManager>().BaseChest);
        RemoveItem(_Parent, "Hands", _Parent.transform.GetComponentInChildren<BodyManager>().BaseHands);
        RemoveItem(_Parent, "Legs", _Parent.transform.GetComponentInChildren<BodyManager>().BaseLegs);
        RemoveItem(_Parent, "Feet", _Parent.transform.GetComponentInChildren<BodyManager>().BaseFeet);

        // add helmet
        if (_Character.EquippedHelmet != null)
        {
            if (_Character.male)
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeMaleHelmets, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseHair);
            else
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeFemaleHelmets, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseHair);
        }

        // add jacket
        if (_Character.EquippedChest != null)
        {
            if (_Character.male)
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeMaleChest, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseChest);
            else
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeFemaleChest, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseChest);
        }
            
        // add legs
        if (_Character.EquippedLegs != null)
        {
            if (_Character.male)
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeMaleLegs, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseLegs);
            else
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeFemaleLegs, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseLegs);
        }

        // add feet
        if (_Character.EquippedBoots != null)
        {
            if (_Character.male)
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeMaleBoots, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseFeet);
            else
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeFemaleBoots, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseFeet);
        }

        // add gloves
        if (_Character.EquippedGloves != null)
        {
            if (_Character.male)
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeMaleGloves, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseHands);
            else
                AddItem(_Parent, _characterScreen, ModelArray.instance.JudgeFemaleGloves, 0, _Parent.transform.GetComponentInChildren<BodyManager>().BaseHands);
        }

        // show weapon
        if ((Combat.instance.combatActivated || _Character.weaponDrawn) && _Character.CombatStyle() != WeaponTypes.Unarmed)
            _Parent.transform.GetComponentInChildren<BodyManager>().ShowWeapon();
        else
            _Parent.transform.GetComponentInChildren<BodyManager>().HideWeapon();
    }



    void AddItem(Transform _Parent, bool _characterScreen, GameObject[] _Array, int _index, GameObject _BasePart)
    {
        // deactivate base part
        if (_BasePart != null)
            _BasePart.gameObject.SetActive(false);

        // instantiate model
        GameObject _Item = Instantiate(_Array[_index]) as GameObject;
        _Item.transform.SetParent(_Parent.transform, true); // not sure why this only works with model parent and not actual model
        _Item.transform.position = _Parent.transform.position;

        // model position, scale and rotation
        if (_characterScreen)
        {
            _Item.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
            _Item.transform.localScale = new Vector3(20.0f, 20.0f, 20.0f);
        }
        else
        {
            _Item.transform.rotation = _Parent.transform.rotation;
        }
    }

    void RemoveItem(Transform _Parent, string _input, GameObject _BasePart)
    {
        foreach (Transform child in _Parent.transform)
        {
            if (child.tag == _input)
                Destroy(child.gameObject);
        }

        if (_BasePart != null)
            _BasePart.gameObject.SetActive(true);
    }
}
