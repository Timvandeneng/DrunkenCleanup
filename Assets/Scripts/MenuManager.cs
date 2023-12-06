using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    public GameObject CameraLookPos;


    private void Update()
    {
        Debug.Log(Camera.ScreenToWorldPoint(Input.mousePosition));
        Vector3 MouseWorldPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        MouseWorldPosition.z = 0;
        CameraLookPos.transform.position = MouseWorldPosition;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene1");
    }
}
