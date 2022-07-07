using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionText : MonoBehaviour
{
    [Header("Action Text")]
    public TextMeshProUGUI ActionTextLabel;



    public IEnumerator Close()
    {
        yield return new WaitForSeconds(2);

        if (gameObject != null)
            DestroyActionText();
    }

    public void DestroyActionText()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }
}
