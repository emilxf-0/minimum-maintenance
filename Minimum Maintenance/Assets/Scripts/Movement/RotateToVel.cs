using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateToVel : MonoBehaviour
{
    private Movement move;
    public float rotationSpeed = 1000;


    private void Start()
    {
        move = GetComponentInParent<Movement>();
    }

    private void Update()
    {

        if(move.dir != Vector2.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, move.dir);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

    }

}
