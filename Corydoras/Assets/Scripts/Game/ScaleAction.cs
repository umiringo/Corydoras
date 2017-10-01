using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.DOScale(new Vector3(0.9f, 0.9f, 1.0f), 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
