using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GardenPlot : MonoBehaviour
{
    [SerializeField] private GameObject weedObj;
    [SerializeField] private GameObject thrownWeed;
    [SerializeField] private GameObject thrownSunflower;
    [SerializeField] private float adjustedSpawnInterval = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public bool harderByTime;
    
    private float weedSpawnInterval;
    private float countdownCounter;
    private int growState;
    private float growTimer;

    private void Start()
    {
        weedSpawnInterval = adjustedSpawnInterval;
        countdownCounter = 5f;
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (growState < 3)
        {
            CountDownToSpawn();
            IncreaseSpawnInterval();
        }
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
        if (harderByTime)
        {
            
        }
    }
    public void HitBySunFlower(Vector2 hitLocation)
    {
        Instantiate(thrownSunflower, hitLocation, transform.rotation);
        if (harderByTime)
        {

        }
    }
}
