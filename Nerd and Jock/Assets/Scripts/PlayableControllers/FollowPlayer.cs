using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    // Start is called before the first frame update
    private Vector3 offset = new Vector3(0,0,-3);


    void LateUpdate()
    {
        // Check if player1 or player2 is null
        if (player1 == null)
        {
            player1 = GameObject.FindGameObjectWithTag("nerd");
        }

        if (player2 == null)
        {
            player2 = GameObject.FindGameObjectWithTag("jock");
        }

        // Check if both players are not null
        if (player1 != null && player2 != null)
        {
            // Follow the midpoint between player1 and player2
            transform.position = (player1.transform.position + player2.transform.position) / 2 + offset;
        }
    }
}
