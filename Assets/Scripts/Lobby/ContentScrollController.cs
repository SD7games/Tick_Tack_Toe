using System;
using UnityEngine;
using UnityEngine.UI;

public class ContentScrollController : MonoBehaviour
{
    [SerializeField]
    private EmojiData _emojiData;
    [SerializeField]
    private GameObject _emojiButtonPrefab;
    [SerializeField]
    private Transform _contentParent;

    public Action<Sprite> OnEmojiSelected;
    public Action OnGenerationComplete;

    private void Start()
    {
        GenerateEmojiButtons();
    }

    private void GenerateEmojiButtons()
    {   
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var emoji in _emojiData._emojiSprites)
        {
            var buttonGO = Instantiate(_emojiButtonPrefab, _contentParent);
            var image = buttonGO.GetComponentInChildren<Image>();
            image.sprite = emoji;

            var button = buttonGO.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                Debug.Log("Emoji selected: " + emoji.name);
                OnEmojiSelected?.Invoke(emoji);
            });
        }

        OnGenerationComplete?.Invoke();
    }
}
