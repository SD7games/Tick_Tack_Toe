using System;

public class HardStrategy : IAIStrategy
{
    public int GetMove(int[] board)
    {
        int bestScore = int.MinValue;
        int move = -1;

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
            {
                board[i] = 2;
                int score = Minimax(board, 0, false);
                board[i] = 0;

                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }

        return move;
    }

    private int Minimax(int[] board, int depth, bool isMaximizing)
    {
        int result = Evaluate(board);
        if (result != 0 || IsBoardFull(board))
            return result;

        if (isMaximizing)
        {
            int best = int.MinValue;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    board[i] = 2;
                    best = Math.Max(best, Minimax(board, depth + 1, false));
                    board[i] = 0;
                }
            }
            return best;
        }
        else
        {
            int best = int.MaxValue;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    board[i] = 1;
                    best = Math.Min(best, Minimax(board, depth + 1, true));
                    board[i] = 0;
                }
            }
            return best;
        }
    }

    private int Evaluate(int[] board)
    {
        if (IsWinner(board, 2)) return +10;
        if (IsWinner(board, 1)) return -10;
        return 0;
    }

    private bool IsWinner(int[] b, int mark)
    {
        int[,] wins = {
            {0,1,2},{3,4,5},{6,7,8},
            {0,3,6},{1,4,7},{2,5,8},
            {0,4,8},{2,4,6}
        };

        for (int i = 0; i < wins.GetLength(0); i++)
        {
            if (b[wins[i, 0]] == mark && b[wins[i, 1]] == mark && b[wins[i, 2]] == mark)
                return true;
        }
        return false;
    }

    private bool IsBoardFull(int[] board)
    {
        foreach (var cell in board)
            if (cell == 0) return false;
        return true;
    }
}
