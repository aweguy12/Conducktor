/*
 * Name: Danny Rosemond
 * Date: 3/24/2025
 * Desc: Used for the settings menu to change volume of sounds
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//Attach script to empty gameobject
public class VolumeControl : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider musicSlider;
    public AudioMixer sfxMixer;
    public AudioMixer musicMixer;

    public string sfxVolumeParameter = "SFXVolume";
    public string musicVolumeParameter = "MusicVolume";
    void Start()
    {
        float sfxValue;
        float musicValue;

        sfxMixer.GetFloat(sfxVolumeParameter, out sfxValue);
        musicMixer.GetFloat(musicVolumeParameter, out musicValue);

        sfxSlider.value = sfxValue;
        musicSlider.value = musicValue;

        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat(sfxVolumeParameter, volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat(musicVolumeParameter, volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}
