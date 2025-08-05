using TMPro;
using UnityEngine;

public class TurnManager
{
    private bool _isPlayerTurn;
    private Sprite _playerSprite;
    private Sprite _aiSprite;
    private TMP_Text _playerName;
    private TMP_Text _aiName;

    public TurnManager(Sprite playerSprite, Sprite aiSprite, TMP_Text playerName, TMP_Text aiName)
    {
        _playerSprite = playerSprite;
        _aiSprite = aiSprite;
        _playerName = playerName;
        _aiName = aiName;
        _isPlayerTurn = true;
    }

    public void Reset()
    {
        _isPlayerTurn = true;
    }

    public void NextTurn()
    {
        _isPlayerTurn = !_isPlayerTurn;
    }

    public Sprite CurrentSprite()
    {
        return _isPlayerTurn ? _playerSprite : _aiSprite;
    }

    public CellState CurrentState()
    {
        return _isPlayerTurn ? CellState.Player : CellState.AI;
    }

    public string CurrentName()
    {
        return _isPlayerTurn ? _playerName.text : _aiName.text;
    }
}
