using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [Header("UI Manager")]
    [SerializeField] private UIManager _uiManager;
    [Header("References and prefabs")]
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private GameObject circlePrefab;
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
    
    private bool _canLaunch = true;
    private bool _circleLoad = false;


    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        _knifesInCircle = 0;
        _currentLevel = 0;
        _score = 0;
        _stage = YandexGame.savesData.currentStage;

        CreateKnife(1f);
        CreateMovingCircle();
        
        _uiManager.UpdateOrangeScore();
        _uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount());
        _uiManager.UpdateStage(_stage);
    }
    
    

    private void CreateKnife(float delay)
    {
        var knife = Instantiate(knifePrefab, startKnifePosition.position, Quaternion.identity);
        knife.transform.DOMove(KnifeTargetPosition.position, 0.1f).SetDelay(delay).OnComplete(() => _canLaunch = true);
        
        _activeKnife = knife.GetComponent<Knife>();
    }

    private void CreateMovingCircle()
    {
        var circle = Instantiate(circlePrefab, startCirclePosition.position, Quaternion.identity);
        
        circle.transform.DOMove(CircleTargetPosition.position, 0.5f).SetDelay(1f).OnComplete((() => _circleLoad = true));
        ActiveCircle = circle.GetComponent<MovingCircle>();

        ActiveCircle.SelectSprite(0, _stage);
        //ActiveCircle.CreateKnifeObstacles();
        //if (_levelSetup.GetLevelInfo(_currentLevel).GetOrangeChance()) ActiveCircle.CreateOrange();
    }

    public void LaunchActiveKnife()
    {
        if (_canLaunch && _circleLoad)
        {
            _canLaunch = false;
            _activeKnife.Launch();
            CreateKnife(0);
        }
    }
    
    public void AddScore(int count)
    {
        _score += count;

        if (_score > YandexGame.savesData.bestScore)
        {
            YandexGame.savesData.bestScore = _score;
        }
        
        _uiManager.UpdateScore(_score);
    }

    public void AddOrange(int count)
    {
        YandexGame.savesData.oranges += count;
        YandexGame.SaveProgress();
        _uiManager.UpdateOrangeScore();
    }

    public void SpendOranges(int count)
    {
        YandexGame.savesData.oranges -= count;
        YandexGame.SaveProgress();
        _uiManager.UpdateOrangeScore();
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
