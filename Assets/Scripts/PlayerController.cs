﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        rb.velocity = new Vector2(2, 0);
    }

}
