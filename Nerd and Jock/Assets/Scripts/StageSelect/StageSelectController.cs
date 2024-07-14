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
    //public static int UnlockedLevels;

    private JSONSaving jsonSaving;

    void Start()
    {
        jsonSaving = FindObjectOfType<JSONSaving>();
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
            //int UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
            if (stageData != null && stageData.unlocked)
            {
                levelButtons[i].interactable = true;
                float bestTime = stageData.bestTime;
                string bestGrade = stageData.bestGrade;
                
                if (bestTime == float.MaxValue) {
                    levelButtons[i].GetComponent<Image>().sprite = unlocked;
                    bestGradeTexts[i].gameObject.SetActive(false);
                    SetStars(starImages, 0);
                } 
                else 
                {
                    levelButtons[i].GetComponent<Image>().sprite = passed;
                    bestGradeTexts[i].text = "BEST : " + bestGrade;
                    bestGradeTexts[i].gameObject.SetActive(true);
                    SetStars(starImages, starCount);
                }
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = locked;
                bestGradeTexts[i].gameObject.SetActive(false);
                SetStars(starImages, 0);
            }
        }
    }

    /*void Start()
    {
        UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string stageName = levelButtons[i].name;
            int starCount = PlayerPrefs.GetInt(stageName + "_stars", 0);

            Transform starGroup = levelButtons[i].transform.Find("stars");
            Image[] starImages = starGroup.GetComponentsInChildren<Image>(true);

            if (UnlockedLevels < i)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = locked;
                bestGradeTexts[i].gameObject.SetActive(false);
                SetStars(starImages, 0);
            }
            else
            {
                levelButtons[i].interactable = true;
                float bestTime = PlayerPrefs.GetFloat(stageName + "_bestTime", 0f);
                string bestGrade = PlayerPrefs.GetString(stageName + "_bestGrade", "NA");
                
                if (bestTime == 0f) {
                    levelButtons[i].GetComponent<Image>().sprite = unlocked;
                    bestGradeTexts[i].gameObject.SetActive(false);
                    SetStars(starImages, 0);
                } 
                else 
                {
                    levelButtons[i].GetComponent<Image>().sprite = passed;
                    bestGradeTexts[i].text = "BEST : " + bestGrade;
                    bestGradeTexts[i].gameObject.SetActive(true);
                    SetStars(starImages, starCount);
                }
            }
        }
    }
*/
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
        SceneManager.LoadScene(stageName);
    }
/*
    public void Reset()
    {
        foreach (Button button in levelButtons)
        {
            PlayerPrefs.DeleteKey(button.name + "_bestGrade");
            PlayerPrefs.DeleteKey(button.name + "_bestTime");
            PlayerPrefs.DeleteKey(button.name + "_stars");
        }
        PlayerPrefs.Save();
    }
    */
}