﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalDefines;
using DG.Tweening;

public class Question : MonoBehaviour {
    public int index;
    public Text riddleText;
    public GameDirector director;
    public Image processBar;
    public Image outImage;
    public Image inImage;

    // Use this for initialization
    void Start () {
        transform.DOScale(new Vector3(0.9f, 0.9f, 1.0f), 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        director = GameObject.Find("MainCamera").GetComponent<GameDirector>();
        director.question = this;
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void StartGame()
    {
        processBar.fillAmount = 0.0f;
    }

    public void ShowRiddle(int idx, KanaType type)
    {
        index = idx;
        riddleText.text = GamePlayMgr.Instance.GetKana(idx, type);
    }

    public void RefreshProcessBar(float amount)
    {
        processBar.fillAmount = 1.0f - amount;
    }

    public void RefreshUIColor(Color outColor, Color inColor, Color txtColor)
    {
        outImage.color = outColor;
        inImage.color = inColor;
        riddleText.color = txtColor;
    }
}
