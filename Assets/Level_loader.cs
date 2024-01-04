using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_loader : MonoBehaviour
{
    public bool Loadlevel;
    public int scenetoload;


    // Update is called once per frame
    void Update()
    {
        if (Loadlevel)
            SceneManager.LoadScene(scenetoload);
    }
}
