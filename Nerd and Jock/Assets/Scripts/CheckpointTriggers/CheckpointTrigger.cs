using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private tutorialDialogueManager dialogueManager;
    [TextArea]
    [SerializeField] private string[] dialogueSentences;
    public bool nerdInCheckpoint = false;
    public bool jockInCheckpoint = false;
    private bool dialogueStarted = false;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("jock")) {
            jockInCheckpoint = true;
        }
        else if (other.CompareTag("nerd")) {
            nerdInCheckpoint = true;
        }
        CheckBothPlayersInCheckpoint();
    }

    private void OnTriggerExit2D(Collider2D other) {
         if (other.CompareTag("jock")) {
            jockInCheckpoint = false;
        }
        else if (other.CompareTag("nerd")) {
            nerdInCheckpoint = false;
        }
    }

    private void CheckBothPlayersInCheckpoint() {
        if (nerdInCheckpoint && jockInCheckpoint && !dialogueStarted) {
            dialogueStarted = true;
            dialogueManager.StartDialogue(dialogueSentences);
        }
    }
}
