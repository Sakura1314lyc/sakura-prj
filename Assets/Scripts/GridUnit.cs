using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridUnit : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = new(0.35f, 0.85f, 1f);

    private GridGameManager gameManager;
    private SpriteRenderer spriteRenderer;

    public Vector2Int GridPosition { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor;
    }

    public void Initialize(GridGameManager manager, Vector2Int startPosition)
    {
        gameManager = manager;
        GridPosition = startPosition;
        transform.position = gameManager.GridToWorld(startPosition);
    }

    public bool TryMove(Vector2Int direction)
    {
        return gameManager != null && gameManager.TryMoveUnit(this, direction);
    }

    public void SetSelected(bool selected)
    {
        spriteRenderer.color = selected ? selectedColor : normalColor;
    }

    public void SetGridPosition(Vector2Int gridPosition, Vector3 worldPosition)
    {
        GridPosition = gridPosition;
        transform.position = worldPosition;
    }
}
