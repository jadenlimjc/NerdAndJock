using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnController : MonoBehaviour
{
    public static respawnController Instance;
    public float lowerBound = -10.0f;
    public float respawnTimer = 1.0f;
    // Start is called before the first frame update
    public Vector2 spawn = new Vector2(0, 0);
    private bool isRespawning = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (!isRespawning && (transform.position.y < lowerBound || !isWithinCameraView()))
        {
            StartCoroutine(respawnAfterDelay());
        }

    }

    private bool isWithinCameraView()
    {
        Camera cam = Camera.main;
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
        return screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    public IEnumerator respawnAfterDelay()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnTimer);
        gameObject.transform.position = spawn;
        isRespawning = false;
    }

    void StartRespawn()
    {
        StartCoroutine(respawnAfterDelay());
    }


}
