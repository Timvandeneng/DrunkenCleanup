using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{

    Rigidbody rbody;
    public float initBounce;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rbody.velocity = new Vector3(0, initBounce, 0);
        initBounce = initBounce / 1.3f;
    }
}
