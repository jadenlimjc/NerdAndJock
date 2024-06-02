using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    public void PlayGame() {
        SceneManager.LoadScene("SampleScene");
    }

    void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return)) // KeyCode.Return corresponds to the Enter key
        {
            PlayGame();
        }
    }
}
