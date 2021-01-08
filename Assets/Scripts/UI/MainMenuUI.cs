using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI bestStageText;
    [SerializeField] private TextMeshProUGUI allOrangesText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button selectKnifeButton;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject texts;
    [SerializeField] private GameObject selectKnifeMenu;
    [SerializeField] private GameObject mainMenu;
    

    private void Start()
    {
        allOrangesText.text = "" + DataManager.GetAllOranges();
        bestScoreText.text = "Best Score: " + DataManager.GetBestScore();
        bestStageText.text = "Stage: " + DataManager.GetBestStage();
        
        playButton.onClick.AddListener(MainMenuStartAnimation);
        selectKnifeButton.onClick.AddListener(OpenSelectKnifeMenu);
    }

    public void MainMenuStartAnimation()
    {
        playButton.GetComponent<RectTransform>().DOLocalMoveY(-1600, 0.7f).SetEase(Ease.InOutCirc);
        selectKnifeButton.GetComponent<RectTransform>().DOLocalMoveY(-1400, 0.7f).SetEase(Ease.InOutCirc);
        logo.GetComponent<RectTransform>().DOLocalMoveY(1600, 0.7f).SetEase(Ease.InOutCirc);
        texts.GetComponent<RectTransform>().DOLocalMoveX(-1200, 0.7f).SetEase(Ease.InOutCirc).OnComplete(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    public void OpenSelectKnifeMenu()
    {
        selectKnifeMenu.SetActive(true);
        selectKnifeMenu.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f).SetEase(Ease.InSine);
        mainMenu.GetComponent<RectTransform>().DOLocalMoveX(-800, 0.5f).SetEase(Ease.InSine);
    }
}
