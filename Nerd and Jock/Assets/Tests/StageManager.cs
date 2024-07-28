using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance; // Singleton instance

    [SerializeField] 
    public List<StageDataSO> allStages; 
    public Dictionary<string, bool> unlockedStages = new Dictionary<string, bool>();
    private JSONSaving jsonSaving;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        jsonSaving = FindObjectOfType<JSONSaving>();
        if (jsonSaving == null)
        {
            Debug.LogError("JSONSaving not found!");
        }
    }

    public void InitializeStages()
    {
        if (allStages != null && unlockedStages.Count == 0)
        {
            foreach (StageDataSO stageDataSO in allStages)
            {
                unlockedStages[stageDataSO.stageName] = false;
            }
            if (allStages.Count > 0)
            {
                unlockedStages[allStages[0].stageName] = true;  // Assume the first stage is initially unlocked
            }
        }
        else
        {
            Debug.LogError("allStages list has not been initialized!");
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
                
                StageData stageData = jsonSaving.GetGameData().stages.Find(s => s.stageName == nextStageName);
                if (stageData == null)
                {
                    stageData = new StageData(nextStageName, "", 0, true, float.MaxValue);
                    jsonSaving.GetGameData().stages.Add(stageData);
                }
                else
                {
                    stageData.unlocked = true;
                    Debug.Log("Unlocked next stage: " + nextStageName);
                }
            }
            jsonSaving.SaveData();
        }
    }

    public bool IsStageUnlocked(string stageName)
    {
        return unlockedStages.TryGetValue(stageName, out bool isUnlocked) && isUnlocked;
    }

    public void InitializeGameDataFromSO()
    {
        jsonSaving.ClearGameData();
        InitializeStages();
        foreach (StageDataSO stageDataSO in allStages)
        {
            bool isUnlocked = false;
            unlockedStages.TryGetValue(stageDataSO.stageName, out isUnlocked);

            StageData newStage = new StageData(stageDataSO.stageName, "", 0, isUnlocked, float.MaxValue);
            jsonSaving.GetGameData().stages.Add(newStage);
        }
        jsonSaving.SaveData();
    }
}