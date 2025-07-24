using UnityEngine;

[CreateAssetMenu(menuName = "Asset/AI Rival Settings")]
public class AiRivalSettings : ScriptableObject
{
    public string aiName;
    public string spriteName;
    public Sprite aiSprite;
    public string difficulty;
}
