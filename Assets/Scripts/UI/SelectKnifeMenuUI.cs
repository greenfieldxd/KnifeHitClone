using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectKnifeMenuUI : MonoBehaviour
{
    [SerializeField] private Button selectKnifeButton;
    [SerializeField] private Button rightArrowButton;
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private GameObject selectIcon;
    [SerializeField] private GameObject selectText;
    [SerializeField] private GameObject selectedText;
    [SerializeField] private GameObject priceIcon;
    [SerializeField] private TextMeshProUGUI priceText;

    [Space]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _selectMenu;
    [Space]
    [SerializeField] private Image _currentKnifeImage1;
    [SerializeField] private Image _currentKnifeImage2;
    [Space] 
    [SerializeField] private KnifeTypeUIElement[] _knifeTypeElements;

    private KnifeTypeUIElement _currentKnifeElement;
    private int _currentIdElement;

    private bool _canPress = true;
    
    void OnEnable()
    {
        InitKnifes();
        
        selectKnifeButton.onClick.AddListener(SelectKnife);
        rightArrowButton.onClick.AddListener(RightArrow);
        leftArrowButton.onClick.AddListener(LeftArrow);
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);
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
        if (_canPress && _currentKnifeElement.IsKnifeOpened())
        {
            DataManager.SetKnifeType(_currentKnifeElement.GetKnifeType());

            UpdateSelectButton();
            BackToMainMenu();
        }
        else if (!_currentKnifeElement.IsKnifeOpened() && _currentKnifeElement.GetPrice() <= DataManager.GetAllOranges())
        {
            DataManager.SetOranges(DataManager.GetAllOranges() - _currentKnifeElement.GetPrice());
            DataManager.SetOpenStatusForKnife(_currentKnifeElement.GetKnifeType());
            UpdateSelectButton();
        }
    }

    private void BackToMainMenu()
    {
        _mainMenu.SetActive(true);
        _selectMenu.GetComponent<RectTransform>().DOLocalMoveY(2500, 0.5f).SetEase(Ease.InSine).OnComplete((() => _selectMenu.SetActive(false)));
        _mainMenu.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.InSine);
    }

    private void LeftArrow()
    {
        if (_canPress)
        {
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
        if (_currentKnifeElement.GetKnifeType().ToString() == DataManager.GetKnifeType())
        {
            priceText.gameObject.SetActive(false);
            priceIcon.SetActive(false);
            selectedText.SetActive(true);
            selectIcon.SetActive(true);
            selectKnifeButton.interactable = false;
            
        }
        else if (_currentKnifeElement.IsKnifeOpened())
        {
            selectText.SetActive(true);
            selectedText.SetActive(false);
            priceText.gameObject.SetActive(false);
            priceIcon.SetActive(false);    
            selectKnifeButton.interactable = true;
        }
        else if (!_currentKnifeElement.IsKnifeOpened() && _currentKnifeElement.GetPrice() <= DataManager.GetAllOranges())
        {
            selectedText.SetActive(false);
            selectedText.SetActive(false);
            priceIcon.SetActive(true);
            priceText.text = _currentKnifeElement.GetPrice().ToString();
            selectKnifeButton.interactable = true;
        }
        else if (!_currentKnifeElement.IsKnifeOpened() && _currentKnifeElement.GetPrice() >= DataManager.GetAllOranges())
        {
            selectedText.SetActive(false);
            selectedText.SetActive(false);
            priceIcon.SetActive(true);
            priceText.text = _currentKnifeElement.GetPrice().ToString();
            selectKnifeButton.interactable = false;
        }
    }

    private void MoveImage(float endPos)
    {
        Sequence moveAnim = DOTween.Sequence();
        
        moveAnim.Append(_currentKnifeImage1.GetComponent<RectTransform>().DOLocalMoveX(endPos, 0.2f).SetEase(Ease.InSine));
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
