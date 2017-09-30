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

    public class DefineNumber
    {
        public static readonly int MaxPreKana = 8;
        public static readonly int AllKanaNum = 46;
        public static readonly int MaxLevel = 4;
        public static readonly int LevelUpNum = 8;
        public static readonly int Cooldown = 5;
        public static readonly int QuestionNumToRand = 24;
    }
}
