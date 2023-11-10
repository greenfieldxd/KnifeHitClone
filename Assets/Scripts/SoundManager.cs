using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioClip clickSound;
    
    private void Start()
    {
        UpdateStatus();
    }

    public void UpdateStatus()
    {
        backgroundMusic.volume = YandexGame.savesData.isSound ? 0.35f : 0;
        audioEffects.volume = YandexGame.savesData.isSound ? 1 : 0;
    }

    public void PlaySound(AudioClip clip = null)
    {
        if (!YandexGame.savesData.isSound) return;
        if (clip == null) clip = clickSound;
        
        audioEffects.PlayOneShot(clip);
    }
}
