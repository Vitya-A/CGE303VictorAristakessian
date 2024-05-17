using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require a Rigidbody2D, Animator, and SpriteRenderer
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyMoveFlyingPatrolChase : MonoBehaviour
{
    // Public references for patrol points
    public GameObject[] patrolPoints;
    private int currentPatrolPointIndex = 0;

    // Public references for movement
    public float speed = 2f;
    public float chaseRange = 3f;

    // Enemy state enum
    public enum EnemyState { PATROLLING, CHASING }

    // Enemy state variable for the enemy's current state
    public EnemyState currentState = EnemyState.PATROLLING;

    // Variables for where the enemy will move
    public GameObject target;
    private GameObject player;

    // Components of the enemy
    private Rigidbody2D rb;
    //private Animator anim;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        // Find transform of player
        player = GameObject.FindWithTag("Player");

        // Get the components of the enemy
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // Check if patrol points are assigned
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points are assigned.");
        }

        // Set initial target to first patrol point
        target = patrolPoints[currentPatrolPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        // Update state based on player/target distance
        UpdateState();

        // Move and face based on current state
        switch (currentState)
        {
            case EnemyState.PATROLLING:
                Patrol();
                break;
            case EnemyState.CHASING:
                ChasePlayer();
                break;
        }

        // Use Debug.DrawLine to draw a line between two points in the Scene view
        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }

    // Update enemy state based on player proximity
    void UpdateState()
    {
        if (IsPlayerInChaseRange() && currentState == EnemyState.PATROLLING)
        {
            currentState = EnemyState.CHASING;
        } else if (!IsPlayerInChaseRange() && currentState == EnemyState.CHASING)
        {
            currentState = EnemyState.PATROLLING;
        }
    }

    bool IsPlayerInChaseRange()
    {
        if (player == null)
        {
            Debug.LogError("Player not found.");
            return false;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        return (distance <= chaseRange);
    }

    void Patrol()
    {
        // If reached current target...
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.5f)
        {
            // ...update target to next patrol point (wrap around)
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        // Set target to current patrol point
        target = patrolPoints[currentPatrolPointIndex];

        MoveTowardsTarget();
    }

    void ChasePlayer()
    {
        target = player;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        // Calculate direction towards target
        Vector2 direction = target.transform.position - transform.position;

        // Normalize direction
        direction.Normalize();

        // Move towards target with rb
        rb.velocity = direction * speed;

        FaceForward(direction);
    }

    private void FaceForward(Vector2 direction)
    {
        sr.flipX = (direction.x > 0);
    }

    // Draw circles for patrol points in the Scene view
    private void OnDrawGizmos()
    {
        if (patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach(GameObject point in patrolPoints)
            {
                Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }
}
