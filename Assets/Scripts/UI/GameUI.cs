using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GameUI : MonoBehaviour
{
    [SerializeField] private DotUIElement[] dotElements;
    [SerializeField] private TextMeshProUGUI stageText;

    
    public void UpdateStageDots(int currentStage, int visualStage)
    {
        for (int i = 0; i < visualStage; i++)
        {
            dotElements[i].SetActive();
        }
        
        stageText.text = $"Stage {currentStage + 1}";
    }


    public void ClearDots()
    {
        foreach (var dot in dotElements)
        {
            dot.ResetDotElement();
        }
    }
}
