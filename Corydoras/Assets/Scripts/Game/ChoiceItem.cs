﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChoiceItem : MonoBehaviour {
    public int index;
    public Transform riddleTrans;

    // Use this for initialization
    void Start () {
        transform.DOScale(new Vector3(0.9f, 0.9f, 1.0f), 1.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
       // transform.DORotate(new Vector3(0.0f, 0.0f, -360.0f), 10.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(riddleTrans.position, new Vector3(0.0f, 0.0f, 1.0f), 20 * Time.deltaTime);
	}

    public void OnClick()
    {
        
    }
}
