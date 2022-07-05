using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGridOutlines : MonoBehaviour
{
    public CombatGrid grid;
    public GameObject outlinePrefabMovement;
    public GameObject outlinePrefabAttack;

    //public MeshFilter mf;
    //private Mesh mesh;
    //public List<Vector3> gridCellVertices = new List<Vector3>();
    //public Vector3[] verts;
    //public int[] tris;
    //public List<GridTris> triangles = new List<GridTris>();
    //public bool generateMesh;

    public List<LineRenderer> outlines = new List<LineRenderer>();

    //private void Update()
    //{
    //    if (generateMesh)
    //    {
    //        generateMesh = false;
    //        GenerateMesh();
    //    }
    //}

    public void DrawMovementPerimiter(List<Vector3> _points)
    {
        GameObject _obj = Instantiate(outlinePrefabMovement, gameObject.transform.position, Quaternion.identity);
        var _line = _obj.GetComponent<LineRenderer>();
        outlines.Add(_line);

        _line.positionCount = _points.Count;

        for (int i = 0; i < _points.Count; i++)
        {
            _line.SetPosition(i, _points[i]);
        }
    }

    public void DrawAttackPerimiter(List<Vector3> _points)
    {
        GameObject _obj = Instantiate(outlinePrefabAttack, gameObject.transform.position, Quaternion.identity);
        var _line = _obj.GetComponent<LineRenderer>();
        outlines.Add(_line);

        _line.positionCount = _points.Count;

        for (int i = 0; i < _points.Count; i++)
        {
            _line.SetPosition(i, _points[i]);
        }
    }
    IEnumerator DrawLineWithDelay(LineRenderer _line, List<Vector3> _points) // for debugging
    {
        _line.positionCount = _points.Count;

        for (int i = 0; i < _points.Count; i++)
        {
            _line.SetPosition(i, _points[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ClearOutlines()
    {
        foreach (LineRenderer line in outlines)
            Destroy(line.gameObject);

        outlines.Clear();
    }

    public void GenerateOutline(CombatGridCell startingCell, float range, bool movement)
    {
        List<CombatGridCell> allCells = grid.GetAllCellsInRange(startingCell, range);
        List<CombatGridCell> obstacleCells = new List<CombatGridCell>();
        List<CombatGridCell> usedCells = new List<CombatGridCell>();

        foreach (CombatGridCell cell in allCells)
        {
            if (cell.accessible && !cell.occupied)
            {
                //cell.movementRange.SetActive(true);
                //Combat.highlighted.Add(cell);
            }
            else if (!cell.accessible)
            {
                obstacleCells.Add(cell);
            }
        }

        usedCells = OutlinePoints(allCells, true, movement);
        HandleObstacles(obstacleCells, allCells, movement);

        //Check any remaining non-interior/non-obstacle cells??
    }

    public List<CombatGridCell> OutlinePoints(List<CombatGridCell> allCells, bool accessible, bool movement)
    {
        List<CombatGridCell> usedCells = new List<CombatGridCell>();
        List<Vector3> points = new List<Vector3>();
        CombatGridCell nextCell = accessible ? GetTopRightCornerCell(allCells) : GetTopRightObstructedCell(allCells);
        CombatGridCell curCell = CheckCellOutlineLeft(nextCell);
        CombatGridCell preCell = null;
        usedCells.Add(nextCell);
        int[] preDir = new int[2] { 0, 0 };
        int[] dir = new int[2] { 0, 0 };
        Vector3 startingPoint = GetCornerPoint(nextCell.boxCol, 1, 0, 0, -1, true);
        Vector3[] cornerPoints = GetCornerPointOffset(startingPoint, 1, 0, 0, -1);
        points.Add(cornerPoints[0]);
        points.Add(cornerPoints[1]);
        Vector3 curPoint = Vector3.zero;

        if (allCells.Count < 3) // single cell
        {
            if (allCells.Count == 1)
            {
                curPoint = GetCornerPoint(nextCell.boxCol, 1, 0, 0, 1, true);
                cornerPoints = GetCornerPointOffset(curPoint, 1, 0, 0, 1);

                points.Add(cornerPoints[0]);
                points.Add(cornerPoints[1]);

                curPoint = GetCornerPoint(nextCell.boxCol, 0, -1, 1, 0, true);
                cornerPoints = GetCornerPointOffset(curPoint, 0, -1, 1, 0);

                points.Add(cornerPoints[0]);
                points.Add(cornerPoints[1]);

                curPoint = GetCornerPoint(nextCell.boxCol, -1, 0, 0, -1, true);
                cornerPoints = GetCornerPointOffset(curPoint, -1, 0, 0, -1);

                points.Add(cornerPoints[0]);
                points.Add(cornerPoints[1]);

                usedCells.Add(nextCell);
            }
            else // two adjacent cells
            {
                dir = accessible ? GetNextCellPointDirection(nextCell, curCell, allCells) :
                    GetNextCellPointDirectionObstructed(nextCell, curCell, allCells);
                preDir[0] = -dir[0];
                preDir[1] = -dir[1];
                curCell = nextCell;
                nextCell = nextCell.grid.gridArray[nextCell.gridPosX + dir[0], nextCell.gridPosY, nextCell.gridPosZ + dir[1]];

                int[] midDir = GetMiddleDirection(preDir[0], preDir[1], dir[0], dir[1]);

                curPoint = GetCornerPoint(curCell.boxCol, preDir[0], preDir[1], midDir[0], midDir[1], true);
                cornerPoints = GetCornerPointOffset(curPoint, preDir[0], preDir[1], midDir[0], midDir[1]);

                points.Add(cornerPoints[0]);
                points.Add(cornerPoints[1]);

                curPoint = GetCornerPoint(curCell.boxCol, midDir[0], midDir[1], dir[0], dir[1], true);
                cornerPoints = GetCornerPointOffset(curPoint, midDir[0], midDir[1], dir[0], dir[1]);

                points.Add(cornerPoints[0]);
                points.Add(cornerPoints[1]);

                midDir = GetMiddleDirection(dir[0], dir[1], preDir[0], preDir[1]);

                curPoint = GetCornerPoint(nextCell.boxCol, dir[0], dir[1], midDir[0], midDir[1], true);
                cornerPoints = GetCornerPointOffset(curPoint, dir[0], dir[1], midDir[0], midDir[1]);

                points.Add(cornerPoints[0]);
                points.Add(cornerPoints[1]);

                curPoint = GetCornerPoint(nextCell.boxCol, midDir[0], midDir[1], preDir[0], preDir[1], true);
                cornerPoints = GetCornerPointOffset(curPoint, midDir[0], midDir[1], preDir[0], preDir[1]);

                points.Add(cornerPoints[0]);
                points.Add(cornerPoints[1]);

                usedCells.Add(curCell);
                usedCells.Add(nextCell);
            }
        }
        else
        {
            int loopCount = 0;
            while (curPoint != startingPoint)
            {
                loopCount++;
                if (loopCount > 999) //infinite loop safety net
                {
                    Debug.LogError($"OutlinePoints While Loop Got Stuck!!!");
                    break;
                }

                dir = accessible ? GetNextCellPointDirection(nextCell, curCell, allCells) :
                    GetNextCellPointDirectionObstructed(nextCell, curCell, allCells);

                //Debug.Log($"Dir from {curCell} to {nextCell} with outline count {allCells.Count}");
                //if (dir != null)
                //    Debug.Log($"[{dir[0]}, {dir[1]}]");
                if (dir == null)
                {
                    Debug.LogError("ERROR: Next cell dir == null");
                    break;
                }

                preCell = curCell;
                curCell = nextCell;
                nextCell = nextCell.grid.gridArray[nextCell.gridPosX + dir[0], nextCell.gridPosY, nextCell.gridPosZ + dir[1]];

                if (nextCell == preCell) // loop around 1 cell end cap
                {
                    if (preDir[0] == 0 && preDir[1] == 0) // assign preDir when topright startingpoint is end cap
                    {
                        preDir[0] = 1;
                    }

                    int[] midDir = GetMiddleDirection(preDir[0], preDir[1], dir[0], dir[1]);

                    curPoint = GetCornerPoint(curCell.boxCol, preDir[0], preDir[1], midDir[0], midDir[1], true);
                    cornerPoints = GetCornerPointOffset(curPoint, preDir[0], preDir[1], midDir[0], midDir[1]);

                    if (curPoint == startingPoint && points.Count > 2) // end loop when startingpoint is end cap
                    {
                        continue;
                    }

                    points.Add(cornerPoints[0]);
                    points.Add(cornerPoints[1]);

                    curPoint = GetCornerPoint(curCell.boxCol, midDir[0], midDir[1], dir[0], dir[1], true);
                    cornerPoints = GetCornerPointOffset(curPoint, midDir[0], midDir[1], dir[0], dir[1]);

                    points.Add(cornerPoints[0]);
                    points.Add(cornerPoints[1]);
                }
                else if (preDir[0] != dir[0] && preDir[1] != dir[1]) // corners
                {
                    if (CheckCurCellConcave(preDir[0], preDir[1], dir[0], dir[1])) // concave corner
                    {
                        //points.RemoveAt(points.Count - 1);
                        curPoint = GetCornerPointConcave(curCell.boxCol, preDir[0], preDir[1], dir[0], dir[1], true);
                        cornerPoints = GetCornerPointOffsetConcave(curPoint, preDir[0], preDir[1], dir[0], dir[1]);

                        points.Add(cornerPoints[0]);
                        points.Add(cornerPoints[1]);
                    }
                    else // convex corner
                    {
                        curPoint = GetCornerPoint(curCell.boxCol, preDir[0], preDir[1], dir[0], dir[1], true);
                        cornerPoints = GetCornerPointOffset(curPoint, preDir[0], preDir[1], dir[0], dir[1]);

                        points.Add(cornerPoints[0]);
                        points.Add(cornerPoints[1]);
                    }
                }
                else // straight
                {
                    curPoint = GetStraightPoint(curCell.boxCol, dir[0], dir[1], true);
                    points.Add(curPoint);
                }

                preDir = dir;
                usedCells.Add(curCell);
            }
        }

        points.Add(points[0]);
        points.Add(points[1]);

        if (movement)
            DrawMovementPerimiter(points);
        else
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] -= Vector3.up * 0.02f;
            }
            DrawAttackPerimiter(points);
        }
            

        return usedCells;
    }


    public void HandleObstacles(List<CombatGridCell> obstacleCells, List<CombatGridCell> allCells, bool movement)
    {
        List<CombatGridCell> usedCells = new List<CombatGridCell>();
        List<CombatGridCell> cellsToRemove = new List<CombatGridCell>();
        bool edge = false;
        int loopCount = 0;
        while (obstacleCells.Count != 0)
        {
            loopCount++;
            if (loopCount > 999) //infinite loop safety net
            {
                Debug.LogError($"HandleObstacles While Loop Got Stuck!!!");
                break;
            }

            CheckAdjacentObstacles(obstacleCells[0], allCells, out edge, out cellsToRemove);

            if (!edge)
            {
                OutlinePoints(cellsToRemove, false, movement);
            }

            foreach (CombatGridCell cell in cellsToRemove)
            {
                usedCells.Add(cell);
                obstacleCells.Remove(cell);
            }

            cellsToRemove.Clear();
            edge = false;
        }
    }

    public void CheckAdjacentObstacles(CombatGridCell startingCell, List<CombatGridCell> allCells, out bool edge, out List<CombatGridCell> usedCells)
    {
        edge = false;
        usedCells = new List<CombatGridCell>();
        usedCells.Add(startingCell);
        int count = 0;
        int index = 0;
        int loopCount = 0;

        while (usedCells.Count != count)
        {
            loopCount++;
            if (loopCount > 999) //infinite loop safety net
            {
                Debug.LogError($"CheckAdjacentObstacles While Loop Got Stuck!!!");
                break;
            }

            count = usedCells.Count;
            for (int i = index; i < usedCells.Count; i++)
            {
                CombatGridCell curCell = usedCells[i];

                if (curCell.gridPosZ > 0)
                {
                    CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                    if (!allCells.Contains(newCell))
                        edge = true;
                    else if (!newCell.accessible && !usedCells.Contains(newCell))
                        usedCells.Add(newCell);
                }
                if (curCell.gridPosX > 0)
                {
                    CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                    if (!allCells.Contains(newCell))
                        edge = true;
                    else if (!newCell.accessible && !usedCells.Contains(newCell))
                        usedCells.Add(newCell);
                }
                if (curCell.gridPosZ < grid.height - 1)
                {
                    CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                    if (!allCells.Contains(newCell))
                        edge = true;
                    else if (!newCell.accessible && !usedCells.Contains(newCell))
                        usedCells.Add(newCell);
                }
                if (curCell.gridPosX < grid.width - 1)
                {
                    CombatGridCell newCell = CheckCellOutlineRight(curCell);
                    if (!allCells.Contains(newCell))
                        edge = true;
                    else if (!newCell.accessible && !usedCells.Contains(newCell))
                        usedCells.Add(newCell);
                }
            }

            index = count;
        }
    }

    public static CombatGridCell GetTopRightCornerCell(List<CombatGridCell> cells)
    {
        int curX = 0;
        int curZ = 0;
        CombatGridCell topRight = null;

        foreach (CombatGridCell cell in cells)
        {
            if (cell.accessible /*&& !cell.occupied*/)
            {
                if (cell.gridPosX >= curX)
                {
                    if (cell.gridPosZ >= curZ)
                    {
                        curX = cell.gridPosX;
                        curZ = cell.gridPosZ;
                        topRight = cell;
                    }
                }
            }
        }

        //Vector3 TR = topRight.boxCol.transform.TransformPoint(new Vector3(topRight.boxCol.size.x * 1, 0.5f, topRight.boxCol.size.z * 1) * 0.5f);
        //Vector3 TL = topRight.boxCol.transform.TransformPoint(new Vector3(topRight.boxCol.size.x * -1, 0.5f, topRight.boxCol.size.z * 1) * 0.5f);
        //Vector3 BL = topRight.boxCol.transform.TransformPoint(new Vector3(topRight.boxCol.size.x * -1, 0.5f, topRight.boxCol.size.z * -1) * 0.5f);
        //Vector3 BR = topRight.boxCol.transform.TransformPoint(new Vector3(topRight.boxCol.size.x * 1, 0.5f, topRight.boxCol.size.z * -1) * 0.5f);

        //outlines.gridCellVertices.Add(TR);
        //outlines.gridCellVertices.Add(TL);
        //outlines.gridCellVertices.Add(BL);
        //outlines.gridCellVertices.Add(BR);

        //outlines.triangles.Add(new GridTris(BR, BL, TL));
        //outlines.triangles.Add(new GridTris(TL, TR, BR));

        //topRight.movementImg.color = Color.cyan;
        return topRight;
    }

    public static CombatGridCell GetTopRightObstructedCell(List<CombatGridCell> cells)
    {
        int curX = 0;
        int curZ = 0;
        CombatGridCell topRight = null;

        foreach (CombatGridCell cell in cells)
        {
            if (cell.gridPosX >= curX)
            {
                if (cell.gridPosZ >= curZ)
                {
                    curX = cell.gridPosX;
                    curZ = cell.gridPosZ;
                    topRight = cell;
                }
            }
        }

        //topRight.movementImg.color = Color.cyan;
        return topRight;
    }

    public static Vector3 GetCornerPoint(BoxCollider col, int preModX, int preModZ, int modX, int modZ, bool inset) // modX + = right, - = left | modZ + = top, - = bottom
    {
        float percent = 1;
        if (inset == true)
            percent = 1f;

        modX = modX == 0 ? preModX : -modX;
        modZ = modZ == 0 ? preModZ : -modZ;

        return col.transform.TransformPoint(col.center +
            new Vector3(col.size.x * modX * percent, 0.1f, col.size.z * modZ * percent) * 0.5f);
    }

    public static Vector3 GetCornerPointConcave(BoxCollider col, int preModX, int preModZ, int modX, int modZ, bool inset) // modX + = right, - = left | modZ + = top, - = bottom
    {
        float percent = 1;
        if (inset == true)
            percent = 1f;

        modX = modX == 0 ? -preModX : modX;
        modZ = modZ == 0 ? -preModZ : modZ;

        return col.transform.TransformPoint(col.center +
            new Vector3(col.size.x * modX * percent, 0.1f, col.size.z * modZ * percent) * 0.5f);
    }

    public static Vector3 GetStraightPoint(BoxCollider col, int modX, int modZ, bool inset) // modX + = right, - = left | modZ + = top, - = bottom
    {
        float percent = 1;
        if (inset == true)
            percent = 1f;

        if (modX == 0)
            modX = modZ == 1 ? -1 : 1;
        if (modZ == 0)
            modZ = modX;

        return col.transform.TransformPoint(col.center +
            new Vector3(col.size.x * modX * percent, 0.1f, col.size.z * modZ * percent) * 0.5f);
    }

    public static Vector3[] GetCornerPointOffset(Vector3 point, int preModX, int preModZ, int modX, int modZ)
    {
        modX = modX == 0 ? -preModX : modX;
        modZ = modZ == 0 ? -preModZ : modZ;

        float offset = 0.1f;
        Vector3[] cornerPoints = new Vector3[2];
        var h = Vector3.right * modX * offset;
        var v = Vector3.forward * modZ * offset;

        if (modX + modZ == 0)
        {
            cornerPoints[0] = point + v;
            cornerPoints[1] = point + h;
        }
        else
        {
            cornerPoints[0] = point + h;
            cornerPoints[1] = point + v;
        }

        return cornerPoints;
    }

    public static Vector3[] GetCornerPointOffsetConcave(Vector3 point, int preModX, int preModZ, int modX, int modZ)
    {
        modX = modX == 0 ? -preModX : modX;
        modZ = modZ == 0 ? -preModZ : modZ;

        float offset = 0.1f;
        Vector3[] cornerPoints = new Vector3[2];
        var h = Vector3.right * modX * offset;
        var v = Vector3.forward * modZ * offset;

        if (modX + modZ == 0)
        {
            cornerPoints[0] = point + h;
            cornerPoints[1] = point + v;
        }
        else
        {
            cornerPoints[0] = point + v;
            cornerPoints[1] = point + h;
        }

        return cornerPoints;
    }

    public static int[] GetMiddleDirection(int preModX, int preModZ, int modX, int modZ)
    {
        int[] midDir = new int[2];

        if (preModX == 0 && modX == 0)
        {
            midDir[0] = preModZ;
            midDir[1] = 0;
        }
        else
        {
            midDir[0] = 0;
            midDir[1] = modX;
        }

        return midDir;
    }

    private int[] GetNextCellPointDirection(CombatGridCell curCell, CombatGridCell preCell, List<CombatGridCell> outline)
    {
        int dirX = 0;
        int dirZ = 0;

        if (preCell != null)
        {
            dirX = curCell.gridPosX - preCell.gridPosX;
            dirZ = curCell.gridPosZ - preCell.gridPosZ;
        }
        else
        {
            dirX = 1;
            dirZ = 0;
        }

        if (dirX == 1) // right
        {
            if (curCell.gridPosZ < grid.height - 1)
            {
                CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, 1 };
                    return dir;
                }
            }
            if (curCell.gridPosX < grid.width - 1)
            {
                CombatGridCell newCell = CheckCellOutlineRight(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ > 0)
            {
                CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, -1 };
                    return dir;
                }
            }
            if (curCell.gridPosX > 0)
            {
                CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                if (newCell != null && newCell.accessible)
                {
                    int[] dir = { -1, 0 };
                    return dir;
                }
            }
        }
        else if (dirX == -1) // left
        {
            if (curCell.gridPosZ > 0)
            {
                CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, -1 };
                    return dir;
                }
            }
            if (curCell.gridPosX > 0)
            {
                CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { -1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ < grid.height - 1)
            {
                CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, 1 };
                    return dir;
                }
            }
            if (curCell.gridPosX < grid.width - 1)
            {
                CombatGridCell newCell = CheckCellOutlineRight(curCell);
                if (newCell != null && newCell.accessible)
                {
                    int[] dir = { 1, 0 };
                    return dir;
                }
            }
        }
        else if (dirZ == 1) // top
        {
            if (curCell.gridPosX > 0)
            {
                CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { -1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ < grid.height - 1)
            {
                CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, 1 };
                    return dir;
                }
            }
            if (curCell.gridPosX < grid.width - 1)
            {
                CombatGridCell newCell = CheckCellOutlineRight(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ > 0)
            {
                CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                if (newCell != null && newCell.accessible)
                {
                    int[] dir = { 0, -1 };
                    return dir;
                }
            }
        }
        else if (dirZ == -1) // bottom
        {
            if (curCell.gridPosX < grid.width - 1)
            {
                CombatGridCell newCell = CheckCellOutlineRight(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ > 0)
            {
                CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, -1 };
                    return dir;
                }
            }
            if (curCell.gridPosX > 0)
            {
                CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                if (newCell != null && newCell.accessible && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { -1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ < grid.height - 1)
            {
                CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                if (newCell != null && newCell.accessible)
                {
                    int[] dir = { 0, 1 };
                    return dir;
                }
            }
        }

        return null;
    }

    private int[] GetNextCellPointDirectionObstructed(CombatGridCell curCell, CombatGridCell preCell, List<CombatGridCell> outline)
    {
        int dirX = 0;
        int dirZ = 0;

        if (preCell != null)
        {
            dirX = curCell.gridPosX - preCell.gridPosX;
            dirZ = curCell.gridPosZ - preCell.gridPosZ;
        }
        else
        {
            dirX = 1;
            dirZ = 0;
        }

        if (dirX == 1) // right
        {
            if (curCell.gridPosZ < grid.height - 1)
            {
                CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, 1 };
                    return dir;
                }
            }
            if (curCell.gridPosX < grid.width - 1)
            {
                CombatGridCell newCell = CheckCellOutlineRight(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ > 0)
            {
                CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, -1 };
                    return dir;
                }
            }
            if (curCell.gridPosX > 0)
            {
                CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied))
                {
                    int[] dir = { -1, 0 };
                    return dir;
                }
            }
        }
        else if (dirX == -1) // left
        {
            if (curCell.gridPosZ > 0)
            {
                CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, -1 };
                    return dir;
                }
            }
            if (curCell.gridPosX > 0)
            {
                CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { -1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ < grid.height - 1)
            {
                CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, 1 };
                    return dir;
                }
            }
            if (curCell.gridPosX < grid.width - 1)
            {
                CombatGridCell newCell = CheckCellOutlineRight(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied))
                {
                    int[] dir = { 1, 0 };
                    return dir;
                }
            }
        }
        else if (dirZ == 1) // top
        {
            if (curCell.gridPosX > 0)
            {
                CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { -1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ < grid.height - 1)
            {
                CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, 1 };
                    return dir;
                }
            }
            if (curCell.gridPosX < grid.width - 1)
            {
                CombatGridCell newCell = CheckCellOutlineRight(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ > 0)
            {
                CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied))
                {
                    int[] dir = { 0, -1 };
                    return dir;
                }
            }
        }
        else if (dirZ == -1) // bottom
        {
            if (curCell.gridPosX < grid.width - 1)
            {
                CombatGridCell newCell = CheckCellOutlineRight(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ > 0)
            {
                CombatGridCell newCell = CheckCellOutlineBelow(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { 0, -1 };
                    return dir;
                }
            }
            if (curCell.gridPosX > 0)
            {
                CombatGridCell newCell = CheckCellOutlineLeft(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied) && outline.Contains(newCell) && newCell != preCell)
                {
                    int[] dir = { -1, 0 };
                    return dir;
                }
            }
            if (curCell.gridPosZ < grid.height - 1)
            {
                CombatGridCell newCell = CheckCellOutlineAbove(curCell);
                if (newCell != null && (!newCell.accessible || newCell.occupied))
                {
                    int[] dir = { 0, 1 };
                    return dir;
                }
            }
        }

        return null;
    }

    private CombatGridCell CheckCellOutlineLeft(CombatGridCell curCell)
    {
        return grid.gridArray[curCell.gridPosX - 1, curCell.gridPosY, curCell.gridPosZ];
    }
    private CombatGridCell CheckCellOutlineRight(CombatGridCell curCell)
    {
        return grid.gridArray[curCell.gridPosX + 1, curCell.gridPosY, curCell.gridPosZ];
    }
    private CombatGridCell CheckCellOutlineAbove(CombatGridCell curCell)
    {
        return grid.gridArray[curCell.gridPosX, curCell.gridPosY, curCell.gridPosZ + 1];
    }
    private CombatGridCell CheckCellOutlineBelow(CombatGridCell curCell)
    {
        return grid.gridArray[curCell.gridPosX, curCell.gridPosY, curCell.gridPosZ - 1];
    }

    private bool CheckCurCellConcave(int preModX, int preModZ, int modX, int modZ)
    {
        if (preModZ == -1 && modX == 1)
            return true;

        if (preModX == -1 && modZ == -1)
            return true;

        if (preModZ == 1 && modX == -1)
            return true;

        if (preModX == 1 && modZ == 1)
            return true;

        return false;
    }

    //public void GenerateMesh()
    //{

    //    verts = new Vector3[gridCellVertices.Count];
    //    for (int i = 0; i < verts.Length; i++)
    //    {
    //        verts[i] = gridCellVertices[i];
    //    }

    //    tris = new int[triangles.Count * 3];

    //    for (int i = 0; i < triangles.Count; i++)
    //    {
    //        int n = (i + 1) * 3;
    //        tris[n - 3] = GetVertIndex(triangles[i].vert1);
    //        tris[n - 2] = GetVertIndex(triangles[i].vert2);
    //        tris[n - 1] = GetVertIndex(triangles[i].vert3);
    //    }

    //    mf.mesh = mesh = new Mesh();
    //    mesh.name = "Test";
    //    mesh.vertices = verts;
    //    mesh.triangles = tris;
    //    mesh.RecalculateNormals();
    //}

    //private void OnDrawGizmos()
    //{
    //    if (verts == null)
    //    {
    //        return;
    //    }

    //    Gizmos.color = Color.yellow;
    //    for (int i = 0; i < verts.Length; i++)
    //    {
    //        Gizmos.DrawSphere(verts[i], 0.05f);
    //    }
    //}

    //private int GetVertIndex(Vector3 vert)
    //{
    //    for(int i = 0; i<verts.Length;  i++)
    //    {
    //        if (verts[i] == vert)
    //            return i;
    //    }

    //    Debug.LogError($"Triangle Vert {vert} missing from mesh vertices");
    //    return 0;
    //}
}
