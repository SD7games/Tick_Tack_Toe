using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoardController
{
    private List<Button> _buttons;
    private Sprite _emptySprite;

    public BoardController(List<Button> buttons, Sprite emptySprite)
    {
        _buttons = buttons;
        _emptySprite = emptySprite;
    }

    public Sprite[,] GetBoardState()
    {
        Sprite[,] board = new Sprite[3, 3];
        for (int i = 0; i < _buttons.Count; i++)
        {
            int row = i / 3;
            int col = i % 3;
            board[row, col] = _buttons[i].image.sprite;
        }
        return board;
    }

    public void Reset()
    {
        foreach (var button in _buttons)
        {
            button.image.sprite = _emptySprite;
            button.interactable = true;
        }
    }

    public void SetCell(int index, Sprite emojiSprite)
    {
        _buttons[index].image.sprite = emojiSprite;
        _buttons[index].interactable = false;
    }

    public bool IsCellEmpty(int index)
    {
        return _buttons[index].image.sprite == _emptySprite;
    }

    public void DisableAll()
    {
        foreach (var button in _buttons)
        {
            button.interactable = false;
        }
    }
}
