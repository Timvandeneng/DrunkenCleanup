using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds;
    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, seconds);  
    }
}
