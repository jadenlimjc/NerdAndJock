using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntouchableGroundInteractable : MonoBehaviour , IInteractable
{
    // Check if the character can enter the zone, kill if cannot
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("nerd") || collider.CompareTag("jock"))
        {
            collider.gameObject.SendMessage("StartRespawn");
        }
        else if (collider.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }

    public void OnInteract()
    {
    }
}
