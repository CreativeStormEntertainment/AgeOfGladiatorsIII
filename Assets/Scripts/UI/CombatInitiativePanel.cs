using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatInitiativePanel : MonoBehaviour
{
    public GameObject Grid;



    public void Populate()
    {
        // clear the conversation options grid
        for (int i = 0; i < Grid.transform.childCount; i++)
            Destroy(Grid.transform.GetChild(i).gameObject);

        // populate grid
        foreach (Character _Character in Combat.instance.CombatList)
        {
            if (Combat.instance.CombatList.IndexOf(_Character) >= Combat.instance.attackerIndex && !_Character.isDead)
            {
                // instantiate
                GameObject _prefab = Instantiate(Resources.Load("UI-InitiativePortrait")) as GameObject;
                _prefab.transform.SetParent(Grid.transform, false);
                _prefab.transform.localPosition = Vector3.zero;

                _prefab.GetComponent<InitiativePortrait>().Portrait.sprite = PortraitSelector.FindPortrait(_Character, 1);
                _prefab.GetComponent<InitiativePortrait>().AttachedCharacter = _Character;
            }
        }
    }
}
