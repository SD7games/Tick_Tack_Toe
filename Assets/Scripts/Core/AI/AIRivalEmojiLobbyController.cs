using UnityEngine;
using UnityEngine.UI;

public class AIRivalLobbyController : MonoBehaviour
{
    [SerializeField]
    private Image _aiSign;
    [SerializeField]
    private EmojiData _emojiData;
    [SerializeField]
    private PlayerLobbyController _playerLobbyController;

    private void Start()
    {
        LoadEmoji();
        SimulateEmojiSelection();
        _playerLobbyController.OnCheckMatchAISign += SimulateEmojiSelection;
    }

    private void OnDestroy()
    {
        _playerLobbyController.OnCheckMatchAISign -= SimulateEmojiSelection;
    }

    private void SimulateEmojiSelection()
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

        Sprite newEmoji = _emojiData._emojiSprites[newEmojiIndex];
        _aiSign.sprite = newEmoji;

        PlayerPrefsAIManager.AI.SetEmojiAIIndex(newEmojiIndex);
        PlayerPrefsAIManager.Save();
    }

    private void LoadEmoji()
    {
        int index = PlayerPrefsAIManager.AI.GetEmojiAIIndex();
        if (index >= 0 && index < _emojiData._emojiSprites.Count)
        {
            _aiSign.sprite = _emojiData._emojiSprites[index];
        }
    }
}
