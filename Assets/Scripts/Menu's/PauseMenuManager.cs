using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static bool PausedGame = false;
    public Canvas PauseMenu;
    public Canvas SettingsMenu;


    public void Start()
    {
        SettingsMenu.enabled = false;
        PauseMenu.enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PausedGame)
            {
                ContinueGame();
            } else
            {
                PauseGame();
            }

        }
    }

    public void PauseGame()
    {
        PauseMenu.enabled = true;
        Time.timeScale = 0;
        PausedGame = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ContinueGame()
    {
        PauseMenu.enabled = false;
        Time.timeScale = 1;
        PausedGame = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenSettings()
    {
        SettingsMenu.enabled = true;
    }

    public void CloseSettings()
    {
        SettingsMenu.enabled = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
