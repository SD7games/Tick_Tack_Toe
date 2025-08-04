using UnityEngine;

public class WinChecker
{
    public bool IsGameOver(Sprite[,] board, out Sprite winner, out BoardView.WinLineType? winLine)
    {
        winner = null;
        winLine = null;

        int[][] lines = new int[][]
        {
            new[] { 0, 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6, 7, 8 },
            new[] { 0, 3, 6 },
            new[] { 1, 4, 7 },
            new[] { 2, 5, 8 },
            new[] { 0, 4, 8 },
            new[] { 2, 4, 6 }
        };

        for (int i = 0; i < lines.Length; i++)
        {
            int a = lines[i][0];
            int b = lines[i][1];
            int c = lines[i][2];

            int rowA = a / 3, colA = a % 3;
            int rowB = b / 3, colB = b % 3;
            int rowC = c / 3, colC = c % 3;

            Sprite cellA = board[rowA, colA];
            Sprite cellB = board[rowB, colB];
            Sprite cellC = board[rowC, colC];
            if (cellA != null && cellA == cellB && cellB == cellC)
            {
                winner = cellA;
                winLine = (BoardView.WinLineType) i;
                return true;
            }
        }

        foreach (var cell in board)
        {
            if (cell == null) return false;
        }

        return true;
    }
}


