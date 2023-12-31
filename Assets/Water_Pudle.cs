using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Pudle : MonoBehaviour
{
    public int puddlehealth = 5;
    [HideInInspector]
    public int starthealth;

    public List<GameObject> Masks;

    Trash_Manager TrashMngr;

    bool dead = false;

    //this is the effect of the U.I.
    public GameObject UIEff;
    Score_Adder_Handler UiHandler;

    // Start is called before the first frame update
    void Start()
    {
        TrashMngr = GameObject.FindFirstObjectByType<Trash_Manager>();
        starthealth = puddlehealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (puddlehealth <= 0)
            {
                dead = true;
            }
        }
        else
        {
            Debug.Log("Destroy Puddle");
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
            if(sprite.color.a < 0)
            {
                for (int i = 0; i < Masks.Count; i++)
                {
                    Destroy(Masks[i]);
                }
                Destroy(this.gameObject);
            }
           
        }
       
    }
}
