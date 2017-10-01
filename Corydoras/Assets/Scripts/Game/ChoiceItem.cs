﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GlobalDefines;

public class ChoiceItem : MonoBehaviour {
    public int index;
    public Transform riddleTrans;
    public GameDirector director;
    public Text itemText;

    // Use this for initialization
    void Start () {
        director = GameObject.Find("MainCamera").GetComponent<GameDirector>();
        riddleTrans = director.question.transform;
        transform.DOScale(new Vector3(0.9f, 0.9f, 1.0f), 1.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        gameObject.GetComponent<Button>().onClick.AddListener(delegate {
            OnClick();
        });
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(riddleTrans.position, new Vector3(0.0f, 0.0f, 1.0f), director.rotateSpeed * Time.deltaTime);
        itemText.gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, -director.rotateSpeed * Time.deltaTime));
	}

    public void Assemble(int idx, KanaType type)
    {
        index = idx;
		itemText.transform.localScale = Vector3.zero;
		itemText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f).SetEase(Ease.Linear);
        itemText.text = GamePlayMgr.Instance.GetChoiceKana(index, type);
    }

    public void OnClick()
    {
        director.OnChoice(index);
    }
}
