using System.Collections.Generic;
using UnityEngine;

public class EasyStrategy : IAIStrategy
{
    private static readonly int[] PreferredOrder = { 4, 0, 2, 6, 8, 1, 3, 5, 7 };

    public int TryGetMove(int[] board)
    {
        // Get all available cells
        List<int> available = GameLogicHelper.GetAvailableMoves(board);
        if (available.Count == 0) return -1; // board full

        // 1. Try to win
        int move = GameLogicHelper.FindWinningMove(board, 2);
        if (move >= 0) return move;

        // 2. Try to block player
        move = GameLogicHelper.FindWinningMove(board, 1);
        if (move >= 0) return move;

        // 3. Pick preferred cell (center → corners → sides)
        foreach (int i in PreferredOrder)
            if (board[i] == 0)
                return i;

        // 4. Fallback random move
        return available[Random.Range(0, available.Count)];
    }
}
