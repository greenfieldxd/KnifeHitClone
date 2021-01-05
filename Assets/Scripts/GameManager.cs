using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Manager")]
    [SerializeField] private UIManager _uiManager;
    [Header("References and prefabs")]
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private GameObject circlePrefab;
    [Space]
    [SerializeField] private Transform startKnifePosition;
    [SerializeField] private Transform startCirclePosition;

    [Header("LevelsSetup")] 
    [SerializeField] private LevelSetup _levelSetup;

    private Knife _activeKnife;
    private MovingCircle _activeCircle;

    private int _currentLevel;
    
    private int _knifesInCircle = 0;
    
    public int _score { get; private set; }
    public int _stage { get; private set; }
    
    private int _orangeCount;

    private bool _canLaunch = true;
    

    public void InitGame()
    {
        _currentLevel = 0;
        _stage = 0;

        CreateKnife();
        CreateMovingCircle();
        
        LoadAllOranges();
        _uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount());
        _uiManager.UpdateStage(_stage);
    }
    
    

    private void CreateKnife()
    {
        var knife = Instantiate(knifePrefab, startKnifePosition);
        _activeKnife = knife.GetComponent<Knife>();

        _canLaunch = true;
    }

    private void CreateMovingCircle()
    {
        var circle = Instantiate(circlePrefab, startCirclePosition);
        _activeCircle = circle.GetComponent<MovingCircle>();

        if (_levelSetup.GetLevelInfo(_currentLevel).GetOrangeChance()) _activeCircle.CreateOrange();
        _activeCircle.CreateKnifeObstacles();
    }

    public void LaunchActiveKnife()
    {
        if (_canLaunch)
        {
            _canLaunch = false;
            
            if (_activeKnife == null)
            {
                CreateKnife();
            }
            
            _activeKnife.Launch();
            Invoke(nameof(CreateKnife), 0.25f);
        }
    }
    
    public void AddScore(int count)
    {
        _score += count;

        if (_score > DataManager.GetBestScore())
        {
            DataManager.SetBestScore(_score);
        }
        
        _uiManager.UpdateScore(_score);
    }

    public void AddOrange(int count)
    {
        _orangeCount += count;
        
        DataManager.SetOrangeScore(_orangeCount);
        _uiManager.UpdateOrangeScore(_orangeCount);
    }

    public void HitTarget()
    {
        AddScore(1);
        UpdateUI();
        CheckLevelProgress();
    }

    private void UpdateUI()
    {
        _uiManager.ActivateHitKnife(_knifesInCircle);
        _knifesInCircle++;
    }

    private void CheckLevelProgress()
    {
        if (_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount() == _knifesInCircle)
        {
            _canLaunch = false;
            _knifesInCircle = 0;
            
            _activeCircle.DestroyCircle();
            Invoke(nameof(LoadNextLevel), 0.5f);
        }
    }

    private void LoadNextLevel()
    {
        _canLaunch = true;
        _currentLevel++;
        _stage++;

        if (_currentLevel == _levelSetup.GetMaxLevelCount())
        {
            _currentLevel = 0;
        }

        _uiManager.UpdateStage(_stage);
        _uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount()); 
        CreateMovingCircle();
    }

    private void LoadAllOranges()
    {
        _orangeCount = DataManager.GetAllOranges();
        _uiManager.UpdateOrangeScore(_orangeCount);
    }

    public void LoseGame()
    {
        _uiManager.LoseGame();
    }

    public void ClearGame()
    {
        _currentLevel = 0;
        _stage = 0;
        _score = 0;
        _knifesInCircle = 0;

        _activeCircle.ClearGame();
        _activeKnife.ClearGame();
        _uiManager.ResetDotsUI();
        
        InitGame();
    }

}
