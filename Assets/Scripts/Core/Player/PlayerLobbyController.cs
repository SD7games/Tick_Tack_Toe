using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLobbyController : MonoBehaviour
{
    [SerializeField]
    private string _playerName;
    [SerializeField]
    private Image _playerSign;
    [SerializeField]
    private TMP_InputField _playerInputField;
    [SerializeField]
    private EmojiData _emojiData;
    [SerializeField]
    private ContentScrollController _contentScrollController;
    [SerializeField]
    private PlayerSettings _playerSettings;

    private const string PlayerNameKey = "PlayerName";
    private const string PlayerSpriteKey = "PlayerSprite";

    private void Start()
    {
        LoadPlayerData();

        _playerInputField.onEndEdit.AddListener(SetName);
        _contentScrollController.OnEmojiSelected += SetPlayerSprite;
    }

    private void OnDestroy()
    {
        _contentScrollController.OnEmojiSelected -= SetPlayerSprite;
    }

    public void SetPlayerSprite(Sprite sprite)
    {
        _playerSign.sprite = sprite;

        _playerSettings.playerSprite = sprite;
        _playerSettings.spriteName = sprite.name;

        SavePlayerSprite(sprite);
    }

    private void SetName(string name)
    {
        _playerName = name;
        _playerSettings.playerName = _playerName;
        PlayerPrefs.SetString(PlayerNameKey, _playerName);
        PlayerPrefs.Save();
    }

    private void SavePlayerSprite(Sprite sprite)
    {
        PlayerPrefs.SetString(PlayerSpriteKey, sprite.name);
        PlayerPrefs.Save();
    }

    private void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            _playerName = PlayerPrefs.GetString(PlayerNameKey);
            _playerInputField.text = _playerName;
            _playerSettings.playerName = _playerName;
        }

        if (PlayerPrefs.HasKey(PlayerSpriteKey))
        {
            string spriteName = PlayerPrefs.GetString(PlayerSpriteKey);
            Sprite foundSprite = _emojiData._emojiSprites.Find(sprite => sprite.name == spriteName);

            if (foundSprite != null)
            {
                _playerSign.sprite = foundSprite;

                _playerSettings.playerSprite = foundSprite;
                _playerSettings.spriteName = spriteName;
            }
            else
            {
                Debug.LogWarning("Emoji not found: " + spriteName);
            }
        }
    }
}
