using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelArray : MonoBehaviour
{
    public static ModelArray instance;

    [Header("Bodies")]
    public GameObject[] BaseMaleModels;
    public GameObject[] BaseFemaleModels;

    [Header("Clothing")]
    public GameObject[] JudgeMaleHelmets;
    public GameObject[] JudgeMaleChest;
    public GameObject[] JudgeMaleLegs;
    public GameObject[] JudgeMaleBoots;
    public GameObject[] JudgeMaleGloves;

    public GameObject[] JudgeFemaleHelmets;
    public GameObject[] JudgeFemaleChest;
    public GameObject[] JudgeFemaleLegs;
    public GameObject[] JudgeFemaleBoots;
    public GameObject[] JudgeFemaleGloves;

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
