using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DotUIElement : MonoBehaviour
{
    [SerializeField] private Sprite dotSpriteIconOn;
    [SerializeField] private Sprite dotSpriteIconOff;

    Sequence animDotElement;

    public void SetActive()
    {
        GetComponent<Image>().sprite = dotSpriteIconOn;

        animDotElement = DOTween.Sequence();
        animDotElement.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InSine));
        animDotElement.Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InSine));
    }

    public void ResetDotElement()
    {
        GetComponent<Image>().sprite = dotSpriteIconOff;
    }
}
