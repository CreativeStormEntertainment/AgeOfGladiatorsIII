using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    float fadeSpeed = 4;

    public bool fadeOut;
    public bool fadeIn;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            fadeOut = true;

        if (Input.GetKeyDown(KeyCode.B))
            fadeIn = true;

        if (fadeOut)
            FadeOut();

        if (fadeIn)
            FadeIn();
    }



    void FadeOut()
    {
        if (GetComponentInChildren<Renderer>() == null) // for now
            return;

        Color _ObjectColor = this.GetComponentInChildren<Renderer>().material.color;
        float _fadeAmount = _ObjectColor.a - (fadeSpeed * Time.deltaTime);

        _ObjectColor = new Color(_ObjectColor.r, _ObjectColor.g, _ObjectColor.b, _fadeAmount);
        this.GetComponentInChildren<Renderer>().material.color = _ObjectColor;

        if (_ObjectColor.a <= 0)
        {
            if (GetComponentInParent<NPC>() != null)
                GetComponentInParent<NPC>().gameObject.SetActive(false);

            fadeOut = false;
        }
            
    }

    void FadeIn()
    {
        if (GetComponentInChildren<Renderer>() == null) // for now
            return;

        Color _ObjectColor = this.GetComponentInChildren<Renderer>().material.color;
        float _fadeAmount = _ObjectColor.a + (fadeSpeed * Time.deltaTime);

        _ObjectColor = new Color(_ObjectColor.r, _ObjectColor.g, _ObjectColor.b, _fadeAmount);
        this.GetComponentInChildren<Renderer>().material.color = _ObjectColor;

        if (_ObjectColor.a >= 1)
            fadeIn = false;
    }
}
