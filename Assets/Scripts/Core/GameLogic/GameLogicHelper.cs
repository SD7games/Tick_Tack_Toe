using System.Collections.Generic;

public static class GameLogicHelper
{
    private static readonly int[,] Wins = {
        {0,1,2},{3,4,5},{6,7,8},
        {0,3,6},{1,4,7},{2,5,8},
        {0,4,8},{2,4,6}
    };

    // Convert linear board to 2D board
    public static CellState[,] To2DBoard(int[] board)
    {
        CellState[,] b = new CellState[3, 3];
        for (int i = 0; i < board.Length; i++)
        {
            b[i / 3, i % 3] = board[i] switch
            {
                1 => CellState.Player,
                2 => CellState.AI,
                _ => CellState.Empty
            };
        }
        return b;
    }

    public static bool CheckWinner(CellState[,] board, out CellState winner, out int? winLineIndex)
    {
        winner = CellState.Empty;
        winLineIndex = null;

        for (int i = 0; i < Wins.GetLength(0); i++)
        {
            int a = Wins[i, 0], b = Wins[i, 1], c = Wins[i, 2];
            int rowA = a / 3, colA = a % 3;
            int rowB = b / 3, colB = b % 3;
            int rowC = c / 3, colC = c % 3;

            CellState cellA = board[rowA, colA];
            CellState cellB = board[rowB, colB];
            CellState cellC = board[rowC, colC];

            if (cellA != CellState.Empty && cellA == cellB && cellB == cellC)
            {
                winner = cellA;
                winLineIndex = i;
                return true;
            }
        }

        // Check for draw
        foreach (var cell in board)
            if (cell == CellState.Empty) return false;

        winner = CellState.Empty;
        return true; 
    }

    public static int FindWinningMove(int[] board, int mark)
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
            {
                board[i] = mark;
                var board2D = To2DBoard(board);
                bool win = CheckWinner(board2D, out CellState winner, out _);
                board[i] = 0;
                if (win && winner != CellState.Empty)
                    return i;
            }
        }
        return -1;
    }

    public static List<int> GetAvailableMoves(int[] board)
    {
        List<int> moves = new List<int>();
        for (int i = 0; i < board.Length; i++)
            if (board[i] == 0)
                moves.Add(i);
        return moves;
    }
}
