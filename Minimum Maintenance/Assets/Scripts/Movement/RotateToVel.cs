using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateToVel : MonoBehaviour
{
    private Movement move;
    private float rotationSpeed = 10;


    private void Start()
    {
        move = GetComponentInParent<Movement>();
    }

    private void Update()
    {

        if(move.dir != Vector2.zero)
        {
            quaternion toRot = Quaternion.LookRotation(move.dir, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(transform.localRotation, toRot, rotationSpeed * Time.deltaTime);
        }

    }

}
