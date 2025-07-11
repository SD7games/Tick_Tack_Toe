using System.Collections.Generic;
using UnityEngine.UI;

public class InputController
{
    private List<Button> _buttons;
    public delegate void CellClicked(int index);
    public event CellClicked OnCellClicked;

    public InputController(List<Button> buttons)
    {
        _buttons = buttons;
        for (int i = 0; i < _buttons.Count; i++)
        {
            int index = i;
            _buttons[i].onClick.RemoveAllListeners();
            _buttons[i].onClick.AddListener(() => OnCellClicked?.Invoke(index));
        }
    }
}
