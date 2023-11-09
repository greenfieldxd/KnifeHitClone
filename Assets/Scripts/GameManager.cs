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
    private int _difficult;
    private bool _canLaunch;
    private bool _circleLoad;
    private bool _isBoss;
    private bool _isLose;

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
        _isBoss = YandexGame.savesData.visualStage == 4;
        _difficult = _levelSetup.GetDifficult(_isBoss);
        _knifesInCircle = 0;
        Score = 0;

        CreateKnife(1f);
        CreateMovingCircle(_isBoss);
        
        uiManager.UpdateStage();
        uiManager.UpdateOrangeScore();
        uiManager.CreateKnifesPanel(_difficult);
        uiManager.GameUi.UpdateStageDots(YandexGame.savesData.currentStage, YandexGame.savesData.visualStage);
    }
    
    

    private void CreateKnife(float delay)
    {
        var knife = Instantiate(knifePrefab, startKnifePosition.position, Quaternion.identity, startKnifePosition);
        knife.transform.DOMove(knifeTargetPosition.position, 0.1f).SetDelay(delay).OnComplete(() => _canLaunch = true);
        
        ActiveKnife = knife.GetComponent<Knife>();
    }

    private void CreateMovingCircle(bool isBoss)
    {
        var circle = Instantiate(circlePrefab, startCirclePosition.position, Quaternion.identity, startCirclePosition);
        
        circle.transform.DOMove(circleTargetPosition.position, 0.5f).SetDelay(1f).OnComplete((() => _circleLoad = true));
        ActiveCircle = circle.GetComponent<MovingCircle>();
        ActiveCircle.SelectSprite(isBoss);
        ActiveCircle.Init(isBoss);
        
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
        if (_difficult <= _knifesInCircle)
        {
            _canLaunch = false;
            _circleLoad = false;
            _knifesInCircle = 0;

            ActiveCircle.DestroyCircle();
            Invoke(nameof(LoadNextLevel), 0.5f);
        }
    }

    private void LoadNextLevel()
    {
        YandexGame.savesData.currentStage++;
        YandexGame.savesData.visualStage++;
        
        if (YandexGame.savesData.visualStage > 4)
        {
            uiManager.GameUi.ClearDots();
            YandexGame.savesData.visualStage = 0;
        }
        
        _isBoss = YandexGame.savesData.visualStage == 4;

        if (_isBoss) YandexGame.savesData.bossCircleId++;
        else YandexGame.savesData.defaultCircleId++;
        
        _difficult = _levelSetup.GetDifficult(_isBoss);
        uiManager.UpdateStage();
        uiManager.CreateKnifesPanel(_difficult);
        
        CreateMovingCircle(_isBoss);
        _canLaunch = true;
        
        YandexGame.SaveProgress();
    }

    

    public void LoseGame()
    {
        if (debug || _isLose) return;

        _isLose = true;
        _canLaunch = false;
        _circleLoad = false;

        StartCoroutine(AnimateLoseWithDelay(0.5f));
    }

    private IEnumerator AnimateLoseWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        uiManager.ResultUi.LoseGame();
        uiManager.GameUi.ScaleUi(false, 0.3f);
        startCirclePosition.DOScale(0, 0.3f);
        startKnifePosition.DOScale(0, 0.3f).OnComplete(() =>
        {
            uiManager.ResultUi.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetEase(Ease.InSine);
        });
    }
    
    public void Continue()
    {
        uiManager.ResultUi.GetComponent<RectTransform>().DOLocalMoveY(2500, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
        {
            uiManager.GameUi.ScaleUi(true, 0.3f);
            startCirclePosition.transform.DOScale(1, 0.3f);
            startKnifePosition.transform.DOScale(1, 0.3f).OnComplete(() =>
            {
                ActiveKnife.transform.DOMove(knifeTargetPosition.position, 0.1f).OnComplete(() => _canLaunch = true);
                _circleLoad = true;
                _isLose = false;
            });
        });
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
