using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> _buttons;
    [SerializeField]
    private UIView _uiView;
    [SerializeField]
    private BoardView _boardView;
    [SerializeField]
    private TMP_Text _playerName;
    [SerializeField]
    private TMP_Text _aiRivalName;
    [SerializeField]
    private Button _backToLobbyButton;
    [SerializeField]
    private Image _sceneFaderImage;
    [SerializeField]
    private EmojiData _emojiData;
    [SerializeField]
    private AIRivalMoveController _aiRivalController;

    private float _fadeDuration = 1.5f;

    private Sprite _playerSprite;
    private Sprite _aiRivalSprite;
    private Sprite _defaultSprite;

    private InputController _input;
    private BoardController _board;
    private TurnManager _turnManager;
    private WinChecker _winChecker;

    private const string LobbyScene = "Lobby";

    private void Start()
    {
        SetSpriteReferences();
        UpdatePlayerNames();
        GetGameLogic();
        GetInput();

        _aiRivalController.Initialize(_input, _board);

        _uiView.OnRestartClicked += RestartGame;

        _backToLobbyButton.onClick.AddListener(() => LoadLobbyScene());

        RestartGame();
    }

    private void UpdatePlayerNames()
    {
        _playerName.text = PlayerPrefsAIManager.Player.GetName();
    }

    private void OnDestroy()
    {
        if (_input != null)
            _input.OnCellClicked -= OnCellClicked;

        if (_board != null)
            _uiView.OnRestartClicked -= RestartGame;
    }

    private void GetGameLogic()
    {
        _board = new BoardController(_buttons, _defaultSprite);
        _turnManager = new TurnManager(_playerSprite, _aiRivalSprite, _playerName, _aiRivalName);
        _winChecker = new WinChecker();
    }

    private void GetInput()
    {
        _input = new InputController(_buttons);
        _input.OnCellClicked += OnCellClicked;
    }

    private void SetSpriteReferences()
    {
        _defaultSprite = _buttons[0].GetComponent<Image>().sprite;

        _playerSprite = _emojiData.GetEmojiByIndex(PlayerPrefsAIManager.Player.GetEmojiIndex());
        _aiRivalSprite = _emojiData.GetEmojiByIndex(PlayerPrefsAIManager.AI.GetEmojiAIIndex());
    }

    private void RestartGame()
    {
        _aiRivalController.StopAllCoroutines();
        _input.AllowInput();
        SceneFader();
        _board.Reset();
        _turnManager.Reset();
        _boardView.HideAllLines();
        _uiView.ShowCurrentPlayer(_turnManager.CurrentName());
    }

    private void SceneFader()
    {
        _sceneFaderImage.canvasRenderer.SetAlpha(1f);
        _sceneFaderImage.gameObject.SetActive(true);
        _sceneFaderImage.CrossFadeAlpha(0f, _fadeDuration, false);
    }

    private void OnCellClicked(int index)
    {
        if (!_board.IsCellEmpty(index)) return;

        CellState currentState = _turnManager.CurrentState();
        Sprite currentSprite = _turnManager.CurrentSprite();

        _board.SetCell(index, currentState, currentSprite);
        CellState[,] boardState = _board.GetBoardState();

        if (_winChecker.IsGameOver(boardState, out CellState winner, out BoardView.WinLineType? winLine))
        {
            if (winner != CellState.Empty)
            {
                if (winLine.HasValue)
                    _boardView.ShowWinLine(winLine.Value);

                string winnerName = GetNameByState(winner);
                _uiView.ShowResult($"\n{winnerName} wins!");
            }
            else
            {
                _uiView.ShowResult("Draw!");
            }

            _board.DisableAll(boardState);
        }
        else
        {
            _turnManager.NextTurn();
            _uiView.ShowCurrentPlayer(_turnManager.CurrentName());

            if (_turnManager.CurrentState() == CellState.AI)
            {
                _aiRivalController.MakeMove();
            }
        }
    }

    private string GetNameByState(CellState state)
    {
        return state switch
        {
            CellState.Player => _playerName.text,
            CellState.AI => _aiRivalName.text,
            _ => "Unknown"
        };
    }

    public void LoadLobbyScene()
    {
        _aiRivalController.StopAllCoroutines();
        _input.AllowInput();
        SceneManager.LoadScene(LobbyScene);
    }
}
