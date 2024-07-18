using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;

    public void PlayHoverSound()
    {
        if (hoverClip != null)
        {
            audioSource.PlayOneShot(hoverClip);
        }
    }

    public void PlayClickSound()
    {
        if (clickClip != null)
        {
            audioSource.PlayOneShot(clickClip);
        }
    }
}
