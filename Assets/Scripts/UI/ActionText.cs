using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionText : MonoBehaviour
{
    [Header("Action Text")]
    public TextMeshProUGUI ActionTextLabel;



    // close action text
    public IEnumerator Close()
    {
        yield return new WaitForSeconds(2);

        if (gameObject != null)
            DestroyActionText();
    }

    // destroy action text
    public void DestroyActionText()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }
}
