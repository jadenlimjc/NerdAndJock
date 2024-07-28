using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject nerd;
    public GameObject jock;
    // Start is called before the first frame update
    private Vector3 offset = new Vector3(0,0,-3);


    void LateUpdate()
    {
        // Check if nerd or jock is null
        if (nerd == null)
        {
            nerd = GameObject.FindGameObjectWithTag("nerd");
        }

        if (jock == null)
        {
            jock = GameObject.FindGameObjectWithTag("jock");
        }

        // Check if both players are not null
        if (nerd != null && jock != null)
        {
            // Follow the midpoint between nerd and jock
            transform.position = (nerd.transform.position + jock.transform.position) / 2 + offset;
        }
    }
}
