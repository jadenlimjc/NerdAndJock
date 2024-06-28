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
    public GameObject continueButton;
    private Animator nerdAnimator;
    private Animator jockAnimator;
    private Rigidbody2D nerdRigidbody;
    private Rigidbody2D jockRigidbody;
    private string[] tutorialDialogueSentences;

    private float speechBubbleAnimationDelay = 1.0f;
    private float continueButtonDelay = 0.2f;
    private bool dialogueActive = false;


    public void Start()
    {
        continueButton.SetActive(false);
        if (initialDialogueSentences != null && initialDialogueSentences.Length > 0)
        {
            StartDialogue(initialDialogueSentences);
        }
        // Initialize animators
        nerdAnimator = nerd.GetComponent<Animator>();
        jockAnimator = jock.GetComponent<Animator>();
        nerdRigidbody = nerd.GetComponent<Rigidbody2D>();
        jockRigidbody = jock.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueDialogue();
        }
    }
    public void StartDialogue(string[] dialogueSentences)
    {
        dialogueActive = true;
        tutorialDialogueSentences = dialogueSentences;
        sentenceIndex = 0;
        StartCoroutine(StartDialogueCoroutine());
    }

    private IEnumerator StartDialogueCoroutine()
    {
        LockPlayerMovement(true);
        tutorialSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypeTutorialDialogue());
    }

    private IEnumerator TypeTutorialDialogue()
    {
        continueButton.SetActive(false);
        tutorialDialogueText.text = string.Empty;
        foreach (char letter in tutorialDialogueSentences[sentenceIndex].ToCharArray())
        {
            tutorialDialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(continueButtonDelay);
        continueButton.SetActive(true);
    }

    private void ContinueDialogue()
    {
        if (continueButton.activeSelf)
        {
            continueButton.SetActive(false);
            if (sentenceIndex < tutorialDialogueSentences.Length - 1)
            {
                sentenceIndex++;
                StartCoroutine(TypeTutorialDialogue());
            }
            else
            {
                StartCoroutine(EndDialogue());
            }
        }
    }

    private IEnumerator EndDialogue()
    {
        tutorialDialogueText.text = string.Empty;
        tutorialSpeechBubbleAnimator.SetTrigger("Close");
        LockPlayerMovement(false);
        dialogueActive = false;
        yield return null;
    }

    void LockPlayerMovement(bool lockMovement)
    {
        if (lockMovement)
        {
            // Set animations to idle
            if (nerdAnimator != null)
            {
                nerdAnimator.SetFloat("Speed", 0);
                nerdAnimator.SetBool("IsJumping", false);

            }

            if (jockAnimator != null)
            {
                jockAnimator.SetFloat("Speed", 0);
                nerdAnimator.SetBool("IsJumping", false);
            }
            if (nerdRigidbody != null)
            {
                nerdRigidbody.velocity = Vector2.zero;
            }

            if (jockRigidbody != null)
            {
                jockRigidbody.velocity = Vector2.zero;
            }
        }
        nerd.GetComponent<NerdController>().enabled = !lockMovement;
        jock.GetComponent<JockController>().enabled = !lockMovement;
    }
}