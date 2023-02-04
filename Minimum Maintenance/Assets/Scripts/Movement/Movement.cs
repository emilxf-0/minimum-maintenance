using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Components
    Rigidbody2D rb = null;

    [Header("Inputs")] 

    [SerializeField] string horizontal = "Horizontal";
    [SerializeField] string vertical = "Vertical";
    [SerializeField] string dash = "Jump";

    [Header("Properties")]
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float dashingMultiplyer = 10f;
    [SerializeField] float dashDuration = .25f;
    [SerializeField] float dashCoolDown = .25f;

    //Movement
    float dirx;
    float diry;
    Vector2 dir = Vector2.zero;
    Vector2 lastDir = new Vector2(1, 0);
    //add last inputted direction

    // Dashing
    bool dashInput;
    bool isDashing = false;
    bool canDash = true;

    //Conditions
    [HideInInspector] public bool isUpRooting = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirx = Input.GetAxisRaw(horizontal);
        diry = Input.GetAxisRaw(vertical);
        dashInput = Input.GetButton(dash);
        if (isDashing == false && isUpRooting == false) //cant control while dashing, maybe want to cancel dashing if youre dasahing into a wall s
        {
            dir = new Vector2(dirx, diry).normalized * movementSpeed; 
        }

        
        if(dir != Vector2.zero)
        {
            lastDir = dir;
        }

        if (dashInput && canDash)
        {
            isUpRooting = false;
            isDashing = true;
            canDash = false;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = dir;
        if (isDashing)
        {
            Dash();
            return;
        }
    }
    private void Dash()
    {
        rb.velocity = lastDir * dashingMultiplyer; // add deltaTime
        IEnumerator stopDashing()
        {
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;

            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }
        StartCoroutine(stopDashing());
    }

}
