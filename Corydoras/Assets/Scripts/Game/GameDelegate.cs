﻿﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefines;

public class GameDelegate : MonoBehaviour {
    private GameDirector gameDirector;

    void Awake()
    {
        gameDirector = gameObject.GetComponent<GameDirector>();
    }
    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    
}
