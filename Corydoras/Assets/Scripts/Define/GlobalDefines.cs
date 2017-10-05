using UnityEngine;

namespace GlobalDefines
{

    public enum Language
    {
        English = 1,
        Japanese = 2,
        TChinese = 3,
        SChinese = 4,
    }

    public enum KanaType
    {
        Hira = 1,
        Kata = 2,
    }

	public class FilePath
	{
		public static readonly string EnglishStringPath = "Localization/EnglishString";
		public static readonly string JapaneseStringPath = "Localization/JapaneseString";
		public static readonly string TChineseStringPath = "Localization/TChineseString";
		public static readonly string SChineseStringPath = "Localization/SChineseString";
        public static readonly string ThemePath = "Theme/Theme";
	}

    public class DefineNumber
    {
        public static readonly int MaxPreKana = 8;
        public static readonly int AllKanaNum = 46;
        public static readonly int MaxLevel = 5;
        public static readonly int LevelUpNum = 5;
        public static readonly float Cooldown = 5.0f;
        public static readonly float HardCooldown = 3.0f;
        public static readonly int QuestionNumToRand = 20;
        public static readonly int HardLevelNum = 32;
        public static readonly float RotateSpeed = 20.0f;
        public static readonly float HardRotateSpeed = 40.0f;
        public static readonly int MaxThemeIndex = 15;
    }

    public class PrefsKey
    {
        public static readonly string HighScore = "HighScore";
        public static readonly string Mute = "Mute";
        public static readonly string TryNum = "TryNum";
        public static readonly string Timeout = "Timeout";
    }

    public class GameCenterKey
    {
        public static readonly string Ladder = "score_rank";
        public static readonly string AchieveScore10 = "Score10";
        public static readonly string AchieveScore100 = "Score100";
        public static readonly string AchieveScore500 = "Score500";
        public static readonly string AchieveScore1000 = "Score1000";
        public static readonly string AchieveQuestion10 = "Question10";
        public static readonly string AchieveQuestion100 = "Question100";
        public static readonly string AchieveTry10 = "Try10";
        public static readonly string AchieveTry50 = "Try50";
        public static readonly string Timeout1 = "Timeout1";
    }
}
