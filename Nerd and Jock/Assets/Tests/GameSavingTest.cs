using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameSavingTests
{
    private JSONSaving jsonSaving;
    private StageManager stageManager;
    private GameObject gameObj;
    [SetUp]
    public void Setup()
    {
        gameObj = new GameObject("TestStageManager");
        stageManager = gameObj.AddComponent<StageManager>();
        jsonSaving = gameObj.AddComponent<JSONSaving>();
        stageManager.allStages = SetupMockStages();
        stageManager.InitializeStages();
    }
    private List<StageDataSO> SetupMockStages()
    {
        var stages = new List<StageDataSO>();
        var stage1 = ScriptableObject.CreateInstance<StageDataSO>();
        stage1.stageName = "Stage1";
        stages.Add(stage1);
        var stage2 = ScriptableObject.CreateInstance<StageDataSO>();
        stage2.stageName = "Stage2";
        stages.Add(stage2);
        return stages;
    }
    [Test]
    public void TestStageInitialization()
    {
        Assert.IsTrue(stageManager.IsStageUnlocked("Stage1"), "Stage1 should be unlocked.");
        Assert.IsFalse(stageManager.IsStageUnlocked("Stage2"), "Stage2 should be locked.");
    }
    [TearDown]
    public void TearDown()
    {
        if (gameObj != null)
        {
            Object.DestroyImmediate(gameObj);
        }
    }
}
