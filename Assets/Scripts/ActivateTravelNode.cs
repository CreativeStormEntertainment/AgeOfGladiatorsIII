using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTravelNode : MonoBehaviour
{
    public MovementNode AttachedMovementNode;


    public void ActivateNode()
    {
        if (AttachedMovementNode != null && AttachedMovementNode.disabled)
            AttachedMovementNode.disabled = false;
    }
}
