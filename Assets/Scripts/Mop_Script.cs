using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop_Script : MonoBehaviour
{
    GameObject puddle;
    Trash_Manager trash;

    //using a sprite mask to hide and show the underlaying puddle
    Transform Ground;
    public GameObject spriteMask;
    public GameObject water;
    public float spawnDistance;
    public float spawnheight;
    Vector3 lastpos;
    Vector3 lastposwater;

    Water_Pudle pudlescrpt;

    Barf_Puddle Barf;

    //this is the effect of the U.I.
    public GameObject UIEff;
    Score_Adder_Handler UiHandler;

    // Start is called before the first frame update
    void Start()
    {
        trash = GameObject.FindFirstObjectByType<Trash_Manager>();
        lastposwater = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Puddle"))
        {
            pudlescrpt = other.GetComponent<Water_Pudle>();
            //when we are X distance from last pos we spawn sprite mask
            if(Vector3.Distance(transform.position, lastpos) > spawnDistance)
            {
                Vector3 desiredRot = new Vector3(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                pudlescrpt.Masks.Add(Instantiate(spriteMask, transform.position + new Vector3(0, Ground.position.y + spawnheight, 0), Quaternion.Euler(desiredRot)));
                pudlescrpt.puddlehealth--;
                UiHandler = Instantiate(UIEff, transform.position, Quaternion.identity).GetComponent<Score_Adder_Handler>();
                UiHandler.Ammount = trash.WaterPuddleValue / pudlescrpt.starthealth;
                trash.currentWaterAmount -= trash.WaterPuddleValue / pudlescrpt.starthealth;
                lastpos = transform.position;
            }

           // puddle = other.gameObject;
           // trash.currentWaterAmount--;
           // Destroy(puddle);
        }

        //making sure the ground is set to the object we are colliding with
        if (other.CompareTag("Ground"))
        {
            Ground = other.gameObject.transform;
        }

        if (other.CompareTag("BarfPool"))
        {
            Barf = other.GetComponent<Barf_Puddle>();
            if(Barf != null)
            {
                Barf.BarfHealth -= 0.35f;
            }
            Debug.Log("BarpoolCleanup");
        }
    }

    /*
    private void Update()
    {
        if (Vector3.Distance(transform.position, lastposwater) > spawnDistance)
        {
            Vector3 desiredRot = new Vector3(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            Instantiate(water, transform.position + new Vector3(0, Ground.position.y + spawnheight, 0), Quaternion.Euler(desiredRot));
            lastposwater = transform.position;
        }
    }
    */
}
