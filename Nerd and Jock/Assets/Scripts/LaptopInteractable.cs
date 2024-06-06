using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInteractable : MonoBehaviour , IInteractable
{
    // Start is called before the first frame update
    public Transform wall;
    

    public void OnInteract() {
        // Destroy the child GameObject
            Destroy(wall.gameObject);

    }
}
