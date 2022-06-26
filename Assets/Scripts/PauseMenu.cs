using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
 * Script which handles Pause Menu behavior
 */
public class PauseMenu : MonoBehaviour
{
    // Flag used to store info if the game is paused
    public static bool GamePaused = false;
    // Variable storing reference to Pause Menu
    public GameObject PauseMenuUI;

    // Update is called once per frame and pauses or resumes game on ESC button clicked
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Hides cursos, deactivates Pause Menu, and resumes time
    void Resume()
    {
        Screen.lockCursor = true;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    // Shows cursos, activates Pause Menu, and stops time
    void Pause()
    {
        Screen.lockCursor = false;
        Cursor.visible = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    // Resumes time, and restarts scene
    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // Resumes time, and selects Main Menu as scene
    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game Menu");
    }

    // Quits game
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
