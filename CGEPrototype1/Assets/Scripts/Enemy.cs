using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // The enemy's health
    public int health = 100;

    // A prefab to spawn when the enemy is defeated
    public GameObject deathEffect;

    // A function to take damage
    public void TakeDamage(int damage)
    {
        // Subtract damage taken from health
        health -= damage;

        // If the resulting health is less than/equal to 0
        if (health <= 0)
        {
            // Enemy dies
            Die();
        }
    }

    // A function to die
    void Die()
    {
        // Instantiate death effect
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Destroy enemy
        Destroy(gameObject);
    }
}
