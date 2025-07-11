using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{   
    [SerializeField]
    private TMP_Text _playerText;
    [SerializeField]
    private Button _restartButton;

    public void SetPlayerText(string text) => _playerText.text = text;
    public void SetRestartListener(UnityEngine.Events.UnityAction callback)
    {
        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(callback);
    }

    public void ShowCurrentPlayer(string symbol)
    {
        _playerText.text = $"Move: Player {symbol}";
    }

    public void ShowResult(string resultText)
    {
        _playerText.text = resultText;
    }  
}
