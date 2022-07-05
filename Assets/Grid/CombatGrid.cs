using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CombatGrid : MonoBehaviour, ISerializationCallbackReceiver
{
    public LayerMask layer;
    public bool secondFloor;
    public LayerMask layer2;
    public GameObject cellPrefab;
    public BoxCollider boxCol;

    [HideInInspector]
    public int width;
    [HideInInspector]
    public int height;
    [HideInInspector]
    public int levels;
    public CombatGridCell[,,] gridArray;
    private Vector3 origin;

    private List<CombatGridCell> openList;
    private List<CombatGridCell> closedList;
    private Vector3 pathOffsetY = new Vector3(0, 0.5f, 0);
    public static CombatGridOutlines outlines;

    public GameObject APCostPrefab;
    public APCostController APCost;
    public LineRenderer APCostLinePrefab;
    public LineRenderer APCostLine;

    [SerializeField, HideInInspector]
    public List<SerializedCell<CombatGridCell>> serializedCells;

    [System.Serializable]
    public struct SerializedCell<TCell>
    {
        public int IndexX;
        public int IndexY;
        public int IndexZ;
        public TCell Cell;
        public SerializedCell(int x, int y, int z, TCell cell)
        {
            IndexX = x;
            IndexY = y;
            IndexZ = z;
            Cell = cell;
        }
    }



    private void Awake()
    {
        outlines = GetComponent<CombatGridOutlines>();
        var obj = Instantiate(APCostPrefab, Vector3.zero, Quaternion.identity);
        APCost = obj.GetComponentInChildren<APCostController>();
        APCostLine = Instantiate(APCostLinePrefab, Vector3.zero, Quaternion.identity);
    }


# if UNITY_EDITOR
    public void GenerateGrid()
    {
        ClearGrid();

        origin = new Vector3(Mathf.FloorToInt(boxCol.bounds.center.x - boxCol.bounds.extents.x), 0, Mathf.FloorToInt(boxCol.bounds.center.z - boxCol.bounds.extents.z));
        Vector3 top = new Vector3(Mathf.FloorToInt(boxCol.bounds.center.x - boxCol.bounds.extents.x), 0, Mathf.FloorToInt(boxCol.bounds.center.z + boxCol.bounds.extents.z));
        Vector3 right = new Vector3(Mathf.FloorToInt(boxCol.bounds.center.x + boxCol.bounds.extents.x), 0, Mathf.FloorToInt(boxCol.bounds.center.z - boxCol.bounds.extents.z));

        var maxX = Mathf.CeilToInt(Vector3.Distance(origin, right)) + 1;
        var maxY = Mathf.CeilToInt(Vector3.Distance(origin, top)) + 1;
        var originRoof = boxCol.bounds.center.y + boxCol.bounds.extents.y;

        width = maxX;
        height = maxY;
        levels = secondFloor ? 2 : 1;
        gridArray = new CombatGridCell[maxX, levels, maxY];

        InstatiateCells(0, maxX, maxY, originRoof, layer);

        if (secondFloor)
            InstatiateCells(1, maxX, maxY, originRoof, layer2);

        StartCoroutine(AdjacentCells());
    }

    public void ClearGrid()
    {
        gridArray = new CombatGridCell[0, 0, 0];
        serializedCells.Clear();
        width = 0;
        height = 0;

        for (int i = transform.childCount; i > 0; i--)
        {
            DestroyImmediate(transform.GetChild(i - 1).gameObject);
        }
    }

    public void InstatiateCells(int h, int maxX, int maxY, float originRoof, LayerMask layer)
    {
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(origin.x + x + 0.5f, originRoof, origin.z + y + 0.5f), -Vector3.up, out hit, boxCol.bounds.size.y, layer))
                {
                    //Debug.DrawRay(new Vector3(origin.x + x + 0.5f, originRoof, origin.z + y + 0.5f), -Vector3.up * 10, Color.green, 2);
                    GameObject cell = PrefabUtility.InstantiatePrefab(cellPrefab) as GameObject;
                    CombatGridCell gc = cell.GetComponent<CombatGridCell>();
                    gc.layer = layer;

                    cell.transform.SetParent(transform);
                    cell.transform.position = new Vector3(origin.x + x, hit.point.y, origin.z + y);
                    cell.transform.rotation = Quaternion.identity;
                    cell.name = $"{x}, {h}, {y}";

                    gc.debugText.text = $"{x}, {h}, {y}";
                    gc.gridPosX = x;
                    gc.gridPosY = h;
                    gc.gridPosZ = y;
                    gc.grid = this;
                    gridArray[x, h, y] = gc;
                    if (hit.normal != Vector3.up)
                    {
                        //Debug.Log($"{x}, {h}, {y} is not flat");
                        //Debug.DrawRay(cell.transform.position, hit.normal * 0.5f, Color.red, 2);
                        AngledCellCalculations(cell, hit.normal, layer);
                    }
                }
                else
                {
                    //Debug.DrawRay(new Vector3(origin.x + x + 0.5f, originRoof, origin.z + y + 0.5f), -Vector3.up * 10, Color.yellow, 2);
                }
            }
        }
    }
