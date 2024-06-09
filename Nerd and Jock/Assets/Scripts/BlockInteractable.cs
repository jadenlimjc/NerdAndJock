using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteractable : MonoBehaviour , IInteractable
{
    public Transform block;

    public void OnInteract() {
        Destroy(block.gameObject); // Destroy the block
    }
}
