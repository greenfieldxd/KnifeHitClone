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
    [SerializeField] private UIManager uiManager;
    [Header("References and prefabs")]
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private GameObject circlePrefab;
    [Space]
    [SerializeField] private Transform startKnifePosition;
    [SerializeField] private Transform knifeTargetPosition;
    [SerializeField] private Transform startCirclePosition;
    [SerializeField] private Transform circleTargetPosition;
    
    [Header("LevelsSetup")] 
    [SerializeField] private LevelSetup _levelSetup;
    
    [Header("Debug")] 
    [SerializeField] private bool debug;
    
    private int _knifesInCircle;
    private bool _canLaunch;
    private bool _circleLoad;

    public int Score { get; private set; }
    public Knife ActiveKnife { get; private set; }
    public MovingCircle ActiveCircle { get; private set; }

    private void Start()
    {
        InitGame();
    }

    private void Update()
    {
        if (!debug) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void InitGame()
    {
        _knifesInCircle = 0;
        Score = 0;

        CreateKnife(1f);
        CreateMovingCircle();
        
        uiManager.UpdateOrangeScore();
        uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(YandexGame.savesData.visualStage).GetLevelKnifesCount());
        uiManager.GameUi.UpdateStageDots(YandexGame.savesData.currentStage, YandexGame.savesData.visualStage);
    }
    
    

    private void CreateKnife(float delay)
    {
        var knife = Instantiate(knifePrefab, startKnifePosition.position, Quaternion.identity);
        knife.transform.DOMove(knifeTargetPosition.position, 0.1f).SetDelay(delay).OnComplete(() => _canLaunch = true);
        
        ActiveKnife = knife.GetComponent<Knife>();
    }

    private void CreateMovingCircle()
    {
        var circle = Instantiate(circlePrefab, startCirclePosition.position, Quaternion.identity);
        
        circle.transform.DOMove(circleTargetPosition.position, 0.5f).SetDelay(1f).OnComplete((() => _circleLoad = true));
        ActiveCircle = circle.GetComponent<MovingCircle>();
        
        ActiveCircle.SelectSprite(YandexGame.savesData.circleId, YandexGame.savesData.currentStage);
        //ActiveCircle.CreateKnifeObstacles();
        //if (_levelSetup.GetLevelInfo(_currentLevel).GetOrangeChance()) ActiveCircle.CreateOrange();
    }

    public void LaunchActiveKnife()
    {
        if (_canLaunch && _circleLoad)
        {
            _canLaunch = false;
            ActiveKnife.Launch();
            CreateKnife(0);
        }
    }
    
    public void AddScore(int count)
    {
        Score += count;

        if (Score > YandexGame.savesData.bestScore)
        {
            YandexGame.savesData.bestScore = Score;
            YandexGame.SaveProgress();
        }
        
        uiManager.UpdateScore(Score);
    }

    public void AddOrange(int count)
    {
        YandexGame.savesData.oranges += count;
        YandexGame.SaveProgress();
        uiManager.UpdateOrangeScore();
    }

    public void HitTarget()
    {
        uiManager.ActivateHitKnife(_knifesInCircle);
        _knifesInCircle++;

        AddScore(1);
        CheckLevelProgress();
    }

    

    private void CheckLevelProgress()
    {
        if (YandexGame.savesData.visualStage != 0 && YandexGame.savesData.visualStage % 4 == 0)
        {
            if (_levelSetup.GetMaxDifficult() <= _knifesInCircle)
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
            if (_levelSetup.GetLevelInfo(YandexGame.savesData.visualStage).GetLevelKnifesCount() <= _knifesInCircle)
            {
                _canLaunch = false;
                _circleLoad = false;
                _knifesInCircle = 0;
                YandexGame.savesData.circleId++;
                YandexGame.SaveProgress();
            
                ActiveCircle.DestroyCircle();
                Invoke(nameof(LoadNextLevel), 0.5f);
            }
        }
        
    }

    private void LoadNextLevel()
    {
        YandexGame.savesData.currentStage++;
        YandexGame.savesData.visualStage++;

        if (YandexGame.savesData.visualStage == _levelSetup.GetMaxLevelCount())
        {
            uiManager.GameUi.ClearDots();
            YandexGame.savesData.visualStage = 0;
        }
        
        uiManager.UpdateStage();

        if (YandexGame.savesData.visualStage == 4) uiManager.CreateKnifesPanel(_levelSetup.GetLevelInfo(YandexGame.savesData.visualStage).GetLevelKnifesCount());
        else uiManager.CreateKnifesPanel(_levelSetup.GetMaxDifficult());
        
        CreateMovingCircle();
        _canLaunch = true;
    }

    

    public void LoseGame()
    {
        if (debug) return;
        uiManager.ResultUi.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.InSine);
    }
    
    public void Continue()
    {
        
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
