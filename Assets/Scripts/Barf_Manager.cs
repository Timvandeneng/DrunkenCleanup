using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

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
    public float BarfYOffset;
    public float maxIdleUI;
    float resetbarfpos;

    [Header("Gameobjects")]
    public Image Barfmeter;
    public ParticleSystem Barfing;
    public Transform BarfLocation;
    public GameObject BarfPool;
    public Animator CharacterAnim;
    public Transform Staartje;
    [HideInInspector]
    public Transform Ground;
    [HideInInspector]
    public Toilet_Script Toilet;

    [Header("Special Effects")]
    public Volume volume;
    private ChromaticAberration chrom;
    private PaniniProjection panproj;
    private Vignette vign;
    public RectTransform BarfMeterWhole;
    Vector3 startpos;
    public float TrembleSpeed;
    float resettremble;
    public float Maxdistance;
    bool tremble = false;

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
        resettremble = TrembleSpeed;
        startpos = BarfMeterWhole.position;
        normalbarf = 0;
        if (volume.profile.TryGet<ChromaticAberration>(out chrom))
        {
        }
        if (volume.profile.TryGet<PaniniProjection>(out panproj))
        {
        }
        if (volume.profile.TryGet<Vignette>(out vign))
        {
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
            targetValue = Random.Range(0f, maxIdleUI);
            CharacterAnim.SetBool("Barf", false);
            targetValue = targetValue / 100;
            randomBarfPosTime = resetbarfpos;
        }


        if (isBarfing)
        {
            tremble = true;
            targetValue = normalbarf;
            normalbarf += Time.deltaTime * BarfRiser;
            //VISUALS
            chrom.intensity.value = normalbarf;
            panproj.distance.value = normalbarf;
            vign.intensity.value = normalbarf;
            if (normalbarf > 0.99f)
            {
                randomBarfPosTime = resetbarfpos;
                Barf();
            }
        }
        else
        {
            tremble = false;
            idletimer -= Time.deltaTime;
            chrom.intensity.value = currentValue;
            panproj.distance.value = currentValue;
            vign.intensity.value = currentValue;
        }

        Barfmeter.fillAmount = currentValue;

        if (tremble)
        {
            trembling();
        }
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
            Vector3 BarfPosition = new Vector3(BarfLocation.position.x, Ground.position.y + BarfYOffset, BarfLocation.position.z);
            Instantiate(BarfPool, BarfPosition, Quaternion.Euler(Vector3.zero));
        }
        else
        {
            Toilet.Dirty = true;
        }
        normalbarf = 0;
        isBarfing = false;
    }

    void trembling()
    {
        TrembleSpeed -= Time.deltaTime;
        if(TrembleSpeed < 0)
        {
            Vector3 Desiredpos = new Vector3(startpos.x - Random.Range(-Maxdistance, Maxdistance), startpos.y - Random.Range(-Maxdistance, Maxdistance), 0);
            BarfMeterWhole.position = Desiredpos;
            TrembleSpeed = resettremble;
        }
    }
}
