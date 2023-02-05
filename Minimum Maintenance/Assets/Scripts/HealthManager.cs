using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private static HealthManager instance;
    public static HealthManager Instance 
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Health manager doesn't exist");
            }
            
            return instance;
        }
    }

    [SerializeField] private Image gardenHealthLeftHouse;
    [SerializeField] private Image gardenHealthRightHouse;
    
    [SerializeField] private Transform healthBarLeftHouse;
    [SerializeField] private Transform healthBarRightHouse;

    private float playerGardenHealth = 1f;
    private float maxHealth = 1f;

    public float leftHealth;
    public float rightHealth;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        gardenHealthLeftHouse.fillAmount = maxHealth;
        gardenHealthRightHouse.fillAmount = maxHealth;
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.D))
        //{
        //    if (Input.GetKeyDown(KeyCode.L))
        //       TakeDamageLeftHouse(0.1f);

        //    if (Input.GetKeyDown(KeyCode.R))
        //       TakeDamageRightHouse(0.1f);
        //}

        //if (Input.GetKey(KeyCode.H))
        //{
        //    if (Input.GetKeyDown(KeyCode.L))
        //        HealLeftHouse(0.05f);

        //    if (Input.GetKeyDown(KeyCode.R))
        //        HealRightHouse(0.05f);
        //}

        leftHealth = gardenHealthLeftHouse.fillAmount;
        rightHealth = gardenHealthRightHouse.fillAmount;
        
    }

    public void TakeDamageLeftHouse(float damageTaken)
    {
        gardenHealthLeftHouse.fillAmount -= damageTaken;
        healthBarLeftHouse.transform.DOShakePosition(0.5f, 8f, 10, 50f, true);
    }
    
    public void TakeDamageRightHouse(float damageTaken)
    {
        gardenHealthRightHouse.fillAmount -= damageTaken;
        healthBarRightHouse.transform.DOShakePosition(0.5f, 8f, 10, 50f, true);
    }

    public void HealRightHouse(float pointsToHeal)
    {
        gardenHealthRightHouse.fillAmount += pointsToHeal;
    }
    
    public void HealLeftHouse(float pointsToHeal)
    {
        gardenHealthLeftHouse.fillAmount += pointsToHeal;
    }
    
    
}
