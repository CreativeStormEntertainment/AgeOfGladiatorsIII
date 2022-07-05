using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridTris
{
    public Vector3 vert1;
    public Vector3 vert2;
    public Vector3 vert3;

    public GridTris(Vector3 V1, Vector3 V2, Vector3 V3)
    {
        vert1 = V1;
        vert2 = V2;
        vert3 = V3;
    }
}
