using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlackTravel : MonoBehaviour
{
    // fade from black
    public IEnumerator ActivateFadeTravel()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}
