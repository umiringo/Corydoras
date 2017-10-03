using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonColor : MonoBehaviour {

    public Image outter;
    public Image inner;
    public Image icon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RefreshUIColor(Color outColor, Color inColor, Color txtColor)
    {
        outter.color = outColor;
        inner.color = inColor;
        icon.color = txtColor;
    }
}
