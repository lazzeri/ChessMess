using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ReservationScript : MonoBehaviour {

    // Use this for initialization
  
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void SetToTrigger()
    {
        if(transform.parent.name == "Player")
        {
         
            transform.tag = "ReservationPlayer";
           

        }

        if (transform.parent.name == "Pawn")
        {
            transform.tag = "ReservationEnemy";
        }
        



    }

    public void SetOffTriger()
    {
     transform.tag = "Untagged";
    }
}
