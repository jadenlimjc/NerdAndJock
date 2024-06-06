using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drillController : MonoBehaviour , IInteractable
{
    public float speed  = 0.8f;
    public float leftBound;
    public float rightBound;
    private bool movingLeft = true;
    public float jumpForce = 5.0f;
    public float jumpIntervalMin = 2.0f; // Minimum time between jumps
    public float jumpIntervalMax = 5.0f; // Maximum time between jumps
    private Rigidbody2D rb;
    private float nextJumpTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ScheduleNextJump();
    }

   // Update is called once per frame
    void Update()
    {
        Move();
        if (Time.time >= nextJumpTime) {
            Jump();
            ScheduleNextJump();
        }
    }

    void Move()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftBound)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.left);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightBound)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.right);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

   void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void ScheduleNextJump()
    {
        nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
    }

    OnInteract() {
        Destroy()
    }
}
