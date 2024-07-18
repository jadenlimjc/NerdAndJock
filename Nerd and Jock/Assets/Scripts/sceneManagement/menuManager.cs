using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    public AudioManager audioManager;

    

    private JSONSaving jsonSaving;
    //public static int UnlockedLevels;

    /*private Dictionary<string, int> levelMap = new Dictionary<string, int>
    {
        { "NJ1001", 0 },
        { "NJ2001", 1 },
        { "NJ3001", 2 },
        { "NJ2012", 3 },
        { "NJ3012", 4 },
        { "NJ2020", 5 },
        { "NJ2021", 6 }
    };
    */

    void Start()
    {
        jsonSaving = FindObjectOfType<JSONSaving>();
        if (jsonSaving == null)
        {
            Debug.LogError("JSONSaving instance not found. Ensure it is loaded in this scene.");
        }
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
        }
    }

    public void NewGame() {
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }
        StartCoroutine(DelayedNewGame());
    }

    public IEnumerator DelayedNewGame() {
        yield return new WaitForSeconds(0.1f); // Delay to allow the click sound to play
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        if (jsonSaving != null) 
        {
            jsonSaving.InitializeGameData();
        }
        SceneManager.LoadScene("StageSelectScene");
    }

    public void Continue() {
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }
        StartCoroutine(DelayedContinue());
    }

    private IEnumerator DelayedContinue()
    {
        yield return new WaitForSeconds(0.1f); // Delay to allow the click sound to play
        if (jsonSaving != null)
        {
            jsonSaving.LoadData();
        }
        SceneManager.LoadScene("StageSelectScene");
    }

    public void Settings() {
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }
        settingsPanel.SetActive(true);
    }

    public void Credits() {
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }
        creditsPanel.SetActive(true);
    }

    public void OnButtonHover()
    {
        if (audioManager != null)
        {
            audioManager.PlayHoverSound();
        }
    }

    
}
