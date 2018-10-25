using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TowerCharacter : MonoBehaviour
{
    public bool B_Moving;
	public SC_Bool B_MovRight, B_MovLeft, B_MovUp, B_MovDown;
    public GameObject[] GO_Positions;
    public float F_Speed;
    public Transform T_Target,T_OldTarget;
    
    public bool B_TestBool1,B_TestBool2,B_TestBool3,B_TestBool4;

    public bool B_TriedMoving;

    public bool B_WanToChange;

    bool B_FirstStep;

    float F_OldX, F_OldY;
    void Start ()
    {
        
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
            if(transform.position.x == F_OldX && transform.position.y == F_OldY)
            {
                B_WanToChange = false;
            }
            else
            {
               float F_Step = F_Speed * Time.deltaTime;
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(F_OldX,F_OldY), F_Step);  RIght OnE
                transform.position = Vector3.MoveTowards(transform.position, T_OldTarget.transform.position, F_Step); 
            }

        } else if(B_Moving && !B_WanToChange)
        {
            float F_Step = F_Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, T_Target.transform.position, F_Step);
        }


    }

    IEnumerator Move(int num, SC_Bool B_CounterMove, SC_Bool B_OwnMove)
    {
        if(!B_TriedMoving)
        {
            B_TriedMoving = true;
            
            if (B_CounterMove.getBool())
            {
                B_Moving = false;
                T_OldTarget = T_Target;
                B_WanToChange = true;
                ResetMoves();
            }
            else 
            {
                T_OldTarget = T_Target;
                if(!B_FirstStep)
                {
                    B_WanToChange = true;

                    if (B_MovUp)
                        ChangeTransform("North");
                    else if (B_MovDown)
                        ChangeTransform("South");
                    else if (B_MovRight)
                        ChangeTransform("East");
                    else if (B_MovLeft)
                        ChangeTransform("West");

                    print(F_OldX);
                    print(F_OldY);
                }
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

    float returnFixedValueX(string way)
    {

        switch (way)
        {
            case "North":
                int i = Mathf.RoundToInt(transform.position.x);
                while (i % 5 != 0)
                {
                    i++;
                    print(i);
                }
                return i;
            case "South":
                int b = Mathf.RoundToInt(transform.position.x);
                while (b % 5 != 0)
                {
                    b--;
                    print(b);
                }
              
                return b;
            default:
                return transform.position.x;
        }
    }

    float returnFixedValueY(string way)
    {

        switch (way)
        {
            case "West":
                int i = Mathf.RoundToInt(transform.position.y);
                while (i % 5 != 0)
                    i++;
                return i;
            case "East":
                int b = Mathf.RoundToInt(transform.position.y);
                while (b % 5 != 0)
                    b--;
                return b;
            default:
                return transform.position.y;
        }
    }

    void ChangeTransform(string way)
    {
        switch (way)
        {
            case "North":
                F_OldX = returnFixedValueX("North");
                F_OldY = T_OldTarget.position.y;
                break;
            case "South":
                F_OldX = returnFixedValueX("South");
                F_OldY = T_OldTarget.position.y;
                break;
            case "East":
                F_OldY = returnFixedValueY("East");
                F_OldX = T_OldTarget.position.x;
                break;
            case "West":
                F_OldY = returnFixedValueY("West");
                F_OldX = T_OldTarget.position.x;
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
}
