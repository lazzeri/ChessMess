using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Field : MonoBehaviour {
    public Renderer M_Mat;
    public int count = 0;
    // Use this for initialization
    void Start()
    {
        M_Mat = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.tag == "Untagged")
            M_Mat.material.color = Color.gray;
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Untagged" && other.tag != "Trigger" && other.name != "Field")
        {
            count++;
        }
      
        
            if (other.name == "Player")
            {
                M_Mat.material.color = Color.green;
                transform.tag = "Player";
            }

            if (other.CompareTag("Enemy"))
            {
                M_Mat.material.color = Color.red;
                transform.tag = "Enemy";
            }

            if (other.CompareTag("ReservationPlayer"))
            {
                M_Mat.material.color = Color.yellow;
                transform.tag = "ReservationPlayer";
            }

            if (other.CompareTag("ReservationEnemy"))
            {
                M_Mat.material.color = Color.yellow;
                transform.tag = "ReservationEnemy";
            }
        
       
    }

    public void OnTriggerStay(Collider other)
    {
        if(count == 2)
        {
            count = 1;
        }
       
            if (other.CompareTag("Player"))
            {
                M_Mat.material.color = Color.green;
                transform.tag = "Player";
            }

            if (other.CompareTag("Enemy"))
            {

                M_Mat.material.color = Color.red;
                transform.tag = "Enemy";
            }

            if (other.CompareTag("ReservationPlayer"))
            {
                M_Mat.material.color = Color.yellow;
                transform.tag = "ReservationPlayer";
            }

            if (other.CompareTag("ReservationEnemy"))
            {
                M_Mat.material.color = Color.yellow;
                transform.tag = "ReservationEnemy";
            }

            if (count == 0)
            {
                if (transform.tag != "ReservationPlayer" && transform.tag != "ReservationEnemy")
                {
                    transform.tag = "Untagged";
                    M_Mat.material.color = Color.gray;
                }
            }


        


    }

  

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Untagged" && other.tag != "Trigger" && other.name != "Field")
        {
            count--;
        }


    }



    
}
        
    


   

