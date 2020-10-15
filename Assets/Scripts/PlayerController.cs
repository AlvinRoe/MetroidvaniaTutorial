using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Component References
    Rigidbody2D rb;
    SpriteRenderer render;

    //Serialized Fields
    [SerializeField] int speed = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");        
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
                
        if(hInput > 0) 
            render.flipX = false;
        else if(hInput < 0)
            render.flipX = true;
    }

}
