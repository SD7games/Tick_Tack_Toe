using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIComlexityUI : MonoBehaviour
{
    [SerializeField] private Button _mainButton;
    [SerializeField] private Button _opt1Button;
    [SerializeField] private Button _opt2Button;
    [SerializeField] private CanvasGroup _opt1CanvasGroup;
    [SerializeField] private CanvasGroup _opt2CanvasGroup;
    [SerializeField] private RectTransform _mainRT;
    [SerializeField] private RectTransform _opt1RT;
    [SerializeField] private RectTransform _opt2RT;

    private Dictionary<string, Color> _difficultyColors = new()
    {
        { "Easy", Color.green },
        { "Norm", Color.yellow },
        { "Hard", Color.red }
    };

    private string _currentDifficulty;
    private bool _optionsVisible = false;
    private bool _canPulse = true;

    public event Action<string> OnDifficultyChanged;

    private void Start()
    {
        _currentDifficulty = PlayerPrefsAIManager.AI.GetStrategy();
        StartCoroutine(PulseRoutine());
        UpdateMainButton(_currentDifficulty);
        HideOptionsInstant();

        _mainButton.onClick.AddListener(ToggleOptions);
        _opt1Button.onClick.AddListener(() => OnSelect(_opt1Button));
        _opt2Button.onClick.AddListener(() => OnSelect(_opt2Button));
    }

    #region Button Animations
    private IEnumerator PulseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            if (_canPulse)
                PulseMainButton();
        }
    }

    private void PulseMainButton()
    {
        _mainRT.DOKill();
        _mainRT.localScale = Vector3.one;
        _mainRT.DOScale(1.2f, 0.2f).SetEase(Ease.InOutSine)
               .OnComplete(() => _mainRT.DOScale(1f, 0.2f).SetEase(Ease.InSine));
    }
    #endregion

    #region Options Logic
    private void ToggleOptions()
    {
        if (_optionsVisible)
            HideOptionsInstant();
        else
            ShowOptions();
    }

    private void ShowOptions()
    {
        _canPulse = false;

        List<string> otherOptions = new(_difficultyColors.Keys);
        otherOptions.Remove(_currentDifficulty);

        SetupOption(_opt1Button, otherOptions[0], _opt1CanvasGroup, _opt1RT);
        SetupOption(_opt2Button, otherOptions[1], _opt2CanvasGroup, _opt2RT);

        _optionsVisible = true;
    }

    private void HideOptionsInstant()
    {
        ResetOption(_opt1CanvasGroup, _opt1RT);
        ResetOption(_opt2CanvasGroup, _opt2RT);

        _optionsVisible = false;
        _canPulse = true;
    }

    private void SetupOption(Button button, string diff, CanvasGroup cg, RectTransform rt)
    {
        button.GetComponentInChildren<TMP_Text>().text = diff;
        button.GetComponent<Image>().color = _difficultyColors[diff];

        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        rt.anchoredPosition = rt.anchoredPosition;
    }

    private void ResetOption(CanvasGroup cg, RectTransform rt)
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        rt.anchoredPosition = rt.anchoredPosition;
    }

    private void OnSelect(Button selectedButton)
    {
        string newDiff = selectedButton.GetComponentInChildren<TMP_Text>().text;
        _currentDifficulty = newDiff;

        PlayerPrefsAIManager.AI.SetStrategy(_currentDifficulty);
        PlayerPrefsAIManager.Save();

        UpdateMainButton(_currentDifficulty);
        HideOptionsInstant();

        OnDifficultyChanged?.Invoke(_currentDifficulty);
    }

    private void UpdateMainButton(string diff)
    {
        _mainButton.GetComponentInChildren<TMP_Text>().text = diff;
        _mainButton.GetComponent<Image>().color = _difficultyColors[diff];
    }
    #endregion
}
