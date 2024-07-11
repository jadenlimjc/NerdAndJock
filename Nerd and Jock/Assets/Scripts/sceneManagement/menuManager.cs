using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    public GameObject stageListPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    public void NewGame() {
        SceneManager.LoadScene("NJ1001");
    }

    public void Continue() {
        stageListPanel.SetActive(true);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && stageListPanel.activeSelf) 
        {
            stageListPanel.SetActive(false);
        }
    }
}
