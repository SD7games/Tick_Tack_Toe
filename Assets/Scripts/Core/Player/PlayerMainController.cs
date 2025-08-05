using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _playerName;
    [SerializeField]
    private Image _playerSprite;
    [SerializeField]
    private EmojiData _emojiData;

    private void Start()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        _playerName.text = PlayerPrefsAIManager.Player.GetName();

        int index = PlayerPrefsAIManager.Player.GetEmojiIndex();

        if (index >= 0 && index < _emojiData._emojiSprites.Count)
        {
            _playerSprite.sprite = _emojiData._emojiSprites[index];
        }
        else
        {
            Debug.Log("invalid player emoji index" + index);
        }
    }
}
