using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public LayerMask groundLayer;
    public Transform groundCheck1;
    public Transform groundCheck2;
    // Start is called before the first frame update
    private Vector3 offset = new Vector3(0,0,-3);
    private bool isP1Grounded = false;
    private bool isP2Grounded = false;


    void Start()
    {
    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float midX = (player1.transform.position.x 
                    + player2.transform.position.x) / 2;
                    
        float midY = transform.position.y;
        /* transform.position.x = (player1.transform.position.x 
                             + player2.transform.position.x) / 2 
                             + offset; */
        isP1Grounded = Physics2D.OverlapCircle(groundCheck1.position, 0.1f, groundLayer);
        isP2Grounded = Physics2D.OverlapCircle(groundCheck2.position, 0.1f, groundLayer);
        if (isP1Grounded && isP2Grounded) {
            midY = (player1.transform.position.y 
                    + player2.transform.position.y) / 2; 
        }

        transform.position = new Vector3(midX, midY, offset.z);
    }
}
