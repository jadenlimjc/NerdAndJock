using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitScene : MonoBehaviour
{
    public string sceneToLoad = "EndScene"; // Name of the scene to load
    public static bool nerdInDoor = false;
    public static bool jockInDoor = false;
    public AudioManager audioManager;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
        }
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
            stopClock();
            saveScoreAndTime();
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void stopClock()
    {
        ScoreManager.Instance.stopClock();
    }

    private void saveScoreAndTime()
    {
        int score = ScoreManager.Instance.score;
        float time = ScoreManager.Instance.getTimeTaken();

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetFloat("Time", time);
        PlayerPrefs.Save();
    }


}
