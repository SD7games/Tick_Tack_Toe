using UnityEngine;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    [SerializeField]
    private Image _targetImage;

    [SerializeField]
    private ScaleAnimator _scaleAnimator;

    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _upButton;

    [SerializeField]
    private Button _downButton;

    private void Start()
    {
        _scaleAnimator.Animate(_targetImage);
    }

    private void OnDestroy()
    {
        _scaleAnimator.DoKillAnimate();
    }
}
