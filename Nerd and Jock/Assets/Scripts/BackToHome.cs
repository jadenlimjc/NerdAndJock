using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToHome : MonoBehaviour
{
    public void backToHome() {
        SceneManager.LoadScene("HomeScreenScene");
    }

    void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return)) // KeyCode.Return corresponds to the Enter key
        {
            backToHome();
        }
    }
}
