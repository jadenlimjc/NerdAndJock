using UnityEngine;

public class JockPortal : MonoBehaviour
{
    public GameObject portalPrefab; // Portal prefab to instantiate
    private GameObject portal1;
    private GameObject portal2;

    public GameObject nerd;

    private JockController jockController;
    private AudioManager audioManager;

    void Start()
    {
        jockController = GetComponent<JockController>();
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreatePortal();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Teleport();
        }
        if (portal1 != null)
        {
            portal1.transform.position = nerd.transform.position;
        }
    }

    void CreatePortal()
    {
        if (portal1 == null)
        {
            portal1 = Instantiate(portalPrefab, nerd.transform.position, Quaternion.identity);
        }
        else if (portal2 == null)
        {
            portal2 = Instantiate(portalPrefab, transform.position, Quaternion.identity);
        }
    }

    void Teleport()
    {
        if (portal1 != null && portal2 != null)
        {
            nerd.transform.position = portal2.transform.position;
            Destroy(portal1);
            Destroy(portal2);
            portal1 = null;
            portal2 = null;
            audioManager.PlaySound(AudioType.Exit);
        }
    }
}
