using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MenuFrameBounce
{
    public float StartPosition;
    public float Enposition;
    RectTransform body;

    [Header("Button Physics")]
    Vector3 DesiredScale;
    public float shrinkspeed;
    public float Growsize;
    public float AlterValue;

    private void OnEnable()
    {   
        currentValue = StartPosition;
        targetValue = Enposition;     
        body.localPosition = new Vector3(body.localPosition.x, currentValue, body.localPosition.z);
    }

    private void Awake()
    {
        body = GetComponent<RectTransform>();
        DesiredScale = body.localScale;
    }

    private void OnBecameVisible()
    {
        body = GetComponent<RectTransform>();
        currentValue = StartPosition;
        targetValue = Enposition;
        DesiredScale = body.localScale;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        body.localPosition = new Vector3(body.localPosition.x, currentValue, body.localPosition.z);
        SpringMechanic();
        body.localScale = Vector3.Lerp(body.localScale, DesiredScale, shrinkspeed * Time.unscaledDeltaTime);
    }

    private void FixedUpdate()
    {
        
    }

    public void onHover()
    {
        body.localScale = DesiredScale * Growsize;
        //currentValue = targetValue + AlterValue;
    }
}
