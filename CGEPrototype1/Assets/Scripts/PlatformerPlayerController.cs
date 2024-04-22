using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private float horizontalInput;

    private Animator animator;

    // An audio clip to hold jump sound
    public AudioClip jumpSound;

    // An audio source to play a sound
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values for horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (JumpPad.onJumpPad == true)
            {
                jumpForce = 15f;
            }
            
            // Apply an upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // Play jump sound effect
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            jumpForce = 10f;
            JumpPad.onJumpPad = false;
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerHealth.hitRecently)
        {
            // Move the player using Rigidboy2D in FixedUpdate
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        }

        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        //  Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        animator.SetBool("onGround", isGrounded);

        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
