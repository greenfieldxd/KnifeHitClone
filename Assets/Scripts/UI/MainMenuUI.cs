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
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject texts;
    

    private void Start()
    {
        allOrangesText.text = "" + DataManager.GetAllOranges();
        bestScoreText.text = "Best Score: " + DataManager.GetBestScore();
        bestStageText.text = "Stage: " + DataManager.GetBestStage();
        
        playButton.onClick.AddListener(MainMenuStartAnimation);
    }

    public void MainMenuStartAnimation()
    {
        playButton.GetComponent<RectTransform>().DOLocalMoveY(-1600, 0.7f).SetEase(Ease.InOutCirc);
        logo.GetComponent<RectTransform>().DOLocalMoveY(1600, 0.7f).SetEase(Ease.InOutCirc);
        texts.GetComponent<RectTransform>().DOLocalMoveX(-1200, 0.7f).SetEase(Ease.InOutCirc).OnComplete(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }
}
