using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    public AudioClip popClip;
    public AudioClip typingClip;
    public AudioClip earthquakeClip;
    public AudioClip mainMenuBGMClip;
    public AudioClip NJ1001BGMClip;
    public AudioClip NJ2001BGMClip;
    public AudioClip NJ2012BGMClip;
    public AudioClip NJ2020BGMClip;
    public AudioClip NJ2021BGMClip;
    public AudioClip NJ3001BGMClip;
    public AudioClip NJ3012BGMClip;
    public AudioClip NJ3020BGMClip;
    public AudioClip NJ3021BGMClip;

    private Dictionary<AudioType, AudioSource> audioSources;
    private Dictionary<string, AudioClip> sceneBGMMapping;
    private AudioSource bgmAudioSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            InitializeSceneBGMMapping();
            SceneManager.sceneLoaded += OnSceneLoaded;
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
            { AudioType.Typing, gameObject.AddComponent<AudioSource>()},
            { AudioType.Earthquake, gameObject.GetComponent<AudioSource>() },
        };

        // Initialize the BGM AudioSource
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true; // Ensure the BGM loops
        bgmAudioSource.volume = 0.5f; // Adjust the volume as needed
    }

    private void InitializeSceneBGMMapping()
    {
        sceneBGMMapping = new Dictionary<string, AudioClip>
        {
            { "HomeScreenScene", mainMenuBGMClip },
            { "NJ1001", NJ1001BGMClip },
            { "NJ2001", NJ2001BGMClip },
            { "NJ2012", NJ2012BGMClip},
            { "NJ2020", NJ2020BGMClip},
            { "NJ2021", NJ2021BGMClip},
            { "NJ3001", NJ3001BGMClip},
            { "NJ3012", NJ3012BGMClip},
            { "NJ3020", NJ3020BGMClip},
            { "NJ3021", NJ3021BGMClip}
        };
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EndScene") {
            bgmAudioSource.Stop();
        }
        else {
            PlayBGM(scene.name);
        }
        
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
              //  Debug.LogError($"AudioClip for {audioType} is null.");
            }
        }
        else
        {
            //Debug.LogError($"AudioSource for {audioType} not found.");
        }
    }

    public void PlayBGM(string sceneName)
    {
        if (sceneBGMMapping.TryGetValue(sceneName, out AudioClip bgmClip))
        {
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.loop = true; // Ensure the audio source loops the BGM
            bgmAudioSource.Play();
           // Debug.Log($"Playing BGM for scene: {sceneName}");
        }
        else
        {
           // Debug.LogWarning($"No BGM found for scene: {sceneName}");
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
            case AudioType.Pop: return popClip;
            case AudioType.Typing: return typingClip;
            case AudioType.Earthquake: return earthquakeClip;
            default: return null;
        }
    }

}
