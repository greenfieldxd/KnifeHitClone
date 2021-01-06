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

    private List<int> _createPositions = new List<int>();

    private int _rotate;

   
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360f), moveSpeedTime, RotateMode.FastBeyond360).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
        
        _rotate = Random.Range(0, 361);
    }

    public void CreateOrange()
    {
        var orange = Instantiate(orangePrefab, transform);
       
       orange.transform.DORotate(new Vector3(0, 0, _rotate), 0f);
       
       _rotate += Random.Range(20, 71);
    }

    public void CreateKnifeObstacles()
    {
        var rand = Random.Range(1, 4);

        for (int i = 0; i < rand; i++)
        {
            var knife = Instantiate(knifeObstaclePrefab, transform);
            knife.transform.DORotate(new Vector3(0, 0, _rotate), 0f);

            _rotate += Random.Range(20, 71);
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

}
