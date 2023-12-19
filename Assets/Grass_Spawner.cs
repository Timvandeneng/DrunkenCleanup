using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Spawner : MonoBehaviour
{
    public Transform Plane; //MUST have a scale of 10 x 10
    public Vector2 BoundDistance;

    // Start is called before the first frame update
    void Start()
    {
        float SurfaceArea = BoundDistance.x + BoundDistance.y;
        float Xpos = 0;
        float Zpos = 0;
        for(int i = 0; i < SurfaceArea; i++)
        {
            Instantiate(Plane, transform.position + new Vector3(Xpos, transform.position.y, Zpos), Quaternion.Euler(Vector3.zero), transform);
            Xpos += 100;
            if(Xpos >= BoundDistance.x)
            {
                Xpos = 0;
                Zpos += 100;
            }
            if(Zpos >= BoundDistance.y)
            {
                i = (int)SurfaceArea;
            }

            Debug.Log("Xpos = " + Xpos + " Zpos = " + Zpos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
