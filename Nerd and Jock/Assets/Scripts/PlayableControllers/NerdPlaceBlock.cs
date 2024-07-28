using UnityEngine;

public class NerdPlaceBlock : MonoBehaviour {
    public GameObject blockPrefab; //block to be placed
    public Transform blockSpawnPoint; //point where block is placed, at nerds feet
    public int maxCharges = 3;
    public float cooldownDuration = 8.0f;

    public int currentCharges;
    public float cooldownTimer;

    public NerdController nerdController;
    public AudioManager audioManager;

    void Start() {
        nerdController = GetComponent<NerdController>();
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
        }
        currentCharges = maxCharges;
        cooldownTimer = 0f;
    }

    void Update() {
        if (currentCharges < maxCharges) {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f) {
                currentCharges++;
                cooldownTimer = cooldownDuration;
            }
        }

        if (!nerdController.isGrounded && !nerdController.isInteracting && Input.GetKeyDown(KeyCode.C) && currentCharges > 0) {
            PlaceBlock();
            currentCharges--;
            if (currentCharges < maxCharges) {
                cooldownTimer = cooldownDuration;
            }
        }
    }
    

    private void PlaceBlock() {
        Instantiate(blockPrefab, blockSpawnPoint.position, Quaternion.identity);
        audioManager.PlaySound(AudioType.Pop);
    }
}