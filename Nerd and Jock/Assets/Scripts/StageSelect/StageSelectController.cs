using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    public Button[] levelButtons;
    public Sprite locked;
    public Sprite passed;
    public Sprite unlocked;

    void Start()
    {
        PlayerPrefs.SetInt("NJ1001_unlocked", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string stageName = levelButtons[i].name;
            bool isUnlocked = PlayerPrefs.GetInt(stageName + "_unlocked", 0) == 1;
            if (!isUnlocked)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = locked;
            }
            else
            {
                levelButtons[i].interactable = true;
                int highScore = PlayerPrefs.GetInt(stageName + "_highScore", 0);
                if (highScore == 0) {
                    levelButtons[i].GetComponent<Image>().sprite = unlocked;
                } 
                else 
                {
                    levelButtons[i].GetComponent<Image>().sprite = passed;
                }
                
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
            PlayerPrefs.DeleteKey(button.name + "_highScore");
        }
        PlayerPrefs.Save();
    }
}