using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip hoverClip;
    public AudioClip clickClip;
    public AudioClip exitClip;
    public AudioClip failClip;
    public AudioClip passClip;
    public AudioClip starClip;
    public AudioClip nerdJumpClip;
    public AudioClip nerdInteractClip;
    public AudioClip jockJumpClip;
    public AudioClip jockInteractClip;

    private Dictionary<AudioType, AudioSource> audioSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSources()
    {
        audioSources = new Dictionary<AudioType, AudioSource>();

        audioSources.Add(AudioType.Hover, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.Click, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.Exit, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.Fail, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.Pass, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.Star, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.NerdJump, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.NerdInteract, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.JockJump, gameObject.AddComponent<AudioSource>());
        audioSources.Add(AudioType.JockInteract, gameObject.AddComponent<AudioSource>());
    }

    public void PlaySound(AudioType audioType)
    {
        AudioClip clip = GetAudioClip(audioType);
        if (clip != null && audioSources.TryGetValue(audioType, out AudioSource audioSource))
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void StopSound(AudioType audioType)
    {
        if (audioSources.TryGetValue(audioType, out AudioSource audioSource))
        {
            audioSource.Stop();
        }
    }
    private AudioClip GetAudioClip(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Hover: return hoverClip;
            case AudioType.Click: return clickClip;
            case AudioType.Exit: return exitClip;
            case AudioType.Fail: return failClip;
            case AudioType.Pass: return passClip;
            case AudioType.Star: return starClip;
            case AudioType.NerdJump: return nerdJumpClip;
            case AudioType.NerdInteract: return nerdInteractClip;
            case AudioType.JockJump: return jockJumpClip;
            case AudioType.JockInteract: return jockInteractClip;
            default: return null;
        }
    }

}
