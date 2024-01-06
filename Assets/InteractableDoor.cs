using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    public bool left, right;

    public float Rotation;
    public float RotationSpeed;

    public Transform Door;

    Quaternion restrot;

    public bool canclose = true;

    public GameObject DoorOpening;
    public GameObject DoorClosing;

    bool alreadyclosed = true;
    bool alreadyopenend = false;

    //Start is called before the first frame update
    void Start()
    {
        restrot = Door.localRotation;
        canclose = true;

        alreadyclosed = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (left && !right)
        {
            if (!alreadyopenend)
            {
                Instantiate(DoorOpening, transform.position, Quaternion.identity);
                alreadyopenend = true;
            }

            alreadyclosed = false;
            Quaternion desiredRot = Quaternion.Euler(restrot.eulerAngles.x, restrot.eulerAngles.y - Rotation, restrot.eulerAngles.z);
            Door.localRotation = Quaternion.Lerp(Door.localRotation, desiredRot, RotationSpeed * Time.deltaTime);
        }

        if (right && !left)
        {
            if (!alreadyopenend)
            {
                Instantiate(DoorOpening, transform.position, Quaternion.identity);
                alreadyopenend = true;
            }

            alreadyclosed = false;
            Quaternion desiredRot = Quaternion.Euler(restrot.eulerAngles.x, restrot.eulerAngles.y + Rotation, restrot.eulerAngles.z);
            Door.localRotation = Quaternion.Lerp(Door.localRotation, desiredRot, RotationSpeed * Time.deltaTime);
        }
        
        if(!right && !left && canclose)
        {
            alreadyopenend = false;
            //Make sure we instantiate the sound ONLY ONCE
            if (!alreadyclosed)
            {
                Instantiate(DoorClosing, transform.position, Quaternion.identity);
                alreadyclosed = true;
            }

            Quaternion desiredRot = Quaternion.Euler(restrot.eulerAngles.x, restrot.eulerAngles.y, restrot.eulerAngles.z);
            Door.localRotation = Quaternion.Lerp(Door.localRotation, desiredRot, RotationSpeed * Time.deltaTime);      
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canclose = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canclose = true;
        }
    }
}
