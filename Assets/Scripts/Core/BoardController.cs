using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class BoardController
{
    private List<Button> _buttons;

    public BoardController(List<Button> buttons)
    {
        _buttons = buttons;
    }

    public string[,] GetBoardState()
    {
        string[,] board = new string[3, 3];
        for (int i = 0; i < _buttons.Count; i++)
        {
            int row = i / 3;
            int col = i % 3;
            board[row, col] = _buttons[i].GetComponentInChildren<TMP_Text>().text;
        }
        return board;
    }

    public void Reset()
    {
        foreach (var button in _buttons)
        {
            button.GetComponentInChildren<TMP_Text>().text = "";
            button.interactable = true;
        }
    }

    public void SetCell(int index, string symbol)
    {
        var text = _buttons[index].GetComponentInChildren<TMP_Text>();
        text.text = symbol;
        _buttons[index].interactable = false;
    }

    public bool IsCellEmpty(int index)
    {
        return string.IsNullOrEmpty(_buttons[index].GetComponentInChildren<TMP_Text>().text);
    }

    public void DisableAll()
    {
        foreach (var button in _buttons)
        {
            button.interactable = false;
        }
    }
}
