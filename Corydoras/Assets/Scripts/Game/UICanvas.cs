using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalDefines;

public class UICanvas : MonoBehaviour {
	private GameDirector director;

    public Text topText;
    public Text bottomText;
    public Text failScoreText;
    public GameObject gamePanel;
    public GameObject failPanel;
    public Image failSound;
    public Image mainSound;

    public UIButtonColor restartBtn;
    public UIButtonColor gameCenterBtn;
    public UIButtonColor soundBtn;
    public UIButtonColor mainGameCenterBtn;
    public UIButtonColor mainSoundBtn;

    public GameObject mainSoundObj;
    public GameObject mainCreditorObj;
    public ParticleSystem firework;

	// Use this for initialization
	void Start () {
	    director = GameObject.Find("MainCamera").GetComponent<GameDirector>();
        director.ui = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        gamePanel.SetActive(true);
        failPanel.SetActive(false);
        topText.text = LocalizeMgr.Instance.GetString("LKTitle");
        topText.fontSize = 128;
        bottomText.text = LocalizeMgr.Instance.GetString("LKHello");
        mainSoundObj.SetActive(true);
        mainCreditorObj.SetActive(true);
    }

    public void LoadLevel()
    {
		mainSoundObj.SetActive(false);
		mainCreditorObj.SetActive(false);
		gamePanel.SetActive(true);
		failPanel.SetActive(false);
        topText.fontSize = 80;
        topText.text = LocalizeMgr.Instance.GetString("LKScore") + "\n" + director.score;
        bottomText.text = LocalizeMgr.Instance.GetString("LKHighScore") + director.highScore;
    }

    public void GameFail(int riddle, KanaType type)
    {
		gamePanel.SetActive(false);
		failPanel.SetActive(true);
        topText.text = "GAME OVER";
        topText.fontSize = 128;
        bottomText.text = LocalizeMgr.Instance.GetString("LKScore") + " : " + director.score.ToString() + "\n" + LocalizeMgr.Instance.GetString("LKHighScore") + director.highScore;
        string riddleStr = GamePlayMgr.Instance.GetKana(riddle, type);
        string answerStr = "";
        if(type == KanaType.Hira)
        {
            answerStr = GamePlayMgr.Instance.GetKana(riddle, KanaType.Kata);
        }
        else
        {
            answerStr = GamePlayMgr.Instance.GetKana(riddle, KanaType.Hira);
        }
            
        failScoreText.text = riddleStr + " : " + answerStr;
        RefreshSoundIcon();
    }

    public void RefreshSoundIcon()
    {
        bool isMute = PlayerPrefs.GetInt(PrefsKey.Mute, 0) > 0;
        if(isMute) 
        {
            failSound.overrideSprite = Resources.Load("Image/mute", typeof(Sprite)) as Sprite;
            mainSound.overrideSprite = Resources.Load("Image/mute", typeof(Sprite)) as Sprite;
        }
        else
        {
            failSound.overrideSprite = Resources.Load("Image/audio", typeof(Sprite)) as Sprite;
            mainSound.overrideSprite = Resources.Load("Image/audio", typeof(Sprite)) as Sprite;
        }
    }

    public void OnRestartClick()
    {
        director.OnRestart();
    }

    public void OnSoundClick()
    {
        director.OnSound();
    }

    public void OnGameCenterClick()
    {
        director.OnGameCenter();
    }

    public void PlayFirework(Vector3 pos)
    {
        firework.transform.position = pos;
        firework.Play();
    }

    public void RefreshUIColor(Color outColor, Color inColor, Color txtColor, Color mainTxtColor)
    {
        topText.color = mainTxtColor;
        bottomText.color = mainTxtColor;
        failScoreText.color = mainTxtColor;
        restartBtn.RefreshUIColor(outColor, inColor, txtColor);
        gameCenterBtn.RefreshUIColor(outColor, inColor, txtColor);
        soundBtn.RefreshUIColor(outColor, inColor, txtColor);
        mainGameCenterBtn.RefreshUIColor(outColor, inColor, txtColor);
        mainSoundBtn.RefreshUIColor(outColor, inColor, txtColor);
    }
}
