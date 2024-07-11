using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public StageData stageData;
    public Button button;
    public GameObject[] childStageButtons;

    void Start()
    {
        UpdateButtonState();
        button.onClick.AddListener(OnStageButtonClicked);
    }

    public void UpdateButtonState()
    {
       

        // Enable button if all parents are completed 
        button.interactable = AreParentsCompleted();

        // Update button visuals based on state
        ColorBlock colorBlock = button.colors;
        if (button.interactable)
        {
            // Set to normal color if interactable
            colorBlock.normalColor = Color.white;
            colorBlock.disabledColor = Color.gray;
        }
        else
        {
            // Set to grey color if not interactable
            colorBlock.normalColor = Color.gray;
        }
        button.colors = colorBlock;
    }

    void OnStageButtonClicked()
    {
        // Handle stage selection and completion logic here
        stageData.isCompleted = true;
        StageManager.Instance.SaveStageCompletion(stageData.stageName);
        Debug.Log($"Stage {stageData.stageName} marked as completed");
        UpdateButtonState();

        // Load the scene associated with this stage
        SceneManager.LoadScene(stageData.stageName);
    }

    bool AreParentsCompleted()
    {
        // If there are no parents, stage has completed parents.
        if (stageData.parentStages == null || stageData.parentStages.Length == 0)
        {
            Debug.Log($"Stage {stageData.stageName} has no parents");
            return true;
        }

        foreach (var parentStage in stageData.parentStages)
        {
            bool isParentCompleted = StageManager.Instance.IsStageCompleted(parentStage.stageName);
            if (!isParentCompleted)
            {
                Debug.Log($"Parent stage {parentStage.stageName} of {stageData.stageName} is not completed");
                return false;
            }
        }
        Debug.Log($"All parents of stage {stageData.stageName} are completed");
        return true;
    }
}
