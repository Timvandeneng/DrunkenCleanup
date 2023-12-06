using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MenuFrameBounce
{

    public float RestPosY;
    public float ActivatePosY;
    // Start is called before the first frame update
    void Start()
    {
        targetValue = RestPosY;
        currentValue = RestPosY;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(transform.position.x, currentValue, transform.position.z);
        transform.position = targetPos;
    }


    public void Activate()
    {
        targetValue = ActivatePosY;
    }

    public void Deactivate()
    {
        targetValue = RestPosY;
    }

}
