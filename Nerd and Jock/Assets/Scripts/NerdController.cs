using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float jumpForce = 10;
    private float speed = 10;
    public int jumpCount = 0;
    public bool gameOver = false;
    public float gravityModifier;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount  < 2) {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount += 1;
        }

        while (Input.GetKeyDown(KeyCode.A)) {
            playerRb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        }
        while (Input.GetKeyDown(KeyCode.D)) {
            playerRb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        jumpCount = 0;
    }
}
