using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        HeavyBlockShoot,
        FastBlockShoot,
        NormalBlockShoot,
    }
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    public static void PlaySound(Sound sound, Vector3 position)
    {
        GameObject soundObj = new GameObject("Sound");
        soundObj.transform.position = position;
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        
        //stats från YT blev ganska nice
        audioSource.clip = GetAudioClip(sound);
        audioSource.maxDistance = 100f;
        audioSource.spatialBlend = 1;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
       
        audioSource.dopplerLevel = 0f;
        audioSource.Play();
        Object.Destroy(soundObj, audioSource.clip.length);
    }
    public static void PlaySound(Sound sound)
    {
        if(oneShotGameObject == null)
        {
            oneShotGameObject =  new GameObject("Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip audioClip in GameAssets.i.soundAudioClipArray)
        {
            if (sound == audioClip.sound)
                return audioClip.audioClip;
        }
        Debug.LogError("Sound not found " + sound);
        return null;
    }
}
