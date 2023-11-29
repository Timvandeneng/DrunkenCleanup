using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumCleanerModel : MonoBehaviour
{

    [Header("Transforms")]
    public Transform Bottom, Hose, Grabpoint;
    public Transform Ground, IdealHosePoint;

    public float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Bottom.hasChanged || Grabpoint.hasChanged)
        {
            ScaleBetweenPoints();
        }
     
        SetBottomtoGround();
    }

    public void ScaleBetweenPoints()
    {
        //first see the distance between the grabpoint and the bottom part
        //this is usefull to indicate how far we want the hose to stretch
        float distance = Vector3.Distance(IdealHosePoint.position, Grabpoint.position);
        Hose.localScale = new Vector3(Hose.localScale.x, Hose.localScale.y, distance);

        //middle point is used to allocate the hose to a better position
        Vector3 middlepoint = (Grabpoint.position + IdealHosePoint.position) / 2;
        Hose.position = middlepoint;

        Hose.position = Grabpoint.position;
        Hose.LookAt(Bottom.position);

    }

    public void SetBottomtoGround()
    {
        //setting the bottom part to the ground making it suck to the ground
        Vector3 idealpos = new Vector3(IdealHosePoint.position.x, Ground.position.y + 0.5f, IdealHosePoint.position.z);
        Bottom.position = idealpos;

        //rotating the bottom to the correct position
        Vector3 lookatpos = new Vector3(Bottom.position.x, Grabpoint.position.y, Bottom.position.z);
        Bottom.LookAt(Bottom.position);
    }
}
