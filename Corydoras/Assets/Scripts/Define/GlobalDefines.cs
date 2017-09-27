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
    }
}
