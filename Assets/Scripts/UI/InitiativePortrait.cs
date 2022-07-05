using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativePortrait : MonoBehaviour
{
    public Image Portrait;
    public Slider HitpointSlider;

    public Character AttachedCharacter;



    private void Update()
    {
        // not sure why, but this only works in update
        if (AttachedCharacter != null)
        {
            HitpointSlider.value = AttachedCharacter.combatHealth;
            HitpointSlider.maxValue = AttachedCharacter.GetHealth();
        }
    }
}
