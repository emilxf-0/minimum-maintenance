using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrownWeedScript : MonoBehaviour
{
    private bool onLeftField;
    private float damageTimer;
    private float damage;
    
    private void Start()
    {
        damage = 0.05f;
        damageTimer = 0f;
        if (transform.position.x < 0)
            onLeftField = true;
        else
            onLeftField = false;
    }

    void Update()
    {
        DealDamage();
    }

    private void DealDamage()
    {
        if (damageTimer >= 5)
        {
            if (onLeftField)
                HealthManager.Instance.TakeDamageLeftHouse(damage);
            else
                HealthManager.Instance.TakeDamageRightHouse(damage);
            damageTimer = 0f;
        }
        else damageTimer += 1 * Time.deltaTime;
    }
}
