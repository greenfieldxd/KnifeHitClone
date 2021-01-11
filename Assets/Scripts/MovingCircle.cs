using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingCircle : MonoBehaviour
{
    [SerializeField] private GameObject sliceCirclePrefab;
    [SerializeField] private GameObject orangePrefab;
    [SerializeField] private GameObject knifeObstaclePrefab;
    [SerializeField] private Animation flashCircleEffect;

    private float moveSpeedTime;
    private int _rotate;

   
    void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();

        if (gm._stage % 5 == 0)
        {
            Sequence animBoss = DOTween.Sequence();

            var rand = Random.Range(0, 2);
            if (rand == 0)
            {
                animBoss.Append(transform.DORotate(new Vector3(0, 0, Random.Range(100, 361)), Random.Range(1.5f, 2.5f),
                    RotateMode.FastBeyond360).SetEase(Ease.InOutSine));
                animBoss.Append(transform.DORotate(new Vector3(0, 0, Random.Range(0, 10)), Random.Range(0.5f, 1.5f),
                    RotateMode.FastBeyond360).SetEase(Ease.InOutSine));
                animBoss.Append(transform.DORotate(new Vector3(0, 0, Random.Range(250, 600)), Random.Range(1.5f, 3f),
                    RotateMode.FastBeyond360).SetEase(Ease.InOutSine));
                animBoss.Append(transform.DORotate(new Vector3(0, 0, Random.Range(600, 1000)), Random.Range(1.5f, 2f),
                    RotateMode.FastBeyond360).SetEase(Ease.InOutSine));
                animBoss.Append(transform.DORotate(new Vector3(0, 0, Random.Range(0, 10)), Random.Range(0.5f, 1.5f),
                    RotateMode.FastBeyond360).SetEase(Ease.InOutSine));
            }
            else
            {
                animBoss.Append(transform.DORotate(new Vector3(0, 0, Random.Range(301, 601)), Random.Range(1.5f, 2.5f),
                    RotateMode.FastBeyond360).SetEase(Ease.InOutSine));
                animBoss.AppendInterval(0.2f);
            }

            animBoss.SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            moveSpeedTime = Random.Range(2.5f, 3f);
            transform.DORotate(new Vector3(0, 0, 360f), moveSpeedTime, RotateMode.FastBeyond360).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

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
