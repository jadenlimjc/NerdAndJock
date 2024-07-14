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

    void Start()
    {
        PlayerPrefs.SetInt("NJ1001_unlocked", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string stageName = levelButtons[i].name;
            bool isUnlocked = PlayerPrefs.GetInt(stageName + "_unlocked", 0) == 1;
            int starCount = PlayerPrefs.GetInt(stageName + "_stars", 0);

            Transform starGroup = levelButtons[i].transform.Find("stars");
            Image[] starImages = starGroup.GetComponentsInChildren<Image>(true);

            if (!isUnlocked)
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

    public void Reset()
    {
        foreach (Button button in levelButtons)
        {
            PlayerPrefs.DeleteKey(button.name + "_unlocked");
            PlayerPrefs.DeleteKey(button.name + "_bestGrade");
            PlayerPrefs.DeleteKey(button.name + "_bestTime");
            PlayerPrefs.DeleteKey(button.name + "_stars");
        }
        PlayerPrefs.Save();
    }
}