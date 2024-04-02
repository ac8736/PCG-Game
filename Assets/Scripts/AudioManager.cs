using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip ---------")]
    public AudioClip normalbgm;
    public AudioClip deathbgm;

    public AudioClip magic; 
    public AudioClip takeDamage; 

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
}
