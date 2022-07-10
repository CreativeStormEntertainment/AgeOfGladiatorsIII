using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [Header("Muzzle Flash")]
    public GameObject MuzzleBurst;



    private void Start()
    {
        if (MuzzleBurst != null)
            MuzzleBurst.gameObject.SetActive(false);
    }



    public void ActivateMuzzleFlash()
    {
        MuzzleBurst.gameObject.SetActive(true);

        StartCoroutine(MuzzleFlashTimer());
    }

    public IEnumerator MuzzleFlashTimer()
    {
        yield return new WaitForSeconds(0.03f);

        DeactivateMuzzleFlash();
    }

    public void DeactivateMuzzleFlash()
    {
        MuzzleBurst.gameObject.SetActive(false);
    }
}
