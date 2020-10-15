using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer render;
    public int speed = 2;

    void Start()
    {
        
    }

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");        
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
                
        //Check to see if we are moving right
        if(hInput > 0)
        {
            render.flipX = false;
        }
        //Check to see if we are moving left
        else if(hInput < 0)
        {
            render.flipX = true;
        }
    }

}
