using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGroundInteractable : MonoBehaviour , IInteractable
{
    public string safeFor;

    // Check if the character can enter the zone, kill if cannot
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag(safeFor))
        {
            collider.gameObject.SendMessage("StartRespawn");
        }
    }

    public void OnInteract()
    {
    }
}
