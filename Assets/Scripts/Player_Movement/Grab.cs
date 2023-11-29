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
    public bool cangrab;

    public bool leftGrab;
    public bool rightGrab;

    public GameObject empty;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO
        //OBJECT WON'T RESET!? I DONT KNOW HOW
        //FIX IT YOU FOKING WANKER!!!


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
                else
                {
                    //DELETE THIS!
                    keepObjectOnHand();
                }
            }

            //checking which hand we are attached to
           if (lefthand)
           {
                if (!leftGrab)
                {
                   DestroyFixedJoint();
                }
           }
            else
            {
                if (!rightGrab)
                {
                   DestroyFixedJoint();
                }
            }

        }
    }

    void AddFixjointMakeParent()
    {
        //Debug.Log("Grab!");
        grabbedObj.layer = 6; //layer 6 is no self intersection
        objscript = grabbedObj.GetComponent<Grabbable_Object>();
        hasgrabbed = true;
    }

    void DestroyFixedJoint()
    {
        if(objscript != null)
        {
            objscript.ResetLAyer();
        }
        grabbedObj = null;
        objscript = null;
        hasgrabbed = false;
    }

    void keepObjectOnHand()
    {
        grabbedObj.transform.position = transform.position;
        grabbedObj.transform.rotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            grabbedObj = other.gameObject;
        }
    }
}
