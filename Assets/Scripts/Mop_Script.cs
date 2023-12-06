using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop_Script : MonoBehaviour
{
    GameObject puddle;
    Trash_Manager trash;

    Barf_Puddle Barf;
    // Start is called before the first frame update
    void Start()
    {
        trash = GameObject.FindFirstObjectByType<Trash_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puddle"))
        {
            puddle = other.gameObject;
            trash.currentWaterAmount--;
            Destroy(puddle);
        }

        if (other.CompareTag("BarfPool"))
        {
            Barf = other.GetComponent<Barf_Puddle>();
            if(Barf != null)
            {
                Barf.BarfHealth -= 0.35f;
            }
            Debug.Log("BarpoolCleanup");
        }
    }
}
