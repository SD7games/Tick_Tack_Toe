using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoardController
{
    private CellState[,] _board = new CellState[3, 3];
    private List<Button> _buttons;
    private Sprite _defaultSprite;

    public BoardController(List<Button> buttons, Sprite defaultSprite)
    {
        _buttons = buttons;
        _defaultSprite = defaultSprite;
        Reset();
    }

    public void Reset()
    {
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                _board[r, c] = CellState.Empty;
                int index = r * 3 + c;
                _buttons[index].image.enabled = true;
                _buttons[index].image.sprite = _defaultSprite;
                _buttons[index].interactable = true;
            }
        }
    }

    public CellState[,] GetBoardState()
    {
        return _board;
    }

    public void SetCell(int index, CellState state, Sprite sprite)
    {
        int row = index / 3;
        int col = index % 3;

        _board[row, col] = state;
        _buttons[index].image.sprite = sprite;
        _buttons[index].interactable = false;
    }

    public bool IsCellEmpty(int index)
    {
        int row = index / 3;
        int col = index % 3;
        return _board[row, col] == CellState.Empty;
    }

    public void DisableAll(CellState[,] boardState)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            int row = i / 3;
            int col = i % 3;

            var button = _buttons[i];

            if (boardState[row, col] == CellState.Empty)
            {
                button.image.enabled = false;
            }

            button.interactable = false;
        }
    }

    public int[] GetBoardAsIntArray()
    {
        var result = new int[9];
        var state = GetBoardState();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int index = i * 3 + j;
                result[index] = state[i, j] switch
                {
                    CellState.Player => 1,
                    CellState.AI => 2,
                    _ => 0
                };
            }
        }

        return result;
    }
}
