using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Barf_Manager : MonoBehaviour
{
    float idletimer;
    float resetidle;
    float normalbarf = 0;
    bool Randomize = false;
    bool isBarfing = false;
    [HideInInspector]
    public bool StandingOnToilet = false;

    [Header("Barf physics")]
    public float minTime;
    public float maxTime;
    public float randomBarfPosTime;
    public float BarfRiser;
    float resetbarfpos;

    [Header("Gameobjects")]
    public Transform Barfmeter;
    public ParticleSystem Barfing;
    public Transform BarfLocation;
    public GameObject BarfPool;
    public Animator CharacterAnim;
    public Transform Staartje;

    [Header("Special Effects")]
    public Volume volume;
    private ChromaticAberration chrom;
    private PaniniProjection panproj;

    [Header("Spring mech")]
    public float currentValue;
    public float currentVelocity;
    float targetValue;
    public float stiffness = 1f;
    public float damping = 0.1f; 
    public float valueThreshold = 0.01f;
    public float velocityThreshold = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        resetbarfpos = randomBarfPosTime;
        normalbarf = 0;
        if (volume.profile.TryGet<ChromaticAberration>(out chrom))
        {
            Debug.Log("we have chromatic abberation");
        }
        if (volume.profile.TryGet<PaniniProjection>(out panproj))
        {
            Debug.Log("we have pannniniiii");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //randomizing the idle animation timer
        if (!Randomize)
        {
            RandomizeTimer();
            Randomize = true;
        }

        //activating barfmode when the idle timer < 0
        if (idletimer < 0)
        {
            isBarfing = true;
            idletimer = resetidle;
        }

        randomBarfPosTime -= Time.deltaTime;

        if (randomBarfPosTime < 0 && !isBarfing)
        {
            targetValue = Random.Range(0f, 90f);
            CharacterAnim.SetBool("Barf", false);
            targetValue = targetValue / 90;
            randomBarfPosTime = resetbarfpos;
        }


        if (isBarfing)
        {
            targetValue = normalbarf;
            normalbarf += Time.deltaTime * BarfRiser;
            //VISUALS
            chrom.intensity.value = normalbarf;
            panproj.distance.value = normalbarf;
            if (normalbarf > 0.99f)
            {
                randomBarfPosTime = resetbarfpos;
                Barf();
            }
        }
        else
        {
            idletimer -= Time.deltaTime;
            chrom.intensity.value = currentValue;
            panproj.distance.value = currentValue;
        }

        Barfmeter.localScale = new Vector3(Barfmeter.localScale.x, currentValue, Barfmeter.localScale.z);
    }

    private void FixedUpdate()
    {
        //Spring mechanic
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

    void Barf()
    {
        CharacterAnim.SetBool("Barf", true);
        Barfing.Play();
        if (!StandingOnToilet)
        {
            Instantiate(BarfPool, BarfLocation.position, Quaternion.Euler(Vector3.zero));
        }
        normalbarf = 0;
        isBarfing = false;
    }
}
