using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalDefines;

public class UICanvas : MonoBehaviour {
    public Image bgImage;
    public Text topText;
    public Text bottomText;
    public Text failScoreText;
    public GameObject gamePanel;
    public GameObject failPanel;
    public Image failSound;
    public Image mainSound;
    private GameDirector director;
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

    public void GameFail()
    {
		gamePanel.SetActive(false);
		failPanel.SetActive(true);
        topText.text = "Game\nOver";
        topText.fontSize = 128;
        bottomText.text = LocalizeMgr.Instance.GetString("LKHighScore") + director.highScore;
        failScoreText.text = LocalizeMgr.Instance.GetString("LKScore") + " : " + director.score.ToString();
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
        Debug.Log("[UICanvas] PlayFirework.");
      //  firework.transform.position = pos;
        firework.Play();
    }
}
