using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_position : MonoBehaviour
{
    public Transform desiredposition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = desiredposition.position;
        transform.rotation = Quaternion.Euler(transform.rotation.x, desiredposition.rotation.eulerAngles.y, transform.rotation.z);
    }
}
