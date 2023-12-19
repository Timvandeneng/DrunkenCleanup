using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Animation : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void PlayAnimation()
    {
        anim.SetBool("Activate", true);
    }

    public void StopAnimation()
    {
        anim.SetBool("Activate", false);
    }
}
