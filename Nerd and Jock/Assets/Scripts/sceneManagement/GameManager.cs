using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string[] modules;
    private int currentModuleIndex = 0;

    void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Use across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void loadNextStage()
    {
        currentModuleIndex++;
        if (currentModuleIndex >= modules.Length)
        {
            BackToHome backToHome = new BackToHome();
            backToHome.backToHome();
        }
        else 
        {
            SceneManager.LoadScene(modules[currentModuleIndex]);
        }
    }

    // To replay the stage
    public void replayStage()
    {
        SceneManager.LoadScene(modules[currentModuleIndex]);
    }    
}
