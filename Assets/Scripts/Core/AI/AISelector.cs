using UnityEngine;

public class AISelector : MonoBehaviour
{
    [SerializeField]
    private AIComlexityLobbyUI _ui;

    private void Start()
    {
        if (_ui != null)
            _ui.OnDifficultyChanged += HandleDifficultyChange;

        string saved = AISettingManager.AI.GetStrategy();
        HandleDifficultyChange(saved);
    }

    private void OnDestroy()
    {
        if (_ui != null)
            _ui.OnDifficultyChanged -= HandleDifficultyChange;
    }

    private void HandleDifficultyChange(string difficulty)
    {
        AISettingManager.AI.SetStrategy(difficulty);
        AISettingManager.Save();
    }
}
