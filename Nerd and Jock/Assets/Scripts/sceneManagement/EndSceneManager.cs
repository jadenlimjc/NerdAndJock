using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public Image star1;
    public Image star2;
    public Image star3;
    // Lit up star
    public Sprite star1Lit;
    public Sprite star2Lit;
    public Sprite star3Lit;
    public Text timeText;
    public Image wellDoneImage;
    public Image failedImage;
    public Button retryButton;
    public Button nextLevelButton;

    public Text gradeText;
    // Adding a delay so it looks better
    public float delay = 0.75f;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        float time = PlayerPrefs.GetFloat("Time", 0);
        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        wellDoneImage.gameObject.SetActive(false);
        failedImage.gameObject.SetActive(false);

        StartCoroutine(displayResults(score, time));
    }

    IEnumerator displayResults(int score, float time)
    {
        // Display score
        // Lit up stars based on score
        if (score >= 1) 
        {
            yield return new WaitForSeconds(delay);
            star1.sprite = star1Lit;
            yield return new WaitForSeconds(delay);
        }

        if (score >= 2)
        {
            star2.sprite = star2Lit;
            yield return new WaitForSeconds(delay);
        }

        if (score >= 3) 
        {
            star3.sprite = star3Lit;
            yield return new WaitForSeconds(delay);
        }
        
        // Display time
        int min = Mathf.FloorToInt(time / 60F);
        int sec = Mathf.FloorToInt(time % 60F);
        timeText.text = string.Format("Time: {0:00}:{1:00}", min, sec);
        timeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);

        // Calculate and display grade
        string grade = calculateGrade(time);
        gradeText.text = string.Format("Grade: " + grade);
        gradeText.gameObject.SetActive(true);

        // Display message based on grade
        if (grade != "F" && grade != "D+" && grade != "D")
        {
            updateHighScore(score);
            wellDoneImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            nextLevelButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
        }
        else
        {
            failedImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            retryButton.gameObject.SetActive(true);
        }

        
    }

    // Calculate grade
    private string calculateGrade(float time)
    {
        if (time <= 50) return "A+";
        if (time <= 60) return "A";
        if (time <= 70) return "A-";
        if (time <= 80) return "B+";
        if (time <= 90) return "B";
        if (time <= 100) return "B-";
        if (time <= 110) return "C+";
        if (time <= 120) return "C";
        if (time <= 130) return "D+";
        if (time <= 140) return "D";
        return "F";
    }

    // Save the player's highscore
    private void updateHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void retryLevel()
    {
        // Get the name of the stage just completed
        // Replay stage if stage failed, optional if passed
        string previousStage = PlayerPrefs.GetString("PreviousStage");
        if (!string.IsNullOrEmpty(previousStage))
        {
            SceneManager.LoadScene(previousStage);
        }
    }

    public void nextLevel()
    {
        // Load the next level or homepage
        BackToHome backToHome = new BackToHome();
        backToHome.backToHome();
    }

}