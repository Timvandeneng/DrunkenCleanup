using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_manager : MonoBehaviour
{

    public GameObject topfloor, Fogvol;
    public bool up;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            topfloor.SetActive(true);
            Fogvol.SetActive(true);
        }
        else
        {
            topfloor.SetActive(false);
            Fogvol.SetActive(false);
        }
    }
}