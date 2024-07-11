using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class StageManagerTests
{
    private StageManager stageManager;
    private StageData stage1;
    private StageData stage2;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        stageManager = new GameObject().AddComponent<StageManager>();

        // Clear PlayerPrefs before each test
        PlayerPrefs.DeleteAll();

        // Create mock stage data
        stage1 = ScriptableObject.CreateInstance<StageData>();
        stage1.stageName = "Stage1";
        stage1.parentStages = new StageData[0];

        stage2 = ScriptableObject.CreateInstance<StageData>();
        stage2.stageName = "Stage2";
        stage2.parentStages = new StageData[] { stage1 };

        stageManager.stages = new StageData[] { stage1 };
    }

    [Test]
    public void TestStageCompletion() {
        // Arrange: ensure stages are not completed at start
        Assert.IsFalse(stageManager.IsStageCompleted("Stage1"));
        Assert.IsFalse(stageManager.IsStageCompleted("Stage2"));

        // Act: Complete Stage1
        stageManager.SaveStageCompletion("Stage1");

        // Assert: Verify Stage1 is completed
        Assert.IsTrue(stageManager.IsStageCompleted("Stage1"));

        // Assert: Stage2 should still be incomplete
        Assert.IsFalse(stageManager.IsStageCompleted("Stage2"));

    }

    [Test]
    
    public void TestStageButtonState() {
        // Arrange: create stage button and assign StageData

        GameObject buttonObj = new GameObject();
        StageButton stageButton = buttonObj.AddComponent<StageButton>();
        stageButton.stageData = stage2;
        stageButton.button = buttonObj.AddComponent<Button>();

        // Update button state before parent is completed
        stageButton.UpdateButtonState();

        // Assert: Verify button is not interactable before completing the parent stage
        Debug.Log($"Button interactable state before completing parent: {stageButton.button.interactable}");
        Assert.IsFalse(stageButton.button.interactable, "Button should not be interactable before parent stage is completed.");

        // Verify color is grey (disabled state)
        ColorBlock colorBlock = stageButton.button.colors;
        Debug.Log($"Button color before completing parent: {colorBlock.normalColor}");
        Assert.AreEqual(Color.gray, colorBlock.normalColor, "Button color should be grey when not interactable.");




        // Act: Complete Stage1
        stageManager.SaveStageCompletion("Stage1");

        // Update button state after parent is completed
        stageButton.UpdateButtonState();

        // Assert: Verify button is interactable after completing the parent stage
        Debug.Log($"Button interactable state after completing parent: {stageButton.button.interactable}");
        Assert.IsTrue(stageButton.button.interactable, "Button should be interactable after parent stage is completed.");

        // Verify color is white (normal state)
        colorBlock = stageButton.button.colors;
        Debug.Log($"Button color after completing parent: {colorBlock.normalColor}");
        Assert.AreEqual(Color.white, colorBlock.normalColor, "Button color should be white when interactable.");
    }
    

    [TearDown]
    public void TearDown()
    {
        // Cleanup any created game objects
        Object.Destroy(stageManager.gameObject);
    }
}
