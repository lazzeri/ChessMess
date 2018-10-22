using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Pawn : MonoBehaviour {
    public GameObject[] GO_Positions;
    public GameObject T_Target;
    public float F_Speed = 10;
    public bool B_Moving;
    public bool[] B_WhatToMove;
    public GameObject[] PlayerSides;
    public GameObject GO_Target;
    public SC_TriggerEnemyPawn[] Triggers;
    public float TICKTOCKSPEED;
    public SC_ReservationScript[] SC_Reserve;
    public bool B_Reserving = false;
    public float F_RESERVERATIONSPEED = 0.5f;
    public SC_GameManager SC_GameManage;
    public bool B_IamAtacking = false;
    public GameObject Player; 
    public bool B_InTick = false;
    public int I_TickCount = 0;
    public CS_TICKTOCK SC_TickTock;

    public bool B_Tried;

    bool B_ToRemove = false;

    // Use this for initialization
    void Start()
    {
        B_Moving = false;
        SC_TickTock = GameObject.Find("TickTock").GetComponent<CS_TICKTOCK>();
        Player = GameObject.Find("Player");
        SC_GameManage = GameObject.Find("GameManager").GetComponent<SC_GameManager>();
        Triggers = new SC_TriggerEnemyPawn[8];
        for(int c = 0; c < 8;c++)
        {
            Triggers[c] = transform.GetChild(c).GetComponent<SC_TriggerEnemyPawn>();
        }

        SC_Reserve = new SC_ReservationScript[8];

        for (int c = 0; c < 8; c++)
        {
            SC_Reserve[c] = transform.GetChild(c + 8).GetComponent<SC_ReservationScript>();
        }


        GO_Target = new GameObject();
        GO_Target.name = "ChessTarget";
        // 0 = NW 1 = NO 2 = SW 3 = SO
        PlayerSides = new GameObject[6];
       if(GameObject.Find("Player") != null) 
       {
        PlayerSides[0] = Player.transform.GetChild(4).gameObject;
        PlayerSides[1] = Player.transform.GetChild(5).gameObject;
        PlayerSides[2] = Player.transform.GetChild(6).gameObject;
        PlayerSides[3] = Player.transform.GetChild(7).gameObject;
        PlayerSides[4] = Player.transform.GetChild(1).gameObject;
        PlayerSides[5] = Player.transform.GetChild(2).gameObject;
        }

        B_WhatToMove = new bool[8];
        for(int i = 0; i > 8; i++)
        {
            B_WhatToMove[i] = false;
        }
      
        GO_Positions = new GameObject[8];
        T_Target = new GameObject();
        T_Target.transform.position = this.transform.position;
        for (int i = 0; i < 8; i++)
        {
            GO_Positions[i] = this.transform.GetChild(i).gameObject;
        }
      
    }
    

    public void SetB_Moving()
    {
        B_Moving = true;
    }

    public bool getB_Moving()
    {
        return B_Moving;
    }



   public void SetPlayersides()
    {
                  Player = GameObject.Find("Player");
                  PlayerSides[0] = Player.transform.GetChild(4).gameObject;
                  PlayerSides[1] = Player.transform.GetChild(5).gameObject;
                  PlayerSides[2] = Player.transform.GetChild(6).gameObject;
                  PlayerSides[3] = Player.transform.GetChild(7).gameObject;
                  PlayerSides[4] = Player.transform.GetChild(1).gameObject;
                  PlayerSides[5] = Player.transform.GetChild(2).gameObject;
                  GO_Target = new GameObject();
                  GO_Target.name = "ChessTarget";   
                  StopCoroutine(TickTock());
                  StartCoroutine(TickTock());
    }

   
    // Update is called once per frame
    void Update()
    {
      if(B_ToRemove)
      {
        B_Moving = false;
        ResetReservation();
      }

        if (!B_Reserving)
        {
          
            if (B_WhatToMove[0] && !B_Moving && !B_Reserving)
            {
                StartCoroutine(Reservation(0));
               
            }
            else if (B_WhatToMove[1] && !B_Moving && !B_Reserving)
            {
                StartCoroutine(Reservation(1));
                
            }
            else if (B_WhatToMove[2] && !B_Moving && !B_Reserving )
            {
                StartCoroutine(Reservation(2));
               
            }
            else if (B_WhatToMove[3] && !B_Moving && !B_Reserving)
            {           
                StartCoroutine(Reservation(3));
               
            }

            //Atacking
            if (B_WhatToMove[4] && !B_Moving)
            {
                StartCoroutine(Reservation(4));
               

            }
            else if (B_WhatToMove[5] && !B_Moving)
            {
                StartCoroutine(Reservation(5));
               
            }
            else if (B_WhatToMove[6] && !B_Moving)
            {
                StartCoroutine(Reservation(6));
               
            }
            else if (B_WhatToMove[7] && !B_Moving)
            {
                StartCoroutine(Reservation(7));
              
            }                

            float F_Step = F_Speed * Time.deltaTime;

                if (B_Moving && transform.position != T_Target.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, T_Target.transform.position, F_Step);
                }
                else if (B_Moving && transform.position.x == T_Target.transform.position.x && transform.position.z == T_Target.transform.position.z && !B_InTick)
                {
                    B_Moving = false;

                    if (B_IamAtacking)
                    {
                    B_IamAtacking = false;
                    SC_GameManage.setAtacking(false);
                    }

                }
                else if(!B_Moving && transform.position.x == T_Target.transform.position.x && transform.position.z == T_Target.transform.position.z && !B_InTick)
                {
                          
                   B_InTick = false;
                            
                }



        }
    
    
    
    }
    public void ResetReservation()
    {
        B_Moving = false;
        for (int i = 0; i < 8; i++)
        {
        SC_Reserve[i].SetToReset();
        }
    }

    public IEnumerator Reservation(int num)
    {
        B_Reserving = true;
        SC_Reserve[num].SetToTrigger();
        
        yield return new WaitForSeconds(F_RESERVERATIONSPEED);
 
        SC_GameManage.setReserving(false);
        SC_Reserve[num].SetOffTriger();
        B_Reserving = false;

        B_WhatToMove[num] = false;
        T_Target.transform.position = GO_Positions[num].transform.position;
        B_Moving = true;



    }
 

    IEnumerator HorizontalMode()
    {
         if(PlayerSides != null)
        {
        yield return new WaitForSeconds(0.0f);
        float NToTarget = Vector3.Distance(PlayerSides[4].transform.position, transform.position);
        float SToTarget = Vector3.Distance(PlayerSides[5].transform.position, transform.position);
       
       
        if (NToTarget < SToTarget)
        {

            if (!Triggers[0].getTriggered() && Triggers[0].getTarget() != null)
            {
                B_WhatToMove[0] = true;
            }

            B_Moving = true;

        }
        else
        {
            if (!Triggers[1].getTriggered() && Triggers[1].getTarget() != null)
            {

                B_WhatToMove[1] = true;
            }else
            B_Moving = true;
   
        }
        }
    B_InTick = false;
    I_TickCount--;
    }
    IEnumerator VerticalMode()
    {
        yield return new WaitForSeconds(0.0f);
        if(PlayerSides != null)
        {
        float WToTarget = Vector3.Distance(PlayerSides[2].transform.position, transform.position);
        float OToTarget = Vector3.Distance(PlayerSides[3].transform.position, transform.position);

        if (WToTarget < OToTarget)
        {
            if (!Triggers[3].getTriggered() && Triggers[3].getTarget() != null)
            {
                B_WhatToMove[3] = true;
            }else
            B_Moving = true;
            //StartCoroutine(HorizontalMode());
            
        }
        else
        {
            if (!Triggers[2].getTriggered() && Triggers[2].getTarget() != null)
            {
                B_WhatToMove[2] = true;
            }else
            B_Moving = true;

           // StartCoroutine(HorizontalMode());
           
        }
        }
         B_InTick = false;
         I_TickCount--;
           

    }
    
    public IEnumerator TickTock()
    {

        I_TickCount++;
        B_InTick = true;
        B_Tried = false;
   
        if(GameObject.Find("Player") != null && !B_ToRemove)
        {
                Player = GameObject.Find("Player");
                PlayerSides[0] = Player.transform.GetChild(4).gameObject;
                PlayerSides[1] = Player.transform.GetChild(5).gameObject;
                PlayerSides[2] = Player.transform.GetChild(6).gameObject;
                PlayerSides[3] = Player.transform.GetChild(7).gameObject;
                PlayerSides[4] = Player.transform.GetChild(1).gameObject;
                PlayerSides[5] = Player.transform.GetChild(2).gameObject;
                yield return new WaitForSeconds(TICKTOCKSPEED);
               //Searching Nearest Target

        if(GameObject.Find("Player") != null && !B_ToRemove)
        {    
                float NW, NO, SW, SO;
                NW = Vector3.Distance(PlayerSides[0].transform.position, transform.position);
                NO = Vector3.Distance(PlayerSides[1].transform.position, transform.position);
                SW = Vector3.Distance(PlayerSides[2].transform.position, transform.position);
                SO = Vector3.Distance(PlayerSides[3].transform.position, transform.position);

                float max = Mathf.Min(NW, NO, SW, SO);

                if (max == NW)
                {
                    if (PlayerSides[0].GetComponent<SC_TriggerPlayerPawn>().getTarget() != null)
                        GO_Target = PlayerSides[0];
                    else
                        GO_Target = PlayerSides[1];
                }
                else if (max == NO)
                {
                    if (PlayerSides[1].GetComponent<SC_TriggerPlayerPawn>().getTarget() != null)
                        GO_Target = PlayerSides[1];
                    else
                        GO_Target = PlayerSides[0];
                }
                else if (max == SW)
                {
                    if (PlayerSides[2].GetComponent<SC_TriggerPlayerPawn>().getTarget() != null)
                        GO_Target = PlayerSides[2];
                    else
                        GO_Target = PlayerSides[3];
                }
                else if (max == SO)
                {
                    if (PlayerSides[3].GetComponent<SC_TriggerPlayerPawn>().getTarget() != null)
                        GO_Target = PlayerSides[3];
                    else
                        GO_Target = PlayerSides[2];
                }
               

                //-------------------------------------------- Calculations For Distance
                float Targetpositionz = GO_Target.transform.position.z;
                float MyPositionz = transform.position.z;
    
                float Max = Targetpositionz + 9.5f;
                float Min = Targetpositionz - 9.5f;

                float Targetpositionx = GO_Target.transform.position.x;
                float MyPositionx = transform.position.x;

                float Maxx = Targetpositionx + 9.5f;
                float Minx = Targetpositionx - 9.5f;


                //If he is on the Right Spot
                if (MyPositionz <= Max && MyPositionz >= Min && MyPositionx <= Maxx && MyPositionx >= Minx)
                {
                    
                    if (GO_Target == PlayerSides[0] &&  Triggers[7].getTarget() == "Player" && !SC_GameManage.getAtacking())
                    {
                        B_WhatToMove[7] = true;
                        SC_GameManage.setAtacking(true);
                        B_IamAtacking = true;

                    }
                    else
                    if (GO_Target == PlayerSides[1] && Triggers[6].getTarget() == "Player" && !SC_GameManage.getAtacking())
                    {

                        B_WhatToMove[6] = true;
                        SC_GameManage.setAtacking(true);
                        B_IamAtacking = true;
                                                


                    }
                    else
                    if (GO_Target == PlayerSides[2] && Triggers[5].getTarget() == "Player" && !SC_GameManage.getAtacking())
                    {
                        B_WhatToMove[5] = true;
                        SC_GameManage.setAtacking(true);
                        B_IamAtacking = true;
                                                

                    }
                    else
                    if (GO_Target == PlayerSides[3] && Triggers[4].getTarget() == "Player" && !SC_GameManage.getAtacking())
                    {
                        B_WhatToMove[4] = true;
                        SC_GameManage.setAtacking(true);
                        B_IamAtacking = true;


                    }
                    I_TickCount--;
                    B_InTick = false;
                    //------------------------------------------------
                    //If he is on The South part alright but not on the north
                } else if (MyPositionz <= Max && MyPositionz >= Min)
                {

                    StartCoroutine(HorizontalMode());

                }
                else
                {
                    //-------------------------------------------------------
                    //If he is on North and Southpart Wrong

                    StartCoroutine(VerticalMode());

                    //----------------------------------

                }
        }
        }
    }




  public void toRemove()
  {
      B_ToRemove = true;
      B_Moving = false;
  }
 
  public bool getToRemove()
  {
      return B_ToRemove;
  }

    }

