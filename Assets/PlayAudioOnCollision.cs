using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{

    public bool useTag;
    public string tagname;
    public AudioSource Audioclip;
    public bool playonce;
    bool isplaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (useTag)
        {
            if (other.collider.CompareTag(tagname)) {
                if (playonce && !isplaying)
                {
                    Audioclip.Play();
                    isplaying = true;
                }
                if (!playonce)
                {
                    Audioclip.Play();
                }
            }
        }
        else
        {
            if (playonce && !isplaying)
            {
                Audioclip.Play();
                isplaying = true;
            }
            if (!playonce)
            {
                Audioclip.Play();
            }
        }
    }
}
