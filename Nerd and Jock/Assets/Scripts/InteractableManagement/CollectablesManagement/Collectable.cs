using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    // Value of the collectable
    public int scoreValue = 1; 
    public AudioManager audioManager;
    void Start()
    {
        
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
        }
    }
    public void OnInteract() 
    {
        // Add the value to the total score
        ScoreManager.Instance.addScore(scoreValue);
        // Destroy collectable
        if (audioManager != null)
        {
            Debug.Log("Playing Star sound.");
            audioManager.PlaySound(AudioType.Star);
        }
        Destroy(gameObject); 
    }
}