using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_TICKTOCK : MonoBehaviour {
    public float TickSpeed;
    public bool PlayerSide = true;
    public Renderer M_Mat;
    // Use this for initialization
    void Start ()
    {
        M_Mat = this.GetComponent<Renderer>();
    }

    private void Awake()
    {
        StartCoroutine(TickTock());
    }
    // Update is called once per frame
    void Update () {
		if(PlayerSide)
            M_Mat.material.color = Color.black;
        else
            M_Mat.material.color = Color.white;

    }


    IEnumerator TickTock()
    {
        while(true)
        {
            yield return new WaitForSeconds(TickSpeed);
            PlayerSide = true;
            yield return new WaitForSeconds(TickSpeed);
            PlayerSide = false;
        }
     
    }

    public bool getPlayerSide()
    {
        return PlayerSide;
    }
}
