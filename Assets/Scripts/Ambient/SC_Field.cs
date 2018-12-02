using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Normal Fieldsize: 05 10 04
public class SC_Field : MonoBehaviour {
    public Renderer M_Mat;
    public int count = 0;
    bool B_DelayUntaged = false;
    // Use this for initialization
    void Start()
    {
        M_Mat = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
       
    }

    public void OnTriggerEnter(Collider other)
    {

            if (!other.CompareTag("Untagged") && !other.CompareTag("Trigger") && !other.CompareTag("Field"))
            {
            count++;
            }

            if (other.CompareTag("Enemy"))
            {
            M_Mat.material.color = Color.red;
            transform.tag = "Enemy";
            }


            if (other.CompareTag("ReservationTower"))
            {
            M_Mat.material.color = Color.blue;
            transform.tag = "ReservationEnemy";
            }

            if (other.name.StartsWith("P")) //== "Player")
            {
                M_Mat.material.color = Color.green;
                transform.tag = "Player";
            }

           

            if (other.CompareTag("ReservationPlayer"))
            {
                M_Mat.material.color = Color.yellow;
                transform.tag = "ReservationPlayer";
            }

            if (other.CompareTag("ReservationEnemy"))
            {
                M_Mat.material.color = Color.blue;
                transform.tag = "ReservationEnemy";
            }
        
       
    }

    public void OnTriggerStay(Collider other)
    {
            if(other.CompareTag("ReservationTower"))
            {
                 M_Mat.material.color = Color.blue;
                 transform.tag = "ReservationEnemy";
            
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
                M_Mat.material.color = Color.blue;
                transform.tag = "ReservationEnemy";
                
            }

            if (count == 0)
            {
                if (!transform.CompareTag("ReservationPlayer")   && !transform.CompareTag("ReservationEnemy"))
                {
                    transform.tag = "Untagged";
                    M_Mat.material.color = Color.gray;
                }

                if(transform.CompareTag("ReservationEnemy") && other.CompareTag("Reset"))
                {
                    transform.tag = "Untagged";
                    M_Mat.material.color = Color.gray;

                }
            }


        


    }
  public void Update()
  {
      if(count > 1)
        {
            count = 1;
        }
        if(count < 0)
        {
           
            count = 0;
        }
  }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ReservationTower") || other.CompareTag("Untagged"))
        {
            transform.tag = "Untagged";
            M_Mat.material.color = Color.gray;
        }
        if (!other.CompareTag("Untagged") && !other.CompareTag("Trigger") && !other.CompareTag("Field"))
        {
            count--;
           
        }


    }

   



}
        
    


   

