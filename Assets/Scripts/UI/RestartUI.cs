using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private Button continueButton;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartButton);
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        continueButton.onClick.AddListener(() => YandexGame.RewVideoShow(1));

        YandexGame.RewardVideoEvent += ContinueGame;
    }

    private void OnEnable()
    {
        bestScoreText.text = YandexGame.savesData.bestScore.ToString();
    }

    private void RestartButton()
    {
        continueButton.interactable = false;
        restartButton.interactable = false;
        backToMainMenuButton.interactable = false;
        GameManager.Instance.RestartGame();
    }

    private void BackToMainMenu()
    {
        continueButton.interactable = false;
        restartButton.interactable = false;
        backToMainMenuButton.interactable = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoseGame()
    {
        continueButton.interactable = true;
        restartButton.interactable = true;
        backToMainMenuButton.interactable = true;
    }

    private void ContinueGame(int id)
    {
        if (id != 1) return;
        
        continueButton.interactable = false;
        restartButton.interactable = false;
        backToMainMenuButton.interactable = false;
        FindObjectOfType<GameManager>().Continue();
    }
}
