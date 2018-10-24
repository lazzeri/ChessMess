using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TowerCharacter : MonoBehaviour
{
    public bool B_Moving;
	public SC_Bool B_MovRight, B_MovLeft, B_MovUp, B_MovDown;
    public GameObject[] GO_Positions;
    public float F_Speed;
    public Transform T_Target;
    
    void Start ()
    {
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

    }

    


    private void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            Move(0, B_MovDown, B_MovUp);
        }
        else if (Input.GetKey("s"))
        {
            Move(1, B_MovUp, B_MovDown);
        }
        else if (Input.GetKey("a"))
        {
            Move(2, B_MovRight, B_MovLeft);
        }
        else if (Input.GetKey("d"))
        {
            Move(3, B_MovLeft, B_MovRight);
        }
    }

    void Update ()
    {
		if(B_Moving)
        {
            float F_Step = F_Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, T_Target.transform.position, F_Step);
        }
    }


    void Move(int num, SC_Bool B_CounterMove, SC_Bool B_OwnMove)
    {
        if (B_CounterMove.getBool())
        {
            B_Moving = false;
            ResetMoves();
        }
        else
        {
            
            B_OwnMove.setBool(true);
            T_Target = GO_Positions[num].transform;
            B_Moving = true;
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
