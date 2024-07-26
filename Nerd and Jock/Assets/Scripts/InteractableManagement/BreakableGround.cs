using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGround : MonoBehaviour
{
    public float destroyDelay = 3.0f;
    private bool isDestroying = false;
    public AudioManager audioManager;


    public void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
        }
    } 
    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision detected with: " + other.gameObject.name); // Log collision detection
        if ((other.CompareTag("nerd") || other.CompareTag("jock")) && !isDestroying) {
            StartCoroutine(DestroyAfterDelay());
        }

   }

    // Update is called once per frame
    private IEnumerator DestroyAfterDelay() {
        isDestroying = true;
        audioManager.PlaySound(AudioType.Earthquake);
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
        audioManager.StopSound(AudioType.Earthquake);
        BreakableGroundManager.Instance.ReactivateObject(gameObject,destroyDelay);
        isDestroying = false;
    }
}

