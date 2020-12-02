using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    //Spawnar en "SoundAsset" ((prefab) från Resources mappen)
    private static GameAssets _i;
    public static GameAssets i
    {
        //codeMonkey
        get {
            if (_i == null)
                _i = Instantiate(Resources.Load("SoundAsset") as GameObject).GetComponent<GameAssets>();
                
           return _i;
        }
    }
    public SoundAudioClip[] soundAudioClipArray;
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
