using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float knockBackForce;

    //Finite State Machine Variables
    AIStates state = AIStates.Patrol;

    //Patrol State Variables
    Vector3 startingPosition;
    [SerializeField] Vector3 leftPatrolPoint;
    [SerializeField] Vector3 rightPatrolPoint;

    void Start()
    {
        startingPosition = transform.position;    
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
            case AIStates.Patrol:
                PatrolState();
                break;
            default:
                break;
        }
    }

    void PatrolState()
    {

    }


}
