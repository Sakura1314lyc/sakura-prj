using UnityEngine;

public class UnitInputController : MonoBehaviour
{
    [SerializeField] private GridGameManager gameManager;

    private int selectedUnitIndex;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GridGameManager>();
        }

        HighlightSelectedUnit();
    }

    private void Update()
    {
        if (gameManager == null || gameManager.Units.Count == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectedUnitIndex = (selectedUnitIndex + 1) % gameManager.Units.Count;
            HighlightSelectedUnit();
        }

        var moveDirection = ReadMoveDirection();
        if (moveDirection == Vector2Int.zero)
        {
            return;
        }

        gameManager.Units[selectedUnitIndex].TryMove(moveDirection);
    }

    private Vector2Int ReadMoveDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            return Vector2Int.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            return Vector2Int.down;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            return Vector2Int.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            return Vector2Int.right;
        }

        return Vector2Int.zero;
    }

    private void HighlightSelectedUnit()
    {
        for (var i = 0; i < gameManager.Units.Count; i++)
        {
            gameManager.Units[i].SetSelected(i == selectedUnitIndex);
        }
    }
}
