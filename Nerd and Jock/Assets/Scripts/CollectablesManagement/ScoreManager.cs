using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public Text scoreText;

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
}
