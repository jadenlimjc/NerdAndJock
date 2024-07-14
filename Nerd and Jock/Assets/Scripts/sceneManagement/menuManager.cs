using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    public LevelButton[] levelObjects;
    public static int UnlockedLevels;
    public Sprite[] StarLit;

    private Dictionary<string, int> levelMap = new Dictionary<string, int>
    {
        { "NJ1001", 0 },
        { "NJ2001", 1 },
        { "NJ3001", 2 },

    };


    void Start() {
        UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels",0);
        for (int i = 0; i < levelObjects.Length; i++) {
            if (UnlockedLevels >= i) {
                levelObjects[i].levelButton.interactable = true;
                int stars = PlayerPrefs.GetInt("stars" + i.ToString(),0);
                for (int j = 0; j < stars; j++) {
                    levelObjects[i].stars[j].sprite = StarLit[j];
                } 
            }
        }
    }

    public void NewGame() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("StageSelectScene");
    }

    public void Continue() {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void Settings() {
        settingsPanel.SetActive(true);
    }

    public void Credits() {
        creditsPanel.SetActive(true);
    }

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

    
}
