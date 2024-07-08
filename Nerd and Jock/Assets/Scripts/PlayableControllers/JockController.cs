using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockController : MonoBehaviour
{
    public Animator animator; // Reference animator component
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 2f; // Speed of left/right movement
    public float jumpForce = 4f; // Force applied for jumping
    public Transform groundCheck; // Empty GameObject to check if the player is on the ground
    public LayerMask groundLayer; // Layer mask to specify what is considered ground

    public LayerMask enemyHead; //Layer mask to specify what is considered the enemies' head
    private Rigidbody2D rb;
    private bool isGrounded;
  
    private GameObject currentInteractable;
    private bool isInteracting = false;
    public GameObject exclamation;

      // Fields used for multiple jump method
    /*
    public int maxJumps = 1;

    private int jumpCount;
    */

    void Start()
    {
        exclamation.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Initialise jumpCount for multiple jumps method
        /*
        jumpCount = maxJumps;
        */
    }

    void Update()
    {
        if (!isInteracting) 
        {
            Move();
            Jump();
        }
        Interact();
        UpdateAnimator();
    }

    // Helper function used to restrict sprite's position to within the camera boundaries
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
            //characterPosition.y = Mathf.Clamp(characterPosition.y, minY + 0.5f, maxY - 0.5f);
            transform.position = characterPosition;
        }
    }

    void UpdateAnimator() 
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        // Check if character is grounded 
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("IsJumping", !isGrounded);
    }
    
    // Move method to check input and corresponding lateral movement of sprites
    void Move()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }
        // Calculate new position 
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        // Flip the sprite based on movement direction
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }

        RestrictPositionWithinCameraBounds();
    }

    // Code for multiple jumps
    /*  void Jump()
    {
        if (groundCheck == null)
        {
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        
        if (isGrounded) {
            jumpCount = maxJumps; //reset jumpCount when on ground
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--; //decrease jumpCount after each jump
        }

        RestrictPositionWithinCameraBounds();
    } */

    // Single jump method to check input and corresponding vertical movement of sprites
    void Jump()
    {
        if (groundCheck == null)
        {
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        RestrictPositionWithinCameraBounds();
    }



    // Interact method to check input and corresponding interaction of sprites and objects
    void Interact()
    {
        if (currentInteractable != null && Input.GetKey(KeyCode.Return) && !isInteracting) {
            IInteractable interactable =  currentInteractable.GetComponent<IInteractable>();
            if (interactable != null) {
                StartCoroutine(InteractCoroutine(interactable));
                //interactable.OnInteract();
            }
        }
    }

    // InteractCoroutine method to disrupt movement and interactions when an interaction is called
    IEnumerator InteractCoroutine(IInteractable interactable) 
    {
        isInteracting = true; // Disable movement and interactions
        animator.SetBool("IsInteracting", true); // Start interaction animation
        rb.velocity = Vector2.zero; // Make the sprite stop moving
        rb.isKinematic = true; // Make the sprite unaffected by other sprites
        interactable.OnInteract(); // Trigger the animation for the block immediately
        yield return new WaitForSeconds(3); // Wait for 3 seconds
        rb.isKinematic = false; // Make the sprite unaffected by other sprites
        animator.SetBool("IsInteracting", false); // Stop interaction animation
        isInteracting = false; // Enable movement and interactions
        exclamation.SetActive(false);
        
        if (currentInteractable == (interactable as MonoBehaviour).gameObject) 
        {
            currentInteractable = null;
        }
    }

    // Enable interact if within collider of object and object tag is nerdInteract
    void OnTriggerEnter2D(Collider2D other) {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            if (other.CompareTag("Collectable") || other.CompareTag("Torchlight"))
            {
                interactable.OnInteract();
            }
            else if (other.CompareTag("jockInteract") && isGrounded) 
            {
                exclamation.SetActive(true);
                currentInteractable = other.gameObject;
            }
        }
    }

    
    // Disable interact when out of range with object
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == currentInteractable) {
            currentInteractable = null;
            exclamation.SetActive(false);
        }
    }
    
    // To visualise groundCheck hitbox
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        }
    }

   
}
