using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetsEmoji", menuName = "Assets/Emoji")]
public class EmojiData : ScriptableObject
{
    public List<Sprite> _emojiSprites;
}
