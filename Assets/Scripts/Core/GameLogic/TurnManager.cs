using TMPro;
using UnityEngine;

public class TurnManager
{
    public Sprite currentSprite;
    public TMP_Text currentName;

    private TMP_Text _playerName;
    private TMP_Text _aiRivalName;
    private Sprite _playerSprite;
    private Sprite _aiRivalSprite;
    private bool _isPlayerTurn;

    public TurnManager(Sprite playerSprite, Sprite aiRivalSprite, TMP_Text playerName, TMP_Text aiRivalName)
    {
        _playerName = playerName;
        _aiRivalName = aiRivalName;
        _playerSprite = playerSprite;
        _aiRivalSprite = aiRivalSprite;
        Reset();
    }

    public void Reset()
    {
        _isPlayerTurn = true;
        currentSprite = _playerSprite;
        currentName = _playerName;
    }

    public void NextTurn()
    {
        _isPlayerTurn = !_isPlayerTurn;

        if (_isPlayerTurn)
        {
            currentSprite = _playerSprite;
            currentName = _playerName;
        }
        else
        {
            currentSprite = _aiRivalSprite;
            currentName = _aiRivalName;
        }
    }

    public string CurrentName()
    {
        return currentName.text;
    }
}
