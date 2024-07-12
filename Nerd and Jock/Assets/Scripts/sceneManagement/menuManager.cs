using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    public GameObject settingsPanel;
    public GameObject creditsPanel;

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

    public void ClosePanel(GameObject panel) {
        panel.SetActive(false);
    }
}
