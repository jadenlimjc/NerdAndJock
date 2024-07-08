using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public int totalCollectables;
    public Text scoreText;
    public Text clockText;

    private float timeTaken = 0f;
    private bool isRunning = false;
    private string currentScene;

    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
            // To make sure the ScoreManager persists across scenes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called every time a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "EndScene")
        {
            currentScene = scene.name;
        }
        currentScene = scene.name;
        scoreText = GameObject.FindWithTag("ScoreText")?.GetComponent<Text>();
        clockText = GameObject.FindWithTag("ClockText")?.GetComponent<Text>();
        initialiseLevel();  // Reset and initialize when a new scene is loaded
    }

    void Start()
    {
        initialiseLevel();
    }

    public void initialiseLevel()
    {
        resetScoreManager();
        countCollectables();
        // Initialize the score text
        updateScoreText();
        updateClockText();
        startClock();
    }

    // Resetting score manager each stage
    public void resetScoreManager()
    {
        score = 0;
        timeTaken = 0f;
        isRunning = false; 
    }

    void Update()
    {
        if (isRunning) 
        {
            timeTaken += Time.deltaTime;
            updateClockText();
        }
    }

    // Count the total number of collectables at the start of each level
    private void countCollectables()
    {
        if (SceneManager.GetActiveScene().name != "EndScene")
        {
            totalCollectables = GameObject.FindGameObjectsWithTag("Collectable").Length;
            PlayerPrefs.SetInt("TotalCollectables", totalCollectables);
            PlayerPrefs.Save();
        }
        else
        {
            totalCollectables = PlayerPrefs.GetInt("TotalCollectables", 0);
        }
    }

    public void addScore(int value) 
    {
        score += value;
        updateScoreText();
    }

    void updateScoreText() 
    {
        if (scoreText != null)
        {
            scoreText.text = string.Format("Extra Credit: {0} / {1}", score, totalCollectables);
        }
    }

    // Start timer when stage starts
    public void startClock()
    {
        isRunning = true;
    }

    // Stop timer when stage ends
    public void stopClock()
    {
        isRunning = false;
    }
    
    // Getter for time taken to be used in exitScene script
    public float getTimeTaken()
    {
        return timeTaken;
    }

    private void updateClockText()
    {
        if (clockText != null) 
        {
            int min = Mathf.FloorToInt(timeTaken / 60F);
            int sec = Mathf.FloorToInt(timeTaken % 60F);
            clockText.text = string.Format("Time Taken: {0:0} : {1:00}", min, sec);
        }
    }

    public string getCurrentScene()
    {
        return currentScene;
    }
}
