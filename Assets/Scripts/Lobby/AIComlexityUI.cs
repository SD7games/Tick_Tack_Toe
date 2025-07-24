using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIComlexityUI : MonoBehaviour
{
    [SerializeField]
    private Button _mainButton;
    [SerializeField]
    private Button _opt1Button;
    [SerializeField]
    private Button _opt2Button;
    [SerializeField]
    private CanvasGroup _opt1CanvasGroup;
    [SerializeField]
    private CanvasGroup _opt2CanvasGroup;
    [SerializeField]
    private RectTransform _mainRT;
    [SerializeField]
    private RectTransform _opt1RT;
    [SerializeField]
    private RectTransform _opt2RT;

    private Vector2 _mainPos, _opt1Pos, _opt2Pos;

    private Dictionary<string, Color> _difficultyColors = new()
    {
        { "Easy", Color.green },
        { "Norm", Color.yellow },
        { "Hard", Color.red }
    };

    private string _currentDifficutly;

    private bool _optionsVisible = false;
    private bool _canPulse = true;

    private void Start()
    {
        _mainPos = _mainRT.anchoredPosition;
        _opt1Pos = _opt1RT.anchoredPosition;
        _opt2Pos = _opt2RT.anchoredPosition;

        _currentDifficutly = PlayerPrefs.GetString("Difficutly", "Easy");
        StartCoroutine(PulseRoutine());
        UpdateMainButton(_currentDifficutly);
        HideOptionsInstant();

        _mainButton.onClick.AddListener(ToggleOptions);
        _opt1Button.onClick.AddListener(() => OnSelect(_opt1Button));
        _opt2Button.onClick.AddListener(() => OnSelect(_opt2Button));
    }

    private IEnumerator PulseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);

            if (_canPulse)
            {
                _mainRT.DOKill();
                _mainRT.localScale = Vector3.one;

                _mainRT.DOScale(1.2f, 0.2f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        _mainRT.DOScale(1f, 0.2f).SetEase(Ease.InSine);
                    });
            }
        }
    }

    private void ToggleOptions()
    {
        if (_optionsVisible)
            HideOptionsInstant();
        else
            ShowOptions();
    }

    private void OnSelect(Button selectedButton)
    {
        string newDiff = selectedButton.GetComponentInChildren<TMP_Text>().text;

        _currentDifficutly = newDiff;
        PlayerPrefs.SetString("Difficutly", _currentDifficutly);
        PlayerPrefs.Save();

        UpdateMainButton(_currentDifficutly);
        HideOptionsInstant();
    }

    private void ShowOptions()
    {
        _canPulse = false;

        List<string> otherOptions = new(_difficultyColors.Keys);
        otherOptions.Remove(_currentDifficutly);

        SetupOption(_opt1Button, otherOptions[0]);
        SetupOption(_opt2Button, otherOptions[1]);

        _opt1RT.anchoredPosition = _opt1Pos;
        _opt2RT.anchoredPosition = _opt2Pos;

        _opt1CanvasGroup.alpha = 1f;
        _opt2CanvasGroup.alpha = 1f;

        _opt1CanvasGroup.interactable = true;
        _opt2CanvasGroup.interactable = true;
        _opt1CanvasGroup.blocksRaycasts = true;
        _opt2CanvasGroup.blocksRaycasts = true;

        _optionsVisible = true;
    }

    private void HideOptionsInstant()
    {
        _opt1CanvasGroup.alpha = 0f;
        _opt2CanvasGroup.alpha = 0f;

        _opt1CanvasGroup.interactable = false;
        _opt2CanvasGroup.interactable = false;

        _opt1CanvasGroup.blocksRaycasts = false;
        _opt2CanvasGroup.blocksRaycasts = false;

        _opt1RT.anchoredPosition = _opt1Pos;
        _opt2RT.anchoredPosition = _opt2Pos;

        _optionsVisible = false;
        _canPulse = true;
    }

    private void SetupOption(Button button, string diff)
    {
        button.GetComponentInChildren<TMP_Text>().text = diff;
        button.GetComponent<Image>().color = _difficultyColors[diff];
    }

    private void UpdateMainButton(string diff)
    {
        _mainButton.GetComponentInChildren<TMP_Text>().text = diff;
        _mainButton.GetComponent<Image>().color = _difficultyColors[diff];
    }
}
