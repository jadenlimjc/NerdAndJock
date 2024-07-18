using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelectController : MonoBehaviour
{
    public Button[] levelButtons;
    public TextMeshProUGUI[] bestGradeTexts;
    public Sprite locked;
    public Sprite passed;
    public Sprite unlocked;
    public Sprite[] starSprites;
    private JSONSaving jsonSaving;
    private StageManager stageManager;

    public void Initialize()
    {
        Debug.Log("stageselectcontroller started");
        jsonSaving = FindObjectOfType<JSONSaving>();
        stageManager = StageManager.Instance;
        jsonSaving.LoadData(); // load the data
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            StageData stageData = jsonSaving.GetGameData().stages.Find(stage => stage.stageName == levelButtons[i].name);
            int starCount = stageData.stars;

            Transform starGroup = levelButtons[i].transform.Find("stars");
            Image[] starImages = starGroup.GetComponentsInChildren<Image>(true);
            if (stageData != null && stageData.unlocked)
            {
                levelButtons[i].interactable = true;
                float bestTime = stageData.bestTime;
                string bestGrade = stageData.bestGrade;
                
                if (bestTime == float.MaxValue) {
                    Debug.Log($"Stage {stageData.stageName} is unlocked.");
                    levelButtons[i].GetComponent<Image>().sprite = unlocked;
                    bestGradeTexts[i].gameObject.SetActive(false);
                    SetStars(starImages, 0);
                } 
                else 
                {
                    Debug.Log($"Stage {stageData.stageName} is passed.");
                    levelButtons[i].GetComponent<Image>().sprite = passed;
                    bestGradeTexts[i].text = "BEST : " + bestGrade;
                    bestGradeTexts[i].gameObject.SetActive(true);
                    SetStars(starImages, starCount);
                }
            }
            else
            {
                Debug.Log($"Stage {stageData.stageName} is locked.");
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = locked;
                bestGradeTexts[i].gameObject.SetActive(false);
                SetStars(starImages, 0);
            }
        }
    }

    private void SetStars(Image[] starImages, int starCount)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < starCount)
            {
                starImages[i].sprite = starSprites[i + 3];
                starImages[i].gameObject.SetActive(true);
            }
            else
            {
                starImages[i].sprite = starSprites[i];
                starImages[i].gameObject.SetActive(true);
            }
             
        }
    }

    public void LoadStage(string stageName)
    {
        if (stageManager.IsStageUnlocked(stageName))
        {
            SceneManager.LoadScene(stageName);
        }
    }

    // public void BackButton()
    // {
    //     SceneManager.LoadScene("HomeScreenScene");
    // }
}