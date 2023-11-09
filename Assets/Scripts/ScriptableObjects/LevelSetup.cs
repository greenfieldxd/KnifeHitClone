using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "LevelsSetup", menuName = "App/LevelsSetup", order = 1)]
public class LevelSetup : ScriptableObject
{
    [SerializeField] private int minRandomKnifesCount;
    [SerializeField] private int maxRandomKnifesCount;
    [SerializeField] private int maxDifficultBoss;
    [SerializeField] private int minDifficultBoss;
    
    public int GetDifficult(bool isBoss)
    {
        return isBoss ? Random.Range(minDifficultBoss, maxDifficultBoss + 1) :Random.Range(minRandomKnifesCount, maxRandomKnifesCount + 1);
    }
}
