using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private static AudioSource _audio;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

}
