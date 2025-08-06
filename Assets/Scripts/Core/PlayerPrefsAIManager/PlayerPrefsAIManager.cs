using UnityEngine;

public static class PlayerPrefsAIManager
{

    public static class Player
    {
        public static void SetName(string name) => PlayerPrefs.SetString("PlayerName", name);
        public static string GetName() => PlayerPrefs.GetString("PlayerName", "Player");

        public static void SetEmojiIndex(int index) => PlayerPrefs.SetInt("PlayerEmojiIndex", index);
        public static int GetEmojiIndex() => PlayerPrefs.GetInt("PlayerEmojiIndex", 0);

    }

    public static class AI
    {
        public static void SetDifficulty(int diff) => PlayerPrefs.SetInt("AIDifficulty", diff);
        public static int GetDifficulty() => PlayerPrefs.GetInt("AIDifficulty", 1);

        public static void SetEmojiAIIndex(int index) => PlayerPrefs.SetInt("AIEmojiIndex", index);
        public static int GetEmojiAIIndex() => PlayerPrefs.GetInt("AIEmojiIndex", 1);

        public static void SetStrategy(string strategy) => PlayerPrefs.SetString("AIStrategy", strategy);
        public static string GetStrategy() => PlayerPrefs.GetString("AIStrategy", "Easy");

    }

    public static void Save() => PlayerPrefs.Save();

    public static void Reset() { PlayerPrefs.DeleteAll(); PlayerPrefs.Save(); }
}
