﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var nextLevel = Input.GetKeyUp(KeyCode.Alpha1);
	    var escape = Input.GetKeyUp(KeyCode.Escape);
        var god = Input.GetKeyUp(KeyCode.Alpha0);
        if (nextLevel) GameManager.Instance.NextLevel();
        if (escape) SceneManager.LoadScene(0);
	}
}
