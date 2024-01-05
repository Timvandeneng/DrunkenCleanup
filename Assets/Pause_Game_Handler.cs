using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class Pause_Game_Handler : MonoBehaviour
{
    [Header("Slow motion Effect")]
    bool paused = false;
    public float slowmotionScale;
    public float lerpSpeed;
    float startFixedDeltaTime;

    [Header("Visual Effects")]
    public Volume volume;
    DepthOfField Depth;
    public float DepthOfFieldIntensity;

    [Header("Menus")]
    public GameObject NormalUI;
    public GameObject Pausemenu;

    // Start is called before the first frame update
    void Start()
    {
        startFixedDeltaTime = Time.fixedDeltaTime;
        if (volume.profile.TryGet<DepthOfField>(out Depth))
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = paused ? false : true;
        }

        if (paused)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowmotionScale, lerpSpeed * Time.unscaledDeltaTime);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime ,startFixedDeltaTime * slowmotionScale, lerpSpeed * Time.unscaledDeltaTime);
            Depth.focalLength.value = Mathf.Lerp(Depth.focalLength.value, DepthOfFieldIntensity, lerpSpeed * Time.unscaledDeltaTime);
            Cursor.lockState = CursorLockMode.None;
            NormalUI.SetActive(false);
            Pausemenu.SetActive(true);
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, lerpSpeed * Time.unscaledDeltaTime);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, startFixedDeltaTime, lerpSpeed * Time.unscaledDeltaTime);
            Depth.focalLength.value = Mathf.Lerp(Depth.focalLength.value, 0, lerpSpeed * Time.unscaledDeltaTime);
            Cursor.lockState = CursorLockMode.Locked;

            NormalUI.SetActive(true);
            Pausemenu.SetActive(false);
        }

    }

    public void Unpause()
    {
        paused = false;
    }

    public void Exit()
    {
        Time.fixedDeltaTime = startFixedDeltaTime;
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene(0);
    }
}
