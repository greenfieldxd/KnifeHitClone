using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource audioEffects;
    
    public void PlaySound(AudioClip clip)
    {
        audioEffects.PlayOneShot(clip);
    }

}
