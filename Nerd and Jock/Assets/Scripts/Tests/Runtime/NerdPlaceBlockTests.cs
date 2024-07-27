/* using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class NerdPlaceBlockTests
{
    [Test]
    public void NerdPlaceBlock_InitialCharges_AreCorrect()
    {
        var gameObject = new GameObject();
        var nerdPlaceBlock = gameObject.AddComponent<NerdPlaceBlock>();
        Assert.AreEqual(2, nerdPlaceBlock.currentCharges);
    }

    [UnityTest]
    public IEnumerator NerdPlaceBlock_Cooldown_WorksCorrectly()
    {
        var gameObject = new GameObject();
        var nerdPlaceBlock = gameObject.AddComponent<NerdPlaceBlock>();

        // Use up a charge
        nerdPlaceBlock.UseCharge();

        // Check if cooldown timer starts
        Assert.AreEqual(10f, nerdPlaceBlock.cooldownTimer);

        // Wait for the cooldown to end
        yield return new WaitForSeconds(10f);

        // Check if charges are refilled
        Assert.AreEqual(1, nerdPlaceBlock.currentCharges);
    }
}
 */