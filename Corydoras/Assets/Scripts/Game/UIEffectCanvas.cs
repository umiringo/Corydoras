using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffectCanvas : MonoBehaviour {
    public Image background;
    private GameDirector director;
	// Use this for initialization
	void Start () {
		director = GameObject.Find("MainCamera").GetComponent<GameDirector>();
        director.uiEffect = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RefreshUIColor(Color bgColor)
    {
        background.color = bgColor;
    }
}
