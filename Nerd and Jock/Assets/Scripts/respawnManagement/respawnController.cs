using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnController : MonoBehaviour
{
    //trigger respawn dialogue
    public static respawnController Instance;
    [SerializeField] private tutorialDialogueManager dialogueManager;
    [TextArea]
    [SerializeField] private string[] dialogueSentences;
    private static bool dialogueStarted = false;
    public float lowerBound = -10.0f;
    public float respawnTimer = 1.0f;
    // Start is called before the first frame update
    public Vector2 spawn = new Vector2(0, 0);
    public Transform groundCheck; 
    public LayerMask groundLayer;
    private bool isRespawning = false;
    private Animator animator;
    private Rigidbody2D rb;
    private MonoBehaviour movementScript; 
    private Collider2D col;
    private bool isDying = false;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        movementScript = GetComponent<MonoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isRespawning && (transform.position.y < lowerBound || (!isWithinCameraView() && isGrounded())))
        {
            StartCoroutine(respawnAfterDelay());
            
            
        }

    }

    private bool isWithinCameraView()
    {
        Camera cam = Camera.main;
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
        return screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    private bool isGrounded() 
    {
        if (groundCheck == null) 
        {
            return false;
        }

        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    public IEnumerator respawnAfterDelay()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnTimer);
        gameObject.transform.position = spawn;
        isRespawning = false;
        // trigger dialogue upon first respawn
        if (!dialogueStarted) {
                dialogueStarted = true;
                dialogueManager.StartDialogue(dialogueSentences);
            }
    }

    public IEnumerator respawnAfterDelayWithAnimation()
    {
        if (isDying) yield break; // Prevent multiple triggers of death animation

        isDying = true;
        isRespawning = true;
        animator.SetTrigger("Die"); // Trigger the death animation
        rb.velocity = Vector2.zero; // Stop movement
        rb.isKinematic = true; // Disable physics interactions
        col.enabled = false; // Disable collider to prevent other interactions
        movementScript.enabled = false; // Disable movement script

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for animation to finish
        yield return new WaitForSeconds(respawnTimer);

        gameObject.transform.position = spawn;
        rb.isKinematic = false; // Re-enable physics interactions
        col.enabled = true; //Re-enable collider
        movementScript.enabled = true; // Enable the movement script
        animator.SetTrigger("Idle"); // Trigger the idle animation to reset the state
        isDying = false; // Reset flag
        isRespawning = false; // Reset flag
    }

    void StartRespawn()
    {
        if (!isDying && !isRespawning)
        {
            StartCoroutine(respawnAfterDelayWithAnimation());
        }
    }


}
