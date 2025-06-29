using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    private const string MainSceneTag = "Main";

    [SerializeField]
    private Image _targetImage;
    [SerializeField]
    private ScaleAnimator _scaleAnimator;
    [SerializeField]
    private Button _startButton;      

    private void Start()
    {
        _scaleAnimator.Animate(_targetImage);
        _startButton.onClick.AddListener(() => LoadMainScene());
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(MainSceneTag);
    }

    private void OnDestroy()
    {
        _scaleAnimator.DoKillAnimate();
    }
}
