using System.Collections;
using UnityEngine;

public class AIRivalMoveController : MonoBehaviour
{
    private IAIStrategy _strategy;
    private InputController _input;
    private BoardController _board;

    public void Initialize(InputController input, BoardController board)
    {
        _input = input;
        _board = board;

        LoadStrategy();
    }

    private void LoadStrategy()
    {
        string strategyName = AISettingManager.AI.GetStrategy();
        switch (strategyName)
        {
            case "Easy":
                _strategy = new EasyStrategy();
                break;
            case "Normal":
                _strategy = new NormalStrategy();
                break;
            case "Hard":
                _strategy = new HardStrategy();
                break;
            default:
                _strategy = new EasyStrategy();
                break;
        }
    }

    public void MakeMove()
    {
        StartCoroutine(MakeMoveRoutine());
    }

    private IEnumerator MakeMoveRoutine()
    {
        _input.BlockInput();

        yield return new WaitForSeconds(0.4f);

        int[] boardState = _board.GetBoardAsIntArray();
        int moveIndex = _strategy.TryGetMove(boardState);

        if (moveIndex >= 0)
        {
            _input.SimulateClick(moveIndex);
            yield return new WaitForSeconds(0.4f);
        }

        _input.AllowInput();
    }
}
