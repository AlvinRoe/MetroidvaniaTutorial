using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Component References
    Rigidbody2D rb;
    SpriteRenderer render;
    Animator anim;
    CapsuleCollider2D coll;

    //Serialized Fields
    [SerializeField] int speed = 2;
    [SerializeField] int jumpPower = 10;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPoint;
    [SerializeField] Collider2D[] hitCollider;


    //Other variables
    PlayerStates state = PlayerStates.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        RunFSM();
    }

    #region Finite State Machine
    //Other FSM Functions
    void RunFSM()
    {
        switch (state)
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
            case PlayerStates.GroundAttack:
                GroundAttackState();
                break;
            case PlayerStates.AirAttack:
                AirAttackState();
                break;
            default:
                break;
        }
    }
    void ChangeState(PlayerStates nextState)
    {
        switch (nextState)
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
            case PlayerStates.GroundAttack:
                anim.Play("GroundAttack");
                break;
            case PlayerStates.AirAttack:
                anim.Play("AirAttack");
                break;
            default:
                break;
        }
        state = nextState;
    }

    //States
    void IdleState()
    {
        CanRun();
        CanJump();
        CanFall();
        CanGroundAttack();
    }
    void RunState()
    {
        HorizontalInput();
        CanStop();        
        CanJump();
        CanFall();
        CanGroundAttack();
    }
    void JumpState()
    {
        HorizontalInput();
        CanFall();
        CanAirAttack();
    }
    void FallState()
    {
        HorizontalInput();
        CanLand();
        CanAirAttack();
    }
    void GroundAttackState()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);        
    }
    void AirAttackState() { }
    //Building Block Functions
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
        if(coll.IsTouchingLayers(groundLayer))
            ChangeState(PlayerStates.Idle);            
    }
    void CanGroundAttack()
    {
        if (Input.GetButtonDown("Attack"))
            ChangeState(PlayerStates.GroundAttack);
    }
    void CanAirAttack()
    {
        //if the attack is pressed then change to the air attack state
        if (Input.GetButtonDown("Attack"))
            ChangeState(PlayerStates.AirAttack);
    }
    #endregion


    #region Animation Events
    void EndGroundAttack()
    {
        ChangeState(PlayerStates.Idle);
    }
    void EndAirAttack()
    {
        if (coll.IsTouchingLayers(groundLayer))
            ChangeState(PlayerStates.Idle);
        else if (rb.velocity.y > 0)
            ChangeState(PlayerStates.Jump);
        else
            ChangeState(PlayerStates.Fall);        
    }
    void CheckAttackCollisions()
    {
        hitCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
    }


    #endregion
}
