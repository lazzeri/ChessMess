using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Tower : MonoBehaviour
{
    public GameObject G_Player;
    public Transform T_PlayerTransform;

    public Transform T_Target;
    public bool B_InTick;
    public int I_Tickcount;
    // Use this for initialization
    public GameObject[] G_Triggers;
    public float F_Speed = 20f;
    public bool B_Moving;
    void Start()
    {
        B_Moving = false;
        I_Tickcount = 0;
        G_Player = GameObject.Find("Player");
        T_PlayerTransform = G_Player.transform;
        T_Target = T_PlayerTransform;

        for(int c = 0; c < 3;c++)
        {
       //     G_Triggers[c] = transform.GetChild(c).gameObject;
        }
        StartCoroutine(TickTock());
    }

    // Update is called once per frame
    void Update()
    {
            if(B_Moving)
            {
            float F_Step = F_Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, T_Target.transform.position, F_Step);
            }
            else if (B_Moving && transform.position.x == T_Target.transform.position.x && transform.position.z == T_Target.transform.position.z && !B_InTick)
            {
            B_Moving = false;
            StartCoroutine(TickTock()); // Needs a delay in TickTock so it doesn't do it too many times
            }

    }
            

    void WhichMode(bool horizontal)
    {
       
    }

    public IEnumerator TickTock()
    {
        T_Target = T_PlayerTransform;
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
          // Target is x axis
           T_Target.transform.position = new Vector3(T_PlayerTransform.position.x,transform.position.y,transform.position.z);
        }
        else
        {
           // Target is z axis
           T_Target.transform.position = new Vector3(transform.position.x,transform.position.y,T_PlayerTransform.position.z);

        }
       
        yield return new WaitForSeconds(0.5f);
        B_Moving = true;
        B_InTick = false;
        I_Tickcount--;
    }
}