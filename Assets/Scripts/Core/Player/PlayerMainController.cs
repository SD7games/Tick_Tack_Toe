using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMainController : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private Image _playerSprite;
    [SerializeField] private List<EmojiData> _emojiDataByColor;

    private void Start()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        _playerName.text = AISettingManager.Player.GetName();

        string colorName = AISettingManager.Player.GetEmojiColor();
        int emojiIndex = AISettingManager.Player.GetEmojiIndex();

        EmojiData colorData = _emojiDataByColor.Find(colorData => colorData.ColorName == colorName);

        if (colorData == null) return;

        if (emojiIndex >= 0 && emojiIndex < colorData.EmojiSprites.Count)
        {
            _playerSprite.sprite = colorData.EmojiSprites[emojiIndex];
        }
    }
}
