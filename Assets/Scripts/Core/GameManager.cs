using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> _buttons;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private TMP_Text _playerText;

    private bool _isPlayerTurn = false;
    private bool _isWin = false;
    private bool _isDraw = false;

    private WinChecker _winChecker = new WinChecker();

    private void Start()
    {
        CleanButtonText();

        for (int i = 0; i < _buttons.Count; i++)
        {
            int index = i;
            _buttons[i].onClick.AddListener(() => OnCellClicked(index));
        }
        _restartButton.onClick.AddListener(RestartGame);
        UpdatePlayerText();
    }

    private void CleanButtonText()
    {
        foreach (var button in _buttons)
        {
            button.GetComponentInChildren<TMP_Text>().text = "";
            button.interactable = true;
        }
    }

    private void RestartGame()
    {
        CleanButtonText();
        _isWin = false;
        _isDraw = false;
        _isPlayerTurn = false;
        UpdatePlayerText();
    }

    private void UpdatePlayerText()
    {
        if (_isWin)
            _playerText.text = _isPlayerTurn ? "Player O wins!" : "Player X wins!";
        else
            _playerText.text = _isPlayerTurn ? "Move:   Player X" : "Move:   Player O";

        if (_isDraw)
            _playerText.text = "Draw!";
    }

    private void OnCellClicked(int index)
    {
        if (_isWin || _isDraw) return;

        TMP_Text buttonText = _buttons[index].GetComponentInChildren<TMP_Text>();

        if (!string.IsNullOrEmpty(buttonText.text)) return;

        buttonText.text = _isPlayerTurn ? "X" : "O";
        _buttons[index].interactable = false;

        CheckWinner();

        if (!_isWin || !_isDraw)
        {
            _isPlayerTurn = !_isPlayerTurn;
            UpdatePlayerText();
        }
    }

    private void CheckWinner()
    {
        string[,] board = GetBoardState();

        string winner = _winChecker.CheckWinner(board);

        if (winner != null)
        {
            _isWin = true;
            foreach (var button in _buttons)
                button.interactable = false;
        }
        else if (_winChecker.IsDraw(board))
        {
            _isDraw = true;
        }

        UpdatePlayerText();
    }

    private string[,] GetBoardState()
    {
        string[,] board = new string[3, 3];
        for (int i = 0; i < _buttons.Count; i++)
        {
            TMP_Text text = _buttons[i].GetComponentInChildren<TMP_Text>();
            int row = i / 3;
            int col = i % 3;
            board[row, col] = text.text;
        }
        return board;
    }
}
