using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefines;

public class GameDirector : MonoBehaviour {

    private int score;
    private int highScore;
    private int level = 1;
    private int curLevelQuestion = 0;
    private int QuestionNum = 0;

    public Question question;
    public Choice choice;

	// Use this for initialization
	void Start () {
		GamePlayMgr.Instance.Init();
        StartMain();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void StartMain()
    {
        // 开始主菜单
        score = 0;
        level = 1;
        curLevelQuestion = 0;
        QuestionNum = 0;

        GamePlayMgr.Instance.GenFirstChoice();
        Question.ShowRiddle(GamePlayMgr.Instance.GetChosenIndex(), KanaType.Hira);
        choice.ShowChoices(level, KanaType.Hira)
    }

    private void LoadLevel()
    {
        QuestionNum++;
        if(level == 1)
        {
            level = 2;
            curLevelQuestion = 0;
        }
        else if(level < DefineNumber.MaxLevel)
        {
            if(curLevelQuestion >= DefineNumber.LevelUpNum) {
                level++;
            }
            curLevelQuestion++;
        }

        GamePlayMgr.Instance.GenChoices(level);
        KanaType type = KanaType.Hira;
        if(QuestionNum > DefineNumber.QuestionNumToRand)
        {
            if(Random.Range(1, 100) > 50) type = KanaType.Kata;
        }
        Question.ShowRiddle(GamePlayMgr.Instance.GetChosenIndex(), type);
        choice.ShowChoices(level, type)
    }

    private void GameFailed()
    {

    }

    public void OnChoice(int index)
    {
        bool ret = GamePlayMgr.Instance.CheckChoice(index);
        if(ret) {
            // 正确，则开始一下关
            LoadLevel();
        }
        else {
            // 失败
            GameFailed();
        }
    }

}
