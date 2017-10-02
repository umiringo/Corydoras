using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefines;

public class GameDirector : MonoBehaviour {

    public int score;
    public int highScore;
    private int level = 1;
    private int curLevelQuestion = 0;
    private int questionNum = 0;
    private bool isStart = false;
    private bool isFailed = false;
    private float curPassTime = 0.0f;
    private float passTime = DefineNumber.Cooldown;
    public float rotateSpeed = DefineNumber.RotateSpeed;

    public Question question;
    public Choice choice;
    public UICanvas ui;
    public AudioPlayer audioPlayer;

    // Use this for initialization
    private void Awake()
    {
		GamePlayMgr.Instance.Init();
		LocalizeMgr.Instance.Init();
    }

    void Start () {

		score = 0;
        highScore = PlayerPrefs.GetInt(PrefsKey.HighScore, 0);
        StartCoroutine(StartMain());
        PlayerPrefs.DeleteAll();
	}
	
	// Update is called once per frame
	void Update () {
        if(isStart)
        {
            curPassTime += Time.deltaTime;
			if (curPassTime > passTime) {
                GameFailed();
                curPassTime = 0;
			}
            question.RefreshProcessBar(curPassTime / passTime);
        }
	}

    IEnumerator StartMain()
    {
        yield return new WaitForSeconds(0.1f);
        // 开始主菜单
        score = 0;
        level = 1;
        curLevelQuestion = 0;
        questionNum = 0;
        isFailed = false;

        GamePlayMgr.Instance.GenFirstChoice();
        question.StartGame();
        question.ShowRiddle(GamePlayMgr.Instance.GetChosenIndex(), KanaType.Hira);
        choice.ShowChoices(level, KanaType.Hira);
        ui.StartGame();
        audioPlayer.PlayeBGM();
    }

    private void LoadLevel()
    {
        questionNum++;
        curPassTime = 0;
        if(level == 1)
        {
            level = 2;
            curLevelQuestion = 0;
            isStart = true;
        }
        else if(level < DefineNumber.MaxLevel)
        {
            if(curLevelQuestion >= DefineNumber.LevelUpNum) {
                level++;
                curLevelQuestion = 0;
            }
            curLevelQuestion++;
        }

        GamePlayMgr.Instance.GenChoices(level);
        KanaType type = KanaType.Hira;
        if(questionNum > DefineNumber.QuestionNumToRand)
        {
            if(Random.Range(1, 100) >= 50) {
                type = KanaType.Kata;   
            }
        }
        if (questionNum > DefineNumber.HardLevelNum)
        {
            passTime = DefineNumber.HardCooldown;
            rotateSpeed = DefineNumber.HardRotateSpeed;
        }
        question.ShowRiddle(GamePlayMgr.Instance.GetChosenIndex(), type);
        choice.ShowChoices(level, type);
        ui.LoadLevel();
    }

    private void GameFailed()
    {
		PlayerPrefs.SetInt(PrefsKey.HighScore, highScore);
        isStart = false;
        isFailed = true;
        curPassTime = 0.0f;
        ui.GameFail();
        audioPlayer.StopBGM();
    }

    public void OnChoice(int index, Vector3 psPos)
    {
        ui.PlayFirework(psPos);
        bool ret = GamePlayMgr.Instance.CheckChoice(index);
        if(ret) {
            audioPlayer.PlayRightSound();
            // 正确，则开始一下关
            int point = (int)Mathf.Ceil(5.0f - curPassTime);
            if(questionNum > DefineNumber.HardLevelNum)
            {
                point *= 2;
            }
            score += point;
            if(score > highScore) {
                highScore = score;
            }
            LoadLevel();
        }
        else {
            // 失败
            audioPlayer.PlayWrongSound();
            GameFailed();
        }
    }

    public void OnRestart()
    {
        StartCoroutine(StartMain());
    }

    public void OnSound()
    {
        if(audioPlayer.GetMute())
        {
            audioPlayer.SetMute(false);
        }
        else
        {
            audioPlayer.SetMute(true);
        }

        ui.RefreshSoundIcon();

        if (!isFailed && audioPlayer.GetMute() == false)
        {
            audioPlayer.PlayeBGM();
        }
        else 
        {
            audioPlayer.StopBGM();
        }
    }

    public void OnGameCenter()
    {
        Debug.Log("[GameDirector] OnGameCenter.");
    }
}
