using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public string player = "Player1";
    public bool unRooting = false;
    public bool isHolding = false;
    string currentState;
    //fields
    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        anim.SetFloat("velocity",rb.velocity.sqrMagnitude);


        if(rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
           // ChangeAnimationState($"{player}_Run");
        }
        else if(rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
           // ChangeAnimationState($"{player}_Run");
        }

        if (unRooting == true)
        {
            anim.SetBool("unRooting", true);
        }
        else
        {
            anim.SetBool("unRooting", false);
        }

        if(isHolding == true)
        {
            anim.SetBool("unRooting", false);
            anim.SetBool("holding", true);
        }
        else
        {
            anim.SetBool("holding", false);
        }
        
        
    }
    
    //private void ChangeAnimationState(string state)
    //{
    //    if (currentState == state)
    //        return;

    //    anim.Play(state,0);

    //    currentState = state;
    //}
}
