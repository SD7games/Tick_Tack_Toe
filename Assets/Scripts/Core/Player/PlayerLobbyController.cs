using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class PlayerLobbyController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Image _playerSign;
    [SerializeField] private ContentScrollController _contentScroll;

    [Header("State")]
    [SerializeField] private EmojiData _currentEmojiData;

    public event Action OnCheckMatchAISign;

    private void Start()
    {
        if (_currentEmojiData != null)
            _contentScroll.SetEmojiData(_currentEmojiData);
    }

    public void SetEmojiData(EmojiData newData)
    {
        if (newData == null) return;

        _currentEmojiData = newData;

        AISettingManager.Player.SetEmojiColor(_currentEmojiData.ColorName);
        AISettingManager.Save();

        _contentScroll.SetEmojiData(_currentEmojiData);

        int idx = Mathf.Clamp(AISettingManager.Player.GetEmojiIndex(), 0,
                              _currentEmojiData.EmojiSprites.Count - 1);
        if (_playerSign != null && _currentEmojiData.EmojiSprites.Count > 0)
            _playerSign.sprite = _currentEmojiData.EmojiSprites[idx];

        OnCheckMatchAISign?.Invoke();
    }

    public EmojiData GetCurrentEmojiData() => _currentEmojiData;
}
