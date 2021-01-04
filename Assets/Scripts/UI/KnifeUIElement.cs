using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class KnifeUIElement : MonoBehaviour
{
    [SerializeField] private Sprite knifeSpriteIconOn;
    [SerializeField] private Sprite knifeSpriteIconOff;

    Sequence animKnifeElement;

    public void ActivateKnifeElement()
    {
        GetComponent<Image>().sprite = knifeSpriteIconOn;

        animKnifeElement = DOTween.Sequence();
        animKnifeElement.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InSine));
        animKnifeElement.Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InSine));
    }

    private void ResetKnifeElement()
    {
        GetComponent<SpriteRenderer>().sprite = knifeSpriteIconOff;
    }
}
