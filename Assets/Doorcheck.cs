using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorcheck : MonoBehaviour
{
    public bool left;
    public InteractableDoor inter;

    private void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (left)
            {
                if(inter.canclose)
                   inter.left = true;
            }
            else
            {
                if (inter.canclose)
                    inter.right = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (left)
            {
                inter.left = false;
            }
            else
            {
                inter.right = false;
            }
        }
    }
}
