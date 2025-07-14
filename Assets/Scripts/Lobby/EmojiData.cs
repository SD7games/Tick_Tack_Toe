using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets/Emoji")]
public class EmojiData : ScriptableObject
{
    [SerializeField]
    private List<Sprite> _emoji;
}
