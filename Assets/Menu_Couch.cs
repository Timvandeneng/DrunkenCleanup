using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu_Couch : MonoBehaviour
{

    public AudioSource softnoise, ambientpeoplesoft;
    public AudioMixer damped, loud;
    public GameObject Smashnoise;


    private void OnCollisionEnter(Collision collision)
    {
        softnoise.outputAudioMixerGroup = loud.outputAudioMixerGroup;
        ambientpeoplesoft.outputAudioMixerGroup = loud.outputAudioMixerGroup;
        Smashnoise.SetActive(true);
    }
}
