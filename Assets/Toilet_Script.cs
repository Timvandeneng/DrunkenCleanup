using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet_Script : MonoBehaviour
{
    public GameObject CleanToilet, BarfToilet;
    public bool Dirty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dirty)
        {
            CleanToilet.SetActive(false);
            BarfToilet.SetActive(true);
        }
        else
        {
            CleanToilet.SetActive(true);
            BarfToilet.SetActive(false);
        }
    }
}
