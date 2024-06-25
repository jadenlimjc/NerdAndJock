using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    // Value of the collectable
    public int scoreValue = 1; 

    public void OnInteract() 
    {
        // Add the value to the total score
        ScoreManager.Instance.addScore(scoreValue);
        // Destroy collectable
        Destroy(gameObject); 
    }
}