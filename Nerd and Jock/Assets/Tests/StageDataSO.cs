using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Stage/Create New Stage")]
public class StageDataSO : ScriptableObject
{
    public string stageName;
    public StageDataSO[] nextLevels; 

    public StageDataSO Initialize(string name, StageDataSO[] nextStages)
    {
        this.stageName = name;
        this.nextLevels = nextStages;
        return this;
    }
}