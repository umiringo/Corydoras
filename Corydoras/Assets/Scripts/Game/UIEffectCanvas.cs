using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffectCanvas : MonoBehaviour {
    public Image background;
    private GameDirector director;
    private int curSeason = 0;
    public ParticleSystem sakura;
    public ParticleSystem maple;
	// Use this for initialization
	void Start () {
		director = GameObject.Find("MainCamera").GetComponent<GameDirector>();
        director.uiEffect = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RefreshUIColor(Color bgColor, int season)
    {
        background.color = bgColor;
        if(curSeason != season)
        {
            curSeason = season;
            DoEffect();
        }
    }

    private void DoEffect()
    {
        if(curSeason == 1)
        {
            maple.Stop();
            sakura.Play();
        }
        else
        {
            sakura.Stop();
            maple.Play();
        }
    }
}
