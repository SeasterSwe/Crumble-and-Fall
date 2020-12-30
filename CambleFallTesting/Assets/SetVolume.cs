using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


// Gör så att slider funkar i alla scens och inte bara menu. 
public class SetVolume : MonoBehaviour
{
    public AudioMixer musicMixer;
    public Slider slider;

    void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }
    public void SetLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 25);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}