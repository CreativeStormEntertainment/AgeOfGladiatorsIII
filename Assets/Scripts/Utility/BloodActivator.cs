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



    // activate (shot)
    public void ActivateShotBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }

    // activate (cut)
    public void ActivateCutBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }

    // activate (shotgun)
    public void ActivateShotgunBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }

    // activate (heavy)
    public void ActivateHeavyBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }

    // activate (kill)
    public void ActivateKillBloodSplatter()
    {
        BloodSplatters[0].gameObject.SetActive(true);

        StartCoroutine(BloodSplatterTimer());
    }



    // timer
    public IEnumerator BloodSplatterTimer()
    {
        yield return new WaitForSeconds(0.5f);

        DeactivateBloodSplatter();
    }

    // deactivate
    public void DeactivateBloodSplatter()
    {
        foreach (GameObject _Blood in BloodSplatters)
            _Blood.gameObject.SetActive(false);
    }
}
