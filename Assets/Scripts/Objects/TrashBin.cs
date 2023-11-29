using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    Trash_Manager trash;

    private void Start()
    {
        trash = GameObject.FindFirstObjectByType<Trash_Manager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            trash.currentBigTrashAmount--;
            Destroy(other.gameObject);
        }
    }
}
