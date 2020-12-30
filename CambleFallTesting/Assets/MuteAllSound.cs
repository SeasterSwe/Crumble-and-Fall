using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteAllSound : MonoBehaviour
{
    public AudioMixer musicMixer;
    //public Slider varförIHelvete;
    //void Start()
    //{
    //    musicMixer.SetFloat("MusicVolume", Mathf.Log10(varförIHelvete.value) * 25);
    //    print(Mathf.Log10(varförIHelvete.value) * 25);
    //}
    private void Update()
    {
        musicMixer.SetFloat("SoundEffects", -80f);
    }
}
