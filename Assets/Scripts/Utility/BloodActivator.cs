using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodActivator : MonoBehaviour
{
    public List<GameObject> BloodSplatters;



    public void Start()
    {
        foreach (GameObject _Blood in BloodSplatters)
            _Blood.gameObject.SetActive(false);
    }



    public void ActivateShotBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }



    public void ActivateCutBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }

    public void ActivateShotgunBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }

    public void ActivateHeavyBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }

    public void ActivateKillBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }



    public IEnumerator BloodSplatterTimer()
    {
        yield return new WaitForSeconds(0.5f);

        DeactivateBloodSplatter();
    }

    public void DeactivateBloodSplatter()
    {
        foreach (GameObject _Blood in BloodSplatters)
            _Blood.gameObject.SetActive(false);
    }
}
