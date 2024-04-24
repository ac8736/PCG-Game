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
    // public AudioClip normalbgm;
    public List<AudioClip> normalbgm;

    public AudioClip deathbgm;

    public AudioClip magic; 
    public AudioClip takeDamage;
    // public int GlobalVars.floor globalVars;

    private static AudioManager instance;
    private int level = 0;

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
        level = GlobalVars.floor;
        // Debug.Log("TAT" + level + GlobalVars.floor);
        musicSource.clip = normalbgm[level];
        if (SceneManager.GetActiveScene().name == "Death")
        {
            musicSource.clip = deathbgm;
        }
        
        musicSource.Play();
    }

    // private void Update()
    // {
    //     level = GlobalVars.floor;
    //     Debug.Log("ATA" + level + GlobalVars.floor);
       
    //     musicSource.clip = normalbgm[level];
    //     musicSource.Play();
    // }
    

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
