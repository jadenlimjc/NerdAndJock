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
    private StageManager stageManager;
    public AudioManager audioManager;

    public GameObject creditsPanel;


    public Text gradeText;
    // Adding a delay so it looks better
    public float delay = 0.75f;

    void Start()
    {
        currentScene = ScoreManager.Instance.getCurrentScene();
        jsonSaving = FindObjectOfType<JSONSaving>();
        stageManager = StageManager.Instance;
        if (jsonSaving == null) 
        {
            Debug.LogError("JSONSaving instance not found in the scene.");
            return;
        }
        if (stageManager == null)
        {
            Debug.LogError("StageManager instance not assigned.");
            return;
        }
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance not found. Ensure it is loaded in this scene.");
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
            audioManager.PlaySound(AudioType.Star);
            star1.sprite = star1Lit;
            yield return new WaitForSeconds(delay);
        }

        if (stars >= 2)
        {
            audioManager.PlaySound(AudioType.Star);
            star2.sprite = star2Lit;
            yield return new WaitForSeconds(delay);
        }

        if (stars >= 3) 
        {
            audioManager.PlaySound(AudioType.Star);
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
            audioManager.PlaySound(AudioType.Pass);
            wellDoneImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            nextLevelButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            UnlockStage();
        }
        else
        {
            failedImage.gameObject.SetActive(true);
            audioManager.PlaySound(AudioType.Fail);
            yield return new WaitForSeconds(delay);
            retryButton.gameObject.SetActive(true);
        }

        DisplayCredits();
        
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
            default:
                return "Not found"; // for my own debugging
        }
    }

    private string calculateGradeForNJ2XXX(float time)
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
        if (time <= 300) return "D";
        return "F";
    }

    private string calculateGradeForNJ3XXX(float time)
    {
        if (time <= 210) return "A+";
        if (time <= 240) return "A";
        if (time <= 270) return "A-";
        if (time <= 300) return "B+";
        if (time <= 330) return "B";
        if (time <= 360) return "B-";
        if (time <= 390) return "C+";
        if (time <= 420) return "C";
        if (time <= 450) return "D+";
        if (time <= 480) return "D";
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
        jsonSaving.SaveData();
    }
    // check if all stages complete, display credits if they are
    public void DisplayCredits() {
        Debug.Log("Checking whether all stages completed");
        List <StageData> stages = jsonSaving.GetGameData().stages;
        bool allCompleted = true;

        foreach (StageData stage in stages)
        {
            if (stage.bestTime == float.MaxValue)
            {
                allCompleted = false;
                break;
            }
        }

        if (allCompleted)
        {
            Debug.Log("All stages completed, displaying credits panel");
            creditsPanel.SetActive(true); // Display the credits panel
        }
    }

    private void UnlockStage()
    {
        // Unlock the next stages based on the current stage
        //Debug.Log($"Trying to unlock stages following {currentScene}");
        stageManager.UnlockNextStages(currentScene);
    }

    public void nextStage()
    {
        audioManager.PlaySound(AudioType.Click);
        audioManager.StopSound(AudioType.Pass);
        audioManager.StopSound(AudioType.Fail);
        PlayerPrefs.SetInt("ShowStageSelect", 1);  // Set flag
        PlayerPrefs.Save();
        SceneManager.LoadScene("HomeScreenScene");
    }

    public void replay()
    {
        audioManager.PlaySound(AudioType.Click);
        audioManager.StopSound(AudioType.Pass);
        audioManager.StopSound(AudioType.Fail);
        SceneManager.LoadScene(ScoreManager.Instance.getCurrentScene());
    }

    private void CompleteStage()
    {
        string currentStageName = ScoreManager.Instance.getCurrentScene();
        stageManager.UnlockNextStages(currentStageName);
    }
    public void OnButtonHover()
    {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Hover);
        }
    }

    public void BackButton() {
        if (audioManager != null)
        {
            audioManager.PlaySound(AudioType.Click);
        }
        creditsPanel.SetActive(false);
    }
}