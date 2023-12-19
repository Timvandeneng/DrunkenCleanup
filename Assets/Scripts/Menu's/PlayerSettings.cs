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
    // fullscreen option
    public Toggle FullscreenToggle;

    public Slider volSlider;

    // Resolution dropdown settings
    Resolution[] resolutions;
    public TMP_Dropdown ResolutionDropdown;
    const string resName = "resolutionOption";

    private int screenInt;
    private bool isFullScreen = false;

    private void Awake()
    {
        screenInt = PlayerPrefs.GetInt("fullscreenstate");

        if (screenInt == 1)
        {
            isFullScreen = true;
            FullscreenToggle.isOn = true;
        }
        else
        {
            isFullScreen = false;
            FullscreenToggle.isOn = false;
        }

        ResolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resName, ResolutionDropdown.value);
        }));
    }

    void Start()
    {
        QualityDropdown.value = QualitySettings.GetQualityLevel();

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

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;

        if(isFullScreen == false)
        {
            PlayerPrefs.SetInt("fullscreenstate", 0);
        }
        else
        {
            isFullScreen = true;
            PlayerPrefs.SetInt("fullscreenstate", 1);
        }
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        QualitySettings.renderPipeline = qualityLevels[index];
        PlayerPrefs.SetInt("QualityLevel", index);
    }

    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("AVolume", volume);
        volMixer.SetFloat("volume", PlayerPrefs.GetFloat("AVoume"));
    }
}
