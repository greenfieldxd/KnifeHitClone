using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KnifeSetup", menuName = "App/KnifeSetup", order = 2)]
public class KnifeSetup : ScriptableObject
{
    [SerializeField] private List<KnifeSprite> _knifeSprites;


    public Sprite GetKnifeSprite()
    {
        var currentKnifeType = _knifeSprites.Find(item => item.KnifeType == DataManager.GetKnifeType());
        return currentKnifeType.Sprite;
    }
    
    
    [Serializable]
    public struct KnifeSprite
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private KnifeType _knifeType;

        public Sprite Sprite => _sprite;
        public KnifeType KnifeType => _knifeType;
    }
}
