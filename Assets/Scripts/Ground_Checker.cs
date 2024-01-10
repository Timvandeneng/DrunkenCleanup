using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Checker : MonoBehaviour
{
    public Barf_Manager BarfMngr;


    //checking which ground we are standing on so we can set the instantiate position to the ground
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            BarfMngr.Ground = collision.gameObject.transform;
            Debug.Log("We just checked the ground boy");
        }
    }
}
