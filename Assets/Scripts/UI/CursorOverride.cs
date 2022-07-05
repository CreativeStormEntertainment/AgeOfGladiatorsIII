using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorOverride : MonoBehaviour
{
    void Update()
    {
        if (Combat.instance.combatActivated && Combat.instance.Attacking != null)
        {
            if (!Combat.instance.Attacking.playerControlledCombat)
                Cursor.SetCursor(Cursors.instance.WaitCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
