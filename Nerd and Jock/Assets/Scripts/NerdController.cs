using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of left/right movement
    public float jumpForce = 4f; // Force applied for jumping
    public Transform groundCheck; // Empty GameObject to check if the player is on the ground
    public LayerMask groundLayer; // Layer mask to specify what is considered ground

    public int maxJumps  = 3; //set max no. of jumps

    private int jumpCount; //current no. of jumps

    private Rigidbody2D rb;
    private bool isGrounded;

    private GameObject currentInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = maxJumps; //initialise jump count
    }

    void Update()
    {
        Move();
        Jump();
        Interact();
    }

    void RestrictPositionWithinCameraBounds()
    {
        Camera cam = Camera.main;
        if (cam != null && cam.orthographic) 
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            // Min and max boundaries
            float minX = cam.transform.position.x - width/2;
            float maxX = cam.transform.position.x + width/2;
            float minY = cam.transform.position.y - height/2;
            float maxY = cam.transform.position.y + height/2;

            Vector2 characterPosition = transform.position;
            characterPosition.x = Mathf.Clamp(characterPosition.x, minX + 0.5f, maxX - 0.5f);
            characterPosition.y = Mathf.Clamp(characterPosition.y, minY + 0.5f, maxY - 0.5f);
            transform.position = characterPosition;
        }
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

        // Calculate new position
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        RestrictPositionWithinCameraBounds();
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

        if (isGrounded) {
            jumpCount = maxJumps; //reset jumpCount when on ground
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpCount > 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;  //decrease jumpCount after each jump
        }

        RestrictPositionWithinCameraBounds();
    }

    void Interact()
    {
        if (currentInteractable != null && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Interacted with: " + currentInteractable.gameObject.name);

            //call  OnInteract
            currentInteractable.GetComponent<InteractableLaptop>().OnInteract();
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
