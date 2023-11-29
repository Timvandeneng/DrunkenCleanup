using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afval : MonoBehaviour
{
    GameObject stofzuiger;
    public float activationDistance = 5;
    public float vacuumStrenght = 5;
    Rigidbody rb;
    public bool SuckedUp;
    public float shrinkSpeed = 2;
    public float destroySize = 0.1f;

    Trash_Manager gameManager;

    Vector3 startpos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stofzuiger = GameObject.FindGameObjectWithTag("Stofzuiger");
        gameManager = GameObject.FindFirstObjectByType<Trash_Manager>();
        startpos = transform.position;
    }


    void LateUpdate()
    {
        //billboard
        transform.LookAt(Camera.main.transform.position);

        Vector3 desiredPos = stofzuiger.transform.position - transform.position;
        if (Vector3.Distance(stofzuiger.transform.position, transform.position) < activationDistance)
        {
            //using Movetowards because that way we won't overshoot
            transform.position = Vector3.MoveTowards(transform.position, stofzuiger.transform.position, gameManager.SuctionSpeed * (activationDistance - Vector3.Distance(transform.position, stofzuiger.transform.position)));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startpos, gameManager.SuctionSpeed);

        }

        if (Vector3.Distance(stofzuiger.transform.position, transform.position) < .01f)
        {
            SuckedUp = true;
        }

        if (SuckedUp)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), shrinkSpeed);
            if (transform.localScale.x < destroySize)
            {
                gameManager.currentSmallTrashAmount--;
                Destroy(this.gameObject);
            }
        }
    }
}
