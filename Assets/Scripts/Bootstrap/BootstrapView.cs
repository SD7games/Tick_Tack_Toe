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

    [SerializeField]
    private ScaleAnimator _scaleAnimator;

    private int _currentProgress = 0;

    private void Start()
    {
        _scaleAnimator.Animate(_progressImage);
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
        _scaleAnimator.DoKillAnimate();
    }
}

