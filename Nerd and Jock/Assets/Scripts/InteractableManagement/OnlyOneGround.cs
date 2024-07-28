using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOneGround : MonoBehaviour , IInteractable
{
    private int playerCount = 0;
    public Animator groundAnimator;
    public Collider2D groundCollider;
    public float respawnTime = 3.0f;
    private bool isBroken = false;


    public void Start()
    {
        if (groundAnimator == null)
        {
            groundAnimator = GetComponent<Animator>();
        }
        if (groundCollider == null)
        {
            groundCollider = GetComponent<Collider2D>();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("jock") || collider.CompareTag("nerd"))
        {
            playerCount++;
            Debug.Log("Player entered: " + playerCount);
            CheckState();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("jock") || collider.CompareTag("nerd"))
        {
            playerCount--;
            Debug.Log("Player exited: " + playerCount);
            CheckState();
        }
    }

    private void CheckState()
    {
        if (playerCount == 1)
        {
            if (!isBroken)
            {
                groundAnimator.SetBool("OnePlayer", true);
                groundAnimator.SetBool("MoreThanOnePlayer", false);
            }
        }
        else if (playerCount == 0)
        {
            if (!isBroken)
            {
                groundAnimator.SetBool("OnePlayer", false);
                groundAnimator.SetBool("MoreThanOnePlayer", false);
            }
        }
        else if (playerCount >= 2)
        {
            groundAnimator.SetBool("OnePlayer", false);
            groundAnimator.SetBool("MoreThanOnePlayer", true);
            if (!isBroken)
            {
                StartCoroutine(BreakGround());
            }
        }
    }

    public void Respawn()
    {
        isBroken = false;
        GetComponent<SpriteRenderer>().enabled = true;
        groundCollider.enabled = true;
        groundAnimator.ResetTrigger("Break");
        groundAnimator.SetTrigger("Respawn");
    }

    // To be called by animator 
    private IEnumerator BreakGround()
    {
        Debug.Log("Breaking ground now");
        isBroken = true;
        groundAnimator.SetTrigger("Break");
        yield return new WaitForSeconds(groundAnimator.GetCurrentAnimatorStateInfo(0).length);
        groundCollider.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(respawnTime);
        Respawn();
    }

    public void OnInteract()
    {
    }
}
