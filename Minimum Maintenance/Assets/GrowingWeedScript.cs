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

    private int newWeedCounter = 0;
    private bool isGrabbed = false;
    public LayerMask invalidSurfaces;

    private void Start()
    {
        countDownToNewWeed = 5f;
    }

    private void Update()
    {
        if (!isGrabbed)
            CountDown();
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
