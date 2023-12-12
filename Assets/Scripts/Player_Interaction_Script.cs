using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction_Script : MonoBehaviour
{
    //Managers
    Barf_Manager barfMngr;
    public float raylength;

    // Start is called before the first frame update
    void Start()
    {
        barfMngr = GameObject.FindFirstObjectByType<Barf_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.right, out hit, raylength))
        {
            if (hit.collider.CompareTag("Toilet"))
            {
                barfMngr.StandingOnToilet = true;
            }
            else
            {
                barfMngr.StandingOnToilet = false;
            }
        }
    }
}
