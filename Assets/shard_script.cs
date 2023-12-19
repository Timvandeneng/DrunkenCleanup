using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shard_script : MonoBehaviour
{
    Trash_Manager trash;
    public float ShrinkSpeed;
    bool remove;

    // Start is called before the first frame update
    void Start()
    {
        trash = GameObject.FindFirstObjectByType<Trash_Manager>();
        trash.CurrentShardsAmmount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (remove)
        {
            if(transform.localScale.x > 0.01f)
            {
                transform.localScale -= new Vector3(ShrinkSpeed * Time.deltaTime, ShrinkSpeed * Time.deltaTime, ShrinkSpeed * Time.deltaTime);
            }
            else
            {
                trash.CurrentShardsAmmount--;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Broom"))
        {
            remove = true;
        }
    }
}
