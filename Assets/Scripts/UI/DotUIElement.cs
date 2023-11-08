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

    private bool _isActive;

    public void SetActive()
    {
        if (_isActive) return;
        
        _isActive = true;
        GetComponent<Image>().sprite = dotSpriteIconOn;
        animDotElement = DOTween.Sequence();
        animDotElement.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InSine));
        animDotElement.Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InSine));
    }

    public void ResetDotElement()
    {
        _isActive = false;
        GetComponent<Image>().sprite = dotSpriteIconOff;
    }
}
