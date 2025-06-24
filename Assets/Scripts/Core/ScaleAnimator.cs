using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScaleAnimator : MonoBehaviour
{
    [SerializeField]
    private float _duration = 0.5f;
    [SerializeField]
    private float _targetScale = 1.2f;
    [SerializeField]
    private Ease _ease = Ease.OutBack;
    [SerializeField]
    private bool _loop = false;

    private Vector3 _originalScale;

    private Image _targetImage;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    public void Animate(Image targetImage)
    {
        _targetImage = targetImage;
        _targetImage.transform.DOScale(_originalScale * _targetScale, _duration)
            .SetEase(_ease)
            .SetLoops(_loop ? -1 : 0, LoopType.Yoyo);
    }

    public void DoKillAnimate()
    {
        _targetImage.transform.DOKill();
    }
}
