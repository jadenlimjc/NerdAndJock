using System.Collections;
using UnityEngine;

public class BreakableGroundManager : MonoBehaviour {

    public static BreakableGroundManager Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void ReactivateObject(GameObject obj, float delay) {
        Debug.Log($"Reactivating object {obj.name} after {delay} seconds.");
        StartCoroutine(ReactivateAfterDelay(obj,delay));
    }

    public IEnumerator ReactivateAfterDelay(GameObject obj, float delay) {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}