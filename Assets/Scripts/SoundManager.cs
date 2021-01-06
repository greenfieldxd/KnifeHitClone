using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioSource _audio;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

}
