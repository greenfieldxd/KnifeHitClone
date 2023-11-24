using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Extensions;
using Source.Scripts.Systems.Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private Translator scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private AudioClip loseSound;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartButton);
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        continueButton.onClick.AddListener(() =>
        {
            OtherExtensions.TransformPunchScale(continueButton.transform);
            SoundManager.Instance.PlaySound();
            YandexGame.RewVideoShow();
        });

        YandexGame.RewardVideoEvent += ContinueGame;
    }

    private void OnDestroy()
    {
        YandexGame.RewardVideoEvent -= ContinueGame;
    }

    private void RestartButton()
    {
        OtherExtensions.TransformPunchScale(restartButton.transform);
        SoundManager.Instance.PlaySound();
        
        continueButton.enabled = false;
        restartButton.enabled = false;
        backToMainMenuButton.enabled = false;
        
        YandexGame.FullscreenShow();
        GameManager.Instance.RestartGame();
    }

    private void BackToMainMenu()
    {
        OtherExtensions.TransformPunchScale(backToMainMenuButton.transform);
        SoundManager.Instance.PlaySound();
        
        continueButton.enabled = false;
        restartButton.enabled = false;
        backToMainMenuButton.enabled = false;
        
        YandexGame.FullscreenShow();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoseGame()
    {
        scoreText.SetValue(GameManager.Instance.Score);
        SoundManager.Instance.PlaySound(loseSound);

        continueButton.enabled = true;
        restartButton.enabled = true;
        backToMainMenuButton.enabled = true;
    }

    private void ContinueGame(int id = 0)
    {
        continueButton.enabled = false;
        restartButton.enabled = false;
        backToMainMenuButton.enabled = false;
        GameManager.Instance.Continue();
    }
}
