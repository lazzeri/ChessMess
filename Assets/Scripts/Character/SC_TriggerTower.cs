using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TriggerTower : MonoBehaviour {

    public bool B_Triggert = true;
    
    void Start()
    {
        B_Triggert = true;
    
    }
    
    void OnTriggerEnter(Collider other)
    {

      B_Triggert = true;
        
    }


    void OnTriggerExit(Collider other)
    {

        B_Triggert = false;

    }


    public bool getTriggered()
    {
        return B_Triggert;
    }


}
