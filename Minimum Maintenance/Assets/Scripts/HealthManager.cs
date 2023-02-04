using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;
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

    private float playerGardenHealth = 1f;
    private float maxHealth = 1f;

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
    }
    
    private void Start()
    {
        gardenHealthLeftHouse.fillAmount = maxHealth;
        gardenHealthRightHouse.fillAmount = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.L))
               TakeDamageLeftHouse(0.1f);
            
            if (Input.GetKeyDown(KeyCode.R))
               TakeDamageRightHouse(0.1f);
        }
        
        if (Input.GetKey(KeyCode.H))
        {
            if (Input.GetKeyDown(KeyCode.L))
                HealLeftHouse(0.05f);
            
            if (Input.GetKeyDown(KeyCode.R))
                HealRightHouse(0.05f);
        }
            
    }

    public void TakeDamageLeftHouse(float damageTaken)
    {
        gardenHealthLeftHouse.fillAmount -= damageTaken;
    }
    
    public void TakeDamageRightHouse(float damageTaken)
    {
        gardenHealthRightHouse.fillAmount -= damageTaken;
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
