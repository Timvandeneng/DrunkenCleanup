using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Store_place : MonoBehaviour
{

    Trash_Manager manager;
    public GameObject Roof;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindFirstObjectByType<Trash_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ragdoll"))
        {
            manager.currentHumanAmmount -= manager.HumanValue;
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Roof.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ragdoll"))
        {
            manager.currentHumanAmmount += manager.HumanValue;
        }

        if (other.CompareTag("Player"))
        {
            Roof.SetActive(true);
        }
    }
}
