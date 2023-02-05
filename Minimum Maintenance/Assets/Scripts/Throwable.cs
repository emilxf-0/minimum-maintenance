using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    private const string KEY_TAG_PLAYER = "Player";
    private const string KEY_TAG_OUTOFBOUNDS = "OutOfBounds";
    private const string KEY_TAG_GROUND = "Ground";
    private const string KEY_TAG_GROWNWEED = "PlantGrown";
    private const string KEY_TAG_THROWABLE = "PlantThrown";

    public GameObject dirtSpray;

    public bool isSunflower = false;
    public bool isGoat = false;

    public void InvokeLanding(float timeToStart)
    {
        Invoke(nameof(LandingCheck), timeToStart);
    }

    private void LandingCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 1f);

        if (hit.transform.CompareTag(KEY_TAG_PLAYER))
        {
            TrySpawnGoat(hit);
            Movement movement = hit.collider.GetComponent<Movement>();
            movement.Stun();
            Destroy(gameObject);
            MusicManager musicManager = FindObjectOfType<MusicManager>();
            musicManager.PlayImpact();
            
            if (dirtSpray != null)
                Instantiate(dirtSpray, hit.transform.gameObject.transform.position + Vector3.up, Quaternion.identity);
        }
        else if (hit.transform.CompareTag(KEY_TAG_GROUND))
        {
            TrySpawnGoat(hit);
            GardenPlot garden = hit.collider.GetComponent<GardenPlot>();
            if(isSunflower == true)
            {
                garden.HitBySunFlower(gameObject.transform.position);
            }
            else if(isGoat == false)
                garden.HitByWeed(gameObject.transform.position);
            Destroy(gameObject);
        }
        else if(hit.transform.CompareTag(KEY_TAG_OUTOFBOUNDS))
        {
            Destroy(gameObject);
        }
        else if (hit.transform.CompareTag(KEY_TAG_GROWNWEED) || hit.transform.tag == KEY_TAG_THROWABLE)
        {
            TrySpawnGoat(hit);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
       
    }
    private void TrySpawnGoat(RaycastHit2D hit)
    {
        if (isGoat == true)
        {
            
            GardenPlot garden = FindObjectOfType<GardenPlot>();
            garden?.HitBySunGoat(gameObject.transform.position);
        }
    }
}
