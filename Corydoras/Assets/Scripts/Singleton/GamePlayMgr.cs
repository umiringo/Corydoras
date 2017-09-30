using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GlobalDefines;

public class GamePlayMgr : Singleton<GamePlayMgr>
{
    protected GamePlayMgr() {}
    public string identify = "GamePlayMgr";

    private readonly string[] allHiragana = new string[] {
        "あ", "い", "う", "え", "お",
        "か", "き", "く", "け", "こ",
        "さ", "し", "す", "せ", "そ",
        "た", "ち", "つ", "て", "と",
        "な", "に", "ぬ", "ね", "の",
        "は", "ひ", "ふ", "へ", "ほ",
        "ま", "み", "む", "め", "も",
        "や", "ゆ", "よ",
        "ら", "り", "る", "れ", "ろ",
        "わ", "を",
        "ん",
    };
    private readonly string[] allKatakana = new string[]{
        "ア", "イ", "ウ", "エ", "オ",
        "カ", "キ", "ク", "ケ", "コ",
        "サ", "シ", "ス", "セ", "ソ",
        "タ", "チ", "ツ", "テ", "ト",
        "ナ", "ニ", "ヌ", "ネ", "ノ",
        "ハ", "ヒ", "フ", "ヘ", "ホ",
        "マ", "ミ", "ム", "メ", "モ",
        "ヤ", "ユ", "ヨ",
        "ラ", "リ", "ル", "レ", "ロ",
        "ワ", "ヲ",
        "ン",
    };
    private int chosenIndex;
    private List<int> choiceItems = new List<int>();
    private Queue<int> preKanas = new Queue<int>();

    public void Init()
    {
        chosenIndex = 0;
        choiceItems.Clear();
        preKanas.Clear();
    }

    public void GenFirstChoice()
    {
        chosenIndex = 0;
        choiceItems.Add(0);
        preKanas.Enqueue(0);
    }

    public void GenChoices(int num)
    {
        do
        {
            chosenIndex = Random.Range(0, DefineNumber.AllKanaNum);
        } while(preKanas.Contains(chosenIndex));

        if(preKanas.Count >= DefineNumber.MaxPreKana)
        {
            preKanas.Dequeue();
        }
        preKanas.Enqueue(chosenIndex);

        choiceItems.Clear();
        choiceItems.Add(chosenIndex);

        for(int i = 1; i < num; i++)
        {
            int choice = -1;
            do
            {
                choice = Random.Range(0, DefineNumber.AllKanaNum);
            } while(choiceItems.Contains(choice));
            choiceItems.Add(choice);
        }
        ShuffleChoiceItems();
    }

    public int GetChosenIndex()
    {
        return chosenIndex;
    }

    public List<int> GetChoiceItems()
    {
        return choiceItems;
    }

    public bool CheckChoice(int index)
    {
        if(index == chosenIndex) return true;
        return false;
    }

    public string GetKana(int index, KanaType type)
    {
        if (index >= DefineNumber.AllKanaNum)
        {
            return "Err";
        }

        if(type == KanaType.Hira) 
        {
            return allHiragana[index];
        }

        if(type == KanaType.Kata)
        {
            return allKatakana[index];
        }

        return "Unknown";
    }

    public string GetChoiceKana(int index, KanaType type)
    {
		if (index >= DefineNumber.AllKanaNum) {
			return "Err";
		}

		if (type == KanaType.Kata) {
			return allHiragana[index];
		}

        if (type == KanaType.Hira) {
			return allKatakana[index];
		}

		return "Unknown";
    }

    private void ShuffleChoiceItems()
    {
        int i = choiceItems.Count;
        while(i > 0)
        {
            int j = Random.Range(0, i);
            int tmp = choiceItems[i-1];
            choiceItems[i-1] = choiceItems[j];
            choiceItems[j] = tmp;
            --i;
        }
    }

    private void DumpChoiceItem()
    {
        Debug.Log("choiceItems: " + string.Join(", ", choiceItems.ConvertAll(i => i.ToString()).ToArray()));
    }

    private void DumpPreKanas()
    {
        string result = "preKanas : ";
        foreach(var i in preKanas)
        {
            result += ", ";
            result += i.ToString();
        }
        Debug.Log(result);
    }
}