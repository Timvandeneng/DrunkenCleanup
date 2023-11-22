using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    public Animator anim;
    GameObject grabbedObj;
    Grabbable_Object objscript;
    Rigidbody rb;
    public bool hasgrabbed = false;
    public bool lefthand;
    FixedJoint fj;
    public bool cangrab;

    public bool leftGrab;
    public bool rightGrab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //checking if we want to grab
        leftGrab = Input.GetKey(KeyCode.Mouse0);
        rightGrab = Input.GetKey(KeyCode.Mouse1);

        //checking if we are allowed to grab
        //is disabled for example in animations
        if (cangrab)
        {
            //setting animations
            anim.SetBool("left", leftGrab);
            anim.SetBool("right", rightGrab);

            //chceking if we are in range for object
            if (grabbedObj != null)
            {
                //grabbing
                if (!hasgrabbed)
                {
                    //checking which hand is enabled
                    if (lefthand && leftGrab && !hasgrabbed)
                    {
                        AddFixjointMakeParent();
                    }
                    if (!lefthand && rightGrab && !hasgrabbed)
                    {
                        AddFixjointMakeParent();
                    }
                }
            }

            //checking which hand we are attached to
            if (lefthand)
            {
                if (!leftGrab && hasgrabbed)
                {
                    DestroyFixedJoint();
                }
            }
            else
            {
                if (!rightGrab && hasgrabbed)
                {
                    DestroyFixedJoint();
                }
            }

        }
    }

    void AddFixjointMakeParent()
    {
        fj = grabbedObj.AddComponent<FixedJoint>();
        objscript = grabbedObj.GetComponent<Grabbable_Object>();
        fj.connectedBody = rb;
        fj.anchor = transform.position;
        fj.breakForce = 100f;
        grabbedObj.layer = 6; //layer 6 is no self intersection
        hasgrabbed = true;
    }

    void DestroyFixedJoint()
    {
        objscript.ResetLAyer();
        Destroy(fj);
        hasgrabbed = false;
        grabbedObj = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            grabbedObj = other.gameObject;
            Debug.Log(grabbedObj);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            grabbedObj = null;
        }
    }
}
