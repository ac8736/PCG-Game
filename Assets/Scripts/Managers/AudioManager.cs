using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip ---------")]
    public AudioClip normalbgm;
    public AudioClip deathbgm;

    public AudioClip magic; 
    public AudioClip takeDamage;

    private static AudioManager instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = normalbgm;
        if (SceneManager.GetActiveScene().name == "Death")
        {
            musicSource.clip = deathbgm;
        }
        
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ToggleMusic()
    {
        musicSource.mute =!musicSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        Debug.Log(volume);
    }

    public void ToggleSFX()
    {
        SFXSource.mute =!SFXSource.mute;
    }

    public void SFXVolume(float volume)
    {
        SFXSource.volume = volume;
        Debug.Log(volume);
    }

    
}
