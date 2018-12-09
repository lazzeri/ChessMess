using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TowerLazer : MonoBehaviour {
    public RaycastHit R_Hit;
    int layer_mask;
    // Use this for initialization
    void Start ()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out R_Hit,layer_mask,layer_mask))
            {
                Debug.DrawLine(this.transform.position, R_Hit.point);
               print( R_Hit.collider.transform.name);

            }
        }
    }
}
