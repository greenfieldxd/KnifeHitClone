using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [Header("UI Manager")]
    [SerializeField] private UIManager _uiManager;
    [Header("References and prefabs")]
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject[] bossPrefab;
    [Space]
    [SerializeField] private Transform startKnifePosition;
    [SerializeField] private Transform KnifeTargetPosition;
    [SerializeField] private Transform startCirclePosition;
    [SerializeField] private Transform CircleTargetPosition;

    [Header("LevelsSetup")] 
    [SerializeField] private LevelSetup _levelSetup;

    public Knife _activeKnife { get; private set; }
    public MovingCircle ActiveCircle { get; private set; }

    private int _currentLevel;
    private int _knifesInCircle;
    
    public int _score { get; private set; }
    public int _stage { get; private set; }
    
    private int _orangeCount;
    private bool _canLaunch = true;
    private bool _circleLoad = false;


    private void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        _knifesInCircle = 0;
        _currentLevel = 0;
        _stage = 1;
        _score = 0;

        CreateKnife();
        CreateMovingCircle();
        
        LoadAllOranges();
        _uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount());
        _uiManager.UpdateStage(_stage);
    }
    
    

    private void CreateKnife()
    {
        var knife = Instantiate(knifePrefab, startKnifePosition);
        knife.transform.DOMove(KnifeTargetPosition.position, 0.1f).OnComplete((() => _canLaunch = true));
        
        _activeKnife = knife.GetComponent<Knife>();
    }

    private void CreateMovingCircle()
    {
        GameObject circle;
        
        if (_stage % 5 == 0)
        {
            circle = Instantiate(bossPrefab[Random.Range(0, bossPrefab.Length)], startCirclePosition.position, Quaternion.identity);
        }
        else
        {
            circle = Instantiate(circlePrefab, startCirclePosition.position, Quaternion.identity);
        }
        
        circle.transform.DOMove(CircleTargetPosition.position, 0.1f).OnComplete((() => _circleLoad = true));
        ActiveCircle = circle.GetComponent<MovingCircle>();

        ActiveCircle.SelectSprite(0);
        //ActiveCircle.CreateKnifeObstacles();
        //if (_levelSetup.GetLevelInfo(_currentLevel).GetOrangeChance()) ActiveCircle.CreateOrange();
    }

    public void LaunchActiveKnife()
    {
        if (_canLaunch && _circleLoad)
        {
            _canLaunch = false;
            
            if (_activeKnife == null)
            {
                CreateKnife();
            }
            
            _activeKnife.Launch();
            Invoke(nameof(CreateKnife), 0.115f);
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
        
        DataManager.SetOranges(_orangeCount);
        _uiManager.UpdateOrangeScore(_orangeCount);
    }

    public void SpendOranges(int count)
    {
        _orangeCount -= count;
        
        DataManager.SetOranges(_orangeCount);
        _uiManager.UpdateOrangeScore(_orangeCount);
    }
    
    private void LoadAllOranges()
    {
        _orangeCount = DataManager.GetAllOranges();
        _uiManager.UpdateOrangeScore(_orangeCount);
    }
    
    private void UpdateUI()
    {
        _uiManager.ActivateHitKnife(_knifesInCircle);
        _knifesInCircle++;
    }

    public void HitTarget()
    {
        AddScore(1);
        UpdateUI();
        CheckLevelProgress();
    }

    

    private void CheckLevelProgress()
    {
        if (_stage <= 5)
        {
            if (_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount() == _knifesInCircle)
            {
                _canLaunch = false;
                _circleLoad = false;
                _knifesInCircle = 0;
            
                ActiveCircle.DestroyCircle();
                Invoke(nameof(LoadNextLevel), 0.5f);
            }
        }
        else
        {
            if (_levelSetup.GetMaxDifficult() == _knifesInCircle)
            {
                _canLaunch = false;
                _circleLoad = false;
                _knifesInCircle = 0;
            
                ActiveCircle.DestroyCircle();
                Invoke(nameof(LoadNextLevel), 0.5f);
            }
        }
        
    }

    private void LoadNextLevel()
    {
        _currentLevel++;
        _stage++;

        if (_currentLevel == _levelSetup.GetMaxLevelCount())
        {
            _currentLevel = 0;
        }

        _uiManager.UpdateStage(_stage);
        if (_stage <= 5) _uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount());
        else _uiManager.CreateKnifesPanel(_levelSetup.GetMaxDifficult());
        
        CreateMovingCircle();
        _canLaunch = true;
    }

    

    public void LoseGame()
    {
        //LoseGame and Activate Lose menu
        _uiManager.LoseGame();
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
