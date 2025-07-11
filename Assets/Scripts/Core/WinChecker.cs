
public class WinChecker
{
    public string CheckWinner(string[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            // Horizontal
            if (!string.IsNullOrEmpty(board[i, 0]) &&
                board[i, 0] == board[i, 1] &&
                board[i, 1] == board[i, 2])
                return board[i, 0];


            // Vertical
            if (!string.IsNullOrEmpty(board[0, i]) &&
                board[0, i] == board[1, i] &&
                board[1, i] == board[2, i])
                return board[0, i];
        }

        // DiagonalUpLeft
        if (!string.IsNullOrEmpty(board[0, 0]) &&
            board[0, 0] == board[1, 1] &&
            board[1, 1] == board[2, 2])
            return board[0, 0];

        //DiagonalUpRight
        if (!string.IsNullOrEmpty(board[0, 2]) &&
            board[0, 2] == board[1, 1] &&
            board[1, 1] == board[2, 0])
            return board[0, 2];

        return null;
    }

    public bool IsDraw(string[,] board)
    {
        foreach (var cell in board)
        {
            if (string.IsNullOrEmpty(cell))
                return false;
        }
        return true;
    }

    public bool IsGameOver(string[,] board, out string winner)
    {
        winner = CheckWinner(board);
        if (winner is not null) return true;
        if (IsDraw(board))
        {
            winner = null;
            return true;
        }
        return false;
    }
}


