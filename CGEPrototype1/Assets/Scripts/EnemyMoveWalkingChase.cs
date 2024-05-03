using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Require a Rigidbody2D and Animator
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class EnemyMoveWalkingChase : MonoBehaviour
{
    // Range at which the enemy will chase the player
    public float chaseRange = 4f;

    // Speed of the enemy movement
    public float enemyMoveSpeed = 1.5f;

    // Transform of the player object
    private Transform playerTransform;

    // Rigidbody/Animator components of the enemy
    private Rigidbody2D rb;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D/Animator components of the enemy
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Get the player transform using the "Player" tag
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // This Vector2 is a 2D arrow from the enemy to the player
        Vector2 playerDirection = playerTransform.position - transform.position;

        // Distance between the enemy and the player
        // The magnitude of the vector is the distance without the direction
        float distanceToPlayer = playerDirection.magnitude;

        // Check if the player is within chase range
        if (distanceToPlayer <= chaseRange)
        {
            // We need the direction to move across the x-axis

            // Normalize gives the direction to the player without the distance
            playerDirection.Normalize();

            // Setting the y-axis to 0 to ensure only x-axis movement
            playerDirection.y = 0f;

            // Rotate the enemy to face the player
            FacePlayer(playerDirection);

            // If there is ground in the way...
            if (IsGroundAhead())
            {
                // ...move in the direction of the player
                MoveTowardsPlayer(playerDirection);
            } else
            {
                // ...don't move
                StopMoving();
            }
        } else
        {
            StopMoving();
        }
    }

    // Checks if there is ground ahead of the enemy to walk on
    bool IsGroundAhead()
    {
        // Ground check variables
        float groundCheckDistance = 2.0f; // Adjust as needed
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        // Determine which direction the enemy is facing
        Vector2 enemyFacingDirection = (transform.rotation.y == 0) ? Vector2.left : Vector2.right;

        // Raycast to check for ground ahead of the enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down + enemyFacingDirection, groundCheckDistance, groundLayer);

        // Return true if the raycast detected a collider
        return hit.collider != null;
    }

    // Checks which direction the player is from the enemy and adjusts the transform accordingly
    private void FacePlayer(Vector2 playerDirection)
    {
        transform.rotation = (playerDirection.x < 0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }

    // Moves enemy in the direction of the player and adjusts the animation accordingly
    private void MoveTowardsPlayer(Vector2 playerDirection)
    {
        // Move the enemy towards the player by setting the velocity to a new Vector2 without changing its y-axis
        rb.velocity = new Vector2(playerDirection.x * enemyMoveSpeed, rb.velocity.y);

        // Set the animator parameter to move
        anim.SetBool("isMoving", true);
    }

    // Stops moving enemy in the direction of the player and adjusts the animation accordingly
    private void StopMoving()
    {
        // Stop moving if the player is out of range
        rb.velocity = new Vector2(0, rb.velocity.y);

        // Set the animator parameter to stop moving
        anim.SetBool("isMoving", false);
    }
}
