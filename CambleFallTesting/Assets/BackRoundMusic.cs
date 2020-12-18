using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRoundMusic : MonoBehaviour
{
    public AudioClip normalMusic;
    public AudioClip hype;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SwapToHype()
    {
        audioSource.clip = hype;
        audioSource.Play();
    }

    public void SwapToNormal()
    {
        audioSource.clip = normalMusic;
        audioSource.Play();
    }
}
