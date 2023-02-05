using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownGoat : MonoBehaviour
{
    void Start()
    {
        
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant") || collision.CompareTag("PlantGrown") || collision.CompareTag("PlantThrown"))
        {
            Destroy(collision.gameObject);
        }
        
    }
   

}
