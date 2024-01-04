using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof_Shard : MonoBehaviour
{

    Rigidbody rbody;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rbody.isKinematic = false;
    }
}
