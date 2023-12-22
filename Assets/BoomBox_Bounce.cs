using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox_Bounce : MonoBehaviour
{
    public float timer;
    float resettimer;

    public float bouncespeed;
    public float rotation;

    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        resettimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            GetComponent<Rigidbody>().velocity = transform.up * bouncespeed;
            rotation = -rotation;
            timer = resettimer;
        }

        transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.forward * rotation), rotSpeed * Time.deltaTime);
    }
}
