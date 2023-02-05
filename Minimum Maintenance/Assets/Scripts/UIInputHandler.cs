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
    [SerializeField] private GameObject winnLeftPanel;
    [SerializeField] private GameObject winnRightPanel;
    [SerializeField] private GameObject winnPanel;

    private bool gameDone;

    private bool gamePaused;

    private void Start()
    {
        gameDone = false;
        if (winnLeftPanel != null)
        {
            winnPanel.SetActive(false);
            winnRightPanel.SetActive(false);
            winnLeftPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (pauseMenu != null)
        {
            if (Input.GetButtonDown("Cancel"))
                PauseGame();
        }

        if(HealthManager.Instance != null)
        {
            if(HealthManager.Instance.leftHealth < 0)
            {
                SetWinner(true);
            }
            else if (HealthManager.Instance.rightHealth < 0)
            {
                SetWinner(false);
            }
        }

    }

    public void StartGame()
    {
        FreezeGame(false);
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMainMenu()
    {
        FreezeGame(false);
        SceneManager.LoadScene("MainMenu");

    }

    public void ResetMatch()
    {
        FreezeGame(false);
        var scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        FreezeGame(false);
        Application.Quit();
    }

    private void PauseGame()
    {
        if (gamePaused == false)
        {
            pauseMenu.SetActive(true);
            gamePaused = true;
            FreezeGame(true);

        }
        else
        {
            FreezeGame(false);
            pauseMenu.SetActive(false);
            gamePaused = false;
        }
    }

    private void FreezeGame(bool freeze)
    {
        if (freeze)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }


    }

    private void SetWinner(bool isLeft)
    {
        FreezeGame(true);
        if(isLeft)
        {
            winnRightPanel.SetActive(false);
            winnLeftPanel.SetActive(true);
        }
        else
        {
            winnRightPanel.SetActive(true);
            winnLeftPanel.SetActive(false);
        }

        winnPanel.SetActive(false);
    }



}
