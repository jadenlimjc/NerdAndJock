using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueActivator : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject player;
    public float activationRange = 5f;


    // Update is called once per frame
    void Update()
    {
        //calculate distance between player and object
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        Debug.Log("distance to player: " + distanceToPlayer);

        //check if player in activation range
        if (distanceToPlayer < activationRange) {
            dialogue.SetActive(true);
        }
        else
            {
                // Deactivate the target object
                dialogue.SetActive(false);
            }
    }
}
