using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    [SerializeField]
    private Image[] _winLines;

    public enum WinLineType
    {
        RowUp, RowMidl, RowDown,
        ColLeft, ColMidl, ColRight,
        DiagonalUpLeft, DiagonalUpRight
    }

    public void ShowWinLine(WinLineType lineType)
    {
        HideAllLines();

        _winLines[(int) lineType].gameObject.SetActive(true);
    }

    public void HideAllLines()
    {
        foreach (var image in _winLines)
        {
            image?.gameObject.SetActive(false);
        }
    }
}
