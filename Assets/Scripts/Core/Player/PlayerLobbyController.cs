using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLobbyController : MonoBehaviour
{
    [SerializeField]
    private Image _playerSign;
    //[SerializeField]
    //private TMP_InputField _playerInputField;
    [SerializeField]
    private EmojiData _emojiData;
    [SerializeField]
    private ContentScrollController _contentScrollController;

    public event Action OnCheckMatchAISign;

    private void Start()
    {
        LoadPlayerData();

        //_playerInputField.onEndEdit.AddListener(SetName);
        _contentScrollController.OnEmojiSelected += SetPlayerSprite;
    }

    private void OnDestroy()
    {
        _contentScrollController.OnEmojiSelected -= SetPlayerSprite;
    }

    //private void SetName(string name)
    //{
    //    PlayerPrefsAIManager.Player.SetName(name);
    //    PlayerPrefs.Save();
    //}

    public void SetPlayerSprite(Sprite sprite)
    {
        int index = _emojiData._emojiSprites.IndexOf(sprite);

        if (index >= 0)
        {
            _playerSign.sprite = sprite;
            PlayerPrefsAIManager.Player.SetEmojiIndex(index);
            PlayerPrefsAIManager.Save();
            OnCheckMatchAISign.Invoke();
        }
    }

    private void LoadPlayerData()
    {
        //_playerInputField.text = PlayerPrefsAIManager.Player.GetName();

        int index = PlayerPrefsAIManager.Player.GetEmojiIndex();
        if (index >= 0 && index < _emojiData._emojiSprites.Count)
        {
            _playerSign.sprite = _emojiData._emojiSprites[index];
        }
        else
        {
            Debug.Log("Invalid player index" + index);
        }
    }
}
