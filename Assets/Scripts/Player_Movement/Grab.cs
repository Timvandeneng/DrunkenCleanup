using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator anim;

    public bool Isgrabbing;
    public float ActivationDistance;

    public bool Pressing;

    public bool righthand;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //CHANGE THIS FOR MULTIPAYER
        //FOR NOW IT WORKS
        Pressing = righthand ? Input.GetKey(KeyCode.Mouse1) : Input.GetKey(KeyCode.Mouse0);
        anim.SetBool(righthand ? "right" : "left", Pressing);
    }

}
