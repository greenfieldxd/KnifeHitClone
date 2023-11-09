using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using YG;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private DotUIElement[] dotElements;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private Transform[] scaleUi;

    
    public void UpdateStageDots(int currentStage, int visualStage)
    {
        for (int i = 0; i < visualStage; i++)
        {
            dotElements[i].SetActive();
        }
        
        arrow.SetParent(dotElements[visualStage].transform);
        arrow.DOLocalMoveX(0, 0.1f);
        stageText.text = $"Stage {currentStage + 1}";
    }


    public void ClearDots()
    {
        foreach (var dot in dotElements)
        {
            dot.ResetDotElement();
        }
    }

    public void ScaleUi(bool status, float time)
    {
        foreach (var scaleItem in scaleUi)
        {
            scaleItem.DOScale(status ? 1 : 0, time);
        }
    }
}
