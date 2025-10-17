using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class AIDifficultyDisplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _background;

    private readonly Dictionary<string, Color> _difficultyColors = new()
    {
        { "Easy", Color.green },
        { "Norm", Color.yellow },
        { "Hard", Color.red }
    };

    private void Start()
    {
        string diff = PlayerPrefsAIManager.AI.GetStrategy();
        //_text.text = diff;

        if (_difficultyColors.TryGetValue(diff, out var color))
        {
            color.a = 0.2f;
            _background.color = color;
        }
    }
}
