using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button knifeButton;
    [SerializeField] private GameObject knifeUiElementPrefab;
    [SerializeField] private Transform panelKnifes;
    
    private GameManager _gameManager;

    private List<GameObject> _knifeUIElementsList = new List<GameObject>();
    
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        
        knifeButton.onClick.AddListener(LaunchKnifeButton);
    }
    
    private void LaunchKnifeButton()
    {
        _gameManager.LaunchActiveKnife();
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

    
}
