using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _titleText;
    [SerializeField]
    private Button _restartButton;

    public void SetRestartListener(UnityAction callback)
    {
        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(callback);
    }

    public void ShowCurrentPlayer(string name)
    {
        _titleText.text = $"Move: \n{name}";
    }

    public void ShowResult(string resultText)
    {
        _titleText.text = resultText;
    }
}
