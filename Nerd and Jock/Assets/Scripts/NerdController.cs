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

    private GameObject currentInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        Interact();
    }

    // move left and right function
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

    //jump function
    void Jump()
    {
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck is not assigned.");
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded)
        {
            Debug.Log("Player is grounded");
        }
        else
        {
            Debug.Log("Player is not grounded");
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Interact()
    {
        if (currentInteractable != null && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Interacted with: " + currentInteractable.gameObject.name);

            //call  OnInteract
            currentInteractable.GetComponent<InteractableObject>().OnInteract();
        }
    }

    //enable interact if within collider of object and object tag is nerdInteract
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("nerdInteract"))
        {
            currentInteractable = other.gameObject;
        }
    }


    //disable interact when out of range with object
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("nerdInteract"))
        {
            if (currentInteractable == other.gameObject)
            {
                currentInteractable = null;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        }
    }



}
