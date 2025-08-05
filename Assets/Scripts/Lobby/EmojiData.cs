using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetsEmoji", menuName = "Assets/Emoji")]
public class EmojiData : ScriptableObject
{
    public List<Sprite> _emojiSprites;

    public Sprite GetEmojiByIndex(int index)
    {
        if (index >= 0 && index < _emojiSprites.Count)
            return _emojiSprites[index];
        return null;
    }
}
