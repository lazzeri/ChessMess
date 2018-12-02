using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Tower : MonoBehaviour
{
    public GameObject G_Player;
    public Transform T_PlayerTransform;

    public Vector3 T_Target;
    public bool B_InTick;
    public int I_Tickcount;
    // Use this for initialization
    public GameObject[] G_Triggers;
    public float F_Speed = 20f;
    public bool B_Moving;
    public SC_ReservationScript[] SC_Reserve;
    void Start()
    {
        SC_Reserve = new SC_ReservationScript[4];
        B_Moving = false;
        I_Tickcount = 0;
        G_Player = GameObject.Find("Player");
        T_PlayerTransform = G_Player.transform;

        for (int c = 4; c < 8; c++)
        {
            SC_Reserve[c - 4] = transform.GetChild(c).GetComponent<SC_ReservationScript>();
        }


        for (int c = 0; c < 3;c++)
        {
       //     G_Triggers[c] = transform.GetChild(c).gameObject;
        }
        StartCoroutine(TickTock());
    }

    // Update is called once per frame
    // Nuies Movement system. Mir setzen wohin er geat wenn mir wellen. es problem hem isch das mor a die x achse miasen richten oder? Weil suscht miasmor worte
    // Und hell isch packo also wia mochmor? 
    // Wenn mor wellen, dass er ziel olleweil verändert miaseter stoppen und erster vor sich die negschte findne wo er a hell miaset wissen.
    // Wenn mir no net die Z richtig hoben ober se updaten miaset hell woll passen oder?
    void Update()
    {
            if(B_Moving)
            {
            float F_Step = F_Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, T_Target, F_Step);
            }
            if (B_Moving && transform.position.x == T_Target.x && transform.position.z == T_Target.z)
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
        ResetRReservation();
        I_Tickcount++;
        if (I_Tickcount > 1)
            print("AYAYAY");
        float Targetpositionz = G_Player.transform.position.z;
        float MyPositionz = transform.position.z;

        float Max = Targetpositionz + 9.5f;
        float Min = Targetpositionz - 9.5f;

        float Targetpositionx = G_Player.transform.position.x;
        float MyPositionx = transform.position.x;

        float Maxx = Targetpositionx + 9.5f;
        float Minx = Targetpositionx - 9.5f;
        // make walkpath shorter then so it can go for a next value faster and make the path a variaton between 3 and 6 blocks
        
        if (MyPositionz <= Max && MyPositionz >= Min)
            //Irgentwos mit wenn zu nohme kimp oder so lmao des findesch schun auser
        {
            float T_FixedTargetx = returnFixedValue(T_PlayerTransform.position.x);
            if (MyPositionz <= Max && MyPositionz >= Min && MyPositionx <= Maxx && MyPositionx >= Minx)
            {
                ResetRReservation();
            }
            else if (T_FixedTargetx - transform.position.x >= 50 || T_FixedTargetx - transform.position.x <= -50) //To far away
            {
            
                if (transform.position.x >= T_FixedTargetx)
                {
                    T_FixedTargetx = Targetpositionx + 30;
            
                }
                else
                {
                    T_FixedTargetx = T_FixedTargetx - 30;
                    

                }
                T_Target = new Vector3(T_FixedTargetx, transform.position.y, transform.position.z);

            }
            else
            {
                T_Target = new Vector3(T_FixedTargetx, transform.position.y, transform.position.z);

            }

            if( Targetpositionx- transform.position.x> 0)
            {
                SC_Reserve[0].SetToTrigger();

            }
            else
            {
                SC_Reserve[1].SetToTrigger();

            }
        }
        else
        {
            // Target is z axis
            //if(position is further then 5 blocks make 3block placements)
            float T_FixedTargetz = returnFixedValue(T_PlayerTransform.position.z);

            if (T_FixedTargetz - transform.position.z >= 50 || T_FixedTargetz - transform.position.z <= -50) //To far away
            {

                if (transform.position.z >= T_FixedTargetz)
                {
                    T_FixedTargetz = Targetpositionz + 30;
                  

                }
                else
                {
                    T_FixedTargetz = Targetpositionz - 30;
                    

                }
                T_Target = new Vector3(transform.position.x, transform.position.y, T_FixedTargetz);
            }
            else
            {
                T_Target = new Vector3(transform.position.x, transform.position.y, T_FixedTargetz);

            }
            if (Targetpositionz - transform.position.z > 0)
            {
                SC_Reserve[2].SetToTrigger();

            }
            else
            {
                SC_Reserve[3].SetToTrigger();

            }
        }
      
        yield return new WaitForSeconds(0f);
        B_Moving = true;
        I_Tickcount--;
    }

    

    float returnFixedValue(float position)
    {
        int i = Mathf.RoundToInt(position);
            //wenb i greaser isch nor blabla suscht klianer mochen
            
            i = i - 5;
            while (i  % 10 != 0)
            {
                i++;
            }
          
            if(position - (i+5) < position - (i-15))
                return i +5;
            else
                return i -15;




            //So now we have the exact position, we want to change the length now to about 3 - maxrange?
            //Like this: if the position is further away then 5 blocks our target is only 3 blocks


                
    }


    public void ResetRReservation()
    {
        for (int c = 0; c < 4; c++)
        {
            SC_Reserve[c].SetOffTriger();
        }
    }

}