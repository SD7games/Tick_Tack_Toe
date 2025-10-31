using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AIRivalMainController : MonoBehaviour
{
    [SerializeField] private Image _aiSign;
    [SerializeField] private List<EmojiData> _emojiDataByColor;

    private void Start()
    {
        LoadAIData();
    }

    private void LoadAIData()
    {
        string colorName = AISettingManager.AI.GetEmojiAIColor();
        int emojiIndex = AISettingManager.AI.GetEmojiAIIndex();

        EmojiData colorData = _emojiDataByColor.Find(colorData => colorData.ColorName == colorName);

        if (colorData == null) return;

        if (emojiIndex >= 0 && emojiIndex < colorData.EmojiSprites.Count)
        {
            _aiSign.sprite = colorData.EmojiSprites[emojiIndex];
        }
    }
}
