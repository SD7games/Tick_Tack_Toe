using System.Collections.Generic;
using UnityEngine;

public class NormalStrategy : IAIStrategy
{
    public int TryGetMove(int[] board)
    {
        List<int> available = GameLogicHelper.GetAvailableMoves(board);
        if (available.Count == 0) return -1;

        // Try to win
        int move = GameLogicHelper.FindWinningMove(board, 2);
        if (move >= 0) return move;

        // Block player
        move = GameLogicHelper.FindWinningMove(board, 1);
        if (move >= 0) return move;

        // Try center with 50% probability
        if (board[4] == 0 && Random.value < 0.5f)
            return 4;

        // Try random corner with 25% probability
        int[] corners = { 0, 2, 6, 8 };
        List<int> freeCorners = new List<int>();
        foreach (int i in corners) if (board[i] == 0) freeCorners.Add(i);
        if (freeCorners.Count > 0 && Random.value < 0.25f)
            return freeCorners[Random.Range(0, freeCorners.Count)];

        // Random fallback
        return available[Random.Range(0, available.Count)];
    }
}
