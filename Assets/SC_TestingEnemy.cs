﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TestingEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        print(other.gameObject.name);
        Time.timeScale = 0;
    }
}
