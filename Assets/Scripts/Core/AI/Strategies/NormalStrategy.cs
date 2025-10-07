using System.Collections.Generic;
using UnityEngine;

public class NormalStrategy : IAIStrategy
{
    public int GetMove(int[] board)
    {
        List<int> availableMoves = new List<int>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
                availableMoves.Add(i);
        }

        if (availableMoves.Count == 0)
            return -1;

        // 1. First try win
        int winMove = FindWinningMove(board, 2);
        if (winMove >= 0)
            return winMove;

        // 2. Then block player
        int blockMove = FindWinningMove(board, 1);
        if (blockMove >= 0)
            return blockMove;

        // 3. 50% select center, if it is free
        if (board[4] == 0 && Random.value < 0.5f)
            return 4;

        // 4. 25% select corner
        List<int> corners = new List<int> { 0, 2, 6, 8 };
        List<int> availableCorners = corners.FindAll(i => board[i] == 0);
        if (availableCorners.Count > 0 && Random.value < 0.25f)
            return availableCorners[Random.Range(0, availableCorners.Count)];

        // 5. Else random move
        return availableMoves[Random.Range(0, availableMoves.Count)];
    }

    private int FindWinningMove(int[] board, int mark)
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
            {
                board[i] = mark;
                bool win = IsWinner(board, mark);
                board[i] = 0;
                if (win) return i;
            }
        }
        return -1;
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
}
