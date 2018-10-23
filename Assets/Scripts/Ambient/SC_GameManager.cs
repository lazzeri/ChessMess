using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameManager : MonoBehaviour {
    public GameObject Pawn,PlayerPawn;
    public bool B_Atacking;
    public bool B_Reserving;
    public bool B_StartOfTickRoutine;
    // Use this for initialization
    bool B_LastToKill;

    public bool Test;
    public List<GameObject> Enemies = new List<GameObject>();


    void Start () {
        B_LastToKill = false;
        B_StartOfTickRoutine = true;
        B_Atacking = false;
        B_Reserving = false;
        //CreatePawn(5,5);
        //CreatePawn(15,15);
       //CreatePawn(35,35);
       // CreatePawn(-25f,25f);
      // CreatePawn(-15,35);
       CreatePawn(25f,-35f); // CLosest one
       //CreatePawn(-15,5);
    }

    void CreatePawn(float x, float z)
    {
        Vector3 posi = new Vector3(x,5,z);
        Quaternion roti = new Quaternion(0, 0, 0,0);
        GameObject Enemie1 = (GameObject)Instantiate(Pawn, posi, roti);
        Enemie1.SetActive(true);
        Enemie1.name = "Pawn";
        Enemies.Add(Enemie1);
    }


	

    public void StopEnemies()
    {
      foreach(GameObject Enemy in Enemies)
      {
        if(Enemy.GetComponent<SC_Pawn>())
        {
        Enemy.GetComponent<SC_Pawn>().enabled = false;
        }
        
      }
    }
    public void StartEnemies()
    {
      for(int i = 0; i < Enemies.Count; i++)
       {
        if(Enemies[i].GetComponent<SC_Pawn>())
        {
        Enemies[i].GetComponent<SC_Pawn>().enabled = true;
        Destroy(Enemies[i].GetComponent<SC_Pawn>().GO_Target);
        Enemies[i].GetComponent<SC_Pawn>().SetPlayersides();
        Enemies[i].GetComponent<SC_Pawn>().SetB_Moving();
        }
     
      
       }
    }
    
    IEnumerator StartTickTocks()
    {
        bool breaker = false;
        B_StartOfTickRoutine = false;

        if(Enemies.Count == 1 && Enemies[0].GetComponent<SC_Pawn>().getToRemove() && Enemies[0] != null && Enemies[0].GetComponent<SC_Pawn>().getB_Moving() == false)
        {
            Enemies[0].GetComponent<SC_Pawn>().StopAllCoroutines();
            yield return new WaitForSeconds(0.1f);
            Destroy(Enemies[0]);
            Enemies.Remove(Enemies[0]);
            breaker = true;


        } else if(Enemies.Count == 1 && Enemies[0].GetComponent<SC_Pawn>().getB_Moving() == false && B_Atacking != true && Enemies[0] != null && !breaker && Enemies[0].GetComponent<SC_Pawn>().B_InTick == false)
        {
            StartCoroutine(Enemies[0].GetComponent<SC_Pawn>().TickTock());
            yield return new WaitForSeconds(0.5f);
        }
        else if(Enemies.Count > 1)
        {
        for(int i = 0; i < Enemies.Count; i++)
        { 
            
            
            if(Enemies[i].GetComponent<SC_Pawn>().getToRemove())
            {
            Enemies[i].GetComponent<SC_Pawn>().ResetReservation();
            Enemies[i].GetComponent<SC_Pawn>().StopAllCoroutines();
            Destroy(Enemies[i]);
            Enemies.Remove(Enemies[i]);
            breaker = true;
            } 
            else if(Enemies[i].GetComponent<SC_Pawn>().getB_Moving() == false && B_Atacking != true && !breaker)
            {
                StartCoroutine(Enemies[i].GetComponent<SC_Pawn>().TickTock());
                
            }
            yield return new WaitForSeconds(0.1f);
            if(Enemies.Count == 2)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }        
        }else
        {
            yield return new WaitForSeconds(0.5f);
        }
        
        breaker = false;    
        B_StartOfTickRoutine = true;

    }




    private void FixedUpdate()
    {
        if (B_StartOfTickRoutine)
        {
            StartCoroutine(StartTickTocks());
        }


        if (Input.GetKey("space"))
        {
      
            GameObject player =  GameObject.Find("Player");

            if(player == null)
            { //  if(!player.activeSelf) {
            Vector3 posi = new Vector3(5,5,5);
            Quaternion roti = new Quaternion(0, 0, 0,0);
            GameObject Player = (GameObject)Instantiate(PlayerPawn, posi, roti);
            Player.SetActive(true);
            Player.name = "Player";
             Destroy(player);
             StartEnemies();
            }
           
        }

         if (Input.GetKey("g"))
        {
        Vector3 posi = new Vector3(5,5,5);
        Quaternion roti = new Quaternion(0, 0, 0,0);
        GameObject Enemy = (GameObject)Instantiate(Pawn, posi, roti);
        Enemy.SetActive(true);
        Enemy.name = "Pawn";
        }

    }
    public bool getAtacking()
    {
        return B_Atacking;

    }

    public void setAtacking(bool boolean)
    {
        B_Atacking = boolean;
    }


     public bool getReserving()
    {
        return B_Reserving;

    }

    public void setReserving(bool boolean)
    {
        B_Reserving = boolean;
    }


}
