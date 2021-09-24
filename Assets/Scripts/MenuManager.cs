using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public Canvas mainMenu, pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown("escape") && !mainMenu.enabled)
            PauseMenu();
    }

    void PauseMenu()
    {
        if (!pauseMenu.enabled)
        {
            pauseMenu.enabled = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            pauseMenu.enabled = false;
            Time.timeScale = 1.0f;
        }
    }

    public void StartButton()
    {
        Camera.main.GetComponent<ClickEvent>().enabled = true;
        Camera.main.GetComponent<AreaMovement>().enabled = true;
        mainMenu.enabled = false;
    }

    public void TutorialButton()
    {
        // CREATE A TUTORIAL TO PLAY
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ResumeButton()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1.0f;
    }

    public void MainMenuButton()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        mainMenu.enabled = true;
    }
}
