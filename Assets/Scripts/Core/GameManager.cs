using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> _buttons;
    [SerializeField]
    private UIController _uiController;
    [SerializeField]
    private BoardView _boardView;
    [SerializeField]
    private Image _playerImage;
    [SerializeField]
    private Image _aiRivalImage;
    [SerializeField]
    private Image _emptyImage;
    [SerializeField]
    private TMP_Text _playerName;
    [SerializeField]
    private TMP_Text _aiRivalName;

    private Sprite _playerSprite;
    private Sprite _aiRivalSprite;
    private Sprite _emptySprite;

    private InputController _input;
    private BoardController _board;
    private TurnManager _turnManager;
    private WinChecker _winChecker;

    private void Start()
    {
        _playerSprite = _playerImage.sprite;
        _aiRivalSprite = _aiRivalImage.sprite;
        _emptySprite = _emptyImage.sprite;

        _input = new InputController(_buttons);
        _input.OnCellClicked += OnCellClicked;

        _board = new BoardController(_buttons, _emptySprite);
        _turnManager = new TurnManager(_playerSprite, _aiRivalSprite, _playerName, _aiRivalName);
        _winChecker = new WinChecker();

        _uiController.SetRestartListener(RestartGame);
        RestartGame();
    }

    private void OnDestroy()
    {
        if (_input != null)
            _input.OnCellClicked -= OnCellClicked;
    }

    private void RestartGame()
    {
        _board.Reset();
        _turnManager.Reset();
        _boardView.HideAllLines();
        _uiController.ShowCurrentPlayer(_turnManager.CurrentName());
    }

    private void OnCellClicked(int index)
    {
        if (!_board.IsCellEmpty(index)) return;

        _board.SetCell(index, _turnManager.currentSprite);
        Sprite[,] boardState = _board.GetBoardState();

        if (_winChecker.IsGameOver(boardState, out Sprite winner, out BoardView.WinLineType? winLine))
        {
            if (winner != null)
            {
                if (winLine.HasValue)
                    _boardView.ShowWinLine(winLine.Value);

                string winnerName = GetNameBySprite(winner);
                _uiController.ShowResult($"Player \n{winnerName} wins!");
            }
            else
            {
                _uiController.ShowResult("Draw!");
            }

            _board.DisableAll();
        }
        else
        {
            _turnManager.NextTurn();
            _uiController.ShowCurrentPlayer(_turnManager.CurrentName());
        }
    }
    private string GetNameBySprite(Sprite sprite)
    {
        if (sprite == _playerSprite) return _playerName.text;
        if (sprite == _aiRivalSprite) return _aiRivalName.text;
        return "Unknown";
    }
}
