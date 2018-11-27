using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
         
public class SC_TriggerEnemyPawn : MonoBehaviour {
    // Use this for initialization

    public bool Triggert = false;
    public GameObject Target;
    public string S_TargetTag;
    bool checking;
    public int I_InTrigger = 0;
   
   
    void Start()
    {
        
        checking = false;
      
    }

    // Update is called once per frame


    void OnTriggerEnter(Collider other)
    {
 
        I_InTrigger++;

        Target = other.gameObject;
        S_TargetTag = Target.tag;
    }

    void OnTriggerExit(Collider other)
    {
        I_InTrigger--;
        Target = null;

        // other code
    }

  

    public void SetToTrigger()
    {
        if (transform.CompareTag("Enemy"))
        {
            transform.tag = "ReservationEnemy";
        }

    }

    public void SetOffTriger()
    {
        if (transform.CompareTag("ReservationEnemy"))
        {
            transform.tag = "Enemy";
        }

    }


    private void Update()
    {
       
    }

    public bool getTriggered()
    {
        if(Target != null)
        {
            if (Target.name == ("Field"))
            {

                if (Target.CompareTag("Enemy") || Target.CompareTag("ReservationEnemy") || Target.CompareTag("Player") || Target.CompareTag("ReservationPlayer"))
                {
                    return true;

                }

                if (Target.CompareTag("Untagged") && Triggert)
                {
                    return false;

                }


            }
            else
            {
                return false;
            }
         
        }
        return false; 

    }



    public string getTarget()
    {
        if (Target != null)
            return Target.tag;

        else return null;
    }

    public Vector3 getTargetPosition()
    {
        return Target.transform.position;
    }

}
