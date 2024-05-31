using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    // Start is called before the first frame update
    private Vector3 offset = new Vector3(0,0,-3);


    void Start()
    {
    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = (player1.transform.position 
                             + player2.transform.position) / 2 
                             + offset;
    }
}
