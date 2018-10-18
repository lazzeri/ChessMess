using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameManager : MonoBehaviour {
    public GameObject Pawn,PlayerPawn;
    public bool B_Atacking;
    public bool B_Reserving;
    // Use this for initialization
    public List<GameObject> Enemies = new List<GameObject>();

    void Start () {
        B_Atacking = false;
        B_Reserving = false;
        CreatePawn(5,5);
        CreatePawn(15,15);
<<<<<<< HEAD
        CreatePawn(35,35);
        CreatePawn(-25f,25f);
        CreatePawn(-15,35);
        CreatePawn(25f,-35f);
        CreatePawn(-15,5);
=======
         CreatePawn(35,35);
         CreatePawn(-25f,25f);
         CreatePawn(-15,35);
         CreatePawn(25f,-35f);
         CreatePawn(-15,5);
>>>>>>> parent of a012a0f... Finally Fixed Pawn Movement

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
	
	// Update is called once per frame
	void Update () {
		

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
       foreach(GameObject Enemy in Enemies)
       {
        if(Enemy.GetComponent<SC_Pawn>())
        {
        Enemy.GetComponent<SC_Pawn>().enabled = true;
        Destroy(Enemy.GetComponent<SC_Pawn>().GO_Target);
        Enemy.GetComponent<SC_Pawn>().SetPlayersides();
        Enemy.GetComponent<SC_Pawn>().SetB_Moving();
        }
     
      
       }
    }


    private void FixedUpdate()
    {
       


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
        B_Atacking = boolean;
    }


}
//in 15 schritten y = 5