using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barf_Manager : MonoBehaviour
{
    float idletimer;
    float resetidle;
    bool Randomize = false;

    [Header("Barf physics")]
    public float minTime;
    public float maxTime;
    public float randomBarfPosTime;
    float resetbarfpos;

    [Header("Gameobjects")]
    public Transform Barfmeter;

    [Header("Spring mech")]
    float currentValue;
    public float currentVelocity;
    float targetValue;
    public float stiffness = 1f; // value highly dependent on use case
    public float damping = 0.1f; // 0 is no damping, 1 is a lot, I think
    public float valueThreshold = 0.01f;
    public float velocityThreshold = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        resetbarfpos = randomBarfPosTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Randomize)
        {
            RandomizeTimer();
            Randomize = true;
        }

        idletimer -= Time.deltaTime;

        if(idletimer < 0)
        {
            //barf here
        }

        randomBarfPosTime -= Time.deltaTime;
        if(randomBarfPosTime < 0)
        {
            targetValue = Random.Range(0f, 90f);
            targetValue = targetValue / 90;
            randomBarfPosTime = resetbarfpos;
        }

        Barfmeter.localScale = new Vector3(Barfmeter.localScale.x, currentValue, Barfmeter.localScale.z);
    }

    private void FixedUpdate()
    {
        float dampingFactor = Mathf.Max(0, 1 - damping * Time.fixedDeltaTime);
        float acceleration = (targetValue - currentValue) * stiffness * Time.fixedDeltaTime;
        currentVelocity = currentVelocity * dampingFactor + acceleration;
        currentValue += currentVelocity * Time.fixedDeltaTime;

        if (Mathf.Abs(currentValue - targetValue) < valueThreshold && Mathf.Abs(currentVelocity) < velocityThreshold)
        {
            currentValue = targetValue;
            currentVelocity = 0f;
        }
    }

    void RandomizeTimer()
    {
        idletimer = Random.Range(minTime, maxTime);
        resetidle = idletimer;
    }
}
