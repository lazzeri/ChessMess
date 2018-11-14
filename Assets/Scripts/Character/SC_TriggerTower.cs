using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TriggerTower : MonoBehaviour {

    public bool B_Triggert = true;
    public SC_TowerCharacter SC_Tower;
    void Start()
    {
        B_Triggert = true;
        SC_Tower = this.transform.GetComponentInParent<SC_TowerCharacter>();

    
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EmptyField"))
        {
            //From now on this calls it so we don't have to do it ourself!
            B_Triggert = false;
            SC_Tower.StopMoving();
        }
        else
        {
            B_Triggert = true;
        }
    }


    public bool getTriggered()
    {
        return B_Triggert;
    }


}
