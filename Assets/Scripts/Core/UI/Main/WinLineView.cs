using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinLineView : MonoBehaviour
{
    [SerializeField] private Image[] _winLines;

    public enum WinLineType
    {
        RowUp, RowMiddle, RowDown,
        ColLeft, ColMiddle, ColRight,
        DiagonalUpLeft, DiagonalUpRight
    }

    [SerializeField] private float _fillDuration = 0.7f;

    public void ShowWinLine(WinLineType lineType)
    {
        HideAllLines();

        Image target = _winLines[(int) lineType];
        target.gameObject.SetActive(true);

        target.fillAmount = 0;
        target.DOFillAmount(1f, _fillDuration)
              .SetEase(Ease.OutQuad);
    }

    public void HideAllLines()
    {
        foreach (var image in _winLines)
        {
            image.DOKill();
            image.fillAmount = 1;
            image.gameObject.SetActive(false);
        }
    }
}
