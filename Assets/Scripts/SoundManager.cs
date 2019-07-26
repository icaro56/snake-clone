using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource secondEfxSource;
    public AudioSource backgroundSource;                 
    public static SoundManager instance = null;            
    public float lowPitchRange = .95f;              
    public float highPitchRange = 1.05f;            


    void Awake()
    {
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public bool IsPlayingSingleSound(AudioClip clip)
    {
        return (efxSource.clip.Equals(clip) && efxSource.isPlaying);
    }

    public void PlaySingle(AudioClip clip, float pitch = 1.0f)
    {
        efxSource.clip = clip;
        efxSource.pitch = pitch;

        efxSource.Play();
    }

    public void PlaySingleSecondLayer(AudioClip clip, float pitch = 1.0f)
    {
        secondEfxSource.clip = clip;
        secondEfxSource.pitch = pitch;

        secondEfxSource.Play();
    }

    public void PlayBackgroundSound(AudioClip clip, float pitch = 1.0f)
    {
        backgroundSource.clip = clip;
        backgroundSource.pitch = pitch;
        backgroundSource.Play(); 
    }

    public bool IsPlayingBackgroundSound(AudioClip clip)
    {
        return (backgroundSource.clip.Equals(clip) && backgroundSource.isPlaying);
    }

    public void setPitchBackgroundSound(float pitch)
    {
        backgroundSource.pitch = pitch;
    }

    public void StopBackgroundSound()
    {
        if (backgroundSource.clip != null)
            backgroundSource.Stop();
    }

    public void StopAllSounds()
    {
        StopBackgroundSound();

        efxSource.Stop();

        secondEfxSource.Stop();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;

        efxSource.clip = clips[randomIndex];

        efxSource.Play();
    }
}