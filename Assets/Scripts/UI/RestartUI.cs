using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI allOrangesText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button continueButton;

    private void Start()
    {
        continueButton.onClick.AddListener(ContinueGameButton);
        restartButton.onClick.AddListener(RestartButton);
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
        GameManager.RestartGame();
    }

    private void ContinueGameButton()
    {
        var uiManager = FindObjectOfType<UIManager>();
        uiManager.ContinueGame();
    }
}
