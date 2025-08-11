using System.Collections.Generic;
using UnityEngine.UI;

public class InputController
{
    private List<Button> _buttons;
    private bool _isBlocked = false;

    public delegate void CellClicked(int index);
    public event CellClicked OnCellClicked;

    public InputController(List<Button> buttons)
    {
        _buttons = buttons;

        for (int i = 0; i < _buttons.Count; i++)
        {
            int index = i;
            _buttons[i].onClick.RemoveAllListeners();
            _buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    public void BlockInput()
    {
        _isBlocked = true;
        SetInteractable(false);
    }

    public void AllowInput()
    {
        _isBlocked = false;
        SetInteractable(true);
    }

    public void SetInteractable(bool interactable)
    {
        foreach (var button in _buttons)
        {
            button.interactable = interactable;
        }
    }

    private void OnButtonClick(int index)
    {
        if (_isBlocked) return;
        if (_buttons[index].interactable)
            OnCellClicked?.Invoke(index);
    }

    public void SimulateClick(int index)
    {
        OnCellClicked?.Invoke(index);
    }
}
