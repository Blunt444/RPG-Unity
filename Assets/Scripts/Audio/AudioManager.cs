using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource sfxSource;
    public AudioSource musicSource;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;
    public AudioClip[] bgMusic;
    public Coroutine musicCoroutine;
    public float timeForNewMusic = 0f;
    public float maxTimeFornewMusic = 200f;
    public AudioClip CurrentAudioClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
        PlayMusic(bgMusic[0]);
    }

    private void FixedUpdate()
    {
        timeForNewMusic += Time.unscaledDeltaTime;
        if (timeForNewMusic > maxTimeFornewMusic)
        {
            PlayRandomMusic();
        }
    }

    public void PlayRandomMusic()
    {
        PlayMusic(bgMusic[UnityEngine.Random.Range(0, bgMusic.Length)]);
    }

    private void OnValidate()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    public void PlayMusic(AudioClip audioClip, bool loop = true)
    {
        Debug.Log(audioClip.ToString());
        timeForNewMusic = 0f;
        if (CurrentAudioClip == audioClip) return;

        CurrentAudioClip = audioClip;
        musicSource.loop = loop;
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
            musicSource.volume = musicVolume;
        }
        musicCoroutine = StartCoroutine(FadeInandOut(audioClip));
    }


    private IEnumerator FadeInandOut(AudioClip audioClip, float fadeDuration = 1f)
    {
        float startVolume = musicSource.volume;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = elapsed / fadeDuration;
            musicSource.volume = Mathf.Lerp(startVolume, 0, progress);
            yield return null;
        }

        musicSource.clip = audioClip;
        musicSource.Play();
        elapsed = 0;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = elapsed / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, startVolume, progress);
            yield return null;
        }
        musicSource.volume = musicVolume;
        musicCoroutine = null;
    }

    public void PlaySFX(AudioClip audioClip, float volumeScale)
    {
        if (audioClip == null) return;
        sfxSource.PlayOneShot(audioClip, volumeScale * sfxVolume);
    }
    public void PlaySFX(AudioClip audioClip)
    {
        if (audioClip == null) return;
        sfxSource.PlayOneShot(audioClip, sfxVolume);
    }
}
