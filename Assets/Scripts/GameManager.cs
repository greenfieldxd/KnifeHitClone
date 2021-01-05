using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
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
    [SerializeField] private Transform KnifeTargetPosition;
    [SerializeField] private Transform startCirclePosition;
    [SerializeField] private Transform CircleTargetPosition;

    [Header("LevelsSetup")] 
    [SerializeField] private LevelSetup _levelSetup;

    private Knife _activeKnife;
    private MovingCircle _activeCircle;

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
        _stage = 0;
        _score = 0;

        CreateKnife();
        CreateMovingCircle();
        
        LoadAllOranges();
        _uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount());
        _uiManager.UpdateStage();
    }
    
    

    private void CreateKnife()
    {
        var knife = Instantiate(knifePrefab, startKnifePosition);
        knife.transform.DOMove(KnifeTargetPosition.position, 0.15f).OnComplete((() => _canLaunch = true));
        
        _activeKnife = knife.GetComponent<Knife>();
    }

    private void CreateMovingCircle()
    {
        var circle = Instantiate(circlePrefab, startCirclePosition);
        circle.transform.DOMove(CircleTargetPosition.position, 0.15f).OnComplete((() => _circleLoad = true));

        _activeCircle = circle.GetComponent<MovingCircle>();

        if (_levelSetup.GetLevelInfo(_currentLevel).GetOrangeChance()) _activeCircle.CreateOrange();
        _activeCircle.CreateKnifeObstacles();
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
            Invoke(nameof(CreateKnife), 0.2f);
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
        if (_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount() == _knifesInCircle)
        {
            _canLaunch = false;
            _circleLoad = false;
            _knifesInCircle = 0;
            
            _activeCircle.DestroyCircle();
            Invoke(nameof(LoadNextLevel), 0.5f);
        }
    }

    private void LoadNextLevel()
    {
        //Win Vibrate
        Vibration.Vibrate(30);

        _currentLevel++;
        _stage++;

        if (_currentLevel == _levelSetup.GetMaxLevelCount())
        {
            _currentLevel = 0;
        }

        _uiManager.UpdateStage();
        _uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(_currentLevel).GetLevelKnifesCount()); 
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
