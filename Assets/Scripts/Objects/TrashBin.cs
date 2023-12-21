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
    private void Start()
    {
        trash = GameObject.FindFirstObjectByType<Trash_Manager>();
        normalscale = bin.localScale;
    }

    private void Update()
    {
        bin.localScale = Vector3.Lerp(bin.localScale, normalscale, ShrinkSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            trash.currentBigTrashAmount--;
            bin.localScale = bin.localScale * growSize;
            Instantiate(trashEffect, trashpos.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
