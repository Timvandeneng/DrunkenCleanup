using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable_Object : MonoBehaviour
{
    public Transform[] Lefthand;
    public Transform[] Righthand;

    bool leftGrab;
    bool rightGrab;

    int hand;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

        //first we set the size of the array to the ammount of hands there are in the scene
        //this is handy for multiplayer later on
        GameObject[] Lobj = GameObject.FindGameObjectsWithTag("Lefthand");
        System.Array.Resize(ref Lefthand, Lobj.Length);
        for (int i = 0; i < Lobj.Length; i++)
        {
            Lefthand[i] = Lobj[i].transform;
        }
        //right hand
        GameObject[] Robj = GameObject.FindGameObjectsWithTag("Righthand");
        System.Array.Resize(ref Righthand, Robj.Length);
        for (int i = 0; i < Robj.Length; i++)
        {
            Righthand[i] = Robj[i].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        leftcheck();
        rightCheck();
        Checktostick();
    }

    void leftcheck()
    {
        //first we cycle through all possible hands that are present in the scene
        //then we check if one of the hands are in reach
        for (int i = 0; i < Lefthand.Length; i++)
        {
            if (Vector3.Distance(transform.position, Lefthand[i].position) < Lefthand[i].gameObject.GetComponent<Grab>().ActivationDistance)
            {
                Debug.Log("can be grabbed");
                if (Lefthand[i].gameObject.GetComponent<Grab>().Pressing && !Lefthand[i].gameObject.GetComponent<Grab>().Isgrabbing)
                {
                    Debug.Log("Should be grabbed");
                    leftGrab = true;
                    hand = i;
                    Lefthand[i].gameObject.GetComponent<Grab>().Isgrabbing = true;
                }
            }

            //making sure the loop is infinite
            if (i == Lefthand.Length)
            {
                i = 0;
            }
        }
    }

    void rightCheck()
    {
        //first we cycle through all possible hands that are present in the scene
        //then we check if one of the hands are in reach
        for (int i = 0; i < Righthand.Length; i++)
        {
            if (Vector3.Distance(transform.position, Righthand[i].position) < Righthand[i].gameObject.GetComponent<Grab>().ActivationDistance)
            {
                Debug.Log("can be grabbed");
                if (Righthand[i].gameObject.GetComponent<Grab>().Pressing && !Righthand[i].gameObject.GetComponent<Grab>().Isgrabbing)
                {
                    Debug.Log("Should be grabbed");
                    rightGrab = true;
                    hand = i;
                    Righthand[i].gameObject.GetComponent<Grab>().Isgrabbing = true;
                }
            }

            //making sure the loop is infinite
            if (i == Righthand.Length)
            {
                i = 0;
            }
        }
    }

    void Checktostick()
    {
        //checking if we want to stick to the hand. also resetting if we let go
        if (leftGrab)
            StickToLeftHand(hand);

        if (!Lefthand[hand].gameObject.GetComponent<Grab>().Pressing)
        {
            leftGrab = false;
            rb.isKinematic = false;
            ResetLayer();
            Lefthand[hand].gameObject.GetComponent<Grab>().Isgrabbing = false;
        }

        if (rightGrab)
            StickToRightHand(hand);

        if (!Righthand[hand].gameObject.GetComponent<Grab>().Pressing)
        {
            rightGrab = false;
            rb.isKinematic = false;
            ResetLayer();
            Righthand[hand].gameObject.GetComponent<Grab>().Isgrabbing = false;
        }
    }

    public void StickToLeftHand(int index)
    {
        rb.position = Lefthand[index].position;
        rb.isKinematic = true;
        rb.rotation = Lefthand[index].rotation;
        this.gameObject.layer = 6;
    }

    public void StickToRightHand(int index)
    {
        rb.position = Righthand[index].position;
        rb.isKinematic = true;
        rb.rotation = Righthand[index].rotation;
        this.gameObject.layer = 6;
    }

    public void ResetLayer()
    {
        this.gameObject.layer = 0;
    }
}
