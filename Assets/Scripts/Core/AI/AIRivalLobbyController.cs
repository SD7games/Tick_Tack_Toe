using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class AIRivalLobbyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _aiSign;
    [SerializeField] private EmojiData _emojiData;
    [SerializeField] private PlayerLobbyController _playerLobbyController;

    private Dissolve _dissolve;

    private void Awake()
    {
        _dissolve = _aiSign.GetComponent<Dissolve>();
    }

    private void Start()
    {
        SetUniqueEmoji();

        _playerLobbyController.OnCheckMatchAISign += HandleEmojiConflict;
    }

    private void OnDestroy()
    {
        _playerLobbyController.OnCheckMatchAISign -= HandleEmojiConflict;
    }
    
    private void SetUniqueEmoji()
    {
        int emojiCount = _emojiData._emojiSprites.Count;
        if (emojiCount < 2)
        {
            Debug.LogWarning("[AI Rival] Недостаточно эмоджи для уникального выбора!");
            return;
        }

        int playerEmojiIndex = PlayerPrefsAIManager.Player.GetEmojiIndex();
        int newEmojiIndex;
        int safetyCounter = 50;

        do
        {
            newEmojiIndex = Random.Range(0, emojiCount);
        }
        while (newEmojiIndex == playerEmojiIndex && --safetyCounter > 0);

        _aiSign.sprite = _emojiData._emojiSprites[newEmojiIndex];
        _dissolve?.PlayDissolve();

        PlayerPrefsAIManager.AI.SetEmojiAIIndex(newEmojiIndex);
        PlayerPrefsAIManager.Save();
    }
        
    private void HandleEmojiConflict()
    {
        int playerEmojiIndex = PlayerPrefsAIManager.Player.GetEmojiIndex();
        int aiEmojiIndex = PlayerPrefsAIManager.AI.GetEmojiAIIndex();

        if (playerEmojiIndex == aiEmojiIndex)
        {
            SetUniqueEmoji();
        }
    }
}
