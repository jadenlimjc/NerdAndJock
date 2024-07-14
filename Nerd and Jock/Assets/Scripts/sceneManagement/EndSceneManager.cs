using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public int stars;
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
    private string currentScene;
    private JSONSaving jsonSaving;


    public Text gradeText;
    // Adding a delay so it looks better
    public float delay = 0.75f;

    void Start()
    {
        currentScene = ScoreManager.Instance.getCurrentScene();
        jsonSaving = FindObjectOfType<JSONSaving>();
        if (jsonSaving == null) 
        {
            Debug.LogError("JSONSaving instance not found in the scene.");
            return;
        }

        int score = PlayerPrefs.GetInt("Score", 0);
        float time = PlayerPrefs.GetFloat("Time", 0);
        int totalCollectables = PlayerPrefs.GetInt("TotalCollectables", 0); // Setting to 0 if it is not set
        stars = calculateStars(score, totalCollectables);
        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        wellDoneImage.gameObject.SetActive(false);
        failedImage.gameObject.SetActive(false);

        StartCoroutine(displayResults(score, time));
    }

    // Calculate stars, accounting for collectables not a multiple of 3
    private int calculateStars(int score, int totalCollectables)
    {
        if (totalCollectables == 0)
        {
            return 3;
        }
        return Mathf.CeilToInt((float)score / (float)totalCollectables * 3); 
    }

    IEnumerator displayResults(int score, float time)
    {
        // Display score
        // Lit up stars based on score
        if (stars >= 1) 
        {
            yield return new WaitForSeconds(delay);
            star1.sprite = star1Lit;
            yield return new WaitForSeconds(delay);
        }

        if (stars >= 2)
        {
            star2.sprite = star2Lit;
            yield return new WaitForSeconds(delay);
        }

        if (stars >= 3) 
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
            updateHighScore(stars, time, grade);
            wellDoneImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            nextLevelButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            /*
            int currLevel = PlayerPrefs.GetInt("currLevel", 0);
            Debug.Log($"Current level {currLevel}");
            int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
            Debug.Log($"Unlocked level {unlockedLevels}");

            if (currLevel == unlockedLevels)
            {
                unlockedLevels++;
                Debug.Log($"New unlocked level {unlockedLevels}");
                PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
            }
            */
            
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
        string scenePrefix = currentScene.Substring(0, 3);

        switch (scenePrefix)
        {
            case "NJ1":
                return "A+";
            case "NJ2":
                return calculateGradeForNJ2XXX(time);
            case "NJ3":
                return calculateGradeForNJ3XXX(time);
            case "NJ4":
                return calculateGradeForNJ4XXX(time);
            default:
                return "Not found"; // for my own debugging
        }
    }

    private string calculateGradeForNJ2XXX(float time)
    {
        if (time <= 90) return "A+";
        if (time <= 100) return "A";
        if (time <= 110) return "A-";
        if (time <= 120) return "B+";
        if (time <= 130) return "B";
        if (time <= 140) return "B-";
        if (time <= 150) return "C+";
        if (time <= 160) return "C";
        if (time <= 170) return "D+";
        if (time <= 180) return "D";
        return "F";
    }

    private string calculateGradeForNJ3XXX(float time)
    {
        if (time <= 120) return "A+";
        if (time <= 130) return "A";
        if (time <= 140) return "A-";
        if (time <= 150) return "B+";
        if (time <= 160) return "B";
        if (time <= 170) return "B-";
        if (time <= 180) return "C+";
        if (time <= 190) return "C";
        if (time <= 200) return "D+";
        if (time <= 210) return "D";
        return "F";
    }

    private string calculateGradeForNJ4XXX(float time)
    {
        if (time <= 150) return "A+";
        if (time <= 160) return "A";
        if (time <= 170) return "A-";
        if (time <= 180) return "B+";
        if (time <= 190) return "B";
        if (time <= 200) return "B-";
        if (time <= 210) return "C+";
        if (time <= 220) return "C";
        if (time <= 230) return "D+";
        if (time <= 240) return "D";
        return "F";
    }

    // Save the player's highscore
    private void updateHighScore(int stars, float time, string grade)
    {
        StageData stageData = jsonSaving.GetGameData().stages.Find(stage => stage.stageName == currentScene);
        if (stageData == null)
        {
            stageData = new StageData(currentScene, grade, stars, true, time);
            jsonSaving.GetGameData().stages.Add(stageData);
        }
        else
        {
            if (time < (stageData.bestTime))
            {
                stageData.bestGrade = grade;
                stageData.stars = stars;
                stageData.bestTime = time;
            }
        }
        int currentIndex = jsonSaving.GetGameData().stages.IndexOf(stageData);
        if (currentIndex != -1 && currentIndex + 1 < jsonSaving.GetGameData().stages.Count)
        {
            StageData nextStageData = jsonSaving.GetGameData().stages[currentIndex + 1];
            if (!nextStageData.unlocked)
            {
                nextStageData.unlocked = true;
                Debug.Log("Unlocked next stage: " + nextStageData.stageName);
            }
        }

        jsonSaving.SaveData();
    }

    /*private void updateHighScore(int stars, float time, string grade)
    {
        float bestTime = PlayerPrefs.GetFloat(currentScene + "_bestTime", 0f);

        if (bestTime == 0 || time < bestTime)
        {
            PlayerPrefs.SetInt(currentScene + "_stars", stars);
            PlayerPrefs.SetFloat(currentScene + "_bestTime", time);
            PlayerPrefs.SetString(currentScene + "_bestGrade", grade);
        }
    }
    */

    public void nextStage()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void replay()
    {
        SceneManager.LoadScene(ScoreManager.Instance.getCurrentScene());
    }


}