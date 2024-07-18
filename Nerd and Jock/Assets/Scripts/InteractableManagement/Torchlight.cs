using UnityEngine;

public class Torchlight : MonoBehaviour, IInteractable
{
    public FogOfWarManager fogOfWarManager;

    void Start() 
    {
        fogOfWarManager = FindObjectOfType<FogOfWarManager>();
    }

    public void OnInteract() 
    {
        if (fogOfWarManager != null)
        {
            fogOfWarManager.collectTorchlight();
        }
        Destroy(gameObject);
    }
}