using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject normal;
    public GameObject broken;
    BoxCollider boxCollider;
    public bool canBreak;

    public float maxFallVel = 0;

    private Rigidbody rb;

    void Start()
    {
        normal.active = true;
        broken.active = false;

        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Update()
    {
        if(rb.velocity.magnitude > maxFallVel)
        {
            canBreak = true;
        }
        else
        {
            canBreak = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.gameObject.name + " collided");
        Break();
    }

    public void Break()
    {
        if (canBreak)
        {
            normal.active = false;
            broken.active = true;
            boxCollider.enabled = false;
        }
    }
}
