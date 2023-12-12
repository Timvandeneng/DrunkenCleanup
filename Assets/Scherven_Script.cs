using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scherven_Script : MonoBehaviour
{

    Trash_Manager trashmngr;
    bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        trashmngr = GameObject.FindFirstObjectByType<Trash_Manager>();
        trashmngr.CurrentShardsAmmount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            if(transform.localScale.x < 0)
            {
                Destroy(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Broom"))
        {
            destroy = true;
            Debug.Log("Destroy Shard");
        }
    }
}
