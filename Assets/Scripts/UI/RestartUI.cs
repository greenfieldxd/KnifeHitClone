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
    [SerializeField] private TextMeshProUGUI allOrangesText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private GameObject texts;
    [SerializeField] private GameObject buttons;

    private void Start()
    {
        continueButton.onClick.AddListener(() => YandexGame.RewardVideoEvent(0));
        restartButton.onClick.AddListener(RestartButton);
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);

        YandexGame.CloseVideoEvent += ContinueGame;
    }

    private void OnEnable()
    {
        var gm = FindObjectOfType<GameManager>();

        allOrangesText.text = "" + YandexGame.savesData.oranges;
        bestScoreText.text = "Score: " + gm._score + ". " + "Stage " + (gm._stage);


        if (DataManager.GetAllOranges() >= 20)
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    private void RestartButton()
    {
        continueButton.interactable = false;
        restartButton.interactable = false;
        backToMainMenuButton.interactable = false;
        
        texts.GetComponent<RectTransform>().DOLocalMoveY(1600, 0.5f).SetEase(Ease.InSine);
        buttons.GetComponent<RectTransform>().DOLocalMoveX(-1400, 0.5f).SetEase(Ease.InSine).OnComplete((() => GameManager.RestartGame()));
    }

    private void BackToMainMenu()
    {
        continueButton.interactable = false;
        restartButton.interactable = false;
        backToMainMenuButton.interactable = false;
        
        texts.GetComponent<RectTransform>().DOLocalMoveY(1600, 0.5f).SetEase(Ease.InSine);
        buttons.GetComponent<RectTransform>().DOLocalMoveX(-1400, 0.5f).SetEase(Ease.InSine).OnComplete((() => SceneManager.LoadScene("MainMenu")));
    }

    private void ContinueGame()
    {
        continueButton.interactable = false;
        restartButton.interactable = false;
        backToMainMenuButton.interactable = false;
        
        texts.GetComponent<RectTransform>().DOLocalMoveY(1600, 0.5f).SetEase(Ease.InSine);
        buttons.GetComponent<RectTransform>().DOLocalMoveX(-1400, 0.5f).SetEase(Ease.InSine).OnComplete((() =>
            {
                var uiManager = FindObjectOfType<UIManager>();
                uiManager.ContinueGame();
            }));
    }
}
