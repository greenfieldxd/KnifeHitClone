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
    
    public Sprite GetKnifeSprite2()
    {
        var currentKnifeType = _knifeSprites.Find(item => item.KnifeType == YandexGame.savesData.knifeType);
        return currentKnifeType.Sprite2;
    }
    
    public Sprite GetKnifeSprite(int id)
    {
        var currentKnifeType = _knifeSprites[id];
        return currentKnifeType.Sprite;
    } 
    
    public Sprite GetKnifeSprite2(int id)
    {
        var currentKnifeType = _knifeSprites[id];
        return currentKnifeType.Sprite2;
    }
    
    
    [Serializable]
    public struct KnifeSprite
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Sprite _sprite2;
        [SerializeField] private KnifeType _knifeType;

        public Sprite Sprite => _sprite;
        public Sprite Sprite2 => _sprite2;
        public KnifeType KnifeType => _knifeType;
    }
}
