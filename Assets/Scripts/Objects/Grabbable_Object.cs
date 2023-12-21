using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable_Object : MonoBehaviour
{
    public Transform[] Lefthand;
    public Transform[] Righthand;

    public Grab[] LeftScripts;
    public Grab[] RightScripts;

    // Start is called before the first frame update
    void Start()
    {
        //first we set the size of the array to the ammount of hands there are in the scene
        //this is handy for multiplayer later on
        GameObject[] Lobj = GameObject.FindGameObjectsWithTag("Lefthand");
        System.Array.Resize(ref Lefthand, Lobj.Length);
        for (int i = 0; i < Lobj.Length; i++)
        {
            Lefthand[i] = Lobj[i].transform;
        }
        //for(int i = 0; i < Lefthand.Length; i++)
       // {
         //   LeftScripts[i] = Lefthand[i].gameObject.GetComponent<Grab>();
     //   }
        //right hand
        GameObject[] Robj = GameObject.FindGameObjectsWithTag("Righthand");
        System.Array.Resize(ref Righthand, Robj.Length);
        for (int i = 0; i < Robj.Length; i++)
        {
            Righthand[i] = Robj[i].transform;
        }
        for(int i = 0; i < Righthand.Length; i++)
        {
            RightScripts[i] = Righthand[i].gameObject.GetComponent<Grab>();
        }
    }

    // Update is called once per frame
    void Update()
    {
      for(int i = 0; i < Lefthand.Length; i++)
      {
          if(Vector3.Distance(transform.position, Lefthand[i].position) < LeftScripts[i].ActivationDistance)
          {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    transform.position = Lefthand[i].position;
                }
          }
      }
    }

    public void ResetLAyer()
    {
        this.gameObject.layer = 0;
    }
}
