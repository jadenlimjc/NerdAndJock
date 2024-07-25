using UnityEngine;

public class NerdPlaceBlock : MonoBehaviour {
    public GameObject blockPrefab; //block to be placed
    public Transform blockSpawnPoint; //point where block is placed, at nerds feet
    public int blockCount = 10;

    public NerdController nerdController;
    public AudioManager audioManager;

    void Start() {
        nerdController = GetComponent<NerdController>();
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
        }
    }

    void Update() {
        if (!nerdController.isGrounded && !nerdController.isInteracting && Input.GetKeyDown(KeyCode.Space) && blockCount >= 0) {
            blockCount --;
            PlaceBlock();
        }
    }

    private void PlaceBlock() {
        Instantiate(blockPrefab, blockSpawnPoint.position, Quaternion.identity);
        audioManager.PlaySound(AudioType.Pop);
    }
}