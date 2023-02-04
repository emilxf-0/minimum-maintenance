using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public string player = "Player1";
   
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
        if(rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
            anim.SetBool("isRunning", true);
           // ChangeAnimationState($"{player}_Run");
        }
        else if(rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
            anim.SetBool("isRunning", true);
           // ChangeAnimationState($"{player}_Run");
        }
        else //if player is not trying to dig something up
        {
            anim.SetBool("isRunning", false);
           // ChangeAnimationState($"{player}_Idle");
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
