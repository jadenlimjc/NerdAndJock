using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    public GameObject stageSelectPanel;

    public GameObject creditsPanel;

    public AudioManager audioManager;
    private StageSelectController stageSelectController;
    private JSONSaving jsonSaving;
  

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
        stageSelectController = FindObjectOfType<StageSelectController>();

        if (PlayerPrefs.GetInt("ShowStageSelect", 0) == 1)
        {
            stageSelectPanel.SetActive(true);
            stageSelectController.Initialize();
            PlayerPrefs.SetInt("ShowStageSelect", 0);  // Reset flag
            PlayerPrefs.Save();
        }
    }

    public void NewGame() {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Click);
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        if (jsonSaving != null) 
        {
            jsonSaving.InitializeGameData();
        }
        stageSelectPanel.SetActive(true);
        stageSelectController.Initialize();
    }

    

    public void Continue() {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Click);
        }
        if (jsonSaving != null)
        {
            jsonSaving.LoadData();
        }
        stageSelectPanel.SetActive(true);
        stageSelectController.Initialize();
    }


    

    public void Credits() {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Click);
        }
        creditsPanel.SetActive(true);
    }

    public void OnButtonHover()
    {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Hover);
        }
    }

    
}
