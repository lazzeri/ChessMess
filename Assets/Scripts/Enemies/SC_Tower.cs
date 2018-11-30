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
    void Start()
    {
        B_Moving = false;
        I_Tickcount = 0;
        G_Player = GameObject.Find("Player");
        T_PlayerTransform = G_Player.transform;
        


        for(int c = 0; c < 3;c++)
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
            if (B_Moving && transform.position.x == T_Target.x && transform.position.z == T_Target.z && !B_InTick)
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
        // make walkpath shorter then so it can go for a next value faster and make the path a variaton between 3 and 6 blocks
        
        if (MyPositionz <= Max && MyPositionz >= Min)
        {
          // Target is x axis
           T_Target = new Vector3(returnFixedValue(T_PlayerTransform.position.x),transform.position.y,transform.position.z);
        }
        else
        {
           // Target is z axis
           T_Target = new Vector3(transform.position.x,transform.position.y,returnFixedValue(T_PlayerTransform.position.z));
        }
       
        yield return new WaitForSeconds(0.1f);
        B_Moving = true;
        B_InTick = false;
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

            
            // posi ist 22
            // also zwischen 15 and 25
            // 22 - 25 = -3  22 - 10 = 12 passt;
            // -22 - -25 = 3 -22 - -10 = -12 passt net HEM NEMMERS GEGENTEIL wenn posi negativ isch
        //wenn de lokation näher on position isch als de position -15 nor passts de posi konn ober a -10 sein
        //beispiel: mir hoben -11 hell geat auf -10 und -25 -11 - -10 = klianer als es uane
        //                     11                15 und 30      

        // mir hoben beispiel etwas auf 15 bis 25  sol es zu 15 oder 25?  nehemen insere posi und comparen wos?
        // 15 -mypos >             
    }

}