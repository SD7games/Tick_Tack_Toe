using UnityEngine;
using UnityEngine.UI;

public class AIRivalMainController : MonoBehaviour
{
    [SerializeField]
    private Image _aiSign;
    [SerializeField]
    private EmojiData _emojiData;

    private void Start()
    {
        LoadEmoji();
    }

    private void LoadEmoji()
    {
        int index = PlayerPrefsAIManager.AI.GetEmojiAIIndex();

        if (index >= 0 && index < _emojiData._emojiSprites.Count)
        {
            _aiSign.sprite = _emojiData._emojiSprites[index];
        }
        else
        {
            Debug.Log("AI emoji index invalid: " + index);
        }
    }
}
