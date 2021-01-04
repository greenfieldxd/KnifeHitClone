using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingCircle : MonoBehaviour
{
    [SerializeField] private float moveSpeedTime = 3f;
    [SerializeField] private GameObject sliceCirclePrefab;
    [SerializeField] private GameObject orangePrefab;
    [SerializeField] private GameObject knifeObstaclePrefab;
    [Space]
    [SerializeField] private Vector3 startScale;

    private int _prevRotateObstacle = 0;
    private int _orangeRotate = -1000;
    
    void Start()
    {
        transform.DOScale(startScale, 0.2f).SetEase(Ease.InSine);
        transform.DORotate(new Vector3(0, 0, 360f), moveSpeedTime, RotateMode.FastBeyond360).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    public void CreateOrange()
    {
        var orange = Instantiate(orangePrefab, transform);
        _orangeRotate = Random.Range(0, 361);
        
        orange.transform.DORotate(new Vector3(0, 0, _orangeRotate), 0f);
    }

    public void CreateObstacles()
    {
        var rand = Random.Range(1, 4);

        for (int i = 0; i < rand; i++)
        {
            var randRotare = Random.Range(0, 361);
            _prevRotateObstacle = randRotare;
            var knife = Instantiate(knifeObstaclePrefab, transform);
            knife.transform.position = new Vector3(0, -3,0);
            knife.transform.DORotate(new Vector3(0, 0, randRotare), 0f);
        }
    }
    
    public void ShakeCircle()
    {
        transform.DOShakePosition(.5f, new Vector3(0.15f, 0.15f, 0.15f), 10, 20f);
    }

    public void DestroyCircle()
    {
        Instantiate(sliceCirclePrefab);
        Destroy(gameObject);
    }

}
