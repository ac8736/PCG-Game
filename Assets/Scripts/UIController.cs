using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the UI in the game
public class UIController : MonoBehaviour
{
    public Slider _sfxSlider, _musicSlider;

    // void Start()
    // {
    //     _bgmSlider.value = bgmAudioSource.volume;
    //     _sfxSlider.value = sfxAudioSources[0].volume;
    // }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void SFXVolume()
    {
        AudioManager.Instance.MusicVolume(_sfxSlider.value);
    }

    // public void SetBGMVolume(float volume)
    // {
    //     bgmAudioSource.volume = volume;
    // }

    // public void SetSFXVolume(float volume)
    // {
    //     foreach (AudioSource sfxSource in sfxAudioSources)
    //     {
    //         sfxSource.volume = volume;
    //     }
    // }
}
