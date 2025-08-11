using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    private const string MainSceneTag = "Main";

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Image _sceneFaderImage;

    private float _fadeDuration = 1.5f;

    private void Start()
    {
        SceneFader();
        _startButton.onClick.AddListener(() => LoadMainScene());
    }

    private void SceneFader()
    {
        _sceneFaderImage.gameObject.SetActive(true);
        _sceneFaderImage.canvasRenderer.SetAlpha(1f);
        _sceneFaderImage.CrossFadeAlpha(0f, _fadeDuration, false);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(MainSceneTag);
    }
}
