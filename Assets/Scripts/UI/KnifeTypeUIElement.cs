using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTypeUIElement : MonoBehaviour
{
    [SerializeField] private KnifeType _knifeType;
    [SerializeField] private KnifeSetup setup;
    [SerializeField] private int _price;

    public KnifeType GetKnifeType()
    {
        return _knifeType;
    }

    public Sprite GetKnifeSprite(int id)
    {
        return setup.GetKnifeSprite(id);
    }
    
    public Sprite GetKnifeSprite2(int id)
    {
        return setup.GetKnifeSprite2(id);
    }

    public int GetPrice()
    {
        return _price;
    }

    public bool IsKnifeOpened()
    {
        if (_knifeType == KnifeType.DEFAULT) return true;
        else
        {
            return DataManager.IsKnifeOpened(_knifeType);

        }
    }
}
