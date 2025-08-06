using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _titleText;
    [SerializeField]
    private Button _restartButton;

    public event Action OnRestartClicked;

    private void Awake()
    {
        _restartButton.onClick.AddListener(() => OnRestartClicked.Invoke());
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
