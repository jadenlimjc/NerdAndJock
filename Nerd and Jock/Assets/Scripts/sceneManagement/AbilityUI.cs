using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    [Header("Nerd UI Elements")]
    public Image nerdImage;
    public Image nerdAbilityIcon;
    public TMP_Text nerdChargesText;
    public TMP_Text nerdCooldownText;
    public NerdPlaceBlock nerdAbilityScript;

    [Header("Jock UI Elements")]
    public Image jockImage;
    public Image jockAbilityIcon;
    public TMP_Text jockCooldownText;
    public JockPortal jockAbilityScript;

    void Update()
    {
        // Update Nerd UI
        nerdChargesText.text = nerdAbilityScript.currentCharges.ToString();
        if (nerdAbilityScript.cooldownTimer > 0)
        {
            nerdCooldownText.text = nerdAbilityScript.cooldownTimer.ToString("F1") + "s";
        }
        else
        {
            nerdCooldownText.text = string.Empty;
        }

        if (jockAbilityScript.teleportCooldownTimer > 0)
        {
            jockCooldownText.text = jockAbilityScript.teleportCooldownTimer.ToString("F1") + "s";
        }
        else
        {
            jockCooldownText.text = string.Empty;
        }
    }
}
