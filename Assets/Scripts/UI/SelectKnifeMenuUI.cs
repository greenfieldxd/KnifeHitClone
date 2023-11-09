using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SelectKnifeMenuUI : MonoBehaviour
{
    [SerializeField] private Button selectKnifeButton;
    [SerializeField] private Button rightArrowButton;
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private GameObject selectIcon;
    [SerializeField] private GameObject selectText;
    [SerializeField] private GameObject selectedText;
    [SerializeField] private GameObject adsText;
    [SerializeField] private GameObject priceIcon;
    [SerializeField] private TextMeshProUGUI priceText;

    [Space] [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _selectMenu;
    [Space] [SerializeField] private Image _currentKnifeImage1;
    [SerializeField] private Image _currentKnifeImage2;
    [Space] [SerializeField] private KnifeTypeUIElement[] _knifeTypeElements;

    private KnifeTypeUIElement _currentKnifeElement;
    private int _currentIdElement;

    private bool _canPress = true;

    private void Start()
    {
        InitKnifes();

        selectKnifeButton.onClick.AddListener(SelectKnife);
        rightArrowButton.onClick.AddListener(RightArrow);
        leftArrowButton.onClick.AddListener(LeftArrow);
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);

        YandexGame.RewardVideoEvent += OpenKnifeWithAds;
    }

    private void InitKnifes()
    {
        _currentIdElement = 0;
        _currentKnifeElement = _knifeTypeElements[_currentIdElement];


        UpdateArrows();
        UpdateSelectButton();
        UpdateKnifeTypeImage();
    }
    
    private void SelectKnife()
    {
        OtherExtensions.TransformPunchScale(selectKnifeButton.transform);
        
        if (_canPress && IsKnifePurchased())
        {
            YandexGame.savesData.knifeType = _currentKnifeElement.GetKnifeType();
            YandexGame.SaveProgress();

            UpdateSelectButton();
            BackToMainMenu();
        }
        else if (!IsKnifePurchased() && _currentKnifeElement.OpenWithAds)
        {
            YandexGame.RewVideoShow(0);
        }
        else if (!IsKnifePurchased() && _currentKnifeElement.GetPrice() <= YandexGame.savesData.oranges)
        {
            SoundManager.Instance.PlaySound();
            YandexGame.savesData.oranges -= _currentKnifeElement.GetPrice();
            YandexGame.savesData.Purchase(_currentKnifeElement.GetKnifeType());
            YandexGame.SaveProgress();
            UpdateSelectButton();
        }
    }

    private void OpenKnifeWithAds(int id)
    {
        if (id != 0) return;

        YandexGame.savesData.Purchase(_currentKnifeElement.GetKnifeType());
        YandexGame.SaveProgress();
        UpdateSelectButton();
    }

    private bool IsKnifePurchased()
    {
        return YandexGame.savesData.IsKnifePurchased(_currentKnifeElement.GetKnifeType());
    }

    private void BackToMainMenu()
    {
        OtherExtensions.TransformPunchScale(backToMainMenuButton.transform);
        SoundManager.Instance.PlaySound();
        
        backToMainMenuButton.enabled = false;
        selectKnifeButton.enabled = false;

        _mainMenu.SetActive(true);
        _mainMenu.GetComponent<MainMenuUI>().UpdateUiText();

        _selectMenu.GetComponent<RectTransform>().DOLocalMoveY(2500, 0.5f).SetEase(Ease.InSine).OnComplete((() =>
        {
            backToMainMenuButton.enabled = true;
            selectKnifeButton.enabled = true;
            _selectMenu.SetActive(false);
        }));
        _mainMenu.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.InSine);
    }

    private void LeftArrow()
    {
        if (_canPress)
        {
            OtherExtensions.TransformPunchScale(leftArrowButton.transform);
            SoundManager.Instance.PlaySound();
            
            _canPress = false;
            SetInteractableButtons(false);

            _currentIdElement--;
            _currentKnifeElement = _knifeTypeElements[_currentIdElement];

            MoveImage(4500);
        }
    }

    private void RightArrow()
    {
        if (_canPress)
        {
            OtherExtensions.TransformPunchScale(rightArrowButton.transform);
            SoundManager.Instance.PlaySound();
            
            _canPress = false;
            SetInteractableButtons(false);

            _currentIdElement++;
            _currentKnifeElement = _knifeTypeElements[_currentIdElement];

            MoveImage(-4500);
        }
    }

    private void UpdateArrows()
    {
        if (_currentKnifeElement == _knifeTypeElements[0])
        {
            leftArrowButton.interactable = false;
            rightArrowButton.interactable = true;
        }

        if (_currentKnifeElement == _knifeTypeElements[_knifeTypeElements.Length - 1])
        {
            rightArrowButton.interactable = false;
            leftArrowButton.interactable = true;
        }

        if (_currentKnifeElement != _knifeTypeElements[_knifeTypeElements.Length - 1] &&
            _currentKnifeElement != _knifeTypeElements[0])
        {
            leftArrowButton.interactable = true;
            rightArrowButton.interactable = true;
        }
    }

    private void UpdateSelectButton()
    {
        if (_currentKnifeElement.GetKnifeType() == YandexGame.savesData.knifeType && IsKnifePurchased())
        {
            priceText.gameObject.SetActive(false);
            priceIcon.SetActive(false);
            selectText.SetActive(false);
            adsText.SetActive(false);
            selectedText.SetActive(true);
            selectIcon.SetActive(true);
            selectKnifeButton.interactable = false;
        }
        else if (IsKnifePurchased())
        {
            priceIcon.SetActive(false);
            priceText.gameObject.SetActive(false);
            selectedText.SetActive(false);
            adsText.SetActive(false);
            selectIcon.SetActive(false);
            selectText.SetActive(true);
            selectKnifeButton.interactable = true;
        }
        else if (!IsKnifePurchased() && _currentKnifeElement.OpenWithAds)
        {
            priceIcon.SetActive(false);
            priceText.gameObject.SetActive(false);
            selectedText.SetActive(false);
            selectIcon.SetActive(false);
            selectText.SetActive(false);
            adsText.SetActive(true);
            selectKnifeButton.interactable = true;
        }
        else if (IsKnifePurchased() && _currentKnifeElement.OpenWithAds)
        {
            priceIcon.SetActive(false);
            priceText.gameObject.SetActive(false);
            selectedText.SetActive(false);
            selectIcon.SetActive(false);
            adsText.SetActive(false);
            selectText.SetActive(true);
            selectKnifeButton.interactable = true;
        }
        else if (!IsKnifePurchased() && _currentKnifeElement.GetPrice() <= YandexGame.savesData.oranges)
        {
            selectText.SetActive(false);
            selectedText.SetActive(false);
            adsText.SetActive(false);
            selectIcon.SetActive(false);
            priceIcon.SetActive(true);
            priceText.gameObject.SetActive(true);
            priceText.text = _currentKnifeElement.GetPrice().ToString();
            selectKnifeButton.interactable = true;
        }
        else if (!IsKnifePurchased() && _currentKnifeElement.GetPrice() >= YandexGame.savesData.oranges)
        {
            selectText.SetActive(false);
            selectedText.SetActive(false);
            adsText.SetActive(false);
            selectIcon.SetActive(false);
            priceText.gameObject.SetActive(true);
            priceIcon.SetActive(true);
            priceText.text = _currentKnifeElement.GetPrice().ToString();
            selectKnifeButton.interactable = false;
        }
    }

    private void MoveImage(float endPos)
    {
        Sequence moveAnim = DOTween.Sequence();

        moveAnim.Append(_currentKnifeImage1.GetComponent<RectTransform>().DOLocalMoveX(endPos, 0.2f)
            .SetEase(Ease.InSine));
        moveAnim.Append(_currentKnifeImage1.GetComponent<RectTransform>().DOLocalMoveX(-endPos, 0));
        moveAnim.AppendCallback(UpdateKnifeTypeImage);
        moveAnim.Append(_currentKnifeImage1.GetComponent<RectTransform>().DOLocalMoveX(0, 0.2f).SetEase(Ease.InSine));
        moveAnim.AppendCallback((() =>
        {
            _canPress = true;
            SetInteractableButtons(true);
            UpdateArrows();
            UpdateSelectButton();
        }));
    }

    private void SetInteractableButtons(bool status)
    {
        rightArrowButton.interactable = status;
        leftArrowButton.interactable = status;
    }

    private void UpdateKnifeTypeImage()
    {
        _currentKnifeImage1.sprite = _currentKnifeElement.GetKnifeSprite(_currentIdElement);
        _currentKnifeImage2.sprite = _currentKnifeElement.GetKnifeSprite2(_currentIdElement);
    }
}