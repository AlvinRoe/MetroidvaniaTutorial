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
        float hInput = Input.GetAxis("Horizontal");
        float vSpeed = Input.GetButtonDown("Jump") ? jumpPower : rb.velocity.y;
                
        if(hInput > 0) 
            render.flipX = false;
        else if(hInput < 0)
            render.flipX = true;

        rb.velocity = new Vector2(hInput * speed, vSpeed);

        if (rb.velocity.y > .5)
            anim.Play("Jump");
        else if (rb.velocity.y < -.5)
            anim.Play("Fall");
        else if (Mathf.Abs(rb.velocity.x) > 0)
            anim.Play("Run");
        else
            anim.Play("Idle");
    }

}
