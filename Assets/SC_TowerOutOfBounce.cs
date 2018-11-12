using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TowerOutOfBounce : MonoBehaviour {
	SC_TowerCharacter SC_Tower;

	// Use this for initialization
	void Start () 
	{
		SC_Tower = this.transform.GetComponentInParent<SC_TowerCharacter>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void OnTriggerEnter(Collider other)
    {
		if(other.transform.tag.StartsWith("A"))
		{
				print(this.transform.name);	
				SC_Tower.StopMoving();
				print(SC_Tower.SC_Triggers[0].getTriggered());
		}
	}

	public void setTick()
	{

	}
}
