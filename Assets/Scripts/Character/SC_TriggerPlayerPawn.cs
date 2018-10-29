using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TriggerPlayerPawn : MonoBehaviour {

    // Use this for initialization
    public bool trying = false;
    public bool Triggert = false;
    public GameObject Target;
    public string TargetName = "Penis";
    public string TargetTag = "Dumbo";
    bool checking;

   

    void Start()
    {
        checking = false;
    
    }

    // Update is called once per frame

    

    void OnTriggerEnter(Collider other)
    {

       Target = other.gameObject;
        
    }


    void OnTriggerExit(Collider other)
    {

        Target = null;

       
        // other code
    }

    void Update()
    {
       
        if (Target != null)
        {
            TargetName = Target.name;
            TargetTag = Target.tag;
        }
      
    }



    public bool getTriggered()
    {
        if(Target == null)
        {
            return true;
        }

      
        if(Target != null)
        {

          //Changed this if problems find here
            
            if (TargetName.StartsWith("F"))
            {
                if (TargetTag.StartsWith("E") || TargetTag.StartsWith("R") || TargetTag.StartsWith("P"))
                {
                    return true;

                }


            }
           
        }
        return false;

    }

    

    public string getTarget()
    {
        if (Target != null)
            return Target.tag;
        else
            return null;
    }

    public Vector3 getTargetPosition()
    {
        return Target.transform.position;
    }

}
