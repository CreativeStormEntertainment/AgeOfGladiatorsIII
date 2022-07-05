using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable, ExecuteInEditMode]
public class CombatGridCell : MonoBehaviour
{
    public BoxCollider boxCol;
    public GameObject cellBorder;
    public GameObject movementRange;
    //for testing
    public Image movementImg;
    public GameObject hoverHighlight;
    public CombatGrid grid;
    public bool occupied;
    public bool accessible = true;
    public LayerMask layer;
    public int gridPosX;
    public int gridPosY;
    public int gridPosZ;

    public GameObject CoverIcon;

    public Text debugText;

    public int gCost;
    public int hCost;
    public int fCost;

    public CombatGridCell prevCell;
    public List<CombatGridCell> adjacentCells = new List<CombatGridCell>();

    public bool canReach;
    public AnimationCurve offsetDis;
    private Coroutine coroutineAPCost;

    private void Awake()
    {
        StartCoroutine(CellInitialization());
    }

    public void CalcFCost()
    {
        fCost = gCost + hCost;
    }

    IEnumerator CellInitialization()
    {
        while (grid == null)
        {
            yield return null;
        }

        CheckCellState();
    }

    public void CheckCellState()
    {
        var colliders = Physics.OverlapSphere(transform.GetChild(0).transform.position, .1f);
        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].gameObject == transform.parent.gameObject || colliders[i].gameObject == transform.GetChild(0).gameObject)
                continue;

            if (colliders[i].gameObject.layer != layer && colliders[i].gameObject.tag != "TriggerNode" && colliders[i].gameObject.tag != "MovementNode")
            {
                if (colliders[i].gameObject.tag == "Player" || colliders[i].gameObject.tag == "Character")
                {
                    occupied = true;
                }
                else
                {
                    //Debug.LogWarning($"Cell {gridPosX}, {gridPosY}, {gridPosZ} inaccessible due to {colliders[i].gameObject.name}", colliders[i].gameObject);
                    accessible = false;
                }

            }
        }
    }

    public void FindAdjacentCells()
    {
        Vector3 center = boxCol.bounds.center;

        var top = RayCastAdjacent(center, transform.forward, Color.magenta);
        if (top != null)
            adjacentCells.Add(top);

        var bottom = RayCastAdjacent(center, -transform.forward, Color.red);
        if (bottom != null)
            adjacentCells.Add(bottom);

        var right = RayCastAdjacent(center, transform.right, Color.cyan);
        if (right != null)
            adjacentCells.Add(right);

        var left = RayCastAdjacent(center, -transform.right, Color.blue);
        if (left != null)
            adjacentCells.Add(left);

        if (top != null && left != null)
        {
            var topLeft = RayCastDiagonal(center, (-transform.right + transform.forward).normalized, Color.yellow);
            if (topLeft != null && CheckDiagonalIncline(top.transform, left.transform, topLeft.transform))
                adjacentCells.Add(topLeft);
        }

        if (top != null && right != null)
        {
            var topRight = RayCastDiagonal(center, (transform.right + transform.forward).normalized, Color.green);
            if (topRight != null && CheckDiagonalIncline(top.transform, right.transform, topRight.transform))
                adjacentCells.Add(topRight);
        }

        if (bottom != null && left != null)
        {
            var bottomLeft = RayCastDiagonal(center, (-transform.right + -transform.forward).normalized, Color.white);
            if (bottomLeft != null && CheckDiagonalIncline(bottom.transform, left.transform, bottomLeft.transform))
                adjacentCells.Add(bottomLeft);
        }

        if (bottom != null && right != null)
        {
            var bottomRight = RayCastDiagonal(center, (transform.right + -transform.forward).normalized, Color.black);
            if (bottomRight != null && CheckDiagonalIncline(bottom.transform, right.transform, bottomRight.transform))
                adjacentCells.Add(bottomRight);
        }

#if UNITY_EDITOR

        EditorUtility.SetDirty(this);

#endif
    }

    private CombatGridCell RayCastAdjacent(Vector3 center, Vector3 direction, Color color)
    {
        CombatGridCell cell = null;
        int mask = LayerMask.GetMask("Cell");
        RaycastHit hit;
        //Debug.DrawRay(center, direction * 1, color, 2);
        if (Physics.Raycast(center, direction, out hit, 1, mask))
        {
            cell = hit.transform.GetComponentInParent<CombatGridCell>();
        }
        return cell;
    }

    private CombatGridCell RayCastDiagonal(Vector3 center, Vector3 direction, Color color)
    {
        CombatGridCell cell = null;
        int mask = LayerMask.GetMask("Cell");
        RaycastHit hit;
        //Debug.DrawRay(center, direction * 1, color, 2);
        if (Physics.Raycast(center, direction, out hit, 1, mask))
        {
            var colliders = Physics.OverlapSphere(hit.point, .1f, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                cell = colliders[i].transform.GetComponentInParent<CombatGridCell>();

                if (cell == this || adjacentCells.Contains(cell))
                    cell = null;
                else
                    break;
            }
        }
        return cell;
    }

    private bool CheckDiagonalIncline(Transform sideA, Transform sideB, Transform diagonal)
    {
        if ((transform.rotation == sideA.rotation || transform.rotation == sideB.rotation) && (diagonal.rotation == sideA.rotation || diagonal.rotation == sideB.rotation))
            return true;

        return false;
    }

    public void GetMovementAPCost(CombatGridCell start)
    {
        if (coroutineAPCost != null)
        {
            StopCoroutine(coroutineAPCost);
            coroutineAPCost = null;
        }

        coroutineAPCost = StartCoroutine(MovementAPCost(start));
    }

    IEnumerator MovementAPCost(CombatGridCell start)
    {
        List<Vector3> _path = grid.GetPath(start, this, true, true);
        float _apCost = gCost / 10f;

        if (_path != null && _apCost <= Combat.instance.Attacking.combatActionPoints && gCost != 0)
        {
            List<Vector3> _optimizedPath = grid.OptimizeNonCombatPath(start, _path);

            grid.APCost.UpdateAP(boxCol.bounds.center, _apCost);
            grid.APCostLine.positionCount = _optimizedPath.Count;

            Vector3 endPoint = _optimizedPath[_optimizedPath.Count - 1];
            Vector3 prePoint = _optimizedPath[_optimizedPath.Count - 2];
            Vector3 dir = Vector3.Normalize(prePoint - endPoint);
            Vector3 offset = dir * offsetDis.Evaluate(Mathf.Abs(dir.x) + Mathf.Abs(dir.z) - 1);
            Vector3 newPoint = endPoint + offset;
            _optimizedPath[_optimizedPath.Count - 1] = newPoint;

            for (int i = 0; i < _optimizedPath.Count; i++)
            {
                grid.APCostLine.SetPosition(i, _optimizedPath[i]);
            }
            grid.APCostLine.gameObject.SetActive(true);

            canReach = true;
        }
        else
        {
            grid.APCost.rt.gameObject.SetActive(false);
            grid.APCostLine.gameObject.SetActive(false);
            hoverHighlight.gameObject.SetActive(false);

            canReach = false;
        }

        yield return null;

        coroutineAPCost = null;
    }

    public bool FindAdjacentCellsCover()
    {
        bool _cover = false;

        List<CombatGridCell> _adjacentCellsLocal = new List<CombatGridCell>();

        Vector3 center = boxCol.bounds.center;

        var top = RayCastAdjacent(center, transform.forward, Color.magenta);
        if (top != null)
            _adjacentCellsLocal.Add(top);

        var bottom = RayCastAdjacent(center, -transform.forward, Color.red);
        if (bottom != null)
            _adjacentCellsLocal.Add(bottom);

        var right = RayCastAdjacent(center, transform.right, Color.cyan);
        if (right != null)
            _adjacentCellsLocal.Add(right);

        var left = RayCastAdjacent(center, -transform.right, Color.blue);
        if (left != null)
            _adjacentCellsLocal.Add(left);

        foreach (CombatGridCell _cell in _adjacentCellsLocal)
        {
            if (!_cell.accessible)
                _cover = true;
        }

        return _cover;
    }

}
