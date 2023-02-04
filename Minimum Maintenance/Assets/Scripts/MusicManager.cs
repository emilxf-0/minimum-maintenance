using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusicGame;
    [SerializeField] private AudioClip backgroundMusicMenu;
    [SerializeField] private AudioClip pickUp;
    [SerializeField] private AudioClip chargeUp;
    [SerializeField] private AudioClip impact;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource pickUpSource;
    [SerializeField] private AudioSource chargeUpSource;
    [SerializeField] private AudioSource impactSource;
    
    private bool isMenu;

    private void Start()
    {
        StartMusic();
    }

    private void StartMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            isMenu = true;
            musicSource.clip = backgroundMusicMenu;
            musicSource.Play();
        }
        else
        {
            isMenu = false;
            musicSource.clip = backgroundMusicGame;
            musicSource.Play();
        }
    }

    public void PlayImpact()
    {
        impactSource.Play();
    }

    public void PlayCharge()
    {
        chargeUpSource.Play();
    }

    public void PlayPickup()
    {
        pickUpSource.Play();
    }
}
