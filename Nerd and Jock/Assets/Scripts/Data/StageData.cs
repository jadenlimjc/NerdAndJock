using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public List<StageData> stages = new List<StageData>();
}

[Serializable]
public class StageData
{
    public string stageName;
    public string bestGrade;
    public int stars;
    public bool unlocked;
    public float bestTime = float.MaxValue;
    public string[] nextLevels;

    public StageData(string stageName, string bestGrade, int stars, bool unlocked, float bestTime, string[] nextLevels)
    {
        this.stageName = stageName;
        this.bestGrade = bestGrade;
        this.stars = stars;
        this.unlocked = unlocked;
        this.bestTime = bestTime;
        this.nextLevels = nextLevels;
    }

    public override string ToString()
    {
        return $"[{unlocked}] Stage {stageName}: Grade {bestGrade} with timing {bestTime}, {stars} stars.";
    }
}