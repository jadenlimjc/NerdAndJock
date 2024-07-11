using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public StageData[] stages; //array of root stagedata objects
    public GameObject stageButtonPrefab;
    public Transform stagePanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Makes the manager persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("StageManager Start called");
        PlayerPrefs.DeleteAll(); // Clear PlayerPrefs for testing purposes
        CreateStageButtons();
    }

    void CreateStageButtons()
    {
        foreach (var stage in stages)
        {
            CreateButton(stage, stagePanel);
        }
    }

    GameObject CreateButton(StageData stageData, Transform parent)
    {
        GameObject buttonObj = Instantiate(stageButtonPrefab, parent);
        StageButton stageButton = buttonObj.GetComponent<StageButton>();
        stageButton.stageData = stageData;
        stageButton.button = buttonObj.GetComponent<Button>();
        stageButton.stageNameText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        stageButton.childStageButtons = new GameObject[0];

        // Update button state to reflect completion status and interaction
        stageButton.UpdateButtonState();

        if (stageData.childStages.Length > 0)
        {
            foreach (var childStage in stageData.childStages)
            {
                GameObject childButtonObj = CreateButton(childStage, buttonObj.transform);
                stageButton.childStageButtons = AppendToArray(stageButton.childStageButtons, childButtonObj);
            }
        }

        stageButton.UpdateButtonState();
        return buttonObj; // Return the created button object
        
        
    }

    GameObject[] AppendToArray(GameObject[] array, GameObject item)
    {
        GameObject[] newArray = new GameObject[array.Length + 1];
        array.CopyTo(newArray, 0);
        newArray[array.Length] = item;
        return newArray;
    }

    public void SaveStageCompletion(string stageName)
    {
        PlayerPrefs.SetInt(stageName, 1); // 1 means completed
        PlayerPrefs.Save();
        Debug.Log($"Saved completion for stage {stageName} with value: 1");
    }

    public bool IsStageCompleted(string stageName)
    {
        int completed = PlayerPrefs.GetInt(stageName, 0);
        Debug.Log($"Checking if stage {stageName} is completed: {completed == 1}");
        return completed == 1;
    }
}
