using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TowerCharacter : MonoBehaviour
{
    public bool B_Moving;
	public SC_Bool B_MovUp, B_MovDown, B_MovRight, B_MovLeft;
    public GameObject[] GO_Positions;
    public float F_Speed;
    public Transform T_Target;
    
    public bool B_TestBool1,B_TestBool2,B_TestBool3,B_TestBool4;

    public bool B_TriedMoving;

    public bool B_WanToChange;

    bool B_FirstStep;

    float F_OldX, F_OldZ;

    public bool B_OutofBounding;

    bool B_JustKilled;

   public SC_TriggerTower[] SC_Triggers;
    void Start ()
    {
        B_JustKilled = false;
        B_OutofBounding = false;
        //TestBool is To Remove
        B_TestBool1 = false;
        B_TestBool2 = false;
        B_TestBool3 = false;
        B_TestBool4 = false;

        B_WanToChange = false;

        GO_Positions = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            GO_Positions[i] = this.transform.GetChild(i).gameObject;
        }

        SC_Triggers = new SC_TriggerTower[4];
        for (int i = 0; i < 4; i++)
        {
            SC_Triggers[i] = this.transform.GetChild(i).gameObject.GetComponent<SC_TriggerTower>();
        }


        B_Moving = false;

        B_MovRight = new SC_Bool();
        B_MovDown =  new SC_Bool();
        B_MovUp =  new SC_Bool();
        B_MovLeft = new SC_Bool();

        B_MovRight.setBool(false);
        B_MovLeft.setBool(false);
        B_MovUp.setBool(false);
        B_MovDown.setBool(false);

        B_TriedMoving = false;
        B_FirstStep = true;
    }

    


    private void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            StartCoroutine(Move(0, B_MovDown, B_MovUp));
        }
        else if (Input.GetKey("s"))
        {
            StartCoroutine(Move(1, B_MovUp, B_MovDown));
        }
        else if (Input.GetKey("a"))
        {
            StartCoroutine(Move(2, B_MovRight, B_MovLeft));
        }
        else if (Input.GetKey("d"))
        {
           StartCoroutine(Move(3, B_MovLeft, B_MovRight));
        }
    }

    void Update ()
    {
        B_TestBool1 = B_MovUp.getBool();
        B_TestBool2 = B_MovDown.getBool();
        B_TestBool3 = B_MovLeft.getBool();
        B_TestBool4 = B_MovRight.getBool();

        


        if (B_WanToChange)
        {
            if(transform.position.x == F_OldX && transform.position.z == F_OldZ && B_OutofBounding)
            {
                ResetMoves();
                B_OutofBounding = false;
                B_WanToChange = false;
            }

            if(transform.position.x == F_OldX && transform.position.z == F_OldZ)
            {
                B_WanToChange = false;
            }
            else
            {
               float F_Step = F_Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(F_OldX,6,F_OldZ), F_Step);  
            }

        } else if(B_Moving && !B_WanToChange)
        {
            float F_Step = F_Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, T_Target.transform.position, F_Step);
        }


    }

    IEnumerator Move(int num, SC_Bool B_CounterMove, SC_Bool B_OwnMove)
    {
        if(!B_TriedMoving && !B_WanToChange && !B_OwnMove.getBool() && !B_OutofBounding && SC_Triggers[num].getTriggered() && !B_JustKilled)
        {
            B_TriedMoving = true; // To reduce Player input
            
            if(!B_FirstStep)
            {
                    B_WanToChange = true;

                    if (B_MovUp.getBool())
                    {
                        ChangeTransform(1);
                    }
                    else if (B_MovDown.getBool())
                        ChangeTransform(2);
                    else if (B_MovRight.getBool())
                        ChangeTransform(3);
                    else if (B_MovLeft.getBool())
                        ChangeTransform(4);
            }

            if (B_CounterMove.getBool())  //To stop player
            {
                B_Moving = false;
                B_WanToChange = true;
                ResetMoves();
            }
            else 
            {
                ResetMoves();
                T_Target = GO_Positions[num].transform;
                B_OwnMove.setBool(true);
                B_Moving = true;
                B_FirstStep = false;
            }
            
            yield return new WaitForSeconds(0.15f);
            B_TriedMoving = false;
        }
       
        
    }

    float returnFixedValueX(int way)
    {

        switch (way)
        {
            case 1:
                int i = Mathf.RoundToInt(transform.position.x);
                 i = i - 5;
                while (i  % 10 != 0)
                {
                    i++;
                    //print(i);
                }
                return i + 5;
            case 2:
                int b = Mathf.RoundToInt(transform.position.x);
                 b = b - 5;
                while (b  % 10 != 0)
                {
                    b--;
                   // print(b);
                }
              
                return b + 5;
            default:
                print("Lmao");
                return transform.position.x;
        }
    }
    
    float returnFixedValueZ(int way)
    {

        switch (way)
        {
            case 4:
                int i = Mathf.RoundToInt(transform.position.z);
                i = i - 5;
                while (i  % 10 != 0)
                {
                    i++;
                }
                return i + 5;
            case 3:
                int b = Mathf.RoundToInt(transform.position.z);
                 b = b - 5;
                while (b  % 10 != 0)
                {
                    b--;
                }
                return b + 5;
            default:
                return transform.position.z;
        }
    }

    void ChangeTransform(int way)
    {
        switch (way)
        {
            case 1:
                F_OldX = returnFixedValueX(1);
                F_OldZ = transform.position.z;
                break;
            case 2:
                F_OldX = returnFixedValueX(2);
                F_OldZ = transform.position.z;
                break;
            case 3:
                F_OldZ = returnFixedValueZ(3);
                F_OldX = transform.position.x;
                break;
            case 4:
                F_OldZ = returnFixedValueZ(4);
                F_OldX = transform.position.x;
                break;
        }
    }

    void ResetMoves()
    {
        B_MovDown.setBool(false);
        B_MovUp.setBool(false);
        B_MovRight.setBool(false);
        B_MovLeft.setBool(false);

    }
    public void CalcStopWay()
    {
        B_WanToChange = true;
        B_OutofBounding = true;
            if (B_MovUp.getBool())
            {
                ChangeTransform(1);
            }
            else if (B_MovDown.getBool())
                ChangeTransform(2);
            else if (B_MovRight.getBool())
                ChangeTransform(3);
            else if (B_MovLeft.getBool())
                ChangeTransform(4);   
    }

    //This method is used for when he is try to get out of a field
     public  void StopMoving()
     {
        if (B_MovUp.getBool() && !SC_Triggers[0].getTriggered())
        {
            B_Moving = false;
            CalcStopWay();
        }
        else if (B_MovDown.getBool() && !SC_Triggers[1].getTriggered())
        {
            B_Moving = false;
            CalcStopWay();
        }
        else if (B_MovRight.getBool() && !SC_Triggers[3].getTriggered())
        {
            B_Moving = false;
            CalcStopWay();

        }
        else if (B_MovLeft.getBool() && !SC_Triggers[2].getTriggered())
        {
            B_Moving = false;           
            CalcStopWay();
        }

    }
    //Just try to disable the input for like 1 second when we kill someone smh
    IEnumerator DelayKill()
    {
        B_JustKilled = true;
        yield return new WaitForSeconds(0.5f);
        B_JustKilled = false;
    }

    public void StopMovingKill()
    {
        StartCoroutine(DelayKill());

        if (B_MovUp.getBool())
        {
            B_Moving = false;
            CalcStopWay();
        }
        else if (B_MovDown.getBool())
        {
            B_Moving = false;
            CalcStopWay();
        }
        else if (B_MovRight.getBool())
        {
            B_Moving = false;
            CalcStopWay();

        }
        else if (B_MovLeft.getBool())
        {
            B_Moving = false;
            CalcStopWay();
        }

    }
    


}

