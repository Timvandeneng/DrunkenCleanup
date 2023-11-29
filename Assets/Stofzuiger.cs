using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stofzuiger : MonoBehaviour
{
    public Grabbable_Object grabbable;

    public float normalScale;

    public Transform Hose, ground, lefthand, righthand;

    Quaternion normalrot;

    // Start is called before the first frame update
    void Start()
    {
        normalScale = Hose.localScale.y;
        normalrot = Hose.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //TO DO
        //Make it so that our cleaner is always on the ground
        //you could approach this problem by stretching the hose to a certain extent
        //making the bottom part follow the hose


        if (grabbable.Isbeingrabbedleft || grabbable.isbeingrabbedright)
        {
      //      scaleBetweenPoints();
        }

        Hose.transform.rotation = normalrot;
    }

    void scaleBetweenPoints()
    {
        float distance = grabbable.isbeingrabbedright ? Vector3.Distance(transform.position, righthand.position) : Vector3.Distance(transform.position, lefthand.position);
        Hose.localScale = new Vector3(Hose.localScale.x, distance, Hose.localScale.z);

        Vector3 middlepoint = (grabbable.Isbeingrabbedleft ? righthand.position : lefthand.position + transform.position) / 2;
        Hose.position = middlepoint;

        Vector3 rotationDirection = grabbable.Isbeingrabbedleft ? righthand.position : lefthand.position - transform.position;
        Hose.up = rotationDirection;

        Vector3 disectPosition = transform.InverseTransformPoint(ground.transform.position);
        Debug.Log(disectPosition);
        if (disectPosition.y < .5f || disectPosition.y > .5f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, ground.position.y, transform.localPosition.z);
        }
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
