using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Collision : MonoBehaviour
{
    private CS_TICKTOCK SC_Tick;
    private SC_GameManager SC_Gamemanage;
    // Use this for initialization
    void Start()
    {
        SC_Tick = GameObject.Find("TickTock").GetComponent<CS_TICKTOCK>();
        SC_Gamemanage = GameObject.Find("GameManager").GetComponent<SC_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (SC_Tick.getPlayerSide())
        {
            Destroy(other.transform.parent.gameObject);
         

        }
        else
        {
            SC_Gamemanage.StopEnemies();
            Destroy(transform.parent.gameObject);
         

        }
    }

}