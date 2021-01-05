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
        bestScoreText.text = "Score: " + gm._score + ". " + "Stage " + gm._stage + 1;
        
        restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        
        texts.GetComponent<RectTransform>().DOLocalMoveY(1600f, 0.7f).SetEase(Ease.InOutCirc);
        restartButton.GetComponent<RectTransform>().DOLocalMoveY(-1600f, 0.7f).SetEase(Ease.InOutCirc).OnComplete((() =>
        {
            FindObjectOfType<UIManager>().RestartGame();
            gm.ClearGame();
        }));
    }
}
