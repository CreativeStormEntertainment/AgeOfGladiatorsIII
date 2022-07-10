using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlack : MonoBehaviour
{
    public static FadeBlack instance;



    private void Start()
    {
        StartCoroutine(ActivateFade());
    }



    public IEnumerator ActivateFade()
    {
        //this.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    public IEnumerator ActivateFadeInOut()
    {
        //this.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        this.gameObject.SetActive(false);
    }
}
