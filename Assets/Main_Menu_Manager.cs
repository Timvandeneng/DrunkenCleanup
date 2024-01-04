using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        Time.timeScale = 0;
        Cam = Camera.main;
        FadeImg.color = new Color(0, 0, 0, 1);
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

        if((FadeImg.color.a - DesiredALphaValue) < disableDistance)
        {
            FadeImg.gameObject.SetActive(false);
        }

    }

    public void Play()
    {
        Time.timeScale = 1;
    }

    public void AddPlayer()
    {
        LastRagdoll = Instantiate(PlayerRagdoll, Spawnlocation.position, Quaternion.identity);
    }

    public void RemovePlayer()
    {
        Destroy(LastRagdoll);
    }
}