# endif

    public void AngledCellCalculations(GameObject cell, Vector3 normal, LayerMask layer)
    {
        var angle = Vector3.Angle(normal, Vector3.up);
        var angleX = Vector3.SignedAngle(normal, Vector3.right, Vector3.up);
        var angleZ = Vector3.SignedAngle(normal, Vector3.forward, Vector3.up);
        var size = 1 / Mathf.Cos(Mathf.Deg2Rad * angle);

        if (angleX == 90)
        {
            AngledCellTransforms(cell, layer, Vector3.up, angle, angle, 0, 1, size);
        }

        if (angleX == -90)
        {
            AngledCellTransforms(cell, layer, Vector3.zero, angle, -angle, 0, 1, size);
        }

        if (angleZ == 90)
        {
            AngledCellTransforms(cell, layer, Vector3.zero, angle, 0, angle, size, 1);
        }

        if (angleZ == -90)
        {
            AngledCellTransforms(cell, layer, Vector3.up, angle, 0, -angle, size, 1);
        }
    }

    public void AngledCellTransforms(GameObject cell, LayerMask layer, Vector3 offset, float angle, float x, float z, float sizeX, float sizeZ)
    {
        cell.transform.rotation = Quaternion.Euler(x, 0, z);
        RaycastHit hit;
        cell.transform.position += offset;
        //Debug.DrawRay(cell.transform.position, -Vector3.up * 1f, Color.magenta, 2);
        if (Physics.Raycast(cell.transform.position, -Vector3.up, out hit, 1.5f, layer))
        {
            cell.transform.position -= new Vector3(0, hit.distance, 0);
            cell.transform.localScale = new Vector3(sizeX, 1, sizeZ);
        }
    }

    IEnumerator AdjacentCells()
    {
        yield return null;

        for (int y = 0; y < levels; y++)
        {
            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    var cell = gridArray[x, y, z];

                    if (cell != null)
                        cell.FindAdjacentCells();
                }
            }
        }
    }

    public void OnBeforeSerialize()
    {
        serializedCells = new List<SerializedCell<CombatGridCell>>();

        for (int y = 0; y < levels; y++)
        {
            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    serializedCells.Add(new SerializedCell<CombatGridCell>(x, y, z, gridArray[x, y, z]));
                }
            }
        }
        //Debug.Log($"OnBeforeSerialize {gridArray.Length} | {serializedCells.Count}");
    }

    public void OnAfterDeserialize()
    {
        gridArray = new CombatGridCell[width, levels, height];
        //Debug.Log($"OnAfterDeserialize {gridArray.Length} | {serializedCells.Count}");
        foreach (var cell in serializedCells)
        {
            gridArray[cell.IndexX, cell.IndexY, cell.IndexZ] = cell.Cell;
        }
    }

    private void ResetCellCosts()
    {
        for (int y = 0; y < levels; y++)
        {
            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    var cell = gridArray[x, y, z];
                    if (cell != null)
                    {
                        cell.gCost = int.MaxValue;
                        cell.CalcFCost();
                        cell.prevCell = null;
                    }
                }
            }
        }
    }

    public List<Vector3> GetPath(CombatGridCell startCell, CombatGridCell endCell, bool checkBlocked, bool checkBlockedDiagonal)
    {
        openList = new List<CombatGridCell> { startCell };
        closedList = new List<CombatGridCell>();

        ResetCellCosts();

        startCell.gCost = 0;
        startCell.hCost = CalcDistanceCost(startCell, endCell);
        startCell.CalcFCost();

        while (openList.Count > 0)
        {
            var curCell = GetLowestFCost(openList);
            if (curCell == endCell)
                return CalcPath(startCell, endCell);

            openList.Remove(curCell);
            closedList.Add(curCell);

            foreach (CombatGridCell adjCell in curCell.adjacentCells)
            {
                if (closedList.Contains(adjCell)) continue;

                if (checkBlocked)
                {
                    if (!adjCell.accessible)
                    {
                        closedList.Add(adjCell);
                        continue;
                    }

                    if (Combat.instance.combatActivated && adjCell.occupied)
                    {
                        closedList.Add(adjCell);
                        continue;
                    }
                }

                int newGCost = curCell.gCost + CalcDistanceCost(curCell, adjCell);
                if (checkBlockedDiagonal && CheckDiagonal(curCell, adjCell))
                    newGCost += 100;

                if (newGCost < adjCell.gCost)
                {
                    adjCell.prevCell = curCell;
                    adjCell.gCost = newGCost;
                    adjCell.hCost = CalcDistanceCost(adjCell, endCell);
                    adjCell.CalcFCost();

                    if (!openList.Contains(adjCell))
                        openList.Add(adjCell);
                }
            }
        }

        return null;
    }

    public int CalcDistanceCost(CombatGridCell a, CombatGridCell b)
    {
        int disX = Mathf.Abs(a.gridPosX - b.gridPosX);
        int disY = Mathf.Abs(a.gridPosY - b.gridPosY);
        int disZ = Mathf.Abs(a.gridPosZ - b.gridPosZ);
        int remainder = Mathf.Abs(disX - disZ) + disY;
        
        return (14 * Mathf.Min(disX, disZ)) + (10 * remainder);
    }

    private CombatGridCell GetLowestFCost(List<CombatGridCell> cellList)
    {
        var cell = cellList[0];
        for (int i = 1; i < cellList.Count; i++)
        {
            if (cellList[i].fCost < cell.fCost)
                cell = cellList[i];
        }
        return cell;
    }

    private bool CheckDiagonal(CombatGridCell a, CombatGridCell b)
    {
        int xDir = -(a.gridPosX - b.gridPosX);
        int zDir = -(a.gridPosZ - b.gridPosZ);

        if (Mathf.Abs(xDir) + Mathf.Abs(zDir) < 2)
            return false;

        if (a.gridPosX + xDir > width - 1 || a.gridPosX + xDir < 0)
            return false;
        var xCell = gridArray[a.gridPosX + xDir, a.gridPosY, a.gridPosZ];
        if (xCell != null && !xCell.accessible)
            return true;

        if (a.gridPosZ + zDir > height - 1 || a.gridPosZ + zDir < 0)
            return false;
        var zCell = gridArray[a.gridPosX, a.gridPosY, a.gridPosZ + zDir];
        if (zCell != null && !zCell.accessible)
            return true;

        return false;
    }

    private List<Vector3> CalcPath(CombatGridCell start, CombatGridCell end)
    {
        List<Vector3> path = new List<Vector3>();

        var cell = end;
        while (cell.prevCell != null)
        {
            path.Add(cell.boxCol.bounds.center);
            cell = cell.prevCell;
        }

        path.Reverse();

        if (!Combat.instance.combatActivated && path != null)
            path = OptimizeNonCombatPath(start, path);

        return path;
    }

    public List<Vector3> OptimizeNonCombatPath(CombatGridCell startingCell, List<Vector3> path)
    {
        List<Vector3> toRemove = new List<Vector3>();
        Vector3 start = startingCell.boxCol.bounds.center;
        path.Insert(0, start);
        int index = 0;
        int preIndex = 0;

        for (int i = 1; i < path.Count; i++)
        {
            preIndex = i - 1;
            Vector3 origin = path[index] + pathOffsetY;
            Vector3 destination = path[i] + pathOffsetY;
            Vector3 direction = destination - origin;

            RaycastHit hit;
            //Debug.DrawRay(origin, direction, Color.red, 2);
            if (Physics.SphereCast(origin, 0.3f, direction, out hit, Vector3.Distance(origin, destination)))
            {
                // check if obstacle between previous and next path points to prevent cutting corners
                Vector3 pathPrev = path[preIndex] + pathOffsetY;
                Vector3 pathNext = path[i + 1] + pathOffsetY;
                Vector3 newDirection = pathNext - pathPrev;
                if (i != path.Count - 1 && Physics.SphereCast(pathPrev, 0.3f, newDirection, out hit, Vector3.Distance(pathPrev, pathNext)))
                {
                    //Debug.DrawRay(pathPrev, newDirection, Color.yellow, 2);
                    i++;
                }
                index = i;
            }
            else
            {
                // check if obstacle between current point and path end
                Vector3 pathEnd = path[path.Count - 1] + pathOffsetY;
                Vector3 newDirection = pathEnd - destination;
                if (Physics.SphereCast(destination, 0.3f, newDirection, out hit, Vector3.Distance(destination, pathEnd)))
                {
                    if (preIndex != 0)
                        toRemove.Add(path[preIndex]);
                }
                else // clear path = remove remaining path points except end
                {
                    //Debug.DrawRay(destination, newDirection, Color.green, 2);
                    for (int n = i + 1; n < path.Count - 1; n++)
                    {
                        toRemove.Add(path[n]);
                    }
                    break;
                }
            }
        }

        foreach (Vector3 point in toRemove)
        {
            path.Remove(point);
        }

        return path;
    }

    public static CombatGridCell GetCellAtPosition(Vector3 position)
    {
        CombatGridCell cell = null;
        int mask = LayerMask.GetMask("Cell");
        RaycastHit hit;
        //Debug.DrawRay(position + Vector3.up, -Vector3.up * 2, Color.green, 2);
        if (Physics.Raycast(position + Vector3.up, -Vector3.up, out hit, 2, mask))
        {
            cell = hit.transform.GetComponentInParent<CombatGridCell>();
        }
        return cell;
    }

    public static CombatGridCell GetNearestAvailableCell(CombatGridCell startingCell, CombatGridCell target)
    {
        CombatGridCell cell = null;
        float distance = 9999;

        foreach (CombatGridCell adj in target.adjacentCells)
        {
            if (adj.accessible && !adj.occupied)
            {
                var dist = Vector3.Distance(startingCell.boxCol.bounds.center, adj.boxCol.bounds.center);
                if (dist < distance)
                {
                    distance = dist;
                    cell = adj;
                }
            }
        }

        return cell;
    }

    public static CombatGridCell GetNearestCellInShootingRange(CombatGridCell startingCell, CombatGridCell target, float range)
    {
        CombatGridCell cell = null;
        float distance = 9999;
        List<CombatGridCell> cellsInRange = startingCell.grid.GetAllCellsInRange(target, range);
        foreach (CombatGridCell c in cellsInRange)
        {
            if (c.accessible && !c.occupied)
            {
                var dist = Vector3.Distance(startingCell.boxCol.bounds.center, c.boxCol.bounds.center);
                if (dist < distance)
                {
                    distance = dist;
                    cell = c;
                }
            }
        }

        return cell;
    }

    public static CombatGridCell GetNearestCellToTarget(CombatGridCell startingCell, CombatGridCell target, float range)
    {
        CombatGridCell cell = null;
        float distance = 9999;
        List<CombatGridCell> cellsInRange = startingCell.grid.GetAllCellsInRange(startingCell, range);

        foreach (CombatGridCell c in cellsInRange)
        {
            if (c.accessible && !c.occupied)
            {
                var dist = Vector3.Distance(target.boxCol.bounds.center, c.boxCol.bounds.center);
                if (dist < distance)
                {
                    distance = dist;
                    cell = c;
                }
            }
        }

        return cell;
    }

    public List<CombatGridCell> GetAllCellsInRange(CombatGridCell startingCell, float range)
    {
        List<CombatGridCell> allCells = new List<CombatGridCell>();
        allCells.Add(startingCell);

        openList = new List<CombatGridCell> { startingCell };
        closedList = new List<CombatGridCell>();

        ResetCellCosts();

        startingCell.gCost = 0;

        while (openList.Count > 0)
        {
            for (int i = openList.Count -1; i >= 0; i--)
            {
                foreach (CombatGridCell adjCell in openList[i].adjacentCells)
                {
                    if (closedList.Contains(adjCell)) continue;

                    int newGCost = openList[i].gCost + CalcDistanceCost(openList[i], adjCell);

                    if (CheckDiagonal(openList[i], adjCell))
                        newGCost += 100;

                    if (newGCost < adjCell.gCost)
                        adjCell.gCost = newGCost;

                    if (newGCost / 10f <= range)
                    {
                        allCells.Add(adjCell);

                        if (!adjCell.accessible || adjCell.occupied)
                        {
                            closedList.Add(adjCell);
                        }
                        else if (!openList.Contains(adjCell))
                            openList.Add(adjCell);

                    }
                }

                closedList.Add(openList[i]);
                openList.Remove(openList[i]);
            }
        }

        return allCells;
    }

    public void DrawOutlines(CombatGridCell startingCell, float range, bool movement)
    {
        outlines.GenerateOutline(startingCell, range, movement);
    }

    public static void ClearOutlines()
    {
        outlines.ClearOutlines();
    }

    public void ClearAP()
    {
        APCostLine.gameObject.SetActive(false);
        APCost.rt.gameObject.SetActive(false);
    }

    public static bool CheckForCover(CombatGridCell startingCell)
    {
        bool _cover = false;

        if (startingCell.FindAdjacentCellsCover())
            _cover = true;

        return _cover;
    }
}
