﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public static bool allowMovement=true;
    // Use this for initialization
     void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
