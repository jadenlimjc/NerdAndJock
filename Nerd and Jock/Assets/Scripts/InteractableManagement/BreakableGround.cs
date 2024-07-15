using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGround : MonoBehaviour
{
    public float destroyDelay = 3.0f;
    //public animator groundAnimator;
    private bool isDestroying = false;
    // Start is called before the first frame update

    /* public void Start() {
        
    } */
    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision detected with: " + other.gameObject.name); // Log collision detection
        if ((other.CompareTag("nerd") || other.CompareTag("jock")) && !isDestroying) {
            StartCoroutine(DestroyAfterDelay());
        }

   }

    // Update is called once per frame
    private IEnumerator DestroyAfterDelay() {
        isDestroying = true;
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
        BreakableGroundManager.Instance.ReactivateObject(gameObject,destroyDelay);
    }
}

