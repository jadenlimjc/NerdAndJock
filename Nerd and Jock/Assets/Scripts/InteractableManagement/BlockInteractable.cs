using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteractable : MonoBehaviour , IInteractable
{
    public Transform block;
    public Animator blockAnimator;

    public void Start()
    {
        if (blockAnimator == null && block != null)
        {
            blockAnimator = block.GetComponent<Animator>();
        }
    }

    public void OnInteract() {
        if (blockAnimator != null) {
            blockAnimator.SetTrigger("Break"); // Trigger destroy animation
        } 
        else 
        {
            Destroy(block.gameObject); // Destroy the block if no animator found
        }
    }

    // Will be called by the Animation Event at the end of the breaking animation
    public void DestroyBlock()
    {
        Collider2D collider = block.GetComponent<Collider2D>();
        if (collider != null)
        {
            Debug.Log("Disabling collider on: " + block.name);
            collider.enabled = false;
        }
        Debug.Log("Destroying block: " + block.name);
        Destroy(block.gameObject); // Destroy the block
    }
}
