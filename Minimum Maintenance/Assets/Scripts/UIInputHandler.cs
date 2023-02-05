using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [Header("Win stuff")]
    [SerializeField] private GameObject winnLeftPoint;
    [SerializeField] private GameObject winnRightPoint;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject winnPanel;
    [SerializeField] private GameObject LosePanel;

    [Header("Intro")]
    [SerializeField] private GameObject IntroPanel;
    private bool playingIntro = false;

    private bool gameDone;

    private bool gamePaused;
    bool HealthManagerExists;


    private void Start()
    {

        if (!playingIntro && IntroPanel != null)
        {
            IntroPanel.transform.position =  Vector3.zero;
            StartCoroutine(PlayIntoScene());
        }

        gameDone = false;
<<<<<<< HEAD


=======
      
>>>>>>> 1265ade91b258842fc426778db1cd595932c437b
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(false);
        }
        

        if (GameObject.Find("HealthManager") != null)
        {

            HealthManagerExists = true;
        }
        else
            HealthManagerExists = false;
<<<<<<< HEAD
=======
       
>>>>>>> 1265ade91b258842fc426778db1cd595932c437b

    }

    private void Update()
    {
<<<<<<< HEAD
        if (!playingIntro)
=======
       

        if (pauseMenu != null)
>>>>>>> 1265ade91b258842fc426778db1cd595932c437b
        {

            if (pauseMenu != null)
            {
                if (Input.GetButtonDown("Cancel"))
                    PauseGame();
            }

            if (HealthManagerExists && !gameDone)
            {
                if (HealthManager.Instance.leftHealth <= 0)
                {
                    SetWinner(false);
                }
                else if (HealthManager.Instance.rightHealth <= 0)
                {
                    SetWinner(true);
                }
            }
        }
    }

    IEnumerator PlayIntoScene()
    {

        playingIntro = true;
        float trans1 = 0.5f;
        float trans2 = 0.5f;
        FreezeGame(true);

        yield return new WaitForSecondsRealtime(trans1);

        IntroPanel.GetComponent<Animator>().SetTrigger("StartAnim");

        yield return new WaitForSecondsRealtime(1.8f);

        IntroPanel.transform.DOMove(Vector3.zero + new Vector3(0, -10), trans2).SetEase(Ease.Linear).SetUpdate(true);

        yield return new WaitForSecondsRealtime(trans2);
        IntroPanel.GetComponent<Animator>().SetTrigger("EndAnim");
        playingIntro = false;
        FreezeGame(false);

    }

    public void StartGame()
    {
        FreezeGame(false);
        SceneManager.LoadScene("TutorialScene");
        HealthManager.Instance.leftHealth = 1f;
        HealthManager.Instance.rightHealth = 1f;
        gameDone = false;

    }

    public void GoToMainMenu()
    {
        FreezeGame(false);
        SceneManager.LoadScene("MainMenu");

    }

    public void ResetMatch()
    {
        FreezeGame(false);
        HealthManager.Instance.leftHealth = 1f;
        HealthManager.Instance.rightHealth = 1f;
        gameDone = false;
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
        Gamepad[] allgamePads = Gamepad.all.ToArray();
        if(allgamePads.Length > 0)
        {
            allgamePads[0]?.SetMotorSpeeds(0, 0);
            allgamePads[1]?.SetMotorSpeeds(0, 0);
        }
        
        gameDone = true;
        GameOverPanel.SetActive(true);
        FreezeGame(true);
        Ease setEase = Ease.OutBounce;
        if (isLeft)
        {
            winnPanel.transform.position = winnLeftPoint.transform.position + new Vector3(0, 10);
            LosePanel.transform.position = winnRightPoint.transform.position + new Vector3(0, 10);

            winnPanel.transform.DOMove(winnLeftPoint.transform.position, 2).SetEase(setEase).SetUpdate(true);
            LosePanel.transform.DOMove(winnRightPoint.transform.position, 2).SetEase(setEase).SetUpdate(true);
        }
        else
        {
            LosePanel.transform.position = winnLeftPoint.transform.position + new Vector3(0, 10);
            winnPanel.transform.position = winnRightPoint.transform.position + new Vector3(0, 10);

            LosePanel.transform.DOMove(winnLeftPoint.transform.position, 2).SetEase(setEase).SetUpdate(true);
            winnPanel.transform.DOMove(winnRightPoint.transform.position, 2).SetEase(setEase).SetUpdate(true);
        }

    }



}
