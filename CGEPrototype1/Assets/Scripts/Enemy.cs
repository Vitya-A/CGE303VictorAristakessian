using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // The enemy's health
    public int health = 100;

    // A prefab to spawn when the enemy is defeated
    public GameObject deathEffect;

    // A reference to the health bar
    private DisplayBar healthBar;

    // The damage the enemy deals to the player
    public int damage = 10;

    private void Start()
    {
        // Find the health bar in the children of the enemy
        healthBar = GetComponentInChildren<DisplayBar>();

        if (healthBar == null)
        {
            Debug.LogError("HealthBar (DisplayBar script) was not found.");
            return;
        }

        // Set the max value of the health bar to the enemy's health
        healthBar.SetMaxValue(health);
    }

    // A function to take damage
    public void TakeDamage(int damage)
    {
        // Subtract damage taken from health
        health -= damage;

        // Update the health bar
        healthBar.SetValue(health);

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

    // Damage the player if the enemy collides with them
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player health script from the player object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Check if the player health script is null
            if (playerHealth == null)
            {
                // Log an error if the player health script is null
                Debug.LogError("PlayerHealth script not found on player.");
                return;
            }

            // Damage the player 
            playerHealth.TakeDamage(damage);

            // Knock the player back
            playerHealth.Knockback(transform.position);
        }
    }
}
