using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Bool : MonoBehaviour {
    public bool B_Boolean;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setBool(bool setter)
    {
        B_Boolean = setter;
    }

    public bool getBool()
    {
        return B_Boolean;
    }
}
