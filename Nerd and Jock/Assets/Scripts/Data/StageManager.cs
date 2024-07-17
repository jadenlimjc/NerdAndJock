using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance; // Singleton instance

    [SerializeField] 
    private List<StageDataSO> allStages; 
    private Dictionary<string, bool> unlockedStages = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            InitializeStages();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
    }

    private void InitializeStages()
    {
        if (unlockedStages.Count == 0)
        {
            // Initialize all stages as locked except the first
            foreach (StageDataSO stageDataSO in allStages)
            {
                unlockedStages[stageDataSO.stageName] = false;
            }
            if (allStages.Count > 0)
            {
                unlockedStages[allStages[0].stageName] = true;
            }
        }
    }

    public void UnlockNextStages(string currentStageName)
    {
        StageDataSO currentStage = allStages.Find(stage => stage.stageName == currentStageName);
        Debug.Log($"Trying to unlock stages following {currentStageName} using UnlockNextStages");
        if (currentStage != null)
        {
            //Debug.Log($"{currentStageName} is not null");
            foreach (StageDataSO nextStage in currentStage.nextLevels)
            {
                string nextStageName = nextStage.stageName;
                //Debug.Log($"Stage: {nextStageName}");
                unlockedStages[nextStageName] = true;
                //Debug.Log("Unlocked next stage: " + nextStageName);
            }
        }
    }

    public bool IsStageUnlocked(string stageName)
    {
        return unlockedStages.TryGetValue(stageName, out bool isUnlocked) && isUnlocked;
    }
}