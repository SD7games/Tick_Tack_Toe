using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainController : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings _playerSettings;
    [SerializeField]
    private TMP_Text _playerName;
    [SerializeField]
    private Image _playerSprite;

    private void Start()
    {
        LoadPlayerSettings();
    }

    private void LoadPlayerSettings()
    {        
        if (_playerSettings == null)
        {
            Debug.Log("PlayerSettings not found in PlayerMain");
            return;
        }

        if (_playerName != null)
        {
            _playerName.text = _playerSettings.playerName;
        }

        if (_playerSprite != null && _playerSettings.playerSprite != null)
        {
            _playerSprite.sprite = _playerSettings.playerSprite;
        }
    }
}
