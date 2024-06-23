using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] private tutorialDialogueManager dialogueManager;
    [TextArea]
    [SerializeField] private string[] dialogueSentences;

    private void Start() {
        dialogueManager.StartDialogue(dialogueSentences);
    }
}