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
    
    void Start()
    {
        transform.DOScale(startScale, 0.2f).SetEase(Ease.InSine);
        transform.DORotate(new Vector3(0, 0, 360f), moveSpeedTime, RotateMode.FastBeyond360).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    public void CreateOrange()
    {
        Instantiate(orangePrefab, transform);
    }

    public void CreateObstacles()
    {
        var rand = Random.Range(1, 4);
        Debug.Log("OBSTACLE CREATE");

        for (int i = 0; i < rand; i++)
        {
            var randRotare = Random.Range(0, 361);
            Debug.Log("OBSTACLE " + i);
            _prevRotateObstacle = randRotare;
            var knife = Instantiate(knifeObstaclePrefab, transform); 
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
