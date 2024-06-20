using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tutorialDialogueManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI tutorialDialogueText;
    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] initialDialogueSentences;
    [SerializeField] private float textSpeed = 0.01f;
    [Header("Animation Controllers")]
    [SerializeField] private Animator tutorialSpeechBubbleAnimator;
    private int sentenceIndex;
    //reference to players
    public Transform nerd; 
    public Transform jock;
    private string[] tutorialDialogueSentences;

    private float speechBubbleAnimationDelay = 1.0f;
    private bool typing = false;
    private bool dialogueActive = false;


    public void Start() {
        if (initialDialogueSentences != null && initialDialogueSentences.Length > 0) {
            StartDialogue(initialDialogueSentences);
        }
    }
    private void Update() {
        if (dialogueActive && !typing && Input.anyKeyDown) {
            StartCoroutine(ContinueDialogue());
        }
    }
    public void StartDialogue(string[] dialogueSentences) {
        tutorialDialogueSentences = dialogueSentences;
        sentenceIndex = 0;
        dialogueActive = true;
        StartCoroutine(StartDialogueCoroutine());
    }

    private IEnumerator StartDialogueCoroutine() {
        LockPlayerMovement(true);
        tutorialSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypeTutorialDialogue());
    }

    private IEnumerator TypeTutorialDialogue() {
        typing = true;
        foreach (char letter in tutorialDialogueSentences[sentenceIndex].ToCharArray()) {
            tutorialDialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        typing = false;
    }

    private IEnumerator ContinueDialogue() {

        if (!typing && sentenceIndex < tutorialDialogueSentences.Length - 1) {
            sentenceIndex++;
            tutorialDialogueText.text = string.Empty;
            StartCoroutine(TypeTutorialDialogue());
        }
        else if (!typing) {
            tutorialDialogueText.text = string.Empty;
            tutorialSpeechBubbleAnimator.SetTrigger("Close");
            LockPlayerMovement(false);
            dialogueActive = false;
            yield return null;
        }
    }

     void LockPlayerMovement(bool lockMovement)
    {
        nerd.GetComponent<NerdController>().enabled = !lockMovement;
        jock.GetComponent<JockController>().enabled = !lockMovement;
    }
}