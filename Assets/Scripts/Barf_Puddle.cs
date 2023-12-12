using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barf_Puddle : MonoBehaviour
{
    [Header("Puddle Aestethics")]
    public float minScale;
    public float maxScale;
    public float growspeed;
    Trash_Manager TrashMngr;
    Vector3 DesiredScale;


    public float BarfHealth;

    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        //making sure we add up trash to the total amount
        TrashMngr = GameObject.FindFirstObjectByType<Trash_Manager>();
        TrashMngr.currentWaterAmount++;

        //resetting the scale to 0 to make the barf grow
        transform.localScale = Vector3.zero;

        //calculating random values
        float randomScale = Random.Range(minScale, maxScale);
        DesiredScale = new Vector3(randomScale, 1, randomScale);

        //getting the material
        mat = GetComponent<Renderer>().material;
        BarfHealth = 1;  //should always be 1 because the alpha channel of the color is directly linked to it
    }

    // Update is called once per frame
    void Update()
    {
        //making the scale grow
        transform.localScale = Vector3.Lerp(transform.localScale, DesiredScale, growspeed * Time.deltaTime);

        Color curcolor = mat.color;
        curcolor.a = BarfHealth;
        mat.color = curcolor;

        if(BarfHealth < -.1)
        {
            Destroy(this);
        }
    }
}
