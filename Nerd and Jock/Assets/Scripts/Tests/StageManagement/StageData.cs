using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "Stage/StageData")]
public class StageData : ScriptableObject
{
    public string stageName;
    public bool isCompleted;
    public StageData[] childStages;
    public StageData[] parentStages;
}