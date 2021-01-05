using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingCircle : MonoBehaviour
{
    [SerializeField] private float moveSpeedTime = 3f;
    [SerializeField] private GameObject sliceCirclePrefab;
    [SerializeField] private GameObject orangePrefab;
    [SerializeField] private GameObject knifeObstaclePrefab;
    [SerializeField] private Animation flashCircleEffect;
    [Space]
    [SerializeField] private Vector3 startScale;

    private List<int> _createPositions = new List<int>();

    private void Awake()
    {
        CreateListPositions();
    }

    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360f), moveSpeedTime, RotateMode.FastBeyond360).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    public void CreateOrange()
    {
        var orange = Instantiate(orangePrefab, transform);
        var orangeRotate = Random.Range(0, _createPositions.Count);
       
       orange.transform.DORotate(new Vector3(0, 0, _createPositions[orangeRotate]), 0f);
       _createPositions.RemoveAt(orangeRotate);
    }

    public void CreateKnifeObstacles()
    {
        var rand = Random.Range(1, 4);

        for (int i = 0; i < rand; i++)
        {
            var knife = Instantiate(knifeObstaclePrefab, transform);
            var randKnifeRotate = Random.Range(0, _createPositions.Count);

            knife.transform.DORotate(new Vector3(0, 0, _createPositions[randKnifeRotate]), 0f); 
            _createPositions.RemoveAt(randKnifeRotate);
        }
    }
    
    public void ShakeCircle()
    {
        transform.DOShakePosition(.2f, new Vector3(0.15f, 0.15f, 0.15f), 10, 20f);
        flashCircleEffect.Play("FlashCircle");
    }

    public void DestroyCircle()
    {
        Instantiate(sliceCirclePrefab);
        Destroy(gameObject);
    }

    private void CreateListPositions()
    {
        for (int i = 0; i < 351; i += 15)
        {
            _createPositions.Add(i);
        }
    }

}
