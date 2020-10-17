using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float knockBackForce;
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;

    //Finite State Machine Variables
    AIStates state = AIStates.PatrolLeft;

    //Patrol State Variables
    Vector3 startingPosition;
    [SerializeField] Vector3 leftPatrolPoint;
    [SerializeField] Vector3 rightPatrolPoint;

    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == (int)Layers.Player)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.SetHurtState(knockBackForce, transform);
        }
    }

    void Update()
    {
        RunAI();    
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(leftPatrolPoint + transform.position, .25f);
        Gizmos.DrawLine(transform.position, leftPatrolPoint + transform.position);
        Gizmos.DrawWireSphere(rightPatrolPoint + transform.position, .25f);
        Gizmos.DrawLine(transform.position, rightPatrolPoint + transform.position);
    }


    void RunAI()
    {
        switch(state)
        {
            case AIStates.PatrolRight:
                PatrolRight();
                break;
            case AIStates.PatrolLeft:
                PatrolLeft();
                break;
            case AIStates.PatrolStop:
                PatrolStop();
                break;
            default:
                break;
        }
    }

    void PatrolRight()
    {
        
    }
    void PatrolLeft()
    {
        if (leftPatrolPoint.x + startingPosition.x <= transform.position.x)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        else
            state = AIStates.PatrolStop;
    }
    void PatrolStop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }


}
