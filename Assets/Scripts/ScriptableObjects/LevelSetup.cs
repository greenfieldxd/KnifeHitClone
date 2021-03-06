using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "LevelsSetup", menuName = "App/LevelsSetup", order = 1)]
public class LevelSetup : ScriptableObject
{
    [SerializeField] private int maxDifficult;
    [SerializeField] private List<Level> _levels;


    public Level GetLevelInfo(int levelNumber)
    {
        return _levels[levelNumber];
    }

    public int GetMaxLevelCount()
    {
        return _levels.Count;
    }
    
    public int GetMaxDifficult()
    {
        return maxDifficult;
    }
    
    
    [Serializable]
    public struct Level
    {
        [SerializeField] private int levelsKnifes;
        [SerializeField] private float orangeChance;


        public int GetLevelKnifesCount()
        {
            return levelsKnifes;
        }
        public bool GetOrangeChance()
        {
            var rand = Random.value * 100;

            return rand < orangeChance;
        }
    }
}
