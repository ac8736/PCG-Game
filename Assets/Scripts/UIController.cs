using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the UI in the game
public class UIController : MonoBehaviour
{
    public Slider _sfxSlider, _musicSlider;

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
}
