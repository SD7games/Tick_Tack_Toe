using UnityEngine;
using UnityEngine.UI;

public class AIRivalLobbyController : MonoBehaviour
{
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
        SetRandomEmoji();

        _playerLobbyController.OnCheckMatchAISign += SimulateEmojiOnConflict;
    }

    private void OnDestroy()
    {
        _playerLobbyController.OnCheckMatchAISign -= SimulateEmojiOnConflict;
    }

    private void SetRandomEmoji()
    {
        int emojiCount = _emojiData._emojiSprites.Count;
        int newEmojiIndex = Random.Range(0, emojiCount);

        _aiSign.sprite = _emojiData._emojiSprites[newEmojiIndex];
        _dissolve?.PlayDissolve();

        PlayerPrefsAIManager.AI.SetEmojiAIIndex(newEmojiIndex);
        PlayerPrefsAIManager.Save();
    }

    private void SimulateEmojiOnConflict()
    {
        int playerEmojiIndex = PlayerPrefsAIManager.Player.GetEmojiIndex();
        int aiEmojiIndex = PlayerPrefsAIManager.AI.GetEmojiAIIndex();

        if (playerEmojiIndex != aiEmojiIndex)
            return;

        int emojiCount = _emojiData._emojiSprites.Count;
        int newEmojiIndex;

        do
        {
            newEmojiIndex = Random.Range(0, emojiCount);
        } while (newEmojiIndex == playerEmojiIndex);

        _aiSign.sprite = _emojiData._emojiSprites[newEmojiIndex];
        _dissolve?.PlayDissolve();

        PlayerPrefsAIManager.AI.SetEmojiAIIndex(newEmojiIndex);
        PlayerPrefsAIManager.Save();
    }
}
