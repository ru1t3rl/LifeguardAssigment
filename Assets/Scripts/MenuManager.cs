using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Camera.main.GetComponent<ClickEvent>().enabled = false;
        Camera.main.GetComponent<AreaMovement>().enabled = false;

        // SOMETHING TO PAUSE SWIMMERS

        pauseMenu.enabled = true;
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
        Camera.main.GetComponent<ClickEvent>().enabled = true;
        Camera.main.GetComponent<AreaMovement>().enabled = true;
        pauseMenu.enabled = false;
    }

    public void MainMenuButton()
    {
        pauseMenu.enabled = false;

        // SOMETHING TO RESET THE GAME

        Camera.main.GetComponent<AreaMovement>().moveTween.Kill();
        Camera.main.GetComponent<AreaMovement>().MoveToArea(AreaMovement.Area.Area2);

        mainMenu.enabled = true;
    }
}
