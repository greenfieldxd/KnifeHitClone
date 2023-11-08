using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject resultUI;
    [Space]
    [SerializeField] private Button knifeButton;
    [Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI orangeText;
    [SerializeField] private GameObject orangeIcon;
    [Space]
    [SerializeField] private GameObject knifeUiElementPrefab;
    [SerializeField] private Transform panelKnifes;
    
    private GameManager _gameManager;

    private int _dotsCount = 0;

    private List<GameObject> _knifeUIElementsList = new List<GameObject>();
    
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        scoreText.text = "" + 0;
        
        knifeButton.onClick.AddListener(LaunchKnifeButton);
    }
    
    private void LaunchKnifeButton()
    {
        _gameManager.LaunchActiveKnife();
    }
    

    public void LoseGame()
    {
        gameUI.SetActive(false);
        resultUI.SetActive(true);
    }

    public void ContinueGame()
    {
        resultUI.SetActive(false);
        gameUI.SetActive(true);
        
        _gameManager.SpendOranges(20);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = "" + newScore;
    }
    
    public void UpdateOrangeScore()
    {
        orangeText.text = "" + YandexGame.savesData.oranges;

        Sequence anim = DOTween.Sequence();
        anim.Append(orangeIcon.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InSine));
        anim.Append(orangeIcon.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetEase(Ease.InSine));
        anim.Append(orangeIcon.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InSine));
    }

    public void UpdateStage(int stage)
    {
        if (_dotsCount == 5)
        {
            ResetDotsUI();
            _dotsCount = 0;
        }
        gameUI.GetComponent<GameUI>().ActivateDotElement(_dotsCount, stage);
        _dotsCount++;
    }

    public void CreateKnifesPanel(int count)
    {
        ResetKnifeUi();
        
        for (int i = 0; i < count; i++)
        {
            var knifeUiElement = Instantiate(knifeUiElementPrefab, panelKnifes);
            _knifeUIElementsList.Add(knifeUiElement);
        }
    }

    public void ActivateHitKnife(int count)
    {
        _knifeUIElementsList[count].GetComponent<KnifeUIElement>().ActivateKnifeElement();
    }

   

    private void ResetKnifeUi()
    {
        foreach (var knifeUI in _knifeUIElementsList)
        {
            Destroy(knifeUI);
        }
        
        _knifeUIElementsList.Clear();
    }

    public void ResetDotsUI()
    {
        gameUI.GetComponent<GameUI>().ClearDots();
    }

    
}
