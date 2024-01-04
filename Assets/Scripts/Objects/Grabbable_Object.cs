using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable_Object : MonoBehaviour
{
    public Transform[] Lefthand;
    public Transform[] Righthand;

    [HideInInspector]
    public bool leftGrab;
    [HideInInspector]
    public bool rightGrab;
    int hand;

    Arrow_Script Arrow;

    Rigidbody rb;

    public Transform us;

    Transform Holding, Topfloor, Bottomfloor;
    public bool CleaningProducts = false;
    Trash_Manager mngr;
    public float Yposition = 12;

    [Header("Big, Human")]
    public string TrashType;

    bool alreadyReset = false;

    // Start is called before the first frame update
    void Start()
    {
        mngr = GameObject.FindFirstObjectByType<Trash_Manager>();
        Holding = mngr.Idle;
        Topfloor = mngr.Topfloor;
        Bottomfloor = mngr.BottomFloor;
        rb = GetComponent<Rigidbody>();

        Arrow = FindFirstObjectByType<Arrow_Script>();

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
                if (Lefthand[i].gameObject.GetComponent<Grab>().Pressing && !Lefthand[i].gameObject.GetComponent<Grab>().Isgrabbing && !rightGrab)
                {
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
                if (Righthand[i].gameObject.GetComponent<Grab>().Pressing && !Righthand[i].gameObject.GetComponent<Grab>().Isgrabbing && !leftGrab)
                {
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

        if (!Lefthand[hand].gameObject.GetComponent<Grab>().Pressing && rightGrab == false)
        {
            leftGrab = false;
            rb.isKinematic = false;
            Lefthand[hand].gameObject.GetComponent<Grab>().Isgrabbing = false;
        }

        if (rightGrab)
            StickToRightHand(hand);

        if (!Righthand[hand].gameObject.GetComponent<Grab>().Pressing && leftGrab == false)
        {
            rightGrab = false;
            rb.isKinematic = false;
            Righthand[hand].gameObject.GetComponent<Grab>().Isgrabbing = false;
        }

        if(!Righthand[hand].gameObject.GetComponent<Grab>().Pressing && !Lefthand[hand].gameObject.GetComponent<Grab>().Pressing)
        {
            ResetLayer();
        }
    }

    public void StickToLeftHand(int index)
    {
        rb.position = Lefthand[index].position;
        rb.isKinematic = true;
        rb.rotation = Lefthand[index].rotation;
        this.gameObject.layer = 6;
        SetArrow();
        if (!CleaningProducts)
        {
            us.SetParent(Holding);
        }
    }

    public void StickToRightHand(int index)
    {
        rb.position = Righthand[index].position;
        rb.isKinematic = true;
        rb.rotation = Righthand[index].rotation;
        this.gameObject.layer = 6;
        SetArrow();
        if (!CleaningProducts)
        {
          us.SetParent(Holding);
        }
    }

    public void ResetLayer()
    {
        this.gameObject.layer = 0;
        if (!alreadyReset)
        {
            Arrow.WhichTarget = 0;
            alreadyReset = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Topfloor"))
        {
            us.SetParent(Topfloor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Topfloor"))
        {
            us.SetParent(Bottomfloor);
        }
    }

    void SetArrow()
    {
        alreadyReset = false;
        if (TrashType == "Big")
        {
            Arrow.WhichTarget = 1;
        }
        if(TrashType == "Human")
        {
            Arrow.WhichTarget = 3;
        }
    }

}
