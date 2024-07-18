using UnityEngine;

public class NerdPlaceBlock : MonoBehaviour {
    public GameObject blockPrefab; //block to be placed
    public Transform blockSpawnPoint; //point where block is placed, at nerds feet

    public NerdController nerdController;

    void Start() {
        nerdController = GetComponent<NerdController>();
    }

    void Update() {
        if (!nerdController.isGrounded && !nerdController.isInteracting && Input.GetKeyDown(KeyCode.E)) {
            PlaceBlock();
        }
    }

    private void PlaceBlock() {
        Instantiate(blockPrefab, blockSpawnPoint.position, Quaternion.identity);
    }
}