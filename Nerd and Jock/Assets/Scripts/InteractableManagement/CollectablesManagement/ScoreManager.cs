using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public Text scoreText;
    public Text clockText;

    private float timeTaken = 0f;
    private bool isRunning = false;

    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
            // To make sure the ScoreManager persists across scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize the score text
        updateScoreText();
        startClock();
    }

    void Update()
    {
        if (isRunning) 
        {
            timeTaken += Time.deltaTime;
            updateClockText();
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
            scoreText.text = "Extra Credit: " + score + " / 3";
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
        int min = Mathf.FloorToInt(timeTaken / 60F);
        int sec = Mathf.FloorToInt(timeTaken % 60F);
        clockText.text = string.Format("Time Taken: {0:0} : {1:00}", min, sec);
    }
}
