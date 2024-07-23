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
    public AudioClip typeClip;
    public AudioClip popClip;
    public AudioClip mainMenuBGMClip;

    private Dictionary<AudioType, AudioSource> audioSources;
    private Dictionary<AudioType, AudioSource> loopingAudioSources;

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
        audioSources = new Dictionary<AudioType, AudioSource>
        {
            { AudioType.Hover, gameObject.AddComponent<AudioSource>() },
            { AudioType.Click, gameObject.AddComponent<AudioSource>() },
            { AudioType.Exit, gameObject.AddComponent<AudioSource>() },
            { AudioType.Fail, gameObject.AddComponent<AudioSource>() },
            { AudioType.Pass, gameObject.AddComponent<AudioSource>() },
            { AudioType.Star, gameObject.AddComponent<AudioSource>() },
            { AudioType.NerdJump, gameObject.AddComponent<AudioSource>() },
            { AudioType.NerdInteract, gameObject.AddComponent<AudioSource>() },
            { AudioType.JockJump, gameObject.AddComponent<AudioSource>() },
            { AudioType.JockInteract, gameObject.AddComponent<AudioSource>() },
            { AudioType.Pop, gameObject.AddComponent<AudioSource>() },
        };

        loopingAudioSources = new Dictionary<AudioType, AudioSource>
        {
            { AudioType.MainMenuBGM, gameObject.AddComponent<AudioSource>() }
        };

        Debug.Log("Audio sources initialized");
    }

    public void PlaySound(AudioType audioType, float volume = 1.0f)
    {
        if (audioSources.TryGetValue(audioType, out AudioSource audioSource))
        {
            AudioClip clip = GetAudioClip(audioType);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.volume = volume;
                audioSource.loop = false;
                audioSource.Play();
                Debug.Log($"Playing sound: {audioType}");
            }
            else
            {
                Debug.LogError($"AudioClip for {audioType} is null.");
            }
        }
        else
        {
            Debug.LogError($"AudioSource for {audioType} not found.");
        }
    }

    public void PlayLoopingSound(AudioType audioType, float volume = 1.0f)
    {
        if (loopingAudioSources.TryGetValue(audioType, out AudioSource audioSource))
        {
            AudioClip clip = GetAudioClip(audioType);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.loop = true;
                audioSource.volume = volume;
                audioSource.Play();
                Debug.Log($"Playing looping sound: {audioType}");
            }
            else
            {
                Debug.LogError($"AudioClip for {audioType} is null.");
            }
        }
        else
        {
            Debug.LogError($"Looping AudioSource for {audioType} not found.");
        }
    }

    public void StopSound(AudioType audioType)
    {
        if (audioSources.TryGetValue(audioType, out AudioSource audioSource))
        {
            audioSource.Stop();
            Debug.Log($"Stopping sound: {audioType}");
        }
        else if (loopingAudioSources.TryGetValue(audioType, out AudioSource loopingAudioSource))
        {
            loopingAudioSource.Stop();
            Debug.Log($"Stopping looping sound: {audioType}");
        }
        else
        {
            Debug.LogError($"AudioSource for {audioType} not found.");
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
            case AudioType.Type: return typeClip;
            case AudioType.Pop: return popClip;
            case AudioType.MainMenuBGM: return mainMenuBGMClip;
            default: return null;
        }
    }

}
