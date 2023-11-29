using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject Heel;
    public GameObject Kapot;
    public BoxCollider boxCollider;

    public float maxFallVel = 0;

    private Rigidbody rb;

    void Start()
    {
        Heel.active = true;
        Kapot.active = false;

        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(rb.velocity.magnitude >= maxFallVel)
        {
            Heel.active = false;
            Kapot.active = true;
        }
    }
}
