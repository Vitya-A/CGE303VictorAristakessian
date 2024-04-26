using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // The player's health
    public int health = 100;

    // A prefab to spawn when the enemy is defeated
    public GameObject playerDeathEffect;

    // A reference to the health bar
    public DisplayBar healthBar;

    private Rigidbody2D rb;

    // Knockback force when the player collides with an enemy
    public float knockbackForce = 5f;

    // A bool to keep track of whether the player has been hit recently
    public static bool hitRecently = false;

    // Time in seconds to recover when a player has been hit
    public float hitRecoveryTime = 0.2f;

    private AudioSource playerAudio;
    public AudioClip playerHitSound;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Set all component references
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Check if the rb is null
        if (rb == null)
        {
            // Log an error if the rb is null
            Debug.LogError("Rigidbody2D not found on player");
        }

        // Set the max value of the health to the player health
        healthBar.SetMaxValue(health);

        // Initialize hitRecently to false
        hitRecently = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // A function to knock the player back when they collide with an enemy
    public void Knockback(Vector3 enemyPosition)
    {
        // Prevents knockback from being again while already in knockback
        if (hitRecently)
        {
            return;
        }

        hitRecently = true;
        
        if (gameObject.activeSelf)
        {
            // Start the coroutine to reset hitRecently
            StartCoroutine(RecoverFromHit());
        }

        // Calculate the direction of the knockback
        Vector2 direction = transform.position - enemyPosition;

        // Normalize the direction vector
        // This gives a consistent knockback force regardless of the distance between player and enemy
        direction.Normalize();

        // Add upward direction to the knockback
        direction.y = direction.y * 0.5f + 0.5f;

        // Add force to the player in the direction of the knockback
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    // Coroutine to reset hitRecently after hitRecoveryTime seconds
    IEnumerator RecoverFromHit()
    {
        // Wait for the amount of time specified by hitRecoveryTime
        yield return new WaitForSeconds(hitRecoveryTime);

        // Set hitRecently to false
        hitRecently = false;

        // Cancel the hit animation
        animator.SetBool("hit", false);
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
            // Player dies
            Die();
        } else
        {
            // Play the player hit SFX/animation
            playerAudio.PlayOneShot(playerHitSound);
            animator.SetBool("hit", true);
        }
    }

    // A function to die
    public void Die()
    {
        // Declare game over
        ScoreManager.gameOver = true;

        // Instantiate death effect, then destroy after 2 seconds
        GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);
        //Destroy(deathEffect, 2f);

        // Disable player
        gameObject.SetActive(false);
    }
}
