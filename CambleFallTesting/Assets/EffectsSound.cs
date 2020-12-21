using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


// Gör så att slider funkar i alla scens och inte bara menu. 
public class EffectsSound : MonoBehaviour
{
    public AudioMixer musicMixer;
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SoundEffects", 0.75f);
    }
    public void SetLevel(float sliderValue)
    {
        musicMixer.SetFloat("SoundEffects", Mathf.Log10(sliderValue) * 25);
        PlayerPrefs.SetFloat("SoundEffects", sliderValue);
    }
}