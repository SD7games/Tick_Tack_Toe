using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect _scrollRect;
    [SerializeField]
    private Button _upButton;
    [SerializeField]
    private Button _downButton;
    [SerializeField]
    private float _scrollStep = 0.1f;
    [SerializeField]
    private float _animationDuration = 0.3f;
    [SerializeField]
    private ContentScrollController _contentScrollController;

    private Tween _currentTween;

    private void Start()
    {
        _contentScrollController.OnGenerationComplete += UpdateCanvasForce;
        _scrollRect.onValueChanged.AddListener(_ => UpdateButtonState());
        _upButton.onClick.AddListener(() => ScrollUp());
        _downButton.onClick.AddListener(() => ScrollDown());
        UpdateCanvasForce();
    }

    private void OnDestroy()
    {
        _contentScrollController.OnGenerationComplete -= UpdateCanvasForce;
    }

    private void UpdateButtonState()
    {
        float pos = _scrollRect.verticalNormalizedPosition;
        _upButton.interactable = pos < 0.99f;
        _downButton.interactable = pos > 0.01f;
    }

    private void UpdateCanvasForce()
    {
        _upButton.interactable = false;
        Canvas.ForceUpdateCanvases();
    }

    private void ScrollUp()
    {
        float target = Mathf.Clamp01(_scrollRect.verticalNormalizedPosition + _scrollStep);
        AnimateScroll(target);
    }

    private void ScrollDown()
    {
        float target = Mathf.Clamp01(_scrollRect.verticalNormalizedPosition - _scrollStep);
        AnimateScroll(target);
    }

    private void AnimateScroll(float target)
    {
        _currentTween?.Kill();

        _currentTween = DOTween.To(
        () => _scrollRect.verticalNormalizedPosition,
        x => _scrollRect.verticalNormalizedPosition = x,
        target,
        _animationDuration
        ).SetEase(Ease.OutCubic)
        .OnUpdate(UpdateButtonState)
        .OnComplete(UpdateButtonState);
    }
}
