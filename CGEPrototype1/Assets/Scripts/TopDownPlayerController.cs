using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    // Adjust value in inspector to set player movement speed
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values for horizontal/vertical movement
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        // Move player using Rigidbody2D in FixedUpdate
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }
}
