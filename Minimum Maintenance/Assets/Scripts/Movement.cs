using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb = null;

    [Header("Inputs")] 

    public string horizontal = "Horizontal";
    public string vertical = "Vertical";
    public string dash = "Jump";

    [Header("Properties")]
    public float movementSpeed = 3f;
    public float dashForce = 10f;

    float dirx;
    float diry;
    Vector2 dir = Vector2.zero;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirx = Input.GetAxisRaw(horizontal);
        diry = Input.GetAxisRaw(vertical);
        dir = new Vector2(dirx, diry).normalized * movementSpeed;

        if (Input.GetButton(dash))
        {
            Dash();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = dir;
    }
    private void Dash()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y) * dashForce;
    }
}
