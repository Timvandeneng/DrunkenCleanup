using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFrameBounce : MonoBehaviour
{
    [Header("Spring mech")]
    public float currentValue;
    public float currentVelocity;
    public float targetValue;
    public float stiffness = 1f;
    public float damping = 0.1f;
    public float valueThreshold = 0.01f;
    public float velocityThreshold = 0.01f;

    //ALWAYS CALL FUNCTION IN UPDATE
    //That way everything works even when timescale is set to 0
    public void SpringMechanic()
    {
        //Spring mechanic

        float dampingFactor = Mathf.Max(0, 1 - damping * Time.unscaledDeltaTime);

        float acceleration = (targetValue - currentValue) * stiffness * Time.unscaledDeltaTime;

        currentVelocity = currentVelocity * dampingFactor + acceleration;

        currentValue += currentVelocity * Time.unscaledDeltaTime;

        if (Mathf.Abs(currentValue - targetValue) < valueThreshold && Mathf.Abs(currentVelocity) < velocityThreshold)

        {

            currentValue = targetValue;

            currentVelocity = 0f;

        }
    }
}
