using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    Trash_Manager trash;
    public Transform trashpos;
    public Transform bin;
    public GameObject trashEffect;
    public float ShrinkSpeed;
    public float growSize;
    Vector3 normalscale;
    Arrow_Script arrow;

    //this is the effect of the U.I.
    public GameObject UIEff;
    Score_Adder_Handler UiHandler;

    private void Start()
    {
        trash = GameObject.FindFirstObjectByType<Trash_Manager>();
        normalscale = bin.localScale;
        arrow = FindFirstObjectByType<Arrow_Script>();
    }

    private void Update()
    {
        bin.localScale = Vector3.Lerp(bin.localScale, normalscale, ShrinkSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            trash.currentBigTrashAmount -= trash.Bigtrashvalue;
            bin.localScale = bin.localScale * growSize;
            Instantiate(trashEffect, trashpos.position, Quaternion.identity);
            UiHandler = Instantiate(UIEff, transform.position, Quaternion.identity).GetComponent<Score_Adder_Handler>();
            UiHandler.Ammount = trash.Bigtrashvalue;
            arrow.WhichTarget = 0;
            Destroy(other.gameObject);
        }
    }
}
