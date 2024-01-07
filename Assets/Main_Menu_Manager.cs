using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    [Header("Ragdoll physics")]
    public Transform Spawnlocation;
    public GameObject PlayerRagdoll;
    GameObject LastRagdoll;

    [Header("Camera PHysics")]
    private Camera Cam;
    public Transform Cube, lookAnchor;
    public float mulitplier;
    public float lookspeed;

    [Header("Fade Physics")]
    public RawImage FadeImg;
    public float DesiredALphaValue = 0;
    public float fadespeed;
    public float disableDistance;

    bool loadLevel;
    public int Scenetoload;

    

    private void Start()
    {
        Time.timeScale = 0;
        Cam = Camera.main;
        FadeImg.color = new Color(0, 0, 0, 1);
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        //Camera follow mouse physics
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= (Screen.width / 2);
        mousePos.y -= (Screen.height / 2);
        Vector3 desiredPos = new Vector3(mousePos.x * mulitplier, mousePos.y * mulitplier, Cube.position.z);
        Cube.position = desiredPos;

        lookAnchor.transform.LookAt(Cube.transform.position);
        Cam.transform.rotation = Quaternion.Lerp(Cam.transform.rotation, lookAnchor.rotation, lookspeed * Time.unscaledDeltaTime);

        //Fade Mechanics
        Color desiredcolor = new Color(0, 0, 0, DesiredALphaValue);
        FadeImg.color = Color.Lerp(FadeImg.color, desiredcolor, fadespeed * Time.unscaledTime);

        if (!loadLevel)
        {
            if ((FadeImg.color.a - DesiredALphaValue) < disableDistance)
            {
                FadeImg.gameObject.SetActive(false);
            }
        }
        else
        {
            DesiredALphaValue = 1;
            FadeImg.gameObject.SetActive(true);
            if ((FadeImg.color.a > 0.99f))
            {
                SceneManager.LoadScene(Scenetoload);
            }
        }
        

    }

    public void Play()
    {
        Time.timeScale = 1;
    }

    public void AddPlayer()
    {

        LastRagdoll = Instantiate(PlayerRagdoll, Spawnlocation.position, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
    }

    public void RemovePlayer()
    {
        Destroy(LastRagdoll);
    }

    public void LoadLevel()
    {
        loadLevel = true;
    }
}
