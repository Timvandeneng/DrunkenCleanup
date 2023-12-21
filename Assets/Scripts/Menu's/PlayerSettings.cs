using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown QualityDropdown;
    public AudioMixer volMixer;
    public Toggle FullscreenToggle;
    public Slider volSlider;

    Resolution[] resolutions;
    public TMP_Dropdown ResolutionDropdown;
    const string resName = "resolutionOption";

    private bool isPaused = false;

    public GameObject volumetrics;

    //FPS configures
    public TextMeshProUGUI FPSDisplay;
    private float pollingTime = 0.5f;
    public float time;
    public int frameCount;

    private void Awake()
    {
        FullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleValueChanged);

        ResolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            if (!isPaused)
                SetResolution(index);
        }));
    }

    void Start()
    {
        FPSDisplay.enabled = false;

        QualityDropdown.value = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
        volSlider.value = PlayerPrefs.GetFloat("AVolume", 1f);
        volMixer.SetFloat("volume", PlayerPrefs.GetFloat("AVolume"));

        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        ResolutionDropdown.RefreshShownValue();
    }

    public void Update()
    {
        if (!isPaused)
        {
            time += Time.deltaTime;
            frameCount++;

            if (time >= pollingTime)
            {
                int frameRate = Mathf.RoundToInt(frameCount / time);
                FPSDisplay.text = "FPS: " + frameRate.ToString();

                time = 0f;
                frameCount = 0;
            }
        }
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(resName, resolutionIndex);
    }

    public void OnFullscreenToggleValueChanged(bool value)
    {
        if (!isPaused)
            SetFullScreen(value);
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt("fullscreenstate", fullScreen ? 1 : 0);
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        QualitySettings.renderPipeline = qualityLevels[index];
        PlayerPrefs.SetInt("QualityLevel", index);
        if (index < 2)
        {
            volumetrics.active = false;
        }
        else
        {
            volumetrics.active = true;
        }
    }

    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("AVolume", volume);
        volMixer.SetFloat("volume", PlayerPrefs.GetFloat("AVolume"));
    }

    public void SetPauseState(bool paused)
    {
        isPaused = paused;
    }

    public void DisplayFPS(bool Enabled)
    {
        FPSDisplay.enabled = !isPaused && Enabled;
    }

}
