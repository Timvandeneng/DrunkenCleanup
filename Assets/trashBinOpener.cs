using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashBinOpener : MonoBehaviour
{
    public Transform lid;
    public float RotationSpeed;
    public float rotation;

    public bool selected;

    Quaternion restrot;

    bool open;

    public GameObject SelectGlow;

    // Start is called before the first frame update
    void Start()
    {
        restrot = lid.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (open)
        {
            Quaternion desiredRot = Quaternion.Euler(restrot.eulerAngles.x + rotation, restrot.eulerAngles.y, restrot.eulerAngles.z);
            lid.localRotation = Quaternion.Lerp(lid.localRotation, desiredRot, RotationSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion desiredRot = Quaternion.Euler(restrot.eulerAngles.x, restrot.eulerAngles.y, restrot.eulerAngles.z);
            lid.localRotation = Quaternion.Lerp(lid.localRotation, desiredRot, RotationSpeed * Time.deltaTime);
        }

        if (selected)
        {
            SelectGlow.SetActive(true);
        }
        else
        {
            SelectGlow.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            open = false;
        }
    }
}
