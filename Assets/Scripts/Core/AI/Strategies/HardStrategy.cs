using System;

public class HardStrategy : IAIStrategy
{
    public int TryGetMove(int[] board)
    {
        int bestScore = int.MinValue;
        int move = -1;

        foreach (int i in GameLogicHelper.GetAvailableMoves(board))
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

        return move;
    }

    private int Minimax(int[] board, int depth, bool isMaximizing)
    {
        var board2D = GameLogicHelper.To2DBoard(board);
        if (GameLogicHelper.CheckWinner(board2D, out CellState winner, out _))
        {
            return winner switch
            {
                CellState.AI => 10 - depth,
                CellState.Player => depth - 10,
                _ => 0 
            };
        }

        if (isMaximizing)
        {
            int best = int.MinValue;
            foreach (int i in GameLogicHelper.GetAvailableMoves(board))
            {
                board[i] = 2;
                best = Math.Max(best, Minimax(board, depth + 1, false));
                board[i] = 0;
            }
            return best;
        }
        else
        {
            int best = int.MaxValue;
            foreach (int i in GameLogicHelper.GetAvailableMoves(board))
            {
                board[i] = 1;
                best = Math.Min(best, Minimax(board, depth + 1, true));
                board[i] = 0;
            }
            return best;
        }
    }
}
