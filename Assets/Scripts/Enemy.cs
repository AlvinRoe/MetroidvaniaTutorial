using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float knockBackForce;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == (int)Layers.Player)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.SetHurtState(knockBackForce, transform);
        }
    }
}
