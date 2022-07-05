using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [Header("Muzzle Flash")]
    public GameObject MuzzleBurst;



    private void Start()
    {
        MuzzleBurst.gameObject.SetActive(false);
    }



    // activate
    public void ActivateMuzzleFlash()
    {
        MuzzleBurst.gameObject.SetActive(true);

        StartCoroutine(MuzzleFlashTimer());
    }

    // timer
    public IEnumerator MuzzleFlashTimer()
    {
        yield return new WaitForSeconds(0.03f);

        DeactivateMuzzleFlash();
    }

    // deactivate
    public void DeactivateMuzzleFlash()
    {
        MuzzleBurst.gameObject.SetActive(false);
    }
}
