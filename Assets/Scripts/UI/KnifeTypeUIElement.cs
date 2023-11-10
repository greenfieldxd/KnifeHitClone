using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class KnifeTypeUIElement : MonoBehaviour
{
    [SerializeField] private KnifeType _knifeType;
    [SerializeField] private KnifeSetup setup;
    [SerializeField] private int _price;
    [SerializeField] private bool opedWithAds;

    public bool OpenWithAds => opedWithAds;
    
    public KnifeType GetKnifeType()
    {
        return _knifeType;
    }

    public Sprite GetKnifeSprite(int id)
    {
        return setup.GetKnifeSprite(id);
    }

    public int GetPrice()
    {
        return _price;
    }
}
