using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLaptop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnInteract() {

        // Find the child named "wallPlaceholder"
        Transform wallPlaceholder = transform.Find("wallPlaceholder");

        // Check if the child exists
        if (wallPlaceholder != null)
        {
            // Destroy the child GameObject
            Destroy(wallPlaceholder.gameObject);
        }
    }
}
