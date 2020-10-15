using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed = 2;

    void Start()
    {
        
    }

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");        
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
        
    }

}
