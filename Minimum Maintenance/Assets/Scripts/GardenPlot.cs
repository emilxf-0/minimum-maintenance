using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class GardenPlot : MonoBehaviour
{
    [SerializeField] private GameObject weedObj;
    [SerializeField] private GameObject thrownWeed;
    [SerializeField] private float adjustedSpawnInterval = 5f;
    
    private float weedSpawnInterval;
    private float countdownCounter;

    private void Start()
    {
        weedSpawnInterval = adjustedSpawnInterval;
        countdownCounter = 5f;
    }

    private void FixedUpdate()
    {
        Debug.Log("Weed Spawn Interval: " + weedSpawnInterval);
        Debug.Log("Adjusted timer: " + adjustedSpawnInterval);
    }

    private void Update()
    {
        CountDownToSpawn();
        IncreaseSpawnInterval();
    }

    private void IncreaseSpawnInterval()
    {
        //Increases spawns successively as the match progresses
        if (countdownCounter <= 0)
        {
            adjustedSpawnInterval--;
            countdownCounter = 5f;
            if (adjustedSpawnInterval < 1)
                adjustedSpawnInterval = 1;
        }
        else
            countdownCounter -= 1 * Time.deltaTime;
    }

    private void CountDownToSpawn()
    {
        if (weedSpawnInterval <= 0)
        {
            SpawnWeed();
            weedSpawnInterval = adjustedSpawnInterval;
            Debug.Log("Spawning weed");
        }
        else
            weedSpawnInterval -= 1 * Time.deltaTime;
    }

    // private void ChooseSpawnDirection()
    // {
    //     switch (Random.Range(0, 4))
    //     {
    //         case 0:
    //             SpawnWeed(1);
    //             break;
    //         case 1:
    //             SpawnWeed(2);
    //             break;
    //         case 2:
    //             SpawnWeed(3);
    //             break;
    //         default:
    //             SpawnWeed(4);
    //             break;
    //     }
    // }

    private void SpawnWeed()
    {
        Bounds bounds = GetComponent<PolygonCollider2D>().bounds;
        Instantiate(weedObj, RandomizeSpawnPosition(bounds), gameObject.transform.rotation);
    }

    private Vector2 RandomizeSpawnPosition(Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }

    public void HitByWeed(Vector2 hitLocation)
    {
        Instantiate(thrownWeed, hitLocation, transform.rotation);
    }
}
