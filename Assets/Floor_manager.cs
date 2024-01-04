using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_manager : MonoBehaviour
{

    public GameObject topfloor, Fogvol, trash, parentsdown;
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
            trash.SetActive(true);
            Fogvol.SetActive(true);
            parentsdown.SetActive(false);
        }
        else
        {
            topfloor.SetActive(false);
            trash.SetActive(false);
            Fogvol.SetActive(false);
            parentsdown.SetActive(true);
        }
    }
}
