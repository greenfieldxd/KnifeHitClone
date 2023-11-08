using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI bestStageText;
    [SerializeField] private TextMeshProUGUI allOrangesText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button selectKnifeButton;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject logoYellow;
    [SerializeField] private GameObject texts;
    [SerializeField] private GameObject selectKnifeMenu;
    [SerializeField] private GameObject mainMenu;
    
    [Header("Cheats")]
    [SerializeField] private bool addOranges;

    

    private void Start()
    {
        if (addOranges)
        {
            YandexGame.savesData.oranges = 100;
            YandexGame.SaveProgress();
        }
        
        UpdateUiText();
        playButton.onClick.AddListener(MainMenuStartAnimation);
        selectKnifeButton.onClick.AddListener(OpenSelectKnifeMenu);
    }

    public void UpdateUiText()
    {
        allOrangesText.text = YandexGame.savesData.oranges.ToString();
        bestScoreText.text = YandexGame.savesData.bestScore.ToString();
        bestStageText.text = (YandexGame.savesData.currentStage + 1).ToString();
    }

    private void MainMenuStartAnimation()
    {
        playButton.interactable = false;
        selectKnifeButton.interactable = false;
        
        playButton.GetComponent<RectTransform>().DOLocalMoveY(-2500, 0.7f).SetEase(Ease.InOutCirc);
        selectKnifeButton.GetComponent<RectTransform>().DOLocalMoveY(-2500, 0.7f).SetEase(Ease.InOutCirc);
        logoYellow.GetComponent<RectTransform>().DOLocalMoveY(-2500, 0.7f).SetEase(Ease.InOutCirc);
        logo.GetComponent<RectTransform>().DOLocalMoveY(2500, 0.7f).SetEase(Ease.InOutCirc);
        texts.GetComponent<RectTransform>().DOLocalMoveX(-2500, 0.7f).SetEase(Ease.InOutCirc).OnComplete(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    private void OpenSelectKnifeMenu()
    {
        playButton.interactable = false;
        selectKnifeButton.interactable = false;
        
        selectKnifeMenu.SetActive(true);
        selectKnifeMenu.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.InSine);
        mainMenu.GetComponent<RectTransform>().DOLocalMoveY(-2500, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
        {
            playButton.interactable = true;
            selectKnifeButton.interactable = true;
        });
    }
}
