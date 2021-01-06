using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private DotUIElement[] dotElements;
    [SerializeField] private TextMeshProUGUI stageText;

    public void ActivateDotElement(int number, int stage)
    {
        dotElements[number].SetActive();
        
        stageText.text = "Stage " + stage;

        if (stage > DataManager.GetBestStage())
        {
            DataManager.SetBestStage(stage);
        }
    }


    public void ClearDots()
    {
        foreach (var dot in dotElements)
        {
            dot.ResetDotElement();
        }
    }
}
