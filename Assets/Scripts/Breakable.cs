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
        normal.SetActive(true);
        broken.SetActive(false);

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
        Break();
    }

    public void Break()
    {
        if (canBreak)
        {
            normal.SetActive(false);
            broken.SetActive(true);
            boxCollider.enabled = false;
        }
    }
}
