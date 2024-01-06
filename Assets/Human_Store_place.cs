using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Store_place : MonoBehaviour
{

    Trash_Manager manager;
    public GameObject Roof;

    //this is the effect of the U.I.
    public GameObject UIEff;
    public GameObject subEff;
    Score_Adder_Handler UiHandler;

    public bool Selected;
    public GameObject Glow;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindFirstObjectByType<Trash_Manager>();
    }

    private void Update()
    {
        if (Selected)
        {
            Glow.SetActive(true);
        }
        else
        {
            Glow.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ragdoll"))
        {
            UiHandler = Instantiate(UIEff, transform.position, Quaternion.identity).GetComponent<Score_Adder_Handler>();
            UiHandler.Ammount = manager.HumanValue;
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
            UiHandler = Instantiate(subEff, transform.position, Quaternion.identity).GetComponent<Score_Adder_Handler>();
            UiHandler.Ammount = -manager.HumanValue;
            manager.currentHumanAmmount += manager.HumanValue;
        }

        if (other.CompareTag("Player"))
        {
            Roof.SetActive(true);
        }
    }
}
