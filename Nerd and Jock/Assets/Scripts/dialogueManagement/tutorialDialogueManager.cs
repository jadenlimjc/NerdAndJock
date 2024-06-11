using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tutorialDialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialDialogueText;
    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] tutorialDialogueSentences;
    [SerializeField] private float textSpeed = 0.01f;
    [Header("Animation Controllers")]
    [SerializeField] private Animator tutorialSpeechBubbleAnimator;
    private int sentenceIndex;

    private float speechBubbleAnimationDelay = 0.6f;
    private bool typing = false;

    private void Start() {
        StartCoroutine(StartDialogue());
    }
    private void Update() {
        if (Input.anyKey) {
            StartCoroutine(ContinueDialogue());
        }
    }
    public IEnumerator StartDialogue() {
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
            yield return null;
        }
    }
}

