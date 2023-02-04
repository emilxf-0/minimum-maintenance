using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Observers
    

    //Components
    Rigidbody2D rb = null;

    [Header("Inputs")]
    [Range(1, 2)] public int player;
    

    [SerializeField] string horizontal = "Horizontal";
    [SerializeField] string vertical = "Vertical";
    [SerializeField] string dash = "Jump";

    [Header("Properties")]
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float dashingMultiplyer = 10f;
    [SerializeField] float dashDuration = .25f;
    [SerializeField] float dashCoolDown = .25f;

    [Header("Effects")]
    [SerializeField] float stunDuration = .5f;
    public GameObject dashEffect;
    //Movement
    float dirx;
    float diry;
    Vector2 dir = Vector2.zero;
    Vector2 lastDir = new Vector2(1, 0);

    // Dashing
    bool dashInput;
    bool isDashing = false;
    bool canDash = true;

    //Conditions
    [HideInInspector] public bool isUpRooting = false;
    [HideInInspector] public bool isStunned = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontal += player.ToString();
        vertical += player.ToString();
        dash += player.ToString();
    }

    void Update()
    {
        //assignment of variables
        dirx = Input.GetAxisRaw(horizontal);
        diry = Input.GetAxisRaw(vertical);
        dashInput = Input.GetButtonDown(dash);

        if (isDashing == false && isUpRooting == false && isStunned == false) //cant control while dashing, maybe want to cancel dashing if youre dasahing into a wall s
        {
            dir = new Vector2(dirx, diry).normalized;
        }

        


        if (dir != Vector2.zero)
        {
            lastDir = dir;
        }

        if (dashInput == true && canDash == true)
        {
            isUpRooting = false;
            isDashing = true;
            canDash = false;
           
            SpawnDash();
        }
        
    }
    private void FixedUpdate()
    {
        if (isUpRooting == true || isStunned == true)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = dir * movementSpeed;
        }

        if (isDashing == true)
        {
            Dash();
            return;
        }
    }
    private void Dash() //is a speedup but you cant steer during it
    {
        rb.velocity = lastDir * dashingMultiplyer;
        IEnumerator stopDashing()
        {
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;

            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }
        StartCoroutine(stopDashing());
    }
    public void Stun()
    {
        IEnumerator StunMe()
        {
            isStunned = true;
            yield return new WaitForSeconds(stunDuration);
            isStunned = false;
        }
        StartCoroutine(StunMe());
    }
    private void SpawnDash()
    {
        bool shouldFlip = false;
        if(rb.velocity.x < 0)
        {
            shouldFlip = true;
        }
        else
        {
            shouldFlip = false;
        }
        GameObject dash = Instantiate(dashEffect,transform.position,Quaternion.identity);
        dash.GetComponent<SpriteRenderer>().flipX = shouldFlip;
        Destroy(dash, .4f);
    }
}
