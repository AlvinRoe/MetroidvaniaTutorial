using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(2, rb.velocity.y);
        }
        
        
    }

}
