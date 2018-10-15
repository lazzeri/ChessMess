using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CharacterMovement : MonoBehaviour {

    public GameObject [] GO_Positions;
    public GameObject T_Target;
    public float F_Speed = 100;
    public bool B_Moving;
    public SC_TriggerPlayerPawn[] Triggers;
    public SC_ReservationScript[] SC_Reserve;
    public bool B_Reserving = false;
    public float F_RESERVERATIONSPEED = 0.5f;

    // Use this for initialization
    void Start ()
    {

        SC_Reserve = new SC_ReservationScript[8];

        for (int c = 0; c < 8; c++)
        {
            SC_Reserve[c] = transform.GetChild(c + 8).GetComponent<SC_ReservationScript>();
        }



        Triggers = new SC_TriggerPlayerPawn[8];
        for (int c = 0; c < 8; c++)
        {
            Triggers[c] = transform.GetChild(c).GetComponent<SC_TriggerPlayerPawn>();
        }

        B_Moving = false;
        GO_Positions = new GameObject[8];
        T_Target = new GameObject();
        T_Target.transform.position = this.transform.position;
		for(int i = 0; i < 8; i++)
        {
            GO_Positions[i] = this.transform.GetChild(i).gameObject;
        }
	}

    public void EnableCollider()
    {
        this.GetComponent<BoxCollider>().enabled = true;
    }
    public void DisableCollider()
    {
        this.GetComponent<BoxCollider>().enabled = false;
    }


    void FixedUpdate()
    {
       
        if (!B_Reserving)
        {
            
                //Moving
            if (Input.GetKey("w") && !B_Moving && !Triggers[0].getTriggered())
            {
                StartCoroutine(Reservation(0));
                T_Target.transform.position = GO_Positions[0].transform.position;
                B_Moving = true;
            }
            else if (Input.GetKey("s") && !B_Moving && !Triggers[1].getTriggered())
            {
                StartCoroutine(Reservation(1));
                T_Target.transform.position = GO_Positions[1].transform.position;
                B_Moving = true;
            }
            else if (Input.GetKey("a") && !B_Moving && !Triggers[2].getTriggered())
            {
                StartCoroutine(Reservation(2));
                T_Target.transform.position = GO_Positions[2].transform.position;
                B_Moving = true;
            }
            else if (Input.GetKey("d") && !B_Moving && !Triggers[3].getTriggered())
            {
                StartCoroutine(Reservation(3));
                T_Target.transform.position = GO_Positions[3].transform.position;
                B_Moving = true;
            }

            //Atacking
            if (Input.GetKey("q") && !B_Moving)  // Dieses GetTarget Umschreiben auf feld
            {
                if (Triggers[4].getTarget() != null)
                    if (Triggers[4].getTarget() == "Enemy")
                    {
                        StartCoroutine(Reservation(4));
                        T_Target.transform.position = GO_Positions[4].transform.position;
                        B_Moving = true;
                    }


            }
            else if (Input.GetKey("e") && !B_Moving)  // Dieses GetTarget Umschreiben auf feld
            {
                if (Triggers[5].getTarget() != null)
                    if (Triggers[5].getTarget() == "Enemy")
                    {
                        StartCoroutine(Reservation(5));
                        T_Target.transform.position = GO_Positions[5].transform.position;
                        B_Moving = true;
                    }


            }
            else if (Input.GetKey("y") && !B_Moving)  // Dieses GetTarget Umschreiben auf feld
            {
                if (Triggers[6].getTarget() != null)
                    if (Triggers[6].getTarget() == "Enemy")
                    {
                        StartCoroutine(Reservation(6));
                        T_Target.transform.position = GO_Positions[6].transform.position;
                        B_Moving = true;
                    }


            }
            else if (Input.GetKey("c") && !B_Moving)  // Dieses GetTarget Umschreiben auf feld
            {
                if (Triggers[7].getTarget() != null)
                    if (Triggers[7].getTarget() == "Enemy")
                    {
                        StartCoroutine(Reservation(7));
                        T_Target.transform.position = GO_Positions[7].transform.position;
                        B_Moving = true;
                    }


            }
        }
       

    }

    public IEnumerator Reservation(int num)
    {
        B_Reserving = true;
        SC_Reserve[num].SetToTrigger();
        yield return new WaitForSeconds(F_RESERVERATIONSPEED);
        SC_Reserve[num].SetOffTriger();
        B_Reserving = false;
    }

 

    // Update is called once per frame
    void Update ()
    {
   

        if (!B_Reserving)
        {
            float F_Step = F_Speed * Time.deltaTime;

            if (B_Moving && transform.position.x == T_Target.transform.position.x && transform.position.z == T_Target.transform.position.z)
            {


                for (int c = 0; c < 8; c++)
                {
                    SC_Reserve[c].SetOffTriger();
                }


                //EnableCollider();

                B_Moving = false;

            }
            else if (B_Moving && transform.position != T_Target.transform.position)
            {
              //  DisableCollider();
                transform.position = Vector3.MoveTowards(transform.position, T_Target.transform.position, F_Step);
            }

        }





    }


  
}
