using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Sliding_Door : MonoBehaviour
{
    public Transform Model;
    Vector3 startpos;
    public Vector3 desirepos;
    public float speed;

    public bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        //do something
        startpos = Model.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Model.localPosition = Vector3.Lerp(Model.localPosition, desirepos, speed * Time.deltaTime);
        }
        else
        {
            Model.localPosition = Vector3.Lerp(Model.localPosition, startpos, speed * Time.deltaTime);
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
