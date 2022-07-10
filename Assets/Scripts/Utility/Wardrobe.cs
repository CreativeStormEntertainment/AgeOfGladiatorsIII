using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public GameObject Hat;
    public GameObject Hair;
    public GameObject Head;
    public GameObject Chest;
    public GameObject Legs;
    public GameObject Hands;
    public GameObject Feet;



    private void Awake()
    {
        DressModel();
    }



    public void DressModel()
    {
        // hat
        if (Hat != null)
        {
            GameObject _New = Instantiate(Hat) as GameObject;
            ReplacePart(GetComponent<BodyManager>().BaseHat, _New);
            GetComponent<BodyManager>().BaseHat = _New;
            GetComponent<BodyManager>().BaseHat.gameObject.SetActive(true);
        }

        // head
        if (Head != null)
        {
            GameObject _New = Instantiate(Head) as GameObject;
            ReplacePart(GetComponent<BodyManager>().BaseHead, _New);
            GetComponent<BodyManager>().BaseHead = _New;
        }

        // chest
        if (Chest != null)
        {
            GameObject _New = Instantiate(Chest) as GameObject;
            ReplacePart(GetComponent<BodyManager>().BaseChest, _New);
            GetComponent<BodyManager>().BaseChest = _New;
        }

        // legs
        if (Legs != null)
        {
            GameObject _New = Instantiate(Legs) as GameObject;
            ReplacePart(GetComponent<BodyManager>().BaseLegs, _New);
            GetComponent<BodyManager>().BaseLegs = _New;
        }

        // hands
        if (Hands != null)
        {
            GameObject _New = Instantiate(Hands) as GameObject;
            ReplacePart(GetComponent<BodyManager>().BaseHands, _New);
            GetComponent<BodyManager>().BaseHands = _New;
        }

        // feet
        if (Feet != null)
        {
            GameObject _New = Instantiate(Feet) as GameObject;
            ReplacePart(GetComponent<BodyManager>().BaseFeet, _New);
            GetComponent<BodyManager>().BaseFeet = _New;
        }

        // hair
        if (Hair != null)
        {
            GameObject _New = Instantiate(Hair) as GameObject;
            ReplacePart(GetComponent<BodyManager>().BaseHair, _New);
            GetComponent<BodyManager>().BaseHair = _New;
        }
    }

    void ReplacePart(GameObject _Original, GameObject _Replacement)
    {
        _Replacement.transform.SetParent(this.transform, true);
        _Replacement.transform.position = GetComponent<BodyManager>().BaseChest.transform.position;
        _Replacement.transform.rotation = GetComponent<BodyManager>().BaseChest.transform.rotation;

        Destroy(_Original);
    }
}
