using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeedScript : MonoBehaviour
{
    private int growState;
    private float growTimer;
    
    void Update()
    {
        Grow();
    }

    private void Grow()
    {
        if (growTimer >= 5f && growState <= 2)
        {
            Debug.Log("Weed grown+1");
            transform.localScale = new Vector3(transform.localScale.x + 0.25f, transform.localScale.y + 0.25f,
                transform.localScale.z);
            growState++;
            growTimer = 0f;
        }
        else
            growTimer += 1 * Time.deltaTime;

    }
}
