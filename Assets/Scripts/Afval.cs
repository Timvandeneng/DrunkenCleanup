using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afval : MonoBehaviour
{
    GameObject[] stofzuiger;
    public VacuumCleanerModel[] vacuumscrpt;
    public float activationDistance = 5;
    public float vacuumStrenght = 5;
    Rigidbody rb;
    public bool SuckedUp;
    public float shrinkSpeed = 2;
    public float destroySize = 0.1f;

    public SpriteRenderer sprite;

    Trash_Manager gameManager;

    Vector3 startpos;

    //this is the effect of the U.I.
    public GameObject UIEff;
    Score_Adder_Handler UiHandler;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stofzuiger = GameObject.FindGameObjectsWithTag("Stofzuiger");
        gameManager = GameObject.FindFirstObjectByType<Trash_Manager>();
        startpos = transform.position;

        float newcolor = Random.Range(0.6f, .7f);
        sprite.color = new Color(newcolor, newcolor, newcolor, 1);

        float newscale = Random.Range(0.25f, 1f);
        transform.localScale = new Vector3(newscale, newscale, newscale);

        float Yrot = Random.Range(0, 360);
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Yrot, transform.rotation.eulerAngles.z);
    }


    void Update()
    {
        //billboard
        //transform.LookAt(Camera.main.transform.position);
        for(int i = 0; i < stofzuiger.Length; i++)
        {
            Vector3 desiredPos = stofzuiger[i].transform.position - transform.position;
            //first checking if we are in range
            if (Vector3.Distance(stofzuiger[i].transform.position, transform.position) < activationDistance)
            {
                //setting the grabobj script to corresponding
                //ALWAYS set in the 'afval' script the number of items in array (possible vacuums)
                vacuumscrpt[i] = stofzuiger[i].GetComponentInParent<VacuumCleanerModel>();
                if (vacuumscrpt[i].grabobj.leftGrab || vacuumscrpt[i].grabobj.rightGrab)
                {
                    //using Movetowards because that way we won't overshoot
                    transform.position = Vector3.MoveTowards(transform.position, stofzuiger[i].transform.position, gameManager.SuctionSpeed * (activationDistance - Vector3.Distance(transform.position, stofzuiger[i].transform.position)));
                }         
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startpos, gameManager.SuctionSpeed);

            }

            if (Vector3.Distance(stofzuiger[i].transform.position, transform.position) < .01f)
            {
                if (vacuumscrpt[i].grabobj.leftGrab || vacuumscrpt[i].grabobj.rightGrab)
                    SuckedUp = true;
            }

            if (SuckedUp)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), shrinkSpeed);
                if (transform.localScale.x < destroySize)
                {
                    gameManager.currentSmallTrashAmount -= gameManager.SmallTrashValue;
                    UiHandler = Instantiate(UIEff, transform.position, Quaternion.identity).GetComponent<Score_Adder_Handler>();
                    UiHandler.Ammount = gameManager.SmallTrashValue;
                    vacuumscrpt[i].Model.localScale = new Vector3(1, 1, 1) * vacuumscrpt[i].growsize;
                    Destroy(this.gameObject);
                }
            }

            if(i == stofzuiger.Length)
            {
                i = 0;
            }
        }
        
    }
}
