using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnOutOfBounds : MonoBehaviour
{
    public float lowerBound = -10.0f;
    public float respawnTimer = 1.0f;
    // Start is called before the first frame update
    public Vector2 spawn = new Vector2(0,0);
    private bool isRespawning = false;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (!isRespawning && (transform.position.y < lowerBound ||
                            !isWithinCameraView())) {
            StartCoroutine(respawnAfterDelay());
        }
        
    }

    private bool isWithinCameraView() 
    {
        Camera cam = Camera.main;
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
        return screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    private IEnumerator respawnAfterDelay() {
        isRespawning = true;
        yield return new WaitForSeconds(respawnTimer);
        gameObject.transform.position = spawn;
        rb.velocity = Vector2.zero;
        isRespawning = false;
    }
}
