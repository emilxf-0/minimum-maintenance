using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image gardenHealth;

    private float playerGardenHealth = 1f;
    private float maxHealth = 1f;

    private void Start()
    {
        gardenHealth.fillAmount = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            TakeDamage(0.1f);
        
        if (Input.GetKeyDown(KeyCode.H))
            Heal(0.05f);
    }

    private void TakeDamage(float damageTaken)
    {
        gardenHealth.fillAmount -= damageTaken;
    }

    private void Heal(float pointsToHeal)
    {
        gardenHealth.fillAmount += pointsToHeal;
    }
}
