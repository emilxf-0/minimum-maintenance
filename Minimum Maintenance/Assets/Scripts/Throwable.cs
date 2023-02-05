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

    public void InvokeLanding(float timeToStart)
    {
        Invoke(nameof(LandingCheck), timeToStart);
    }

    private void LandingCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 1f);

        if (hit.transform.CompareTag(KEY_TAG_PLAYER))
        {
            Movement movement = hit.collider.GetComponent<Movement>();
            movement.Stun();
            Destroy(gameObject);
            MusicManager musicManager = FindObjectOfType<MusicManager>();
            musicManager.PlayImpact();
        }
        else if (hit.transform.CompareTag(KEY_TAG_GROUND))
        {
            GardenPlot garden = hit.collider.GetComponent<GardenPlot>();
            garden.HitByWeed(gameObject.transform.position);
            Destroy(gameObject);
        }
        else if(hit.transform.CompareTag(KEY_TAG_OUTOFBOUNDS))
        {
            Destroy(gameObject);
        }
        else if (hit.transform.CompareTag(KEY_TAG_GROWNWEED) || hit.transform.tag == KEY_TAG_THROWABLE)
            Destroy(gameObject);
        else
            Destroy(gameObject);
    }
}
