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
    private string _currentSymbol => _isPlayerTurn ? "X" : "O";

    private WinChecker _winChecker = new WinChecker();
    private BoardController _board;

    private void Start()
    {
        _board = new BoardController(_buttons);

        for (int i = 0; i < _buttons.Count; i++)
        {
            RegisterClick(i);
        }
        _restartButton.onClick.AddListener(RestartGame);
        RestartGame();
    }

    private void RegisterClick(int index)
    {
        _buttons[index].onClick.RemoveAllListeners();
        _buttons[index].onClick.AddListener(() => OnCellClicked(index));
    }

    private void RestartGame()
    {
        _board.Reset();
        _isPlayerTurn = false;
        _playerText.text = $"Move: Player {_currentSymbol}";
    }

    private void OnCellClicked(int index)
    {
        if (!_board.IsCellEmpty(index)) return;

        _board.SetCell(index, _currentSymbol);

        string[,] boardState = _board.GetBoardState();

        if (_winChecker.IsGameOver(boardState, out string winner))
        {
            if (winner != null)
                _playerText.text = $"Player {winner} wins!";
            else
                _playerText.text = "Draw!";
            _board.DisableAll();
        }
        else
        {
            _isPlayerTurn = !_isPlayerTurn;
            _playerText.text = $"Move: Player {_currentSymbol}";
        }
    }
}
