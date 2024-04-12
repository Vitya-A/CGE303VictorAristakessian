using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Rigidbody component for the projectile
    private Rigidbody2D rb;

    // Speed at which the projectile will move
    public float speed = 20f;

    // Damage the projectile will deal
    public int damage = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set the velocity of the projectile to move right
        rb.velocity = transform.right * speed;
    }

    //When the projectile collides with another object
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Get the Enemy component of the object that was hit
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        // If the object was hit has an Enemy component
        if (enemy != null)
        {
            // Call the TakeDamage function
            enemy.TakeDamage(damage);
        }

        // Ensures the player collision does not affect the projectile (FirePoint is within the player)
        if (hitInfo.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
