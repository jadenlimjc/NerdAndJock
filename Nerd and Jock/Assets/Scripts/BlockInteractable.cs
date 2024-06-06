using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteractable : MonoBehaviour , IInteractable
{
    

    public void OnInteract() {

        // example interaction, subject to change
        Destroy(gameObject);
    }
}
