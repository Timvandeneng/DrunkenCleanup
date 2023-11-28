using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable_Object : MonoBehaviour
{

    Grabbable_Object_Manager mngr;
    Rigidbody rb;
    Grab[] grab;

    bool cangetsuckedleft;
    bool cangetsuckedright;

    Vector3 velocity = Vector3.zero;

    public bool DebugChecker;

    // Start is called before the first frame update
    void Start()
    {
        mngr = GameObject.FindFirstObjectByType<Grabbable_Object_Manager>();
        rb = GetComponent<Rigidbody>();
        grab = FindObjectsOfType<Grab>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceLeft = Vector3.Distance(transform.position, mngr.lefthand.position);
        float distanceRight = Vector3.Distance(transform.position, mngr.rigthhand.position);

        if (DebugChecker)
        {
            //Debug.Log(cangetsuckedleft);
            //Debug.Log(cangetsuckedright);
        }

        //checking if we haven't already grabbed the object
        //ITS FUCKING BROKEN MATE!
        if (!grab[1].hasgrabbed && grab[1].leftGrab)
        {
            cangetsuckedleft = true;
        }
        else if (grab[1].hasgrabbed || !grab[1].rightGrab)
        {
            cangetsuckedleft = false;
        }

        if (!grab[0].hasgrabbed && grab[0].rightGrab)
        {
            cangetsuckedright = true;
        }
        else if(grab[0].hasgrabbed || !grab[0].rightGrab)
        {
            cangetsuckedright = false;
        }

        if (distanceLeft < mngr.ActivationDistance || distanceRight < mngr.ActivationDistance)
        {
            if (distanceLeft < distanceRight && cangetsuckedleft)
            {
                Vector3 desiredpos = mngr.lefthand.position;
                float desiredForce = mngr.AttractionForce;
                rb.MovePosition(Vector3.SmoothDamp(transform.position, desiredpos, ref velocity, desiredForce));
            }
            else if(distanceRight < distanceLeft && cangetsuckedright)
            {
                Vector3 desiredpos = mngr.rigthhand.position;
                float desiredForce = mngr.AttractionForce;
                rb.MovePosition(Vector3.SmoothDamp(transform.position, desiredpos, ref velocity, desiredForce));
            }
        }
        if(distanceLeft > mngr.ActivationDistance)
        {
            cangetsuckedleft = false;
        }
        if (distanceRight > mngr.ActivationDistance)
        {
            cangetsuckedright = false;
        }
    }

    public void ResetLAyer()
    {
        this.gameObject.layer = 0;
    }
}
