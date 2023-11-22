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

        //TODO: MAKE IT SO THAT WE CHECK IF WE AREN'T ALREADY GRABBING ONTO SOMETHING
        //ALSO MAKE IT SO THAT IT ONLY GETS PULLED IN WHEN WE PRESSED A BUTTON
        if (!grab[0].hasgrabbed && grab[0].leftGrab)
        {
            cangetsuckedleft = true;
        }
        else if (grab[0].hasgrabbed || !grab[0].rightGrab)
        {
            cangetsuckedleft = false;
        }

        if (!grab[1].hasgrabbed && grab[1].rightGrab)
        {
            cangetsuckedright = true;
        }
        else if(grab[1].hasgrabbed || !grab[1].rightGrab)
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
    }

    public void ResetLAyer()
    {
        this.gameObject.layer = 0;
    }
}
