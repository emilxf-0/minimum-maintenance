using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownGoat : MonoBehaviour
{
    public GameObject dirtSpray;
    void Start()
    {
        
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant") || collision.CompareTag("PlantGrown") || collision.CompareTag("PlantThrown"))
        {
            Vector2 dirtLocation = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
            Destroy(Instantiate(dirtSpray, dirtLocation, Quaternion.identity), .2f);
        }
        
    }
   

}
