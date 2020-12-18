using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRoundMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip normalMusic;
    public AudioClip hype;

    private AudioSource audioSource;

    static BackRoundMusic instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
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

    public void SwapToMenu()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }
}
