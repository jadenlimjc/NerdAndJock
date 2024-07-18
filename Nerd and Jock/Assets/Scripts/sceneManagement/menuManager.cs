using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    public GameObject settingsPanel;
    public GameObject creditsPanel;

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
    }

    public void NewGame() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        if (jsonSaving != null) 
        {
            jsonSaving.InitializeGameData();
        }
        SceneManager.LoadScene("StageSelectScene");
    }

    public void Continue() {
        if (jsonSaving != null)
        {
            jsonSaving.LoadData();
        }
        SceneManager.LoadScene("StageSelectScene");
    }

    public void Settings() {
        settingsPanel.SetActive(true);
    }

    public void Credits() {
        creditsPanel.SetActive(true);
    }
/*
    public void OnClickBack() {
         stageListPanel.SetActive(false);
        //  settingsPanel.SetActive(false);
        //  creditsPanel.SetACtive(false);
    }
    

    public void OnClickLevelButton(string level) {
        if (levelMap.TryGetValue(level, out int levelNum))
        {
            PlayerPrefs.SetInt("currLevel", levelNum);
            SceneManager.LoadScene(level);
        }
        else
        {
            Debug.LogError($"Level '{level}' not found in the level map.");
        }
    }
    */

    
}
