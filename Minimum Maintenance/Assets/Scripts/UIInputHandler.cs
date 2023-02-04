using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool gamePaused;
    
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            PauseGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetMatch()
    {
        var scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        if (gamePaused == false)
        {
            pauseMenu.SetActive(true);
            gamePaused = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            gamePaused = false;
        }
    }

}
