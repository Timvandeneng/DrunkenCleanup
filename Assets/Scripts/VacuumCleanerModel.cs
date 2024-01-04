using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumCleanerModel : MonoBehaviour
{

    [Header("Transforms")]
    public Transform Bottom;
    public Transform Hose, Grabpoint;
    public Transform Ground, IdealHosePoint;
    public Transform Model;
    public Transform IdealModelPos;

    [Header("Physics")]
    public float minDistanceModel;
    public float modelSpeed;
    public bool IsVacuum;
    public Grabbable_Object grabobj;

    [Header("AnimationPhysics")]
    public float shrinkspeed;
    public float growsize;
    Vector3 originalsize;

    // Start is called before the first frame update
    void Start()
    {
        Model.position = new Vector3(IdealModelPos.position.x, Ground.position.y, IdealModelPos.position.z);
        originalsize = Model.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Bottom.hasChanged || Grabpoint.hasChanged)
        {
            ScaleBetweenPoints();
        }
     
        SetBottomtoGround();

        if (IsVacuum)
        {
            SetModelToIdealPos();
        }

        Model.localScale = Vector3.Lerp(Model.localScale, originalsize, shrinkspeed * Time.deltaTime);
    }

    public void ScaleBetweenPoints()
    {
        //first see the distance between the grabpoint and the bottom part
        //this is usefull to indicate how far we want the hose to stretch
        float distance = Vector3.Distance(Bottom.localPosition, Grabpoint.localPosition);
        Hose.localScale = new Vector3(Hose.localScale.x, Hose.localScale.y, distance);

        //Hose has to lookat bottom part
        Hose.localPosition = Grabpoint.localPosition;
        Hose.LookAt(Bottom.position);

    }

    public void SetBottomtoGround()
    {
        //setting the bottom part to the ground making it suck to the ground
        Vector3 idealpos = new Vector3(IdealHosePoint.position.x, Ground.position.y + 0.5f, IdealHosePoint.position.z);
        Bottom.position = idealpos;

        //rotating the bottom to the correct position
        Vector3 desiredLook = new Vector3(Grabpoint.position.x, Bottom.position.y, Grabpoint.position.z);
        Bottom.LookAt(desiredLook);

    }

    public void SetModelToIdealPos()
    {
        //rotating the model
        Vector3 desiredLook = new Vector3(Grabpoint.position.x, Model.position.y, Grabpoint.position.z);
        Model.LookAt(desiredLook);

        if(Vector3.Distance(Model.position, Grabpoint.position) > minDistanceModel)
        {
            float multiplier = Vector3.Distance(Model.position, Grabpoint.position) * 0.5f;
            //Vector3 DesiredPos = new Vector3(IdealModelPos.position.x, Ground.position.y, IdealModelPos.position.z);
            //Vector3 Force = DesiredPos - Model.position;
            //Model.GetComponent<Rigidbody>().AddForce(Force);
            Model.position = Vector3.Lerp(Model.position, new Vector3(IdealModelPos.position.x, Ground.position.y, IdealModelPos.position.z), modelSpeed * multiplier);
            Debug.Log("GO CLEANER GO!");
        }
    }
}
