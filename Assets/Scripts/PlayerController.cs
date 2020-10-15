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
        switch(state)
        {
            case PlayerStates.Idle:
                IdleState();
                break;
            case PlayerStates.Run:
                RunState();
                break;
            case PlayerStates.Jump:
                JumpState();
                break;
            case PlayerStates.Fall:
                FallState();
                break;
            default:
                break;
        }
    }
    void IdleState()
    {
        CanRun();
        CanJump();
        CanFall();
    }
    void RunState()
    {
        HorizontalInput();
        CanStop();        
        CanJump();
        CanFall();
    }
    void JumpState()
    {
        HorizontalInput();
        CanFall();        
    }
    void FallState()
    {
        HorizontalInput();
        CanLand();        
    }

    void ChangeState(PlayerStates nextState)
    {
        switch(nextState)
        {
            case PlayerStates.Idle:
                anim.Play("Idle");
                break;
            case PlayerStates.Run:
                anim.Play("Run");
                break;
            case PlayerStates.Jump:
                anim.Play("Jump");
                break;
            case PlayerStates.Fall:
                anim.Play("Fall");
                break;
            default:
                break;
        }
        state = nextState;
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
    void CanJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            ChangeState(PlayerStates.Jump);
        }
    }
    void CanRun()
    {
        float hInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(hInput) > Mathf.Epsilon)
            ChangeState(PlayerStates.Run);
    }
    void CanStop()
    {
        if (Mathf.Abs(rb.velocity.x) < Mathf.Epsilon)
            ChangeState(PlayerStates.Idle);
    }
    void CanFall()
    {
        if (rb.velocity.y < -.001)
            ChangeState(PlayerStates.Fall);
    }
    void CanLand()
    {
        if (rb.velocity.y >= 0)
            ChangeState(PlayerStates.Idle);
    }
    #endregion
}
