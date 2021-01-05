using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject resultUI;
    [Space]
    [SerializeField] private Button playButton;
    [SerializeField] private Button knifeButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI orangeText;
    [SerializeField] private GameObject knifeUiElementPrefab;
    [SerializeField] private Transform panelKnifes;
    
    private GameManager _gameManager;

    private List<GameObject> _knifeUIElementsList = new List<GameObject>();
    
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        scoreText.text = "" + 0;
        
        knifeButton.onClick.AddListener(LaunchKnifeButton);
        playButton.onClick.AddListener(PlayGame);
    }
    
    private void LaunchKnifeButton()
    {
        _gameManager.LaunchActiveKnife();
    }

    private void PlayGame()
    {
        mainMenuUI.GetComponent<MainMenuUI>().MainMenuStartAnimation((() =>
        {
            gameUI.SetActive(true);
            mainMenuUI.SetActive(false);
            _gameManager.InitGame();
        }));
    }

    public void LoseGame()
    {
        gameUI.SetActive(false);
        resultUI.SetActive(true);
    }
    public void RestartGame()
    {
        gameUI.SetActive(true);
        resultUI.SetActive(false);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = "" + newScore;
    }
    
    public void UpdateOrangeScore(int newOrangeScore)
    {
        orangeText.text = "" + newOrangeScore;
    }

    public void UpdateStage(int number)
    {
        gameUI.GetComponent<GameUI>().ActivateDotElement(number);
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
