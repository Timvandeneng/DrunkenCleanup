using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    private Camera Camera;
    public GameObject Cube;
    public float mulitplier;
    public bool PuasedGame = false;
    public Canvas PauseMenu;

    private void Start()
    {
        Camera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= (Screen.width / 2);
        mousePos.y -= (Screen.height / 2);
        Vector3 desiredPos = new Vector3(mousePos.x * mulitplier, mousePos.y * mulitplier, Cube.transform.position.z);
        Cube.transform.position = desiredPos;

        Camera.transform.LookAt(Cube.transform.position);

        Debug.Log(mousePos);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Startup_animation_1");
    }
}
