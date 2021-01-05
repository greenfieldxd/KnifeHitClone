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
    [SerializeField] private GameObject texts;

    private void Start()
    {
        var gm = FindObjectOfType<GameManager>();
        
        allOrangesText.text = "" + DataManager.GetAllOranges();
        bestScoreText.text = "Score: " + gm._score + ". " + "Stage " + (gm._stage + 1);
        
        restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        FindObjectOfType<GameManager>().RestartGame();
    }
}
