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
    private GameCenter gameCenter;
    private int tryNum;
    private int timeout;
    private int season;
    private int themeIndex;
    private KanaType kanaType;


    public Question question;
    public Choice choice;
    public UICanvas ui;
    public AudioPlayer audioPlayer;
    public UIEffectCanvas uiEffect;

    // Use this for initialization
    private void Awake()
    {
		GamePlayMgr.Instance.Init();
		LocalizeMgr.Instance.Init();
        ThemeMgr.Instance.Init();
        gameCenter = GetComponent<GameCenter>();
    }

    void Start () 
    {
        //PlayerPrefs.DeleteAll();    
		score = 0;
        highScore = PlayerPrefs.GetInt(PrefsKey.HighScore, 0);
        StartCoroutine(StartMain());
        PlayerPrefs.DeleteAll();
        tryNum = PlayerPrefs.GetInt(PrefsKey.TryNum, 0);
        timeout = PlayerPrefs.GetInt(PrefsKey.Timeout, 0);
        gameCenter.Login();
    }
	
	// Update is called once per frame
	void Update () {
        if (isStart)
        {
            curPassTime += Time.deltaTime;
			if (curPassTime > passTime) {
                if(timeout == 0)
                {
                    gameCenter.UpdateReportProgress(GameCenterKey.Timeout1, 1, 1);
                    timeout++;
                    PlayerPrefs.SetInt(PrefsKey.Timeout, timeout);
                }
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
        RefreshTheme();
    }

    private void LoadLevel()
    {
        questionNum++;
        if(questionNum == 10)
        {
            gameCenter.UpdateReportProgress(GameCenterKey.AchieveQuestion10, 50, 50);
        }
        if(questionNum == 100)
        {
            gameCenter.UpdateReportProgress(GameCenterKey.AchieveQuestion100, 100, 100);
        }
        curPassTime = 0;
        if(level == 1)
        {
            level = 2;
            curLevelQuestion = 0;
            isStart = true;
            AddTryNum();
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
        kanaType = KanaType.Hira;
        if(questionNum > DefineNumber.QuestionNumToRand)
        {
            if(Random.Range(1, 100) >= 50) {
                kanaType = KanaType.Kata;   
            }
        }
        if (questionNum > DefineNumber.HardLevelNum)
        {
            passTime = DefineNumber.HardCooldown;
            rotateSpeed = DefineNumber.HardRotateSpeed;
        }
        question.ShowRiddle(GamePlayMgr.Instance.GetChosenIndex(), kanaType);
        choice.ShowChoices(level, kanaType);
        ui.LoadLevel();
        RefreshTheme();
    }

    private void GameFailed()
    {
        if (highScore > 0) {
            gameCenter.UpdateScore(highScore);
        }
		PlayerPrefs.SetInt(PrefsKey.HighScore, highScore);
        isStart = false;
        isFailed = true;
        curPassTime = 0.0f;
        ui.GameFail(GamePlayMgr.Instance.GetChosenIndex(), kanaType);
        audioPlayer.StopBGM();
    }

    public void OnChoice(int index, Vector3 psPos)
    {
        bool ret = GamePlayMgr.Instance.CheckChoice(index);
        if(ret) {
            ui.PlayFirework(psPos);
            audioPlayer.PlayRightSound();
            // 正确，则开始一下关
            int point = (int)Mathf.Ceil(5.0f - curPassTime);
            if(questionNum > DefineNumber.HardLevelNum)
            {
                point *= 2;
            }
            score += point;
            if(score > highScore) {
                if(highScore < 10 && score >= 10)
                {
                    gameCenter.UpdateReportProgress(GameCenterKey.AchieveScore10, 1, 1);
                }
                else if(highScore < 100 && score >= 100)
                {
                    gameCenter.UpdateReportProgress(GameCenterKey.AchieveScore100, 1, 1);
                }
                else if(highScore < 500 && score >= 500)
                {
                    gameCenter.UpdateReportProgress(GameCenterKey.AchieveScore500, 1, 1);
                }
                else if(highScore < 1000 && score >= 1000)
                {
                    gameCenter.UpdateReportProgress(GameCenterKey.AchieveScore1000, 1, 1);
                }
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
        gameCenter.ShowGameCenter();
    }

    private void AddTryNum()
    {
        tryNum++;
        PlayerPrefs.SetInt(PrefsKey.TryNum, tryNum);
        if(tryNum <= 10)
        {
            gameCenter.UpdateReportProgress(GameCenterKey.AchieveTry10, tryNum, 10);
        }
        else if(tryNum <= 100)
        {
            gameCenter.UpdateReportProgress(GameCenterKey.AchieveTry50, tryNum, 100);
        }
    }

    private void RefreshTheme()
    {
        if(season <= 0)
        {
            season = Random.Range(1, 3);
            themeIndex = 0;
        }
        else {
            themeIndex++;
            if (themeIndex > DefineNumber.MaxThemeIndex) {
                themeIndex = 0;
                NextSeason();
            }
        }
        Color bgColor = ThemeMgr.Instance.GetBgColor(season, themeIndex);
        Color outColor = ThemeMgr.Instance.GetOutColor(season, themeIndex);
        Color inColor = ThemeMgr.Instance.GetInnerColor(season, themeIndex);
        Color txtColor = ThemeMgr.Instance.GetTextColor(season, themeIndex);
        Color mainTxtColor = ThemeMgr.Instance.GetMainTextColor(season, themeIndex);
        ui.RefreshUIColor(outColor, inColor, txtColor, mainTxtColor);
        uiEffect.RefreshUIColor(bgColor, season);
        choice.RefreshUIColor(outColor, inColor, txtColor);
        question.RefreshUIColor(outColor, inColor, txtColor);
    }

    private void NextSeason()
    {
		if (season <= 0) {
			season = Random.Range(1, 3);
			return;
		}
		if (season >= 2) {
			season = 1;
			return;
		}
		season++;
    }
}
