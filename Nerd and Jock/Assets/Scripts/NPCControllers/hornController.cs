using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hornController : MonoBehaviour
{
    public float speed = 0.8f;
    public float leftBound;
    public float rightBound;
    private bool movingLeft = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("jock") || other.gameObject.CompareTag("nerd"))
        {
            // Check if the collision is from above
            if (IsCollisionFromAbove(other))
            {
                Destroy(gameObject); // Destroy the horn
            }
            else
            {
                // If not from above, trigger respawn logic
                other.gameObject.SendMessage("StartRespawn");
            }
        }
    }

    bool IsCollisionFromAbove(Collision2D collision)
    {
        // Get the collision point
        ContactPoint2D contact = collision.GetContact(0);
        // Get the relative velocity to determine if the character is jumping
        Vector2 relativeVelocity = collision.relativeVelocity;
        // Check if character is falling from a jump
        bool isFalling = relativeVelocity.y < 0;
        // Compare the positions
        bool isAbove = contact.point.y > transform.position.y;
        return isFalling && isAbove;
    }


}
