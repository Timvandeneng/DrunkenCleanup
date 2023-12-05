using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction_Script : MonoBehaviour
{
    //Managers
    Barf_Manager barfMngr;

    // Start is called before the first frame update
    void Start()
    {
        barfMngr = GameObject.FindFirstObjectByType<Barf_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //TODO
        //FIX LOOKAT BUG! FOUND IN BARF_MANAGER SCRIPT

        if (other.CompareTag("Toilet"))
        {
          barfMngr.StandingOnToilet = true;
          //setting the other to the gamobject
          barfMngr.Toilet = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Toilet"))
        {
          barfMngr.StandingOnToilet = false;  
        }
    }
}
