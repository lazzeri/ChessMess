using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Tower : MonoBehaviour
{
    public GameObject G_Player;
    public Transform T_PlayerTransform;
    public bool B_InTick;
    public int I_Tickcount;
    // Use this for initialization
    void Start()
    {
        I_Tickcount = 0;
       G_Player = GameObject.Find("Player");
       T_PlayerTransform = G_Player.transform;


    }

    // Update is called once per frame
    void Update()
    {

    }

    void WhichMode(bool horizontal)
    {
        if(horizontal)
        {
            if(transform.position.x - G_Player.transform.position.x > )
                //nach norden
        }
        else
        {

        }
    }

    public IEnumerator TickTock()
    {
        B_InTick = true;
        I_Tickcount++;
        float Targetpositionz = G_Player.transform.position.z;
        float MyPositionz = transform.position.z;

        float Max = Targetpositionz + 9.5f;
        float Min = Targetpositionz - 9.5f;

        float Targetpositionx = G_Player.transform.position.x;
        float MyPositionx = transform.position.x;

        float Maxx = Targetpositionx + 9.5f;
        float Minx = Targetpositionx - 9.5f;


        if (MyPositionz <= Max && MyPositionz >= Min)
        {

          yield return StartCoroutine(HorizontalMode());

        }
        else
        {
            //-------------------------------------------------------
            //If he is on North and Southpart Wrong

            yield return StartCoroutine(VerticalMode());

            //----------------------------------

        }

        

        yield return new WaitForSeconds(0.5f);



        B_InTick = false;
        I_Tickcount--;
    }
}