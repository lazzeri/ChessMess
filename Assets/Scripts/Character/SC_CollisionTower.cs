using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CollisionTower : MonoBehaviour
{
    private CS_TICKTOCK SC_Tick;
    private SC_GameManager SC_Gamemanage;
    private SC_TowerCharacter SC_Tower;
    // Use this for initialization
    void Start()
    {
        SC_Tick = GameObject.Find("TickTock").GetComponent<CS_TICKTOCK>();
        SC_Gamemanage = GameObject.Find("GameManager").GetComponent<SC_GameManager>();
        SC_Tower = this.transform.GetComponentInParent<SC_TowerCharacter>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (SC_Tick.getPlayerSide() && other.name == "EnemyCollision")
        {
        other.transform.parent.gameObject.GetComponent<SC_Pawn>().ResetReservation();
        other.transform.parent.gameObject.GetComponent<SC_Pawn>().toRemove();
        SC_Gamemanage.setAtacking(false);
            SC_Tower.StopMovingKill();
        }
        else
        {
         SC_Gamemanage.StopEnemies();
        Destroy(transform.parent.gameObject);
         

        }
    }

}