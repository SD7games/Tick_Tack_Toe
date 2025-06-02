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
        TMP_Text buttonText = _buttons[index].GetComponentInChildren<TMP_Text>();

        if (!string.IsNullOrEmpty(buttonText.text)) return;

        buttonText.text = _isPlayerTurn ? "X" : "O";
        _buttons[index].interactable = false;

        CheckWinner();

        _isPlayerTurn = !_isPlayerTurn;
        UpdatePlayerText();
    }

    private void CheckWinner()
    {
        string[,] board = new string[3, 3];
        for (int i = 0; i < _buttons.Count; i++)
        {
            TMP_Text text = _buttons[i].GetComponentInChildren<TMP_Text>();
            int row = i / 3;
            int col = i % 3;
            board[row, col] = text.text;
        }

        string winner = null;


        for (int i = 0; i < 3; i++)
        {
            if (!string.IsNullOrEmpty(board[i, 0]) &&
                        board[i, 0] == board[i, 1] &&
                        board[i, 1] == board[i, 2])
            {
                winner = board[i, 0];
            }

            if (!string.IsNullOrEmpty(board[0, i]) &&
                        board[0, i] == board[1, i] &&
                        board[1, i] == board[2, i])
            {
                winner = board[0, i];
            }
        }

        if (!string.IsNullOrEmpty(board[0, 0]) &&
                    board[0, 0] == board[1, 1] &&
                    board[1, 1] == board[2, 2])
        {
            winner = board[0, 0];
        }

        if (!string.IsNullOrEmpty(board[0, 2]) &&
                    board[0, 2] == board[1, 1] &&
                    board[1, 1] == board[2, 0])
        {
            winner = board[0, 2];
        }

        if (winner is not null)
        {
            _isWin = true;
            foreach (var button in _buttons)
                button.interactable = false;

            return;
        }

        foreach (var button in _buttons)
        {
            string checkButton = button.GetComponentInChildren<TMP_Text>().text;
            if (string.IsNullOrEmpty(checkButton))
            {
                _isDraw = false;
                break;
            }
            else
            {
                _isDraw = true;
            }
        }

        UpdatePlayerText();
    }
}
