using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    // Game settings
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown QualityDropdown;

    public AudioMixer volMixer;
    public Toggle FullscreenToggle;
    public Slider volSlider;

    public TMP_Dropdown FpsLimit;
    public List<string> FpsOptions = new List<string> { "30", "60", "120", "Unlimited" };

    Resolution[] resolutions;
    public TMP_Dropdown ResolutionDropdown;
    const string resName = "resolutionOption";
    public GameObject volumetrics;

    // FPS display
    public TextMeshProUGUI FPSDisplay;
    private float pollingTime = 0.5f; // Time interval for updating FPS display
    private float time;
    private int frameCount;

    private bool isPaused = false; // Check if the game is currently paused

    private void Awake()
    {
        // Set up event listeners for UI interactions
        FullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleValueChanged);
        ResolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            if (!isPaused)
                SetResolution(index);
        }));
        FpsLimit.onValueChanged.AddListener(SetFPS);
    }

    void Start()
    {
        // Initialize game settings on start
        FPSDisplay.enabled = false; // Disable FPS display by default

        // Get default values or load saved values from PlayerPrefs
        int defaultFpsIndex = 1; // Default to 60 FPS
        FpsLimit.value = PlayerPrefs.GetInt("FpsLimit", defaultFpsIndex);
        QualityDropdown.value = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
        volSlider.value = PlayerPrefs.GetFloat("AVolume", 1f);
        volMixer.SetFloat("volume", PlayerPrefs.GetFloat("AVolume"));

        // Screen resolution options
        resolutions = Screen.resolutions;

        // Give screen resolution dropdown the available options
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            // Identify the current screen resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        ResolutionDropdown.RefreshShownValue();

        // FPS options
        FpsLimit.AddOptions(FpsOptions);
    }

    public void Update()
    {
        // Calculate and display FPS in real-time
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
        // Set the game screen resolution
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(resName, resolutionIndex);
    }

    public void OnFullscreenToggleValueChanged(bool value)
    {
        // Toggle fullscreen mode based on player input
        if (!isPaused)
            SetFullScreen(value);
    }

    public void SetFullScreen(bool fullScreen)
    {
        // Set fullscreen mode and save the choice in PlayerPrefs
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt("fullscreenstate", fullScreen ? 1 : 0);
    }

    public void ChangeQuality(int index)
    {
        // Change graphics quality settings based on player choice
        QualitySettings.SetQualityLevel(index);
        QualitySettings.renderPipeline = qualityLevels[index];
        PlayerPrefs.SetInt("QualityLevel", index);

        // Activate volumetric effects for higher quality levels
        volumetrics.SetActive(index >= 2);
    }

    public void ChangeVolume(float volume)
    {
        // Adjust the games audio volume based on player input
        PlayerPrefs.SetFloat("AVolume", volume);
        volMixer.SetFloat("volume", PlayerPrefs.GetFloat("AVolume"));
    }

    public void SetPauseState(bool paused)
    {
        // Set or unset the pause state of the game
        isPaused = paused;
    }

    public void DisplayFPS(bool Enabled)
    {
        // Toggle the visibility of the FPS display based on player preference
        FPSDisplay.enabled = !isPaused && Enabled;
    }

    public void SetFPS(int fpsIndex)
    {
        // Set the target frame rate based on player-selected option
        int targetFrameRate = 0;

        switch (fpsIndex)
        {
            case 0:
                targetFrameRate = 30;
                break;
            case 1:
                targetFrameRate = 60;
                break;
            case 2:
                targetFrameRate = 120;
                break;
            case 3:
                targetFrameRate = -1; // this means unlimited
                break;
            default:
                targetFrameRate = 60;
                break;
        }

        // Apply the target frame rate and save the choice in PlayerPrefs
        Application.targetFrameRate = targetFrameRate;
        PlayerPrefs.SetInt("FpsLimit", fpsIndex);
    }
}