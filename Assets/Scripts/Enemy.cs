using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float knockBackForce;
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;
    Animator anim;

    //Finite State Machine Variables
    AIStates state = AIStates.PatrolLeft;

    //Patrol State Variables
    Vector3 startingPosition;
    [SerializeField] Vector3 leftPatrolPoint;
    [SerializeField] Vector3 rightPatrolPoint;
    [SerializeField] float patrolStopTime = 2f;

    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        if (rightPatrolPoint.x + startingPosition.x >= transform.position.x)
            rb.velocity = new Vector2(speed, rb.velocity.y);
        else
        {
            state = AIStates.PatrolStop;
            StartCoroutine(StopForSeconds(patrolStopTime, AIStates.PatrolLeft));
        }
    }
    void PatrolLeft()
    {
        if (leftPatrolPoint.x + startingPosition.x <= transform.position.x)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        else
        {
            state = AIStates.PatrolStop;
            StartCoroutine(StopForSeconds(patrolStopTime, AIStates.PatrolRight));
        }
    }
    void PatrolStop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        anim.speed = 0;
    }

    IEnumerator StopForSeconds(float seconds, AIStates nextState)
    {
        yield return new WaitForSeconds(seconds);
        state = nextState;
        transform.localScale = nextState == AIStates.PatrolLeft ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        anim.speed = 1;
    }


}
