using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownSunflower : ThrownWeedScript
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0)
            onLeftField = true;
        else
            onLeftField = false;

        StartCoroutine(DoHealing(onLeftField));
    }
    IEnumerator DoHealing(bool onLeft)
    {
        if (onLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(.5f);
                HealthManager.Instance.HealLeftHouse(.05f);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(.5f);
                HealthManager.Instance.HealRightHouse(.05f);
            }    
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    // Update is called once per frame
   

}
