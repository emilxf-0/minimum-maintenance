using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrowingWeedScript : MonoBehaviour
{
    [SerializeField] private GameObject weedObj;
    [SerializeField] private float countDownToNewWeed;
    [SerializeField] private Sprite[] growStateSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private int newWeedCounter = 0;
    private bool isGrabbed = false;
    private int growState;
    private float growTimer;
    private bool onLeftField;
    private float damage;
    
    public LayerMask invalidSurfaces;

    private void Start()
    {
        damage = 0.05f;
        countDownToNewWeed = 5f;
        if (transform.position.x < 0)
            onLeftField = true;
        else
            onLeftField = false;
    }

    private void Update()
    {
        if (!isGrabbed && growState > 2)
            CountDown();
        else if (!isGrabbed && growState <= 2)
            Grow();

        if (growState > 1)
            DealDamage();
    }

    private void DealDamage()
    {
        if (growTimer >= 5)
        {
            if (onLeftField)
                HealthManager.Instance.TakeDamageLeftHouse(damage);
            else
                HealthManager.Instance.TakeDamageRightHouse(damage);
            growTimer = 0f;
        }
        else growTimer += 1 * Time.deltaTime;
    }

    private void Grow()
    {
        if (growTimer >= 5f && growState <= 1)
        {
            growState++;
            spriteRenderer.sprite = growStateSprites[growState];
            growTimer = 0f;
            if (onLeftField)
                HealthManager.Instance.TakeDamageLeftHouse(damage);
            else
                HealthManager.Instance.TakeDamageRightHouse(damage);
        }
        else
            growTimer += 1 * Time.deltaTime;

    }

    private void CountDown()
    {
        if (countDownToNewWeed <= 0 && newWeedCounter < 2)
        {
            SpawnNewWeed();
            countDownToNewWeed = 5f;
            newWeedCounter++;
        }
        else
        {
            countDownToNewWeed -= 1 * Time.deltaTime;
        }
    }

    private void SpawnNewWeed()
    {
        Instantiate(weedObj, ChooseSpawnLocation(), transform.rotation);
    }

    private Vector2 ChooseSpawnLocation()
    {
        Vector2 direction = RandomizeDirection();
        int distance = RandomizeDistance();
        
        do
        {
            RandomizeDirection();
            RandomizeDistance();
        } while (Physics2D.Raycast(transform.position, direction, distance, invalidSurfaces));
        
        return Physics2D.Raycast(transform.position, direction, distance).point;
    }

    private Vector2 RandomizeDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private int RandomizeDistance()
    {
        return Random.Range(3, 7);
    }
}
