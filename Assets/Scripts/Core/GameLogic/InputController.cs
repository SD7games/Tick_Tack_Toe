using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputController
{
    private readonly List<Button> _buttons;
    private bool _isBlocked;

    public delegate void CellClicked(int index);
    public event CellClicked OnCellClicked;

    public InputController(List<Button> buttons)
    {
        _buttons = buttons ?? throw new System.ArgumentNullException(nameof(buttons));

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
    }
    
    public void AllowInput()
    {
        _isBlocked = false;
    }
    
    public void SetInteractable(bool interactable)
    {
        foreach (var button in _buttons)
        {
            if (button != null)
                button.interactable = interactable;
        }
    }
    
    public void TemporarilyBlock(MonoBehaviour context, float duration)
    {
        if (context == null)
        {
            Debug.LogWarning("InputController: context is null, cannot start coroutine.");
            return;
        }

        context.StartCoroutine(UnblockAfterDelay(duration));
    }

    private IEnumerator UnblockAfterDelay(float duration)
    {
        BlockInput();
        yield return new WaitForSeconds(duration);
        AllowInput();
    }

    private void OnButtonClick(int index)
    {
        if (_isBlocked) return;
        if (_buttons[index] == null || !_buttons[index].interactable) return;

        OnCellClicked?.Invoke(index);
    }
   
    public void SimulateClick(int index)
    {
        if (_buttons[index] == null || !_buttons[index].interactable) return;
        OnCellClicked?.Invoke(index);
    }
}
