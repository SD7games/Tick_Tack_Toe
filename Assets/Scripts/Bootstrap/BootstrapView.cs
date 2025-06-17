using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BootstrapView : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Image _progressImage;

    [SerializeField]
    private TMP_Text _progressText;

    private int _currentProgress = 0;
    private Tween _imageTween;

    private void Start()
    {
        AnimationImage();
    }

    private void AnimationImage()
    {
        _imageTween = _progressImage.rectTransform.DOScale(1.2f, 0.6f).SetEase(Ease.OutBack).SetLoops(10, LoopType.Yoyo);
    }

    public void SetProgress(int progress)
    {
        DOTween.Kill(this);

        DOTween.To(() => _currentProgress, x =>
        {
            _currentProgress = x;
            _slider.value = x / 100f;
            _progressText.text = $"{x}%";
        }, progress, (progress - _currentProgress) * 0.02f).SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        if (_imageTween != null && _imageTween.IsActive())
        {
            _imageTween.Kill();
        }
    }
}

