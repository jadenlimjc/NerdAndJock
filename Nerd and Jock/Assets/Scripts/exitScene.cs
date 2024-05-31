using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitScene : MonoBehaviour
{
    public string sceneToLoad = "EndScene"; // Name of the scene to load
    public static bool nerdInDoor = false;
    public static bool jockInDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("door")) {
            if (gameObject.CompareTag("nerd")) {
                nerdInDoor = true;
            }
            else if (gameObject.CompareTag("jock")) {
                jockInDoor = true;
            }
            CheckBothPlayersInDoor();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("door"))
        {
            if (gameObject.CompareTag("nerd"))
            {
                nerdInDoor = false;
            }
            else if (gameObject.CompareTag("jock"))
            {
                jockInDoor = false;
            }
        }
    }

    private void CheckBothPlayersInDoor()
    {
        if (nerdInDoor && jockInDoor)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
