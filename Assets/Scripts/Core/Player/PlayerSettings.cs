using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public string playerName;
    public string spriteName;
    public Sprite playerSprite;
}
