using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLaptop : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform wall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnInteract() {


        // Check if the child exists
        if (wall!= null)
        {
            // Destroy the child GameObject
            Destroy(wall.gameObject);
        }
    }
}
