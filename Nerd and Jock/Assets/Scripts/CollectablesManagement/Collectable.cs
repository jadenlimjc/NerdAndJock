using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Value of the collectable
    public int scoreValue = 1; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the value to the total score
            ScoreManager.Instance.addScore(scoreValue);
            // Destroy collectable
            Destroy(gameObject); 
        }
    }
}