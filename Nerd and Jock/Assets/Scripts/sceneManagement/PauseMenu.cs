using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public AudioManager audioManager;
    void Start()
    {
        
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Click);
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;

    }

    void Pause()
    {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Click);
        }
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }


    public void QuitGame() {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Click);
        }
        Time.timeScale = 1.0f;
        gamePaused = false;
        SceneManager.LoadScene("HomeScreenScene");
    }
    public void OnButtonHover()
    {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Hover);
        }
    }
}
