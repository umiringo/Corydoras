using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefines;

public class GamePlayMgr : Singleton<GamePlayMgr>
{
    protected GamePlayMgr() {}
    public string identify = "GamePlayMgr";

    private readonly List<string> allHiragana = new List<string>{};
    private readonly List<string> allKatakana = new List<string>{};
    private int chosenIndex;
    private List<int> choiceItems = new List<int>();
    private Queue<int> preKanas = new Queue<int>();
    private Random rnd;
    private int kanaNum;

    public void Init()
    {
        chosenIndex = 0;
        choiceItems.Clear();
        preKanas.Clear();
        rnd = new Random();
        kanaNum = allHiragana.Count;
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
            chosenIndex = rnd.Next(0, kanaNum);
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
                choice = rnd.Next(0, kanaNum);
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
        if(index >= kanaNum)
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

    private void ShuffleChoiceItems()
    {
        Random rd = new Random();
        int i = choiceItems.Count;
        while(i > 0)
        {
            j = rd.Next() % (i + 1);
            int tmp = choiceItems[i];
            choiceItems[i] = choiceItems[j];
            choiceItems[j] = tmp;
            --i;
        }
    }

    private void DumpChoiceItem()
    {
        Debug.Log("choiceItems: " + string.Join(",", choiceItems));
    }

    private void DumpPreKanas()
    {
        Debug.Log("preKanas : " + string.Join(",", preKanas));
    }
}