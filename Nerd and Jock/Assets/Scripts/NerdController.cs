using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of left/right movement
    public float jumpForce = 4f; // Force applied for jumping
    public Transform groundCheck; // Empty GameObject to check if the player is on the ground
    public LayerMask groundLayer; // Layer mask to specify what is considered ground

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }

        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;
    }

    void Jump()
    {
        if (groundCheck == null)
        {
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
