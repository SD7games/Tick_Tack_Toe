using UnityEngine;

public class AISelector : MonoBehaviour
{
    [SerializeField]
    private AIComlexityUI _ui;

    private void Start()
    {
        if (_ui != null)
            _ui.OnDifficultyChanged += HandleDifficultyChange;

        string saved = PlayerPrefsAIManager.AI.GetStrategy();
        HandleDifficultyChange(saved);
    }

    private void OnDestroy()
    {
        if (_ui != null)
            _ui.OnDifficultyChanged -= HandleDifficultyChange;
    }

    private void HandleDifficultyChange(string difficulty)
    {
        PlayerPrefsAIManager.AI.SetStrategy(difficulty);
        PlayerPrefsAIManager.Save();
    }
}
