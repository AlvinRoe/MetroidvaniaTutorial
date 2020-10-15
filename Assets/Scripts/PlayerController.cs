using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Component References
    Rigidbody2D rb;
    SpriteRenderer render;
    Animator anim;

    //Serialized Fields
    [SerializeField] int speed = 2;
    [SerializeField] int jumpPower = 10;
    [SerializeField] PlayerStates state = PlayerStates.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        RunFSM();
    }

    #region Finite State Machine
    void RunFSM()
    {
        if (state == PlayerStates.Idle)
            IdleState();
        else if (state == PlayerStates.Run)
            RunState();
        else if (state == PlayerStates.Jump)
            JumpState();
        else if (state == PlayerStates.Fall)
            FallState();
    }
    void IdleState()
    {
        anim.Play("Idle");

        float hInput = Input.GetAxis("Horizontal");
        
        if (Mathf.Abs(hInput) > Mathf.Epsilon)
            state = PlayerStates.Run;
        Jump();       
               
    }
    void RunState()
    {
        anim.Play("Run");

        HorizontalInput();

        if (Mathf.Abs(rb.velocity.x) < Mathf.Epsilon)
            state = PlayerStates.Idle;
        Jump();
    }
    void JumpState()
    {
        anim.Play("Jump");

        HorizontalInput();

        if (rb.velocity.y < -.001)
            state = PlayerStates.Fall;
    }
    void FallState()
    {
        anim.Play("Fall");

        HorizontalInput();

        if (rb.velocity.y >= 0)
            state = PlayerStates.Idle;
    }

    void HorizontalInput()
    {
        float hInput = Input.GetAxis("Horizontal");

        if (hInput > 0)
            render.flipX = false;
        else if (hInput < 0)
            render.flipX = true;

        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            state = PlayerStates.Jump;
        }
    }
    #endregion
}
