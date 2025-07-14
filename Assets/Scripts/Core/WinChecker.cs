
public class WinChecker
{
    public bool IsGameOver(string[,] board, out string winner, out BoardView.WinLineType? winType)
    {
        winner = null;
        winType = null;

        for (int row = 0; row < 3; row++)
        {
            if (!string.IsNullOrEmpty(board[row, 0]) &&
                board[row, 0] == board[row, 1] &&
                board[row, 1] == board[row, 2])
            {
                winner = board[row, 0];
                winType = (BoardView.WinLineType) row; 
                return true;
            }
        }

        for (int col = 0; col < 3; col++)
        {
            if (!string.IsNullOrEmpty(board[0, col]) &&
                board[0, col] == board[1, col] &&
                board[1, col] == board[2, col])
            {
                winner = board[0, col];
                winType = (BoardView.WinLineType) (3 + col);
                return true;
            }
        }

        if (!string.IsNullOrEmpty(board[0, 0]) &&
            board[0, 0] == board[1, 1] &&
            board[1, 1] == board[2, 2])
        {
            winner = board[0, 0];
            winType = BoardView.WinLineType.DiagonalUpLeft;
            return true;
        }

        if (!string.IsNullOrEmpty(board[0, 2]) &&
            board[0, 2] == board[1, 1] &&
            board[1, 1] == board[2, 0])
        {
            winner = board[0, 2];
            winType = BoardView.WinLineType.DiagonalUpRight;
            return true;
        }

        // Draw
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                if (string.IsNullOrEmpty(board[r, c]))
                    return false; 

        return true; 
    }  
}


