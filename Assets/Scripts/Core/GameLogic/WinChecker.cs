public class WinChecker
{
    public bool IsGameOver(CellState[,] board, out CellState winner, out WinLineView.WinLineType? winLine)
    {
        winner = CellState.Empty;
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

            CellState cellA = board[rowA, colA];
            CellState cellB = board[rowB, colB];
            CellState cellC = board[rowC, colC];

            if (cellA != CellState.Empty && cellA == cellB && cellB == cellC)
            {
                winner = cellA;
                winLine = (WinLineView.WinLineType) i;
                return true;
            }
        }

        foreach (var cell in board)
        {
            if (cell == CellState.Empty) return false;
        }

        return true;
    }
}
