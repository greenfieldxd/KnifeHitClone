using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Extensions;
using Source.Scripts.Systems.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Translator bestScoreText;
    [SerializeField] private Translator bestStageText;
    [SerializeField] private TextMeshProUGUI allOrangesText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button selectKnifeButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button languageButton;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject logoYellow;
    [SerializeField] private GameObject texts;
    [SerializeField] private GameObject selectKnifeMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject soundOff;
    [SerializeField] private Sprite ruSprite;
    [SerializeField] private Sprite enSprite;
    
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
        soundOff.SetActive(!YandexGame.savesData.isSound);
        languageButton.GetComponent<Image>().sprite = YandexGame.savesData.language == "ru" ? ruSprite : enSprite;

        playButton.onClick.AddListener(MainMenuStartAnimation);
        selectKnifeButton.onClick.AddListener(OpenSelectKnifeMenu);
        languageButton.onClick.AddListener(SwitchLanguage);
        soundButton.onClick.AddListener(SwitchSound);
    }

    private void SwitchSound()
    {
        OtherExtensions.TransformPunchScale(soundButton.transform);
        YandexGame.savesData.isSound = !YandexGame.savesData.isSound;
        soundOff.SetActive(!YandexGame.savesData.isSound);
        SoundManager.Instance.UpdateStatus();
        SoundManager.Instance.PlaySound();
        YandexGame.SaveProgress();
    }

    private void SwitchLanguage()
    {
        OtherExtensions.TransformPunchScale(languageButton.transform);
        YandexGame.SwitchLanguage(YandexGame.savesData.language == "ru" ? "en" : "ru");
        languageButton.GetComponent<Image>().sprite = YandexGame.savesData.language == "ru" ? ruSprite : enSprite;
        SoundManager.Instance.PlaySound();
        YandexGame.SaveProgress();
    }

    public void UpdateUiText()
    {
        allOrangesText.text = YandexGame.savesData.oranges.ToString();
        bestScoreText.SetValue(YandexGame.savesData.bestScore);
        bestStageText.SetValue(YandexGame.savesData.bestStage + 1);
    }

    private void MainMenuStartAnimation()
    {
        playButton.enabled = false;
        selectKnifeButton.enabled = false;

        OtherExtensions.TransformPunchScale(mainMenu.transform);
        SoundManager.Instance.PlaySound();

        playButton.GetComponent<RectTransform>().DOLocalMoveY(-2500, 0.7f).SetEase(Ease.InOutCirc);
        selectKnifeButton.GetComponent<RectTransform>().DOLocalMoveY(-2500, 0.7f).SetEase(Ease.InOutCirc);
        soundButton.GetComponent<RectTransform>().DOLocalMoveY(2500, 0.7f).SetEase(Ease.InOutCirc);
        languageButton.GetComponent<RectTransform>().DOLocalMoveY(2500, 0.7f).SetEase(Ease.InOutCirc);
        logoYellow.GetComponent<RectTransform>().DOLocalMoveY(-2500, 0.7f).SetEase(Ease.InOutCirc);
        logo.GetComponent<RectTransform>().DOLocalMoveY(2500, 0.7f).SetEase(Ease.InOutCirc);
        texts.GetComponent<RectTransform>().DOLocalMoveX(-2500, 0.7f).SetEase(Ease.InOutCirc).OnComplete(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    private void OpenSelectKnifeMenu()
    {
        playButton.enabled = false;
        selectKnifeButton.enabled = false;
        
        OtherExtensions.TransformPunchScale(selectKnifeButton.transform);
        SoundManager.Instance.PlaySound();
        
        selectKnifeMenu.SetActive(true);
        selectKnifeMenu.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.InSine);
        mainMenu.GetComponent<RectTransform>().DOLocalMoveY(-2500, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
        {
            playButton.enabled = true;
            selectKnifeButton.enabled = true;
        });
    }
}
