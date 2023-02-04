using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GardenPlot : MonoBehaviour
{
    [SerializeField] private BoxCollider2D topCollider;
    [SerializeField] private BoxCollider2D bottomCollider;
    [SerializeField] private BoxCollider2D leftCollider;
    [SerializeField] private BoxCollider2D rightCollider;
    [SerializeField] private GameObject weedObj;
    [SerializeField] private float adjustedSpawnInterval = 5f;
    
    private float weedSpawnInterval;

    private void Start()
    {
        weedSpawnInterval = adjustedSpawnInterval;
    }

    private void Update()
    {
        CountDownToSpawn();
        IncreaseSpawnInterval();
    }

    private void IncreaseSpawnInterval()
    {
        //Increases spawns successively as the match progresses
        
    }

    private void CountDownToSpawn()
    {
        if (weedSpawnInterval <= 0)
        {
            ChooseSpawnDirection();
            weedSpawnInterval = adjustedSpawnInterval;
        }
        else
            weedSpawnInterval = weedSpawnInterval - 1 * Time.deltaTime;
    }

    private void ChooseSpawnDirection()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                SpawnWeed(1);
                break;
            case 1:
                SpawnWeed(2);
                break;
            case 2:
                SpawnWeed(3);
                break;
            default:
                SpawnWeed(4);
                break;
        }
    }

    
    private void SpawnWeed(int direction)
    {
    //Direction:
    //1 - up
    //2 - down
    //3 - left
    //4 - right
        
        switch (direction)
        {
            case 1:
                Bounds spawnAreaTop = topCollider.bounds;
                Instantiate(weedObj, RandomizeSpawnPosition(spawnAreaTop), gameObject.transform.rotation);
                break;
            case 2:
                Bounds spawnAreaBottom = bottomCollider.bounds;
                Instantiate(weedObj, RandomizeSpawnPosition(spawnAreaBottom), gameObject.transform.rotation);
                break;
            case 3:
                Bounds spawnAreaLeft = leftCollider.bounds;
                Instantiate(weedObj, RandomizeSpawnPosition(spawnAreaLeft), gameObject.transform.rotation);
                break;
            case 4:
                Bounds spawnAreaRight = rightCollider.bounds;
                Instantiate(weedObj, RandomizeSpawnPosition(spawnAreaRight), gameObject.transform.rotation);
                break;
        }
            
    }

    private Vector2 RandomizeSpawnPosition(Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }
}
