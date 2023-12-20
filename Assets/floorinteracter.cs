using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorinteracter : MonoBehaviour
{
    Floor_manager mngr;
    public bool down;

    // Start is called before the first frame update
    void Start()
    {
        mngr = GameObject.FindFirstObjectByType<Floor_manager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (down)
            {
                mngr.up = false;
            }
            else
            {
                mngr.up = true;
            }
        }
    }
}
