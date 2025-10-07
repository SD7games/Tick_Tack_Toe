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
    private LoopType _loopType = LoopType.Yoyo;
    [SerializeField]
    private bool _loop = false;
    [SerializeField]
    private Image _targetImage;

    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private void Start()
    {
        Animate();
    }

    private void OnDestroy()
    {
        if (_targetImage != null && _targetImage.transform != null)
        {
            _targetImage.transform.DOKill();
        }
    }

    public void Animate()
    {
        _targetImage.transform.DOScale(_originalScale * _targetScale, _duration)
            .SetEase(_ease)
            .SetLoops(_loop ? -1 : 0, _loopType)
            .SetAutoKill(true);
    }
}
