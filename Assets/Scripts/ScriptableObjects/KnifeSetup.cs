using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


[CreateAssetMenu(fileName = "KnifeSetup", menuName = "App/KnifeSetup", order = 2)]
public class KnifeSetup : ScriptableObject
{
    [SerializeField] private List<KnifeSprite> _knifeSprites;


    public Sprite GetKnifeSprite()
    {
        var currentKnifeType = _knifeSprites.Find(item => item.KnifeType == YandexGame.savesData.knifeType);
        return currentKnifeType.Sprite;
    }
    
    public Color GetEffectColor()
    {
        var currentKnifeType = _knifeSprites.Find(item => item.KnifeType == YandexGame.savesData.knifeType);
        return currentKnifeType.EffectColor;
    }

    public Sprite GetKnifeSprite(int id)
    {
        var currentKnifeType = _knifeSprites[id];
        return currentKnifeType.Sprite;
    }

    [Serializable]
    public struct KnifeSprite
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color effectColor;
        [SerializeField] private KnifeType _knifeType;

        public Sprite Sprite => _sprite;
        public Color EffectColor => effectColor;
        public KnifeType KnifeType => _knifeType;
    }
}