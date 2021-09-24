using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public Canvas mainMenu, pauseMenu, inGameMenu;

    private void Update()
    {
        if (Input.GetKeyDown("escape") && !mainMenu.enabled && SceneManager.GetActiveScene().name == "GameScene")
            PauseMenu();
        else if (Input.GetKeyDown("escape") && SceneManager.GetActiveScene().name == "TutorialScene")
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
        inGameMenu.enabled = true;
    }

    public void TutorialButton()
    {
        // CREATE A TUTORIAL TO PLAY
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
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
        inGameMenu.enabled = false;
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("GameScene");

        mainMenu.enabled = true;
    }
}
