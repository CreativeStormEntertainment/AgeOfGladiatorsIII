using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocateOpenMap
{
    // find current open map and return it
    public static Map GetOpenMap()
    {
        Map _OpenMap = null;

        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject _object in allObjects)
        {
            if (_object.activeInHierarchy)
            {
                if (_object.GetComponent<Map>() != null)
                    _OpenMap = _object.GetComponent<Map>();
            }
        }

        return _OpenMap;
    }
}
