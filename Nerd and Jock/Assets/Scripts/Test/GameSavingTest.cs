/* 
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestRunner;

public class GameStateTest
{
    private StageManager stageManager;
    private JSONSaving jsonSaving;

    [SetUp]
    public void Setup()
    {
        GameObject gameObj = new GameObject();
        stageManager = gameObj.AddComponent<StageManager>();
        jsonSaving = gameObj.AddComponent<JSONSaving>();
        stageManager.InitializeStages();
    }

    [Test]
    public void TestStageUnlocking()
    {
        string currentStage = "NJ1001";
        stageManager.UnlockNextStages(currentStage);
        
        // "NJ2001, NJ2012, NJ2020, NJ2020" are the next stages and should be unlocked after completing "NJ1001"
        Assert.IsTrue(stageManager.IsStageUnlocked("NJ2001"), "The next stage should be unlocked after completing the current stage.");
        Assert.IsTrue(stageManager.IsStageUnlocked("NJ2012"), "The next stage should be unlocked after completing the current stage.");
        Assert.IsTrue(stageManager.IsStageUnlocked("NJ2020"), "The next stage should be unlocked after completing the current stage.");
        Assert.IsTrue(stageManager.IsStageUnlocked("NJ2021"), "The next stage should be unlocked after completing the current stage.");
    }

    [Test]
    public void TestSaveLoadFunctionality()
    {
        StageData expectedStage = new StageData("NJ1001", "A+", 3, true, 120f);
        jsonSaving.GetGameData().stages.Add(expectedStage);
        jsonSaving.SaveData();

        jsonSaving.ClearGameData();
        jsonSaving.LoadData();

        StageData loadedStage = jsonSaving.GetGameData().stages.Find(s => s.stageName == "NJ1001");
        Assert.IsNotNull(loadedStage, "Stage data should be loaded.");
        Assert.AreEqual(expectedStage.bestGrade, loadedStage.bestGrade, "Loaded stage grade should match saved grade.");
    }

    [Test]
    public void TestStageInitialization()
    {
        stageManager.InitializeStages();
        Assert.IsFalse(stageManager.IsStageUnlocked("NJ2001"), "Initially, only the first stage should be unlocked.");
        Assert.IsFalse(stageManager.IsStageUnlocked("NJ2012"), "Initially, only the first stage should be unlocked.");
        Assert.IsFalse(stageManager.IsStageUnlocked("NJ2020"), "Initially, only the first stage should be unlocked.");
        Assert.IsFalse(stageManager.IsStageUnlocked("NJ2021"), "Initially, only the first stage should be unlocked.");
        Assert.IsFalse(stageManager.IsStageUnlocked("NJ3001"), "Initially, only the first stage should be unlocked.");
        Assert.IsFalse(stageManager.IsStageUnlocked("NJ3012"), "Initially, only the first stage should be unlocked.");
        Assert.IsFalse(stageManager.IsStageUnlocked("NJ3020"), "Initially, only the first stage should be unlocked.");

        stageManager.UnlockNextStages("NJ1001");
        Assert.IsTrue(stageManager.IsStageUnlocked("NJ2001"), "NJ2001 should be unlocked after NJ1001 is completed.");
        Assert.IsTrue(stageManager.IsStageUnlocked("NJ2012"), "NJ2012 should be unlocked after NJ1001 is completed.");
        Assert.IsTrue(stageManager.IsStageUnlocked("NJ2020"), "NJ2020 should be unlocked after NJ1001 is completed.");
        Assert.IsTrue(stageManager.IsStageUnlocked("NJ2021"), "NJ2021 should be unlocked after NJ1001 is completed.");
    }
}
 */