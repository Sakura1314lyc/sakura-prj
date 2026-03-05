using System.Collections.Generic;
using UnityEngine;

public class GridGameManager : MonoBehaviour
{
    [Header("网格")]
    [SerializeField] private int width = 12;
    [SerializeField] private int height = 8;
    [SerializeField] private float cellSize = 1f;

    [Header("预制体")]
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GridUnit unitPrefab;

    [Header("布局")]
    [SerializeField] private List<Vector2Int> obstaclePositions = new()
    {
        new Vector2Int(3, 2),
        new Vector2Int(3, 3),
        new Vector2Int(3, 4),
        new Vector2Int(6, 5),
        new Vector2Int(7, 5)
    };

    [SerializeField] private List<Vector2Int> unitSpawnPositions = new()
    {
        new Vector2Int(1, 1),
        new Vector2Int(1, 3)
    };

    private readonly HashSet<Vector2Int> obstacles = new();
    private readonly Dictionary<Vector2Int, GridUnit> occupiedCells = new();
    private readonly List<GridUnit> units = new();

    public IReadOnlyList<GridUnit> Units => units;

    private void Start()
    {
        BuildFloor();
        PlaceObstacles();
        SpawnUnits();
    }

    private void BuildFloor()
    {
        if (floorPrefab == null)
        {
            Debug.LogWarning("未设置地板预制体。");
            return;
        }

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Instantiate(floorPrefab, GridToWorld(new Vector2Int(x, y)), Quaternion.identity, transform);
            }
        }
    }

    private void PlaceObstacles()
    {
        foreach (var cell in obstaclePositions)
        {
            if (!IsInsideGrid(cell) || !obstacles.Add(cell))
            {
                continue;
            }

            if (obstaclePrefab != null)
            {
                Instantiate(obstaclePrefab, GridToWorld(cell), Quaternion.identity, transform);
            }
        }
    }

    private void SpawnUnits()
    {
        if (unitPrefab == null)
        {
            Debug.LogError("未设置单位预制体。");
            return;
        }

        foreach (var spawnCell in unitSpawnPositions)
        {
            if (!IsCellWalkable(spawnCell))
            {
                Debug.LogWarning($"无法在 {spawnCell} 生成单位：该格子被阻挡或超出网格范围。");
                continue;
            }

            var unit = Instantiate(unitPrefab, GridToWorld(spawnCell), Quaternion.identity);
            unit.Initialize(this, spawnCell);
            occupiedCells.Add(spawnCell, unit);
            units.Add(unit);
        }
    }

    public bool TryMoveUnit(GridUnit unit, Vector2Int direction)
    {
        if (direction == Vector2Int.zero)
        {
            return false;
        }

        var origin = unit.GridPosition;
        var destination = origin + direction;

        if (!IsCellWalkable(destination, unit))
        {
            return false;
        }

        occupiedCells.Remove(origin);
        occupiedCells[destination] = unit;
        unit.SetGridPosition(destination, GridToWorld(destination));

        return true;
    }

    public bool IsCellWalkable(Vector2Int cell, GridUnit movingUnit = null)
    {
        if (!IsInsideGrid(cell) || obstacles.Contains(cell))
        {
            return false;
        }

        if (occupiedCells.TryGetValue(cell, out var unitAtCell))
        {
            return unitAtCell == movingUnit;
        }

        return true;
    }

    public bool IsInsideGrid(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < width && cell.y >= 0 && cell.y < height;
    }

    public Vector3 GridToWorld(Vector2Int cell)
    {
        return new Vector3(cell.x * cellSize, cell.y * cellSize, 0f);
    }
}
