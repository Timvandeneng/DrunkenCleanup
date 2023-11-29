using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject Bord;
    public GameObject KapotBord;
    public BoxCollider boxCollider;

    public float maxFallVel = 0;

    private Rigidbody rb;

    void Start()
    {
        Bord.active = true;
        KapotBord.active = false;

        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(rb.velocity.magnitude >= maxFallVel)
        {
            Bord.active = false;
            Destroy(Bord);
            KapotBord.active = true;
        }
    }
}
