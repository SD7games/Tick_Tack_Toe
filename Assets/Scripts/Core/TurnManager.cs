
public class TurnManager
{
    private bool _isPlayerTurn;

    public string CurrentSymbol => _isPlayerTurn ? "X" : "O";

    public void NextTurn() => _isPlayerTurn = !_isPlayerTurn;
    public void Reset() => _isPlayerTurn = false;
}
