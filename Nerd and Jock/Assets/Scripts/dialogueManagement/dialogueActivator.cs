using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueActivator : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject other;
    public float activationRange = 1f;


    // Update is called once per frame
    void Update()
    {
         // Check if the other GameObject exists
        if (other == null || other.gameObject == null)
        {
            dialogue.SetActive(false);
            return;
        }

        // Calculate distance between player and object
        float distanceToOther = Vector2.Distance(other.transform.position, transform.position);

        // Check if player is in activation range
        if (distanceToOther > activationRange)
        {
            dialogue.SetActive(false);
        }
        else
        {
            // Activate the dialogue object
            dialogue.SetActive(true);
        }
    }
}
