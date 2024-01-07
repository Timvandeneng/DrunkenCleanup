using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof_Shard : MonoBehaviour
{

    Rigidbody rbody;
    bool broken;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rbody.isKinematic = false;
        if (!broken)
        {
            float randomyvel = Random.Range(-10, -1);
            rbody.velocity = new Vector3(rbody.velocity.x, randomyvel, rbody.velocity.z);
            broken = true;
        }
 
    }
}
