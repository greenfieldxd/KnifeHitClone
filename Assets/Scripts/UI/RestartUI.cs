using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        continueButton.onClick.AddListener(ContinueGameButton);
        restartButton.onClick.AddListener(RestartButton);
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    private void OnEnable()
    {
        var gm = FindObjectOfType<GameManager>();

        allOrangesText.text = "" + DataManager.GetAllOranges();
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
        texts.GetComponent<RectTransform>().DOLocalMoveY(1600, 0.5f).SetEase(Ease.InSine);
        buttons.GetComponent<RectTransform>().DOLocalMoveX(-1400, 0.5f).SetEase(Ease.InSine).OnComplete((() => GameManager.RestartGame()));
    }

    private void BackToMainMenu()
    {
        texts.GetComponent<RectTransform>().DOLocalMoveY(1600, 0.5f).SetEase(Ease.InSine);
        buttons.GetComponent<RectTransform>().DOLocalMoveX(-1400, 0.5f).SetEase(Ease.InSine).OnComplete((() => SceneManager.LoadScene("MainMenu")));
    }

    private void ContinueGameButton()
    {
        texts.GetComponent<RectTransform>().DOLocalMoveY(1600, 0.5f).SetEase(Ease.InSine);
        buttons.GetComponent<RectTransform>().DOLocalMoveX(-1400, 0.5f).SetEase(Ease.InSine).OnComplete((() =>
            {
                var uiManager = FindObjectOfType<UIManager>();
                uiManager.ContinueGame();
            }));
    }
}
